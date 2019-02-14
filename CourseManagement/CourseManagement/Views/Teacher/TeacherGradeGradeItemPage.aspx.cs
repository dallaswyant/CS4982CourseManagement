using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CourseManagement
{
    public partial class TeacherGradeGradeItemPage : System.Web.UI.Page
    {
       

        private GradedItemDAL gradeItemDAL = new GradedItemDAL();
        private GradedItem currentGrade;
        private Student currentStudent;
        private double workingGrade;

        private string workingFeedback;
        
       
         #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var studentDAL = new StudentDAL();
            
            if (!IsPostBack)
            {
                this.currentStudent = HttpContext.Current.Session["SelectedStudent"] as Student;
                populateStudentDDL(studentDAL);
                this.currentGrade = HttpContext.Current.Session["CurrentGradedItem"] as GradedItem;
                this.ddlStudentNames_OnSelectedIndexChanged(null, null);
                int count = 0;
                foreach(var item in this.ddlAssignmentNames.Items)
                {
                    if (item.ToString() == this.currentGrade.Name)
                    {
                        this.ddlAssignmentNames.SelectedIndex = count;
                    }
                    count++;
                }
                this.ddlAssignmentNames_SelectedIndexChanged(null,null);
                DataBind();
            }

            this.currentGrade = HttpContext.Current.Session["CurrentGradedItem"] as GradedItem;
            


            if (currentGrade != null)
            {
                var user = HttpContext.Current.Session["User"] as User;
                TeacherDAL teacherDAL = new TeacherDAL();
                Teacher teacher = teacherDAL.GetTeacherByTeacherID(user.UserId);
                var course = HttpContext.Current.Session["CurrentCourse"] as Course;
                this.lblCourse.Text = course.CourseInfo.Name;
                this.lblTeacher.Text = teacher.Name;
                this.lblEmail.Text = teacher.Email;
                double.TryParse(TextBox2.Text, out this.workingGrade);
                //this.currFeedBack = this.currentGrade.Feedback == null ? "" : this.currentGrade.Feedback;
                this.workingFeedback = this.tbxDescription.Text;

            }
            
            
            
        }

        private void populateStudentDDL(StudentDAL studentDAL)
        {
            var course =(Course) HttpContext.Current.Session["CurrentCourse"];
            var crn = course.CourseInfo.CRN;
            var students = studentDAL.GetStudentsByCRN(crn);
            int counter = 0;
            int index = 0;
            
            foreach (var student in students)
            {
                if (student.StudentUID == this.currentStudent.StudentUID)
                {
                    index = counter;
                }
                this.ddlStudentNames.Items.Add(new ListItem(student.name, student.StudentUID));
                counter++;
            }
            this.ddlStudentNames.SelectedIndex = index;

        }

       

        protected void ddlStudentNames_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int itemId = this.ddlAssignmentNames.SelectedIndex;
            this.ddlAssignmentNames.Items.Clear();
            this.ddlAssignmentNames.Items.Add(new ListItem("Assignment Name"));
            var course = (Course)HttpContext.Current.Session["CurrentCourse"];
            var gradedItems = gradeItemDAL.GetGradedItemsByStudentId(this.ddlStudentNames.SelectedValue,course.CourseInfo.CRN);
            int count = 0;

            foreach (var item in gradedItems)
            {
                this.ddlAssignmentNames.Items.Add(new ListItem(item.Name, item.GradeId.ToString()));
                if (count == itemId)
                {
                    this.currentGrade = item;
                    HttpContext.Current.Session["CurrentGradedItem"] = item;
                    this.ddlAssignmentNames.SelectedIndex = count;
                }

                count++;
            }

            this.ddlAssignmentNames_SelectedIndexChanged(null,null);
            this.UpdatePanel1.Update();
        }

        protected void ddlAssignmentNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            var course = (Course)HttpContext.Current.Session["CurrentCourse"];
            var gradedItems = gradeItemDAL.GetGradedItemsByStudentId(this.ddlStudentNames.SelectedValue, course.CourseInfo.CRN);
            int.TryParse(this.ddlAssignmentNames.SelectedValue, out int itemId);
            var totalPoints = 0.0;
            GradedItem currGradedItem = null;
            foreach (var item in gradedItems)
            {
                if (item.GradeId == itemId)
                {
                    currGradedItem = item;
                    this.currentGrade = item;
                    HttpContext.Current.Session["CurrentGradedItem"] = item;
                }
            }

            if (currGradedItem != null)
            {
                totalPoints = currGradedItem.PossiblePoints;
                this.grade.Text = "/" + totalPoints;
                this.TextBox2.Text = currGradedItem.Grade.ToString();
                this.tbxDescription.Text = currGradedItem.Feedback;
                
            }
            else
            {
                totalPoints = 0;
                this.grade.Text = "";
                this.TextBox2.Text = "";
                this.tbxDescription.Text = "";
            }

            this.UpdatePanel2.Update();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            gradeGradeItem();
            
        }

        private void gradeGradeItem()
        {
            var updatedGrade = new GradedItem()
            {
                Feedback = tbxDescription.Text,
                Grade = int.Parse(this.TextBox2.Text),
                GradeId = this.currentGrade.GradeId,
                Name = this.currentGrade.Name
            };
            var course = (Course) HttpContext.Current.Session["CurrentCourse"];
            var crn = course.CourseInfo.CRN;

            this.gradeItemDAL.gradeGradedItemByCRNAndStudentUID(updatedGrade, crn, this.ddlStudentNames.SelectedValue);
            this.currentGrade.Grade = this.workingGrade;
            this.currentGrade.Feedback = this.workingFeedback;
            this.gradedModal.Show();
        }

        protected  void Button4_Click(object sender, EventArgs e)
        {

            if (this.currentGrade.Grade != this.workingGrade || !this.currentGrade.Feedback.Equals(this.workingFeedback)  )
            {
                this.unsavedChangesModal.Show();
            }
            else
            {
                showNextStudent();
            }

            
        }

        private void showNextStudent()
        {
            if (this.ddlStudentNames.SelectedIndex < this.ddlStudentNames.Items.Count)
            {
                this.ddlStudentNames.SelectedIndex = this.ddlStudentNames.SelectedIndex + 1;
            }
            else
            {
                this.ddlStudentNames.SelectedIndex = 0;
            }

            this.ddlStudentNames_OnSelectedIndexChanged(null, null);
            int count = 0;
            foreach (var item in this.ddlAssignmentNames.Items)
            {
                if (item.ToString() == this.currentGrade.Name)
                {
                    this.ddlAssignmentNames.SelectedIndex = count;
                }

                count++;
            }

            this.ddlAssignmentNames_SelectedIndexChanged(null, null);
            DataBind();
        }


        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(TextBox2.Text, out this.workingGrade);

        }

        protected void savebtn_OnClick(object sender, EventArgs e)
        {
            this.gradeGradeItem();
            

        }

        protected void continuebtn_OnClick(object sender, EventArgs e)
        {
            showNextStudent();
        }

        #endregion

        protected void tbxDescription_TextChanged(object sender, EventArgs e)
        {
            this.workingFeedback = tbxDescription.Text;
        }
    }
}
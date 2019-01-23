using System;
using System.Web;
using System.Web.UI.WebControls;
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CourseManagement
{
    public partial class TeacherGradeGradeItemPage : System.Web.UI.Page
    {
        #region Methods

        private GradedItemDAL gradeItemDAL = new GradedItemDAL();
        private GradedItem currentGrade;
       
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var studentDAL = new StudentDAL();
            
            if (!IsPostBack)
            {
                populateStudentDDL(studentDAL); 
                DataBind();
            }

             this.currentGrade = HttpContext.Current.Session["CurrentGradedItem"] as GradedItem;


            if (currentGrade != null)
            {
                var user = HttpContext.Current.Session["User"] as User;
                TeacherDAL teacherDAL = new TeacherDAL();
                Teacher teacher = teacherDAL.GetTeacherByTeacherID(user.UserId);
                var course = HttpContext.Current.Session["CurrentCourse"] as string;
                this.lblCourse.Text = course;
                this.lblTeacher.Text = teacher.Name;
                this.lblEmail.Text = teacher.Email;
                
            }
            
            
        }

        private void populateStudentDDL(StudentDAL studentDAL)
        {
            var course =(Course) HttpContext.Current.Session["CurrentCourse"];
            var crn = course.CourseInfo.CRN;
            var students = studentDAL.GetStudentsByCRN(crn);
            foreach (var student in students)
            {
                this.ddlStudentNames.Items.Add(new ListItem(student.name, student.StudentUID));
            }
        }

        #endregion

        protected void ddlStudentNames_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlAssignmentNames.Items.Clear();
            this.ddlAssignmentNames.Items.Add(new ListItem("Assignment Name"));
            var gradedItems = gradeItemDAL.GetGradedItemsByStudentId(this.ddlStudentNames.SelectedValue, 1);
            foreach (var item in gradedItems)
            {
                this.ddlAssignmentNames.Items.Add(new ListItem(item.Name, item.GradeId.ToString()));
            }
            this.UpdatePanel1.Update();
        }

        protected void ddlAssignmentNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var gradedItems = gradeItemDAL.GetGradedItemsByStudentId(this.ddlStudentNames.SelectedValue, 1);
            int.TryParse(this.ddlAssignmentNames.SelectedValue, out int itemId);
            var totalPoints = 0;
            GradedItem currGradedItem = null;
            foreach (var item in gradedItems)
            {
                if (item.GradeId == itemId)
                {
                    currGradedItem = item;
                }
            }

            if (currGradedItem != null)
            {
                totalPoints = currGradedItem.PossiblePoints;
                this.grade.Text = "/" + totalPoints;
                this.TextBox2.Text = currGradedItem.Grade.ToString();
                this.TextBox1.Text = currGradedItem.Feedback;
                
            }
            else
            {
                totalPoints = 0;
                this.grade.Text = "";
                this.TextBox2.Text = "";
                this.TextBox1.Text = "";
            }

            this.UpdatePanel2.Update();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var studentDAL = new StudentDAL();
            var updatedGrade = new GradedItem()
            {
                Feedback = TextBox1.Text,
                Grade = int.Parse(this.TextBox2.Text),
                GradeId = this.currentGrade.GradeId,
                Name = this.currentGrade.Name

            };
            var course =(Course) HttpContext.Current.Session["CurrentCourse"];
            var crn = course.CourseInfo.CRN;
               
            this.gradeItemDAL.gradeGradedItemByCRNAndStudentUID(updatedGrade,crn,this.ddlStudentNames.SelectedValue);
        }
    }
}
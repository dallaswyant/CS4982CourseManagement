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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var studentDAL = new StudentDAL();
            
            if (!IsPostBack)
            {
                populateStudentDDL(studentDAL); //TODO right now this just uses psychology course
                DataBind();
            }

            GradedItem currentGrade = HttpContext.Current.Session["CurrentGradedItem"] as GradedItem;

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
            var students = studentDAL.GetStudentsByCRN(1);//TODO change "1" to be what ever course the teacher is viewing
            foreach (var student in students)
            {
                this.ddlStudentNames.Items.Add(new ListItem(student.name, student.StudentUID));
            }
        }

        #endregion

        protected void ddlStudentNames_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
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
            var itemID = int.Parse(this.ddlAssignmentNames.SelectedValue);
            var totalPoints = 0;
            GradedItem currGradedItem = null;
            foreach (var item in gradedItems)
            {
                if (item.GradeId == itemID)
                {
                    currGradedItem = item;
                }
            }

            totalPoints = currGradedItem.PossiblePoints;
            this.grade.Text = "/" + totalPoints;
            this.UpdatePanel2.Update();
        }
    }
}
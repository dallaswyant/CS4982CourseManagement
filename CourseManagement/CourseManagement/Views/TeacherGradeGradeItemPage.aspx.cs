using System;
using System.Web.UI.WebControls;
using CourseManagement.DAL;

namespace CourseManagement
{
    public partial class TeacherGradeGradeItemPage : System.Web.UI.Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            var studentDAL = new StudentDAL();
            var gradeItemDAL = new GradedItemDAL();
            if (!IsPostBack)
            {
                populateStudentDDL(studentDAL); //TODO right now this just uses psychology course
                DataBind();
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
            var gradeItemDAL = new GradedItemDAL();
            var gradedItems = gradeItemDAL.GetGradedItemsByStudentId(this.ddlStudentNames.SelectedValue, 1);
            foreach (var item in gradedItems)
            {
                this.ddlAssignmentNames.Items.Add(new ListItem(item.Name, item.GradeId.ToString()));
            }
            this.UpdatePanel1.Update();
        }
    }
}
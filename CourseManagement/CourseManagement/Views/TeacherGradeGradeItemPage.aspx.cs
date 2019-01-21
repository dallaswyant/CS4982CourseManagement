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
            var students = studentDAL.GetStudentsByCRN(1);
            foreach (var student in students)
            {
                this.ddlStudentNames.Items.Add(new ListItem(student.name,student.StudentUID));
            }
           
        }

        #endregion
    }
}
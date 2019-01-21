using System;
using System.Web.UI.WebControls;
using CourseManagement.App_Code;

namespace CourseManagement.Views
{
    public partial class TeacherViewAllGrades : System.Web.UI.Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void gvwGrade_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GradedItem course = this.gvwGradeItems.SelectedRow.DataItem as GradedItem;


        }

        #endregion
    }
}
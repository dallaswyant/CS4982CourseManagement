using System;
using System.Web.UI.WebControls;

namespace CourseManagement.Views
{
    public partial class StudentViewAllGrades : System.Web.UI.Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Response.Redirect("StudentViewGradeItem.aspx");
        }

        #endregion
    }
}
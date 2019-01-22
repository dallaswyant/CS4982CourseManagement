using System;

namespace CourseManagement.Views
{
    public partial class RubricPage : System.Web.UI.Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        protected void GridView1_RowUpdated(object sender, System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
        {
            this.Validate("gridview");
        }
    }
}
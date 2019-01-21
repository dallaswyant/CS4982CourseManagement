using System;
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CourseManagement
{
    public partial class ManageCreateGradeItem : System.Web.UI.Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            var GradeDal = new GradedItemDAL();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            var assignmentName = this.tbxAssignmentName.Text;
            //GradedItem item = new GradedItem()
                // {
               
           // };
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
        }

        #endregion
    }
}
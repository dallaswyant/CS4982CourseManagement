using System;
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CourseManagement
{
    public partial class ManageCreateGradeItem : System.Web.UI.Page
    {
        #region Methods

        private GradedItemDAL GradeDAL;
        protected void Page_Load(object sender, EventArgs e)
        {
             this.GradeDAL = new GradedItemDAL();
            var RubricDAL = new CourseRubricDAL();
            //RubricDAL.GetCourseRubricByCRN();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            var assignmentName = this.tbxAssignmentName.Text;
            var possiblePoints = Convert.ToInt32(this.tbxPossiblePoints.Text);
            var gradeType = this.ddlAssignmentType.SelectedValue;
            GradedItem item = new GradedItem(assignmentName, null, 0, string.Empty, possiblePoints, gradeType, 0);
            this.GradeDAL.InsertNewGradedItemByCRNForAllStudents(item,1);

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
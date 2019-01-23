using System;
using System.Web;
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CourseManagement
{
    public partial class ManageCreateGradeItem : System.Web.UI.Page
    {
        #region Methods

        private GradedItemDAL GradeDAL;
        private GradedItem currentGradedItem;
        private Course currentCourse;
        private bool wasDeleted;
        protected void Page_Load(object sender, EventArgs e) {

            if (this.wasDeleted)
            {
                this.tbxAssignmentName.Text = string.Empty;
                this.tbxPossiblePoints.Text = string.Empty;
                this.btnCreate.Text = "Create";
                this.wasDeleted = false;
            }
            else
            {
                if (!IsPostBack)
                {
                    if (HttpContext.Current.Session["CurrentGradedItem"] != null)
                    {
                        this.currentGradedItem = HttpContext.Current.Session["CurrentGradedItem"] as GradedItem;
                        this.tbxAssignmentName.Text = this.currentGradedItem.Name;
                        this.tbxPossiblePoints.Text = this.currentGradedItem.PossiblePoints.ToString();
                        this.btnCreate.Text = "Update";
                    }
                }

                if (HttpContext.Current.Session["CurrentGradedItem"] != null)
                {
                    this.currentGradedItem = HttpContext.Current.Session["CurrentGradedItem"] as GradedItem;
                }

                this.currentCourse = HttpContext.Current.Session["CurrentCourse"] as Course;
                this.GradeDAL = new GradedItemDAL();
                var RubricDAL = new CourseRubricDAL();
                //RubricDAL.GetCourseRubricByCRN();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["CurrentGradedItem"] != null)
            {
                /**
                this.currentCourse = HttpContext.Current.Session["CurrentCourse"] as Course;
                this.currentGradedItem = HttpContext.Current.Session["CurrentGradedItem"] as GradedItem;
                this.tbxAssignmentName.Text = this.currentGradedItem.Name;
                this.tbxPossiblePoints.Text = this.currentGradedItem.PossiblePoints.ToString();**/
                var assignmentName = this.tbxAssignmentName.Text;
                var possiblePoints = Convert.ToInt32(this.tbxPossiblePoints.Text);
                var gradeType = this.ddlAssignmentType.SelectedValue;
                GradedItem item = new GradedItem(assignmentName, null, 0, string.Empty, possiblePoints, gradeType, 0);
                this.GradeDAL.UpdateGradeItemByCRNAndOldNameForAllStudents(item, this.currentCourse.CourseInfo.CRN,this.currentGradedItem.Name);
            }
            else
            {
                var assignmentName = this.tbxAssignmentName.Text;
                var possiblePoints = Convert.ToInt32(this.tbxPossiblePoints.Text);
                var gradeType = this.ddlAssignmentType.SelectedValue;
                GradedItem item = new GradedItem(assignmentName, null, 0, string.Empty, possiblePoints, gradeType, 0);
                this.GradeDAL.InsertNewGradedItemByCRNForAllStudents(item, int.Parse(this.ddlCourses.SelectedValue));
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var assignmentName = this.tbxAssignmentName.Text;
            var possiblePoints = Convert.ToInt32(this.tbxPossiblePoints.Text);
            var gradeType = this.ddlAssignmentType.SelectedValue;
            GradedItem item = new GradedItem(assignmentName, null, 0, string.Empty, possiblePoints, gradeType, 0);
            this.GradeDAL.deleteGradedItemByCRNForAllStudents(item, this.currentCourse.CourseInfo.CRN);
            this.wasDeleted = true;
        }

        #endregion
    }
}
using System;
using System.Web;
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CourseManagement
{
    public partial class ManageCreateGradeItem : System.Web.UI.Page
    {
        #region Methods

        private GradeItemDAL GradeDAL;
        private GradeItem currentGradedItem;
        private Course currentCourse;
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack)
            {
                this.handleExistingGradeItemOnLoad();
            }

            this.displayExistingGradeItem();
            this.handleCourses();


        }

        private void handleCourses()
        {
            if (HttpContext.Current.Session["CurrentCourse"] != null)
            {
                this.currentCourse = HttpContext.Current.Session["CurrentCourse"] as Course;
                this.GradeDAL = new GradeItemDAL();
                var RubricDAL = new CourseRubricDAL();
                RubricDAL.GetCourseRubricByCRN(this.currentCourse.CourseInfo.CRN);
            }
        }

        private void displayExistingGradeItem()
        {
            if (HttpContext.Current.Session["CurrentGradedItem"] != null&&HttpContext.Current.Session["editing"] != null)
            {
                bool isEditing = (bool)HttpContext.Current.Session["editing"];
                if (isEditing)
                {
                    this.currentGradedItem = HttpContext.Current.Session["CurrentGradedItem"] as GradeItem;
                }
            }
        }

        private void handleExistingGradeItemOnLoad()
        {
            if (HttpContext.Current.Session["CurrentGradedItem"] != null && HttpContext.Current.Session["editing"] != null)
            {
                this.currentGradedItem = HttpContext.Current.Session["CurrentGradedItem"] as GradeItem;
                this.tbxAssignmentName.Text = this.currentGradedItem.Name;
                this.tbxPossiblePoints.Text = this.currentGradedItem.PossiblePoints.ToString();
                this.btnCreate.Text = "Update";
                this.cbxIsVisible.Checked = this.currentGradedItem.IsPublic;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            this.GradeDAL = new GradeItemDAL();
            if (HttpContext.Current.Session["CurrentGradedItem"] != null && HttpContext.Current.Session["editing"] != null)
            {
                this.handleWhenGradeItemExists();
            }
            else
            {
                this.handleWhenGradeITemNotExists();
            }

            
                
                this.createdModal.Show();
           
            

        }

        private void handleWhenGradeITemNotExists()
        {
            var assignmentName = this.tbxAssignmentName.Text;
            var possiblePoints = Convert.ToInt32(this.tbxPossiblePoints.Text);
            var gradeType = this.ddlAssignmentType.SelectedValue;
            GradeItem item = new GradeItem(assignmentName, null, 0, string.Empty, possiblePoints, gradeType, 0, this.cbxIsVisible.Checked, null);
            this.GradeDAL.InsertNewGradedItemByCRNForAllStudents(item, int.Parse(this.ddlCourses.SelectedValue));
        }

        private void handleWhenGradeItemExists()
        {
            var assignmentName = this.tbxAssignmentName.Text;
            var possiblePoints = Convert.ToInt32(this.tbxPossiblePoints.Text);
            var gradeType = this.ddlAssignmentType.SelectedValue;
            GradeItem item = null;
            if (HttpContext.Current.Session["CurrentGradedItem"] != null && HttpContext.Current.Session["editing"] != null)
            {
                this.currentGradedItem = HttpContext.Current.Session["CurrentGradedItem"] as GradeItem;
                item = new GradeItem(assignmentName, null, 0, string.Empty, possiblePoints, gradeType, 0, this.cbxIsVisible.Checked, this.currentGradedItem.TimeGraded);
            }
            else
            {
                item = new GradeItem(assignmentName, null, 0, string.Empty, possiblePoints, gradeType, 0, this.cbxIsVisible.Checked, null);
            }
            
                
            this.GradeDAL.UpdateGradeItemByCRNAndOldNameForAllStudents(item, this.currentCourse.CourseInfo.CRN,
                this.currentGradedItem.Name);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var assignmentName = this.tbxAssignmentName.Text;
            var possiblePoints = Convert.ToInt32(this.tbxPossiblePoints.Text);
            var gradeType = this.ddlAssignmentType.SelectedValue;
            GradeItem item = new GradeItem(assignmentName, null, 0, string.Empty, possiblePoints, gradeType, 0, false, null);
            this.GradeDAL.deleteGradedItemByCRNForAllStudents(item, this.currentCourse.CourseInfo.CRN);
            HttpContext.Current.Session["CurrentGradedItem"] = null;
            HttpContext.Current.Session["editing"] = null;

            
            this.deleteModal.Show();
        }

        #endregion

        protected void okayBtn1_Click(object sender, EventArgs e)
        {
            this.btnCreate.Text = "Update";
            HttpContext.Current.Session["editing"] = null;
            HttpContext.Current.Session["CurrentGradedItem"] = null;
            Response.Redirect("TeacherViewAllGrades.aspx");
        }

        protected void okayBtn2_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["CurrentGradedItem"] = null;
            HttpContext.Current.Session["editing"] = null;
            Response.Redirect("ManageCreateGradeItem.aspx");
        }
    }
}
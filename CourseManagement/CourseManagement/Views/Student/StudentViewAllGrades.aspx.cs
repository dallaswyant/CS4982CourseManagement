using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CourseManagement.Views
{
    public partial class StudentViewAllGrades : System.Web.UI.Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {

        }



        private void loadGrades(User currentUser)
        {

                CourseRubricDAL rubricGetter = new CourseRubricDAL();
                GradedItemDAL gradeGetter = new GradedItemDAL();
                List<RubricItem> rubric =
                    rubricGetter.GetCourseRubricByCRN(int.Parse(this.ddlStudentCourses.SelectedItem.Value));
                List<GradedItem> grades =
                    gradeGetter.GetGradedItemsByStudentId(currentUser.UserId,
                        int.Parse(this.ddlStudentCourses.SelectedItem.Value));

                assignCurrentGrade(rubric, grades);
            
        }

        private void assignCurrentGrade(List<RubricItem> rubric, List<GradedItem> grades)
        {
            double overallGrade = computeOverallGrade(rubric, grades);
            GridViewRow currentRow = this.gvwCourses.Rows[0];
            TableCell currentCell = currentRow.Cells[this.gvwCourses.Rows[0].Cells.Count - 1];
            currentCell.Text = overallGrade.ToString();
        }

        private static double computeOverallGrade(List<RubricItem> rubric, List<GradedItem> grades)
        {
            double overallGrade = 0.0;
            foreach (var rubricItem in rubric)
            {
                foreach (var grade in grades)
                {
                    if (grade.GradeType.Equals(rubricItem.AssignmentType))
                    {
                        overallGrade += (grade.Grade / grade.PossiblePoints) * rubricItem.AssignmentWeight;
                    }
                }
            }

            return overallGrade;
        }

        protected void CourseGrid_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["User"] != null)
            {
                User currentUser = HttpContext.Current.Session["User"] as User;
                loadGrades(currentUser);
            }
        }

        protected void GradesGrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Response.Redirect("StudentViewGradeItem.aspx");
        }

        #endregion

        protected void ddlStudentCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            User currentUser = HttpContext.Current.Session["User"] as User;
            loadGrades(currentUser);
        }

        protected void gvwGrades_Load(object sender, EventArgs e)
        {
            User currentUser = HttpContext.Current.Session["User"] as User;
            loadGrades(currentUser);
        }
    }
}
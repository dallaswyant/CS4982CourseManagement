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

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Response.Redirect("StudentViewGradeItem.aspx");
        }

        #endregion

        protected void GridView2_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["User"] != null)
            {
                
                User currentUser = HttpContext.Current.Session["User"] as User;
                for (int i = 0; i<this.GridView2.Rows.Count;i++)
                {
                    CourseRubricDAL rubricGetter = new CourseRubricDAL();
                    GradedItemDAL gradeGetter = new GradedItemDAL();
                    List<RubricItem> rubric = rubricGetter.GetCourseRubricByCRN(int.Parse(this.ddlStudentCourses.SelectedItem.Value));
                    List<GradedItem> grades =
                        gradeGetter.GetGradedItemsByStudentId(currentUser.UserId, int.Parse(this.ddlStudentCourses.SelectedItem.Value));
                    
                   double overallGrade = computeOverallGrade(rubric, grades);

                    this.GridView2.Rows[i].Cells[this.GridView2.Rows[i].Cells.Count - 1].Text = overallGrade.ToString();
                }
            }
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
    }
}
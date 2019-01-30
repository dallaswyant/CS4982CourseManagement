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
                    List<RubricItem> rubric = rubricGetter.GetCourseRubricByCRN(this.ddlStudentCourses.SelectedIndex);
                    List<GradedItem> grades =
                        gradeGetter.GetGradedItemsByStudentId(currentUser.UserId, this.ddlStudentCourses.SelectedIndex);
                    double overallGrade = 0.0;
                    foreach (var t in grades)
                    {
                        RubricItem item = rubric.Find(x => x.AssignmentType.Equals(t.GradeType));
                        overallGrade += t.Grade / t.PossiblePoints * item.AssignmentWeight;
                    }

                    this.GridView2.Rows[i].Cells[this.GridView2.Rows[i].Cells.Count - 1].Text = overallGrade.ToString();
                }
            }
        }
    }
}
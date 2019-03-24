using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CourseManagement.Utilities;

namespace CourseManagement.Views.Teacher
{
    public partial class FinalGradePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
      

            this.pointsEarned.Text = "";
            this.gradePercentage.Text = "";
        }

        protected void gvwGrades_DataBound(object sender, GridViewRowEventArgs e)
        {
            double pointsEarnedSum = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                pointsEarnedSum += Convert.ToDouble(e.Row.Cells[1].Text);

            }
            double pointsPossibleSum = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                pointsPossibleSum += Convert.ToDouble(e.Row.Cells[1].Text);
            }

            pointsEarned.Text = pointsEarnedSum + "/" + pointsPossibleSum;
            double grade = (pointsEarnedSum / pointsPossibleSum);
            this.gradePercentage.Text = grade.ToString("P");
            this.finalGrade.Attributes["placeholder"] = "Suggested Grade: " + GradeSuggester.GetSuggestedGrade(grade);
        }
    }
}
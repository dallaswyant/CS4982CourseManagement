using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CourseManagement.DAL;
using CourseManagement.Utilities;

namespace CourseManagement.Views.Teacher
{
    public partial class FinalGradePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void gvwGrades_DataBound(object sender, EventArgs e)
        {
            this.confirmation.Text = "";
            double pointsEarnedSum = 0;
            double pointsPossibleSum = 0;
            foreach (GridViewRow row in this.gvwGrades.Rows)
            {
                pointsEarnedSum += Convert.ToDouble(row.Cells[1].Text);
                pointsPossibleSum += Convert.ToDouble(row.Cells[2].Text);
            }
            if (pointsPossibleSum > 0)
            {
                this.handleValidStudentGradeState(pointsEarnedSum, pointsPossibleSum);
            }
            else
            {
                this.handleInvalidGradeState();
            }
        }

        private void handleInvalidGradeState()
        {
            this.finalLetterGrade.Attributes["placeholder"] = "";
            this.pointsEarned.Text = "";
            this.gradePercentage.Text = "";
        }

        private void handleValidStudentGradeState(double pointsEarnedSum, double pointsPossibleSum)
        {
            this.pointsEarned.Text = pointsEarnedSum + "/" + pointsPossibleSum;
            double grade = (pointsEarnedSum / pointsPossibleSum);
            this.gradePercentage.Text = grade.ToString("P");
            StudentDAL dal = new StudentDAL();
            char? letterGrade = dal.GetGradeByCourseAndStudentID(int.Parse(this.ddlCourses.SelectedValue),
                this.ddlStudents.SelectedValue);
            if (letterGrade == '\0' || letterGrade == null)
            {
                this.finalLetterGrade.Attributes["placeholder"] = "Suggested Grade: " + GradeSuggester.GetSuggestedGrade(grade);
            }
            else
            {
                this.finalLetterGrade.Attributes["placeholder"] = "Last Saved Grade: " + letterGrade;
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            TeacherDAL dal = new TeacherDAL();
            dal.UpdateFinalGradeByCRNAndStudentID(int.Parse(this.ddlCourses.SelectedValue),
                this.ddlStudents.SelectedValue, this.finalLetterGrade.Text[0]);
            this.confirmation.Text = "Grade saved.";
            this.finalLetterGrade.Text = "";

        }
    }
}
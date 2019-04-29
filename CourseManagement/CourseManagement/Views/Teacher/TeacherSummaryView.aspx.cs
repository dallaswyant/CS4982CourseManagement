using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CourseManagement.DAL;
using CourseManagement.Models;

namespace CourseManagement.Views.Teacher
{
    public partial class TeacherSummaryView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
            this.lblError.Text = String.Empty;
            this.PopulateDataGridWithSummaryDataBasedOnCRN(Convert.ToInt32(this.ddlCourses.SelectedValue));
        }

        private void PopulateDataGridWithSummaryDataBasedOnCRN(int crn)
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                StudentDAL studentGetter = new StudentDAL();
                List<Models.Student> studentList = studentGetter.GetStudentsByCRN(crn);
                GradeItemDAL gradeGetter = new GradeItemDAL();
                Dictionary<string, string> gradeList = gradeGetter.GetUniqueGradedItemsByCRN(crn);
                List<List<GradeItem>> listOfAllGrades = new List<List<GradeItem>>();
                CourseRubricDAL rubricGetter = new CourseRubricDAL();
                List<RubricItem> rubric = rubricGetter.GetCourseRubricByCRN(crn);
                foreach (var student in studentList)
                {
                    listOfAllGrades.Add(gradeGetter.GetGradedItemsByStudentId(student.StudentUID, crn));
                }

                dt.Columns.Add("Student Names", System.Type.GetType("System.String"));
                foreach (var grade in listOfAllGrades[0])
                {
                    dt.Columns.Add(grade.Name, System.Type.GetType("System.String"));
                }

                dt.Columns.Add("Overall Grade", System.Type.GetType("System.String"));
                int counter = 0;
                foreach (var listOfGrades in listOfAllGrades)
                {
                    dr = dt.NewRow();
                    dr["Student Names"] = studentList[counter].Name;
                    foreach (var grade in listOfGrades)
                    {
                        dr[grade.Name] = (grade.Grade / grade.PossiblePoints).ToString("P");
                    }

                    dr["Overall Grade"] = computeOverallGrade(rubric, listOfGrades).ToString("F") + "%";
                    dt.Rows.Add(dr);
                    counter++;
                }

                dt.AcceptChanges();
                this.gvwSummary.DataSource = dt;
                this.gvwSummary.DataBind();
            }
            catch (Exception e)
            {
                this.lblError.Text = "This course has no data to display";
            }

        }

        private static double computeOverallGrade(List<RubricItem> rubric, List<GradeItem> grades)
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.DAL;
using CourseManagement.Models;
using CoursesManagementDesktop.Model;

namespace CoursesManagementDesktop.Controllers
{
    class SummaryViewController
    {

        private TeacherSummaryViewPage summaryPage;
        private CourseDAL courseDAL;
        private SemesterDAL semesterDAL;

        public SummaryViewController(TeacherSummaryViewPage summaryPage)
        {
            this.summaryPage = summaryPage;
            this.courseDAL = new CourseDAL();
            this.semesterDAL = new SemesterDAL();
        }

        /// <summary>
        ///     Populates the combo boxes.
        /// </summary>
        public void populateComboBoxes()
        {
            this.populateSemesterComboBox();
            this.populateCourseComboBox();
           
        }

        

       

        private void populateCourseComboBox()
        {
            var semester = this.summaryPage.semesterBox.Text;
            var courses = this.courseDAL.GetCoursesByTeacherAndSemester(CourseManagementTools.TeacherID, semester);

            foreach (var name in courses)
            {
                this.summaryPage.CourseCombo.Items.Add(name.Name);
            }

            this.summaryPage.CourseCombo.SelectedIndex = 0;
        }

        private void populateSemesterComboBox()
        {
            var index = 0;
            var semesters = this.semesterDAL.GetAllSemesters();
            foreach (var semester in semesters)
            {
                this.summaryPage.semesterBox.Items.Add(semester.SemesterID);
                if ((semester.StartDate < DateTime.Now) & (semester.EndDate > DateTime.Now))
                {
                    this.summaryPage.semesterBox.SelectedIndex = index;
                }

                index++;
            }
        }

        public void LoadDataGrid()
        {
           var crn =  CourseManagementTools.findCrn(this.summaryPage.CourseCombo.SelectedItem as string,
                this.summaryPage.semesterBox.SelectedItem as string);
            this.PopulateDataGridWithSummaryDataBasedOnCRN(crn);
        }

         private void PopulateDataGridWithSummaryDataBasedOnCRN(int crn)
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                StudentDAL studentGetter = new StudentDAL();
                List<CourseManagement.Models.Student> studentList = studentGetter.GetStudentsByCRN(crn);
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
                this.summaryPage.dataGridGrades.ItemsSource = dt.DefaultView;
                //this.gvwSummary.DataSource = dt;
                //this.gvwSummary.DataBind();
            }
            catch (Exception e)
            {
                //this.lblError.Text = "This course has no data to display";
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

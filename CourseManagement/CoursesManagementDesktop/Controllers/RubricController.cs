using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.DAL;
using CoursesManagementDesktop.Model;

namespace CoursesManagementDesktop.Controllers
{
    public class RubricController
    {

        public ManageRubricPage rubricPage { get; private set; }
        public CourseDAL courseDAL { get; private set; }
        public SemesterDAL semesterDal { get; private set; }

        public RubricController(ManageRubricPage rubricPage)
        {
            this.rubricPage = rubricPage;
            this.courseDAL = new CourseDAL();
            this.semesterDal = new SemesterDAL();
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
            string semester = this.rubricPage.semesterComboBox.Text;
            var courses = this.courseDAL.GetCoursesByTeacherAndSemester(CourseManagementTools.TeacherID,semester);

            foreach (var name in courses)
            {
                this.rubricPage.courseComboBox.Items.Add(name.Name);
            }

            this.rubricPage.courseComboBox.SelectedIndex = 0;
        }

        private void populateSemesterComboBox()
        {
            int index = 0;
            var semesters = this.semesterDal.GetAllSemesters();
            foreach (var semester in semesters)
            {
                this.rubricPage.semesterComboBox.Items.Add(semester.SemesterID);
                if (semester.StartDate < DateTime.Now & semester.EndDate > DateTime.Now)
                {
                    this.rubricPage.semesterComboBox.SelectedIndex = index;
                }

                index++;
            }
        }

        /// <summary>
        ///     Loads the data grid.
        /// </summary>
        //public void LoadDataGrid()
        //{

        //    var name = this.homePage.AssignmentCombo.SelectedItem == null ? "" : this.homePage.AssignmentCombo.SelectedItem.ToString();
        //    var crn = CourseManagementTools.findCrn(this.homePage.CourseCombo.Text, this.homePage.semesterBox.Text);

        //    this.desktopGradedItemDal.populateDataGrid(crn, name, this.homePage.dataGridGrades);
        //    this.currentCrn = crn;

        //}


    }
}

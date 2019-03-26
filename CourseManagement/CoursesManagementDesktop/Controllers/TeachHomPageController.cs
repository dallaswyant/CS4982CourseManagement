using System;
using System.Threading.Tasks;
using CourseManagement.DAL;
using CoursesManagementDesktop.DAL;
using CoursesManagementDesktop.Model;

namespace CoursesManagementDesktop.Controllers
{
    /// <summary>
    ///     Defines the Teacher home page controller
    /// </summary>
    public class TeacherHomePageController
    {
        #region Data members

        
        private readonly CourseDAL courseDAL;
        private readonly GradeItemDAL gradedItemDal;
        private readonly DesktopGradedItemDAL desktopGradedItemDal;
        private readonly SemesterDAL semesterDal;
         /// <summary>
         /// public property for current crn
         /// </summary>
        public int currentCrn { get; private set; }
        /// <summary>
        /// public property for selected student index
        /// </summary>
        public int selectedStudent { get;  set; }

        public int selectedAssignment { get; set; }
        /// <summary>
        /// public property for the main teacher page
        /// </summary>
        public TeacherHomePAge homePage { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TeacherHomePageController" /> class.
        /// </summary>
        /// <param name="page">The page.</param>
        public TeacherHomePageController(TeacherHomePAge page)
        {
            if (page == null)
            {
                throw new ArgumentException("page cannot be null");
            }
            this.homePage = page;
            this.courseDAL = new CourseDAL();
            this.gradedItemDal = new GradeItemDAL();
            this.desktopGradedItemDal = new DesktopGradedItemDAL();
            this.semesterDal = new SemesterDAL();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Populates the combo boxes.
        /// </summary>
        public void populateComboBoxes()
        {
            this.populateSemesterComboBox();
            this.populateCourseComboBox();
            this.populateAssignmentComboBox();
        }
        /// <summary>
        /// updates the assignment combo box to the new assignments
        /// </summary>
        public void updateAssignmentBox()
        {
           this.homePage.AssignmentCombo.Items.Clear();
           this.populateAssignmentComboBox();

        }

        private  void  populateAssignmentComboBox()
        {
            var crn = CourseManagementTools.findCrn(this.homePage.CourseCombo.Text,this.homePage.semesterBox.SelectedItem as string);
            var assignments = this.gradedItemDal.GetUniqueGradedItemsByCRN(crn);

            foreach (var name in assignments)
            {
                this.homePage.AssignmentCombo.Items.Add(name.Value);
            }

            this.homePage.AssignmentCombo.SelectedIndex = 0;
        }

        private void populateCourseComboBox()
        {
            string semester = this.homePage.semesterBox.Text;
            var courses = this.courseDAL.GetCoursesByTeacherAndSemester(CourseManagementTools.TeacherID,semester);

            foreach (var name in courses)
            {
                this.homePage.CourseCombo.Items.Add(name.Name);
            }

            this.homePage.CourseCombo.SelectedIndex = 0;
        }

        private void populateSemesterComboBox()
        {
            int index = 0;
            var semesters = this.semesterDal.GetAllSemesters();
            foreach (var semester in semesters)
            {
                this.homePage.semesterBox.Items.Add(semester.SemesterID);
                if (semester.StartDate < DateTime.Now & semester.EndDate > DateTime.Now)
                {
                    this.homePage.semesterBox.SelectedIndex = index;
                }

                index++;
            }
        }

        /// <summary>
        ///     Loads the data grid.
        /// </summary>
        public void LoadDataGrid()
        {
         
                var name = this.homePage.AssignmentCombo.SelectedItem ==null ? "":this.homePage.AssignmentCombo.SelectedItem.ToString();
                var crn = CourseManagementTools.findCrn(this.homePage.CourseCombo.Text,this.homePage.semesterBox.SelectedItem as string);
                this.desktopGradedItemDal.populateDataGrid(crn, name, this.homePage.dataGridGrades);
                this.currentCrn = crn;

        }

        

        #endregion
    }
}
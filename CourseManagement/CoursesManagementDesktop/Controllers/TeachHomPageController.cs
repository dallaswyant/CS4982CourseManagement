using System;
using System.Threading.Tasks;
using CourseManagement.DAL;
using CoursesManagementDesktop.DAL;

namespace CoursesManagementDesktop.Controllers
{
    /// <summary>
    ///     Defines the Teacher home page controller
    /// </summary>
    public class TeacherHomePageController
    {
        #region Data members

        private readonly TeacherHomePAge homePage;
        private readonly CourseDAL courseDAL;
        private readonly GradeItemDAL gradedItemDal;
        private readonly DesktopGradedItemDAL desktopGradedItemDal;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TeacherHomePageController" /> class.
        /// </summary>
        /// <param name="page">The page.</param>
        public TeacherHomePageController(TeacherHomePAge page)
        {
            this.homePage = page;
            this.courseDAL = new CourseDAL();
            this.gradedItemDal = new GradeItemDAL();
            this.desktopGradedItemDal = new DesktopGradedItemDAL();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Populates the combo boxes.
        /// </summary>
        public void populateComboBoxes()
        {
            this.populateCourseComboBox();
            this.populateAssignmentComboBox();
        }

        public void updateAssignmentBox()
        {
           this.homePage.AssignmentCombo.Items.Clear();
           this.populateAssignmentComboBox();

        }

        private  void  populateAssignmentComboBox()
        {
            var crn = this.findCrn(this.homePage.CourseCombo.Text);
            var assignments = this.gradedItemDal.GetUniqueGradedItemsByCRN(crn);

            foreach (var name in assignments)
            {
                this.homePage.AssignmentCombo.Items.Add(name.Value);
            }

            this.homePage.AssignmentCombo.SelectedIndex = 0;
        }

        private void populateCourseComboBox()
        {
            var courses = this.courseDAL.GetCoursesByTeacherID(this.homePage.TeacherId);

            foreach (var name in courses)
            {
                this.homePage.CourseCombo.Items.Add(name.Name);
            }

            this.homePage.CourseCombo.SelectedIndex = 0;
        }

        /// <summary>
        ///     Loads the data grid.
        /// </summary>
        public void LoadDataGrid()
        {
         
                var name = this.homePage.AssignmentCombo.SelectedItem ==null ? "":this.homePage.AssignmentCombo.SelectedItem.ToString();
                var crn = this.findCrn(this.homePage.CourseCombo.Text);
                this.desktopGradedItemDal.populateDataGrid(crn, name, this.homePage.dataGridGrades);
            
            
           
            
        }

        private int findCrn(string courseName)
        {
            var crn = -1;

            var courses = this.courseDAL.GetCoursesByTeacherID(this.homePage.TeacherId);
            foreach (var course in courses)
            {
                if (course.Name.Equals(this.homePage.CourseCombo.Text))
                {
                    crn = course.CRN;
                }
            }

            return crn;
        }

        #endregion
    }
}
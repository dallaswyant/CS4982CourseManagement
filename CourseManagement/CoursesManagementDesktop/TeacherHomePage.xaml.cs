using System.Windows;
using System.Windows.Controls;
using CoursesManagementDesktop.Controllers;
//using CourseManagement.App_Code;
//using CourseManagement.DAL;

//

namespace CoursesManagementDesktop
{
    /// <summary>
    ///     Interaction logic for TeacherHomePAge.xaml
    /// </summary>
    public partial class TeacherHomePAge : Page
    {
        #region Data members

        private readonly TeacherHomePageController controller;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TeacherHomePAge" /> class.
        /// </summary>
        /// <param name="teacherId">The teacher identifier.</param>
        public TeacherHomePAge()
        {
            this.InitializeComponent();

            this.controller = new TeacherHomePageController(this);
            this.controller.populateComboBoxes();
        }

        #endregion

        #region Methods

        private void AssignmentCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.controller.LoadDataGrid();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var page = new LoginPage();
            var navigationService = NavigationService;
            navigationService?.Navigate(page);
        }

        private void SelectGradeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataGridGrades.SelectedIndex > -1)
            {
                this.controller.selectedStudent = this.dataGridGrades.SelectedIndex;
                this.controller.selectedAssignment = this.AssignmentCombo.SelectedIndex;
            }

            var page = new GradePage(this.controller);
            var navigationService = NavigationService;
            navigationService?.Navigate(page);
        }

        private void SemesterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.controller.LoadDataGrid();
        }

        #endregion
    }
}
using System.Windows;
using System.Windows.Controls;
using CoursesManagementDesktop.Controllers;
using CoursesManagementDesktop.Model;

namespace CoursesManagementDesktop
{
    /// <summary>
    ///     Interaction logic for ManageAssignmentPage.xaml
    /// </summary>
    public partial class ManageAssignmentPage : Page
    {
        #region Data members

        private readonly ManageGradeItemsController gradeItemsController;
        public int CRN { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ManageAssignmentPage" /> class.
        /// </summary>
        public ManageAssignmentPage()
        {
            this.InitializeComponent();
            this.gradeItemsController = new ManageGradeItemsController(this);
            this.gradeItemsController.PopulateComboBoxes();
        }

        #endregion

        #region Methods

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            this.gradeItemsController.AddAssignment();
        }

        private void ViewGrades_Click(object sender, RoutedEventArgs e)
        {
            var page = new TeacherHomePAge();
            var navigationService = NavigationService;
            navigationService?.Navigate(page);
        }

        private void AssignmentCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.gradeItemsController.DisplayGradeItemDetails();
            this.CRN =  CourseManagementTools.findCrn(this.courseComboBox.SelectedItem as string,
                this.semesterComboBox.SelectedItem as string);
           
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            this.gradeItemsController.HandleDeletion();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.gradeItemsController.HandleEditing();
        }

        #endregion

        private void SemesterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          this.CRN =  CourseManagementTools.findCrn(this.courseComboBox.SelectedItem as string,
                this.semesterComboBox.SelectedItem as string);
        }

        private void VisibilityCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.gradeItemsController.updateVisibility();
        }

        private void VisibilityCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.gradeItemsController.updateVisibility();
        }
    }
}
using System.Windows;
using System.Windows.Controls;
using CoursesManagementDesktop.Controllers;

namespace CoursesManagementDesktop
{
    /// <summary>
    ///     Interaction logic for ManageAssignmentPage.xaml
    /// </summary>
    public partial class ManageAssignmentPage : Page
    {
        #region Data members

        private readonly ManageGradeItemsController gradeItemsController;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ManageAssignmentPage" /> class.
        /// </summary>
        public ManageAssignmentPage()
        {
            this.InitializeComponent();
            this.gradeItemsController = new ManageGradeItemsController(this);
            this.gradeItemsController.populateComboBoxes();
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
    }
}
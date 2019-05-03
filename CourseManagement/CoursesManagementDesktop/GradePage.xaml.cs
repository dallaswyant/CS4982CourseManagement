using System;
using System.Windows;
using System.Windows.Controls;
using CoursesManagementDesktop.Controllers;

namespace CoursesManagementDesktop
{
    /// <summary>
    ///     Interaction logic for GradePage.xaml
    /// </summary>
    public partial class GradePage : Page
    {
        #region Data members

        private readonly GradePageController gradePageController;

        #endregion

        #region Properties

        public TeacherHomePageController homePageController { get; }

        public int CRN { get; }

        public bool changesMade { get;  set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Grade page constructor
        /// </summary>
        /// <param name="homePageController">the programs homePageController</param>
        /// <precondition>
        ///     homePageController != null
        /// </precondition>
        public GradePage(TeacherHomePageController homePageController)
        {
            if (homePageController == null)
            {
                throw new ArgumentException("homePageController cannot be null");
            }

            this.InitializeComponent();
            this.gradePageController = new GradePageController(this);
            this.homePageController = homePageController;
            this.CRN = this.homePageController.currentCrn;
            this.gradePageController.PopulateStudentCombo();
            this.gradePageController.SetGradeInfo();
            this.changesMade = false;
        }

        #endregion

        #region Methods

        private void GradeButton_Click(object sender, RoutedEventArgs e)
        {
            this.gradePageController.GradeCurrentItem();
        }

        private void updateGradeInfo()
        {
            this.assignmentBox.SelectionChanged -= this.AssignmentBox_SelectionChanged;
            this.assignmentBox.Items.Clear();
            this.assignmentBox.SelectionChanged += this.AssignmentBox_SelectionChanged;
            this.gradePageController.PopulateAssignmentBox();
            this.gradePageController.SetGradeInfo();
        }

        private void StudentCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.updateGradeInfo();
            this.changesMade = false;
        }

        private void AssignmentBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.gradePageController.SetGradeInfo();
            this.changesMade = false;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.gradePageController.ShowNextStudent();
        }

        private void ViewGrades_Click(object sender, RoutedEventArgs e)
        {
            this.homePageController.LoadDataGrid();
            NavigationService.GoBack();
        }

        #endregion

        private void earnedPointsBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.changesMade = true;
        }

        private void feedBackBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.changesMade = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CourseManagement.App_Code;
using CourseManagement.DAL;
using CoursesManagementDesktop.Controllers;
using CoursesManagementDesktop.DAL;
using GradeItemDAL = CourseManagement.DAL.GradeItemDAL;

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

        #region Properties

        /// <summary>
        ///     Gets the teacher identifier.
        /// </summary>
        /// <value>
        ///     The teacher identifier.
        /// </value>
        public string TeacherId { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TeacherHomePAge" /> class.
        /// </summary>
        /// <param name="teacherId">The teacher identifier.</param>
        public TeacherHomePAge(string teacherId)
        {
            this.InitializeComponent();
            this.TeacherId = teacherId;
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

        

        private  void CourseCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.AssignmentCombo.Items.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //this.controller.updateAssignmentBox();
        }

        private void SelectGradeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataGridGrades.SelectedIndex > -1)
            {
                this.controller.selectedStudent = dataGridGrades.SelectedIndex;
            }
            GradePage page = new GradePage(this.controller); 
            var navigationService = NavigationService;
            navigationService?.Navigate(page);

        }

        
    }
}
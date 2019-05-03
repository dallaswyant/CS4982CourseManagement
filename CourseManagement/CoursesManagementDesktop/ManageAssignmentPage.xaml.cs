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
using CourseManagement.DAL;
using CourseManagement.Models;
using CoursesManagementDesktop.Controllers;
using CoursesManagementDesktop.Model;

namespace CoursesManagementDesktop
{
    /// <summary>
    /// Interaction logic for ManageAssignmentPage.xaml
    /// </summary>
    public partial class ManageAssignmentPage : Page
    {
        private ManageGradeItemsController gradeItemsController;
        private GradeItem item;
      
        public ManageAssignmentPage()
        {
            InitializeComponent();
            
            this.gradeItemsController = new ManageGradeItemsController(this);
            this.gradeItemsController.populateComboBoxes();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            this.gradeItemsController.addAssignmnet();
        }

        private void ViewGrades_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null) NavigationService.GoBack();
        }

        private void AssignmentCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GradeItemDAL dal = new GradeItemDAL();
            var crn = CourseManagementTools.findCrn(this.courseComboBox.Text, this.semesterComboBox.Text);
             item = dal.GetGradedItemByCRNAndGradeName(crn, this.AssignmentCombo.Text);
            if (item != null)
            {
                this.assignmentNameBox.Text = item.Name;
                this.pointsBox.Text = item.PossiblePoints.ToString();
            }
            
        }

        private void AssignmentCombo_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}

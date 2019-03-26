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
        private GradeItemDAL gradeItemDal; 
      
        public ManageAssignmentPage()
        {
            InitializeComponent();
            this.gradeItemDal = new GradeItemDAL();
            this.gradeItemsController = new ManageGradeItemsController(this);
            this.gradeItemsController.populateComboBoxes();
            
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            this.gradeItemsController.addAssignmnet();
            this.AssignmentCombo.Items.Clear();
            this.gradeItemsController.populateAssignmentComboBox();
            this.AssignmentCombo.SelectedIndex = 0;
        }

        private void ViewGrades_Click(object sender, RoutedEventArgs e)
        {
            TeacherHomePAge page = new TeacherHomePAge();
            var navigationService = this.NavigationService;
            navigationService?.Navigate(page);
        }

        private void AssignmentCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var crn = CourseManagementTools.findCrn(this.courseComboBox.Text, this.semesterComboBox.Text);
             item = this.gradeItemDal.GetGradedItemByCRNAndGradeName(crn, this.AssignmentCombo.SelectedItem as string);
            if (item != null)
            {
                this.assignmentNameBox.Text = item.Name;
                this.pointsBox.Text = item.PossiblePoints.ToString();
            }
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var value = showConfirmDialog("Delete This Item?");
            if (value != null && value == true)
            {

                var crn = CourseManagementTools.findCrn(this.courseComboBox.Text, this.semesterComboBox.Text);
                if (item != null)
                {
                
                    this.gradeItemDal.deleteGradedItemByCRNForAllStudents(item, crn);
                    this.AssignmentCombo.Items.Remove(item.Name);
                    this.AssignmentCombo.SelectedIndex = 0;
                }
                
            }
            
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var value = showConfirmDialog("Edit This Item?");
            if (value != null && value == true)
            {
                var crn = CourseManagementTools.findCrn(this.courseComboBox.Text, this.semesterComboBox.Text);
                this.item.Name = this.assignmentNameBox.Text;
                this.item.PossiblePoints = Int32.Parse(this.pointsBox.Text);
                this.gradeItemDal.UpdateGradeItemByCRNAndOldNameForAllStudents(item, crn,
                    this.AssignmentCombo.SelectedItem as string);
                this.AssignmentCombo.Items.Clear();
                this.gradeItemsController.populateAssignmentComboBox();
                this.AssignmentCombo.SelectedIndex = 0;
            }
        }

        private static bool? showConfirmDialog(String dialogText)
        {
            confirmationWindow confirmWindow = new confirmationWindow();
            confirmWindow.dialogText.Text =dialogText ;
            confirmWindow.confirmButton.Margin = new Thickness(50, 146, 0, 0);
            confirmWindow.declineButton.Visibility = Visibility.Visible;
            var value = confirmWindow.ShowDialog();
            return value;
        }
    }
}

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
using CourseManagement.Models;
using CoursesManagementDesktop.Controllers;

namespace CoursesManagementDesktop
{
    /// <summary>
    /// Interaction logic for ManageAssignmentPage.xaml
    /// </summary>
    public partial class ManageAssignmentPage : Page
    {
        private ManageGradeItemsController gradeItemsController;
      
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
    }
}

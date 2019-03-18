using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
using CoursesManagementDesktop.Annotations;
using CoursesManagementDesktop.Controllers;
using CoursesManagementDesktop.Model;

namespace CoursesManagementDesktop
{
    /// <summary>
    /// Interaction logic for ManageRubricPage.xaml
    /// </summary>
    public partial class ManageRubricPage : Page,INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RubricController controller { get; set; }

        public CourseRubricDAL rubricDAL { get; set; }

        public ObservableCollection<RubricItem> rubricItems { get; set; }
        public RubricItem originalItem { get; private set; }
        public ManageRubricPage()
        {
            InitializeComponent();
            this.controller = new RubricController(this);
            this.controller.populateComboBoxes();
            this.rubricDAL = new CourseRubricDAL();
            this.rubricItems = new ObservableCollection<RubricItem>();
            this.loadRubric();
            this.setWarningText();
            DataContext = this;
        
        }

        private void loadRubric()
        {
            var crn = CourseManagementTools.findCrn(this.courseComboBox.Text,this.semesterComboBox.Text);
           var items =  this.rubricDAL.GetCourseRubricByCRN(crn);
            foreach (var rubricItem in items)
            {
                this.rubricItems.Add(rubricItem);
            }

        }

        private void refresh()
        {
            this.rubricItems.Clear();
            this.loadRubric();
        }
        

        

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RubricDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.rubricDataGrid.SelectedItem is RubricItem rubricItem)
            {
                this.originalItem = rubricItem;
                this.assignmentTypeBox.Text = originalItem.AssignmentType;
                this.assignmentWeightBox.Text = originalItem.AssignmentWeight.ToString();
            }
           
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var crn = CourseManagementTools.findCrn(this.courseComboBox.Text,this.semesterComboBox.Text);
            var assignmentType = this.assignmentTypeBox.Text;
            var assignmentWeight = Int32.Parse(this.assignmentWeightBox.Text);
            var index = this.originalItem.Index;
            var orginalCRN = this.originalItem.CRN;

            this.rubricDAL.UpdateCourseRubric(crn,assignmentType,assignmentWeight,this.originalItem.AssignmentType,this.originalItem.AssignmentWeight,index,index,orginalCRN);
            this.refresh();
            this.setWarningText();
            this.assignmentTypeBox.Clear();
            this.assignmentWeightBox.Clear();
            
        }

        private void setWarningText()
        {
            var crn = CourseManagementTools.findCrn(this.courseComboBox.Text,this.semesterComboBox.Text);
            var items =  this.rubricDAL.GetCourseRubricByCRN(crn);
            int sum = 0;
            foreach (var currRubricItem in items)
            {
                sum += currRubricItem.AssignmentWeight;

            }
            this.warningText.Foreground = new SolidColorBrush(Colors.Blue); 
            if (sum > 100)
            {
                this.warningText.Text = "Caution: rubric values add to " + sum + " which is over 100 percent";
            } else if (sum < 100)
            {
                this.warningText.Text = "Caution: rubric values add to " + sum + " which is less than 100 percent";
            }
            else
            {
                this.warningText.Text = "Rubric values sum to 100";
                this.warningText.Foreground = new SolidColorBrush(Colors.LimeGreen);
            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            var crn = CourseManagementTools.findCrn(this.courseComboBox.Text,this.semesterComboBox.Text);
            var assignmentWeightBox = Int32.Parse(this.assignmentWeightBox.Text);
            this.rubricDAL.InsertCourseRubric(this.assignmentTypeBox.Text,assignmentWeightBox,crn);
            this.refresh();
            this.setWarningText();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var crn = CourseManagementTools.findCrn(this.courseComboBox.Text,this.semesterComboBox.Text);
            var assignmentType = this.assignmentTypeBox.Text;
            var assignmentWeight = Int32.Parse(this.assignmentWeightBox.Text);
            var index = this.originalItem.Index;
            var orginalCRN = this.originalItem.CRN;

            this.rubricDAL.DeleteCourseRubric(crn, assignmentType, assignmentWeight, this.originalItem.AssignmentType,
                this.originalItem.AssignmentWeight, index, index, orginalCRN);
          
            this.refresh();
            this.setWarningText();
            this.assignmentTypeBox.Clear();
            this.assignmentWeightBox.Clear();
            
        }

        private void ViewGrades_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}

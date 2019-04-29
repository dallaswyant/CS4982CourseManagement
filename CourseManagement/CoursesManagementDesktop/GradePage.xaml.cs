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

namespace CoursesManagementDesktop
{
    /// <summary>
    /// Interaction logic for GradePage.xaml
    /// </summary>
    public partial class GradePage : Page
    {
        private TeacherHomePageController controller;
        private readonly StudentDAL studentDal;
        private readonly GradeItemDAL gradeItemDal;
        private List<Student> students;
        private List<GradeItem> assignments;
        private Dictionary<int, string> studentIds;
        /// <summary>
        /// Grade page constructor
        /// </summary>
        /// <param name="controller">the programs controller</param>
        /// <precondition>
        ///controller != null
        /// </precondition>
        public GradePage(TeacherHomePageController controller)
        {
            if (controller == null)
            {
                throw new ArgumentException("controller cannot be null");
            }
            InitializeComponent();
            this.studentDal = new StudentDAL();
            this.gradeItemDal = new GradeItemDAL();
            this.studentIds = new Dictionary<int, string>();
            this.controller = controller;
            populateStudentCombo();
            setGradeInfo();
        }

        private void setGradeInfo()
        {
            var assignment = this.assignments[this.assignmentBox.SelectedIndex];
            this.earnedPointsBox.Text = assignment.Grade.ToString();
            this.feedBackBox.Text = assignment.Feedback;
            this.possiblePoints.Text = "/" + assignment.PossiblePoints;
        }


        private void populateStudentCombo()
        {
            var index = 0; 
            this.students = this.studentDal.GetStudentsByCRN(this.controller.currentCrn);

            foreach (var student in students)
            {
                this.studentCombo.Items.Add(student.Name);
                studentIds.Add(index,student.StudentUID);
                index++;
            }

            if (this.controller.selectedStudent > -1)
            {
                this.studentCombo.SelectedIndex = this.controller.selectedStudent;
            }
            else
            {

                this.studentCombo.SelectedIndex = 0;
            }
        }

        private void populateAssignmentBox()
        {
            var studentId = studentIds[this.studentCombo.SelectedIndex];
            this.assignments = this.gradeItemDal.GetGradedItemsByStudentId(studentId ,this.controller.currentCrn);
            foreach (var assignment in assignments)
            {
                this.assignmentBox.Items.Add(assignment.Name);
            }

            this.assignmentBox.SelectedIndex = 0;
        }

        private void GradeButton_Click(object sender, RoutedEventArgs e)
        {
            confirmationWindow window = new confirmationWindow();
            window.Owner = Window.GetWindow(this);
            window.ShowDialog();
            var currentGrade = this.assignments[this.assignmentBox.SelectedIndex];
            var studentId = studentIds[this.studentCombo.SelectedIndex];
            var updatedGrade = new GradeItem()
            {
                Feedback = this.feedBackBox.Text,
                Grade = int.Parse(this.earnedPointsBox.Text),
                GradeId = currentGrade.GradeId,
                Name = currentGrade.Name
            };
            this.gradeItemDal.gradeGradedItemByCRNAndStudentUID(updatedGrade,this.controller.currentCrn,studentId);
            
            showNextStudent();

        }

        private void updateGradeInfo()
        {
            this.assignmentBox.SelectionChanged -= AssignmentBox_SelectionChanged;
            this.assignmentBox.Items.Clear();
            this.assignmentBox.SelectionChanged += AssignmentBox_SelectionChanged;
            populateAssignmentBox();
            setGradeInfo();
        }

        

        private void StudentCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateGradeInfo();
        }

        private void AssignmentBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setGradeInfo();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            showNextStudent();
        }

        private void showNextStudent()
        {
            if (this.studentCombo.SelectedIndex + 1 == this.studentCombo.Items.Count)
            {
                this.studentCombo.SelectedIndex = 0;
            }
            else
            {
                this.studentCombo.SelectedIndex += 1;
            }
        }

        private void ViewGrades_Click(object sender, RoutedEventArgs e)
        {
            this.controller.LoadDataGrid();
            NavigationService.GoBack();
            
        }
    }
}

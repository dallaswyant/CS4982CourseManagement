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
using CoursesManagementDesktop.DAL;
using GradeItemDAL = CourseManagement.DAL.GradeItemDAL;

namespace CoursesManagementDesktop
{
    /// <summary>
    /// Interaction logic for TeacherHomePAge.xaml
    /// </summary>
    public partial class TeacherHomePAge : Page
    {
        private CourseManagement.DAL.GradeItemDAL gradedItemDal;
        private DesktopGradedItemDAL desktopGradedItemDal;
        private CourseDAL courseDAL;
        private string teacherID;
        public TeacherHomePAge(string teacherID)
        {
            InitializeComponent();
            this.teacherID = teacherID;
             gradedItemDal = new GradeItemDAL();
            desktopGradedItemDal = new DesktopGradedItemDAL();
            this.courseDAL = new CourseDAL();
            populateComboBoxes();
            loadDataGrid();
        }

        private void populateComboBoxes()
        {
            //TODO
            //need to figure out what to replace this with
            
            var courses = this.courseDAL.GetCoursesByTeacherID(this.teacherID);  

            foreach (var name in courses)
            {
                this.CourseCombo.Items.Add(name.Name);
            }
            
            this.CourseCombo.SelectedIndex = 0;

            var crn = findCRN(this.CourseCombo.Text);
            var assignments =  this.gradedItemDal.GetUniqueGradedItemsByCRN(crn); //TODO fix this

            foreach (var name in assignments)
            {
                this.AssignmentCombo.Items.Add(name.Value);
            }

            
            this.AssignmentCombo.SelectedIndex = 0;
            

        }

        private void loadDataGrid()
        {
            
            var name = this.AssignmentCombo.SelectedItem.ToString();
            var crn = findCRN(this.CourseCombo.Text); 
          this.desktopGradedItemDal.populateDataGrid(crn,name , this.dataGridGrades);
        }

        private void AssignmentCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadDataGrid();
        }

        private int findCRN(string courseName)
        {
            int crn = -1;
            
            var courses = this.courseDAL.GetCoursesByTeacherID(this.teacherID);
            foreach (var course in courses)
            {
                if (course.Name.Equals(this.CourseCombo.Text))
                {
                    crn = course.CRN;
                }
            }
            
            return crn;
        }
    }
}

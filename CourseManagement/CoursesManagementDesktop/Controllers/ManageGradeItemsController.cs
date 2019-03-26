using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.DAL;
using CourseManagement.Models;
using CoursesManagementDesktop.Model;

namespace CoursesManagementDesktop.Controllers
{
    
    class ManageGradeItemsController
    {
        private ManageAssignmentPage assignmentPage;
        private readonly SemesterDAL semesterDal;
        private readonly CourseDAL courseDal;
        private readonly CourseRubricDAL rubricDal;
        private GradeItemDAL gradeItemDal;

        public ManageGradeItemsController(ManageAssignmentPage assignmentPage)
        {
            this.assignmentPage = assignmentPage;
            this.semesterDal = new SemesterDAL();
            this.courseDal = new CourseDAL();
            this.rubricDal = new CourseRubricDAL();
            this.gradeItemDal = new GradeItemDAL();
        }


        public void populateComboBoxes()
        {
            this.populateSemesterComboBox();
            this.populateCourseComboBox();
            this.populateAssignmentComboBox();
            this.populateAssignmentTypeComboBox();
        }

        private void populateSemesterComboBox()
        {
            int index = 0;
            var semesters = this.semesterDal.GetAllSemesters();
            foreach (var semester in semesters)
            {
                this.assignmentPage.semesterComboBox.Items.Add(semester.SemesterID);
                if (semester.StartDate < DateTime.Now & semester.EndDate > DateTime.Now)
                {
                    this.assignmentPage.semesterComboBox.SelectedIndex = index;
                }

                index++;
            }
        }

        private void populateCourseComboBox()
        {
            string semester = this.assignmentPage.semesterComboBox.Text;
            var courses = this.courseDal.GetCoursesByTeacherAndSemester(CourseManagementTools.TeacherID,semester);

            foreach (var name in courses)
            {
                this.assignmentPage.courseComboBox.Items.Add(name.Name);
            }

            this.assignmentPage.courseComboBox.SelectedIndex = 0;
        }

        private void populateAssignmentTypeComboBox()
        {
            var crn = CourseManagementTools.findCrn(this.assignmentPage.courseComboBox.Text,this.assignmentPage.semesterComboBox.Text);
            var rubricItems = this.rubricDal.GetCourseRubricByCRN(crn);
            foreach (var item in rubricItems)
            {
                this.assignmentPage.assignmentTypeComboBox.Items.Add(item.AssignmentType);
            }

            this.assignmentPage.assignmentTypeComboBox.SelectedIndex = 0;
        }

        public  void  populateAssignmentComboBox()
        {
            var crn = CourseManagementTools.findCrn(this.assignmentPage.courseComboBox.Text,this.assignmentPage.semesterComboBox.Text);
            var assignments = this.gradeItemDal.GetUniqueGradedItemsByCRN(crn);

            foreach (var name in assignments)
            {
                this.assignmentPage.AssignmentCombo.Items.Add(name.Value);
            }

            
        }

        public void addAssignmnet()
        {

            handleWhenGradeITemNotExists();

        }

        private void handleWhenGradeITemNotExists()
        {
            var crn = CourseManagementTools.findCrn(this.assignmentPage.courseComboBox.Text,this.assignmentPage.semesterComboBox.Text);
            var assignmentName = this.assignmentPage.assignmentNameBox.Text;
            var possiblePoints = Convert.ToInt32(this.assignmentPage.pointsBox.Text);
            var gradeType = this.assignmentPage.assignmentTypeComboBox.Text;
            GradeItem item = new GradeItem(assignmentName, null, 0, string.Empty, possiblePoints, gradeType, String.Empty, 0, this.assignmentPage.visibilityCheckBox.IsChecked.Value, null);
            this.gradeItemDal.InsertNewGradedItemByCRNForAllStudents(item, crn);
        }
    }
}

using System.Collections.Generic;
using CourseManagement.DAL;
using CourseManagement.Models;

namespace CoursesManagementDesktop.Controllers
{
    internal class GradePageController
    {
        #region Data members

        private readonly GradePage gradePage;
        private List<GradeItem> assignments;
        private List<Student> students;
        private readonly StudentDAL studentDal;
        private readonly Dictionary<int, string> studentIds;
        private readonly GradeItemDAL gradeItemDal;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GradePageController" /> class.
        /// </summary>
        /// <param name="gradePage">The grade page.</param>
        public GradePageController(GradePage gradePage)
        {
            this.gradePage = gradePage;
            this.studentIds = new Dictionary<int, string>();
            this.gradeItemDal = new GradeItemDAL();
            this.studentDal = new StudentDAL();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the grade information.
        /// </summary>
        public void SetGradeInfo()
        {
            var assignment = this.assignments[this.gradePage.assignmentBox.SelectedIndex];
            this.gradePage.earnedPointsBox.Text = assignment.Grade.ToString();
            this.gradePage.feedBackBox.Text = assignment.Feedback;
            this.gradePage.possiblePoints.Text = "/" + assignment.PossiblePoints;
        }

        /// <summary>
        ///     Populates the student combo.
        /// </summary>
        public void PopulateStudentCombo()
        {
            var index = 0;
            this.students = this.studentDal.GetStudentsByCRN(this.gradePage.CRN);

            foreach (var student in this.students)
            {
                this.gradePage.studentCombo.Items.Add(student.Name);
                this.studentIds.Add(index, student.StudentUID);
                index++;
            }

            if (this.gradePage.homePageController.selectedStudent > -1)
            {
                this.gradePage.studentCombo.SelectedIndex = this.gradePage.homePageController.selectedStudent;
            }
            else
            {
                this.gradePage.studentCombo.SelectedIndex = 0;
            }
        }

        /// <summary>
        ///     Populates the assignment box.
        /// </summary>
        public void PopulateAssignmentBox()
        {
            var studentId = this.studentIds[this.gradePage.studentCombo.SelectedIndex];
            this.assignments =
                this.gradeItemDal.GetGradedItemsByStudentId(studentId, this.gradePage.homePageController.currentCrn);
            foreach (var assignment in this.assignments)
            {
                this.gradePage.assignmentBox.Items.Add(assignment.Name);
            }

            this.gradePage.assignmentBox.SelectedIndex = this.gradePage.homePageController.selectedAssignment;
        }

        /// <summary>
        ///     Grades the current item.
        /// </summary>
        public void GradeCurrentItem()
        {
            this.gradeStudent();
            
            this.ShowNextStudent();
        }

        private void gradeStudent()
        {
            var window = new confirmationWindow();
            window.ShowDialog();
            var currentGrade = this.assignments[this.gradePage.assignmentBox.SelectedIndex];
            var studentId = this.studentIds[this.gradePage.studentCombo.SelectedIndex];
            var updatedGrade = new GradeItem
            {
                Feedback = this.gradePage.feedBackBox.Text,
                Grade = int.Parse(this.gradePage.earnedPointsBox.Text),
                GradeId = currentGrade.GradeId,
                Name = currentGrade.Name
            };
            this.gradeItemDal.gradeGradedItemByCRNAndStudentUID(updatedGrade,
                this.gradePage.homePageController.currentCrn, studentId);
            

        }

        /// <summary>
        ///     Shows the next student.
        /// </summary>
        public void ShowNextStudent()
        {
            if (this.gradePage.changesMade)
            {
                
               this.gradeStudent();

            }
            if (this.gradePage.studentCombo.SelectedIndex + 1 == this.gradePage.studentCombo.Items.Count)
            {
                this.gradePage.studentCombo.SelectedIndex = 0;
            }
            else
            {
                this.gradePage.studentCombo.SelectedIndex += 1;
            }
        }

        #endregion
    }
}
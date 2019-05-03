using System;
using System.Globalization;
using System.Windows;
using CourseManagement.DAL;
using CourseManagement.Models;
using CoursesManagementDesktop.Model;

namespace CoursesManagementDesktop.Controllers
{
    internal class ManageGradeItemsController
    {
        #region Data members

        private readonly ManageAssignmentPage assignmentPage;
        private readonly SemesterDAL semesterDal;
        private readonly CourseDAL courseDal;
        private readonly CourseRubricDAL rubricDal;
        private readonly GradeItemDAL gradeItemDal;
        private GradeItem selectedGradeItem;
        private int CRN;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ManageGradeItemsController" /> class.
        /// </summary>
        /// <param name="assignmentPage">The assignment page.</param>
        public ManageGradeItemsController(ManageAssignmentPage assignmentPage)
        {
            this.assignmentPage = assignmentPage;
            this.semesterDal = new SemesterDAL();
            this.courseDal = new CourseDAL();
            this.rubricDal = new CourseRubricDAL();
            this.gradeItemDal = new GradeItemDAL();
            this.CRN = assignmentPage.CRN;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Populates the combo boxes.
        /// </summary>
        public void PopulateComboBoxes()
        {
            this.populateSemesterComboBox();
            this.populateCourseComboBox();
            this.PopulateAssignmentComboBox();
            this.populateAssignmentTypeComboBox();
        }

        private void populateSemesterComboBox()
        {
            var index = 0;
            var semesters = this.semesterDal.GetAllSemesters();
            foreach (var semester in semesters)
            {
                this.assignmentPage.semesterComboBox.Items.Add(semester.SemesterID);
                if ((semester.StartDate < DateTime.Now) & (semester.EndDate > DateTime.Now))
                {
                    this.assignmentPage.semesterComboBox.SelectedIndex = index;
                }

                index++;
            }
        }

        private void populateCourseComboBox()
        {
            var semester = this.assignmentPage.semesterComboBox.Text;
            var courses = this.courseDal.GetCoursesByTeacherAndSemester(CourseManagementTools.TeacherID, semester);

            foreach (var name in courses)
            {
                this.assignmentPage.courseComboBox.Items.Add(name.Name);
            }

            this.assignmentPage.courseComboBox.SelectedIndex = 0;
        }

        private void populateAssignmentTypeComboBox()
        {
            var crn = CourseManagementTools.findCrn(this.assignmentPage.courseComboBox.SelectedItem as string,
                this.assignmentPage.semesterComboBox.SelectedItem as string);
            var rubricItems = this.rubricDal.GetCourseRubricByCRN(crn);
            foreach (var item in rubricItems)
            {
                this.assignmentPage.assignmentTypeComboBox.Items.Add(item.AssignmentType);
            }

            this.assignmentPage.assignmentTypeComboBox.SelectedIndex = 0;
        }

        /// <summary>
        ///     Populates the assignment ComboBox.
        /// </summary>
        public void PopulateAssignmentComboBox()
        {
            var crn = CourseManagementTools.findCrn(this.assignmentPage.courseComboBox.SelectedItem as string,
                this.assignmentPage.semesterComboBox.SelectedItem as string);
            var assignments = this.gradeItemDal.GetUniqueGradedItemsByCRN(crn);

            foreach (var name in assignments)
            {
                this.assignmentPage.AssignmentCombo.Items.Add(name.Value);
            }
        }

        /// <summary>
        ///     Adds the assignment.
        /// </summary>
        public void AddAssignment()
        {
            this.handleWhenGradeItemDoesNotExists();
            this.assignmentPage.AssignmentCombo.Items.Clear();
            this.PopulateAssignmentComboBox();
            this.assignmentPage.AssignmentCombo.SelectedIndex = 0;
        }

        private void handleWhenGradeItemDoesNotExists()
        {
            var crn = CourseManagementTools.findCrn(this.assignmentPage.courseComboBox.SelectedItem as string,
                this.assignmentPage.semesterComboBox.SelectedItem as string);
            var assignmentName = this.assignmentPage.assignmentNameBox.Text;
            var possiblePoints = Convert.ToInt32(this.assignmentPage.pointsBox.Text);
            var gradeType = this.assignmentPage.assignmentTypeComboBox.Text;
            var isChecked = this.assignmentPage.visibilityCheckBox.IsChecked;
            var item = new GradeItem(assignmentName, null, 0, string.Empty, possiblePoints, gradeType, string.Empty, 0,
                isChecked != null && isChecked.Value, null);
            this.gradeItemDal.InsertNewGradedItemByCRNForAllStudents(item, crn);
        }

        /// <summary>
        ///     Displays the grade item details.
        /// </summary>
        public void DisplayGradeItemDetails()
        {
            var crn = CourseManagementTools.findCrn(this.assignmentPage.courseComboBox.SelectedItem as string,
                this.assignmentPage.semesterComboBox.SelectedItem as string);
            this.selectedGradeItem =
                this.gradeItemDal.GetGradedItemByCRNAndGradeName(crn,
                    this.assignmentPage.AssignmentCombo.SelectedItem as string);
            if (this.selectedGradeItem != null)
            {
                this.assignmentPage.assignmentNameBox.Text = this.selectedGradeItem.Name;
                this.assignmentPage.pointsBox.Text =
                    this.selectedGradeItem.PossiblePoints.ToString(CultureInfo.CurrentCulture);
                this.assignmentPage.visibilityCheckBox.IsChecked = this.selectedGradeItem.IsPublic;
            }
        }

        /// <summary>
        ///     Handles the deletion.
        /// </summary>
        public void HandleDeletion()
        {
            var crn = CourseManagementTools.findCrn(this.assignmentPage.courseComboBox.SelectedItem as string,
                this.assignmentPage.semesterComboBox.SelectedItem as string);
            var value = showConfirmDialog("Delete This Item?");
            if (value != null && value == true)
            {
                
                if (this.selectedGradeItem != null)
                {
                    this.gradeItemDal.deleteGradedItemByCRNForAllStudents(this.selectedGradeItem, crn);
                    this.assignmentPage.AssignmentCombo.Items.Remove(this.selectedGradeItem.Name);
                    this.assignmentPage.AssignmentCombo.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        ///     Handles the editing.
        /// </summary>
        public void HandleEditing()
        {
            var value = showConfirmDialog("Edit This Item?");
            if (value != null && value == true)
            {
                var crn = CourseManagementTools.findCrn(this.assignmentPage.courseComboBox.SelectedItem as string,
                    this.assignmentPage.semesterComboBox.SelectedItem as string);
                this.selectedGradeItem.Name = this.assignmentPage.assignmentNameBox.Text;
                this.selectedGradeItem.PossiblePoints = int.Parse(this.assignmentPage.pointsBox.Text);
                var isChecked = this.assignmentPage.visibilityCheckBox.IsChecked;
                if (isChecked != null)
                    this.selectedGradeItem.IsPublic = (bool) isChecked;
                this.gradeItemDal.UpdateGradeItemByCRNAndOldNameForAllStudents(this.selectedGradeItem, crn,
                    this.assignmentPage.AssignmentCombo.SelectedItem as string);
                this.assignmentPage.AssignmentCombo.Items.Clear();
                this.PopulateAssignmentComboBox();
                this.assignmentPage.AssignmentCombo.SelectedIndex = 0;
            }
        }

        public void updateVisibility()
        {
            var crn = CourseManagementTools.findCrn(this.assignmentPage.courseComboBox.SelectedItem as string,
                this.assignmentPage.semesterComboBox.SelectedItem as string);
            var assignmentName = this.assignmentPage.AssignmentCombo.SelectedItem as string;
            var isChecked = this.assignmentPage.visibilityCheckBox.IsChecked;
            bool isPublic = isChecked != null && (bool) isChecked;
            this.gradeItemDal.PublishGradeItemByNameAndCRNForAllStudents(crn,assignmentName,isPublic);
        }

        private static bool? showConfirmDialog(string dialogText)
        {
            var confirmWindow = new confirmationWindow();
            confirmWindow.dialogText.Text = dialogText;
            confirmWindow.confirmButton.Margin = new Thickness(50, 146, 0, 0);
            confirmWindow.declineButton.Visibility = Visibility.Visible;
            var value = confirmWindow.ShowDialog();
            return value;
        }

        #endregion
    }
}
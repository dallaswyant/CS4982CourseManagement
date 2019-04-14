﻿using System;
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
            // TODO handle here
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
            //TODO handle here
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
            var crn = CourseManagementTools.findCrn(this.assignmentPage.courseComboBox.Text,
                this.assignmentPage.semesterComboBox.Text);
            //TODO handle here
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
           //TODO handle here
            var assignments = this.gradeItemDal.GetUniqueGradedItemsByCRN(this.CRN);

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
           
            var assignmentName = this.assignmentPage.assignmentNameBox.Text;
            var possiblePoints = Convert.ToInt32(this.assignmentPage.pointsBox.Text);
            var gradeType = this.assignmentPage.assignmentTypeComboBox.Text;
            var isChecked = this.assignmentPage.visibilityCheckBox.IsChecked;
            var item = new GradeItem(assignmentName, null, 0, string.Empty, possiblePoints, gradeType, string.Empty, 0,
                isChecked != null && isChecked.Value, null);
            //TODO handle here
            this.gradeItemDal.InsertNewGradedItemByCRNForAllStudents(item, this.CRN);
        }

        /// <summary>
        ///     Displays the grade item details.
        /// </summary>
        public void DisplayGradeItemDetails()
        {
           //TODO handle here
            this.selectedGradeItem =
                this.gradeItemDal.GetGradedItemByCRNAndGradeName(this.CRN,
                    this.assignmentPage.AssignmentCombo.SelectedItem as string);
            if (this.selectedGradeItem != null)
            {
                this.assignmentPage.assignmentNameBox.Text = this.selectedGradeItem.Name;
                this.assignmentPage.pointsBox.Text =
                    this.selectedGradeItem.PossiblePoints.ToString(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        ///     Handles the deletion.
        /// </summary>
        public void HandleDeletion()
        {
            var value = showConfirmDialog("Delete This Item?");
            if (value != null && value == true)
            {
                
                if (this.selectedGradeItem != null)
                {
                    //TODO handle here
                    this.gradeItemDal.deleteGradedItemByCRNForAllStudents(this.selectedGradeItem, this.CRN);
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
               
                this.selectedGradeItem.Name = this.assignmentPage.assignmentNameBox.Text;
                this.selectedGradeItem.PossiblePoints = int.Parse(this.assignmentPage.pointsBox.Text);
                //TODO handle here
                this.gradeItemDal.UpdateGradeItemByCRNAndOldNameForAllStudents(this.selectedGradeItem, this.CRN,
                    this.assignmentPage.AssignmentCombo.SelectedItem as string);
                this.assignmentPage.AssignmentCombo.Items.Clear();
                this.PopulateAssignmentComboBox();
                this.assignmentPage.AssignmentCombo.SelectedIndex = 0;
            }
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
using System;
using System.Windows.Media;
using CourseManagement.DAL;
using CoursesManagementDesktop.Model;

namespace CoursesManagementDesktop.Controllers
{
    public class RubricController
    {
        #region Properties

        /// <summary>
        ///     Gets the rubric page.
        /// </summary>
        /// <value>
        ///     The rubric page.
        /// </value>
        public ManageRubricPage rubricPage { get; }

        /// <summary>
        ///     Gets the course dal.
        /// </summary>
        /// <value>
        ///     The course dal.
        /// </value>
        public CourseDAL courseDAL { get; }

        /// <summary>
        ///     Gets the semester dal.
        /// </summary>
        /// <value>
        ///     The semester dal.
        /// </value>
        public SemesterDAL semesterDal { get; }

        /// <summary>
        ///     Gets or sets the rubric dal.
        /// </summary>
        /// <value>
        ///     The rubric dal.
        /// </value>
        public CourseRubricDAL rubricDAL { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RubricController" /> class.
        /// </summary>
        /// <param name="rubricPage">The rubric page.</param>
        public RubricController(ManageRubricPage rubricPage)
        {
            this.rubricPage = rubricPage;
            this.courseDAL = new CourseDAL();
            this.semesterDal = new SemesterDAL();
            this.rubricDAL = new CourseRubricDAL();
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
        }

        private void populateCourseComboBox()
        {
            var semester = this.rubricPage.semesterComboBox.Text;
            var courses = this.courseDAL.GetCoursesByTeacherAndSemester(CourseManagementTools.TeacherID, semester);

            foreach (var name in courses)
            {
                this.rubricPage.courseComboBox.Items.Add(name.Name);
            }

            this.rubricPage.courseComboBox.SelectedIndex = 0;
        }

        private void populateSemesterComboBox()
        {
            var index = 0;
            var semesters = this.semesterDal.GetAllSemesters();
            foreach (var semester in semesters)
            {
                this.rubricPage.semesterComboBox.Items.Add(semester.SemesterID);
                if ((semester.StartDate < DateTime.Now) & (semester.EndDate > DateTime.Now))
                {
                    this.rubricPage.semesterComboBox.SelectedIndex = index;
                }

                index++;
            }
        }

        /// <summary>
        ///     Loads the rubric.
        /// </summary>
        public void LoadRubric()
        {
            var crn = CourseManagementTools.findCrn(this.rubricPage.courseComboBox.Text,
                this.rubricPage.semesterComboBox.SelectedItem as string);
            var items = this.rubricDAL.GetCourseRubricByCRN(crn);
            foreach (var rubricItem in items)
            {
                this.rubricPage.rubricItems.Add(rubricItem);
            }
        }

        /// <summary>
        ///     Edits the rubric.
        /// </summary>
        public void EditRubric()
        {
            var crn = CourseManagementTools.findCrn(this.rubricPage.courseComboBox.Text,
                this.rubricPage.semesterComboBox.SelectedItem as string);
            var assignmentType = this.rubricPage.assignmentTypeBox.Text;
            var assignmentWeight = int.Parse(this.rubricPage.assignmentWeightBox.Text);
            var index = this.rubricPage.originalItem.Index;
            var orginalCRN = this.rubricPage.originalItem.CRN;

            this.rubricDAL.UpdateCourseRubric(crn, assignmentType, assignmentWeight,
                this.rubricPage.originalItem.AssignmentType, this.rubricPage.originalItem.AssignmentWeight, index,
                index, orginalCRN);
            this.refresh();
            this.SetWarningText();
            this.rubricPage.assignmentTypeBox.Clear();
            this.rubricPage.assignmentWeightBox.Clear();
        }

        private void refresh()
        {
            this.rubricPage.rubricItems.Clear();
            this.LoadRubric();
        }

        /// <summary>
        ///     Sets the warning text.
        /// </summary>
        public void SetWarningText()
        {
            var crn = CourseManagementTools.findCrn(this.rubricPage.courseComboBox.Text,
                this.rubricPage.semesterComboBox.SelectedItem as string);
            var items = this.rubricDAL.GetCourseRubricByCRN(crn);
            var sum = 0;
            foreach (var currRubricItem in items)
            {
                sum += currRubricItem.AssignmentWeight;
            }

            this.rubricPage.warningText.Foreground = new SolidColorBrush(Colors.Blue);
            if (sum > 100)
            {
                this.rubricPage.warningText.Text =
                    "Caution: rubric values add to " + sum + " which is over 100 percent";
            }
            else if (sum < 100)
            {
                this.rubricPage.warningText.Text =
                    "Caution: rubric values add to " + sum + " which is less than 100 percent";
            }
            else
            {
                this.rubricPage.warningText.Text = "Rubric values sum to 100";
                this.rubricPage.warningText.Foreground = new SolidColorBrush(Colors.LimeGreen);
            }
        }

        /// <summary>
        ///     Inserts the rubric item.
        /// </summary>
        public void InsertRubricItem()
        {
            var crn = CourseManagementTools.findCrn(this.rubricPage.courseComboBox.Text,
                this.rubricPage.semesterComboBox.SelectedItem as string);
            var assignmentWeightBox = int.Parse(this.rubricPage.assignmentWeightBox.Text);
            this.rubricDAL.InsertCourseRubric(this.rubricPage.assignmentTypeBox.Text, assignmentWeightBox, crn);
            this.refresh();
            this.SetWarningText();
        }

        /// <summary>
        ///     Deletes the rubric item.
        /// </summary>
        public void DeleteRubricItem()
        {
            var crn = CourseManagementTools.findCrn(this.rubricPage.courseComboBox.Text,
                this.rubricPage.semesterComboBox.SelectedItem as string);
            var assignmentType = this.rubricPage.assignmentTypeBox.Text;
            var assignmentWeight = int.Parse(this.rubricPage.assignmentWeightBox.Text);
            var index = this.rubricPage.originalItem.Index;
            var orginalCRN = this.rubricPage.originalItem.CRN;

            this.rubricDAL.DeleteCourseRubric(crn, assignmentType, assignmentWeight,
                this.rubricPage.originalItem.AssignmentType,
                this.rubricPage.originalItem.AssignmentWeight, index, index, orginalCRN);

            this.refresh();
            this.SetWarningText();
            this.rubricPage.assignmentTypeBox.Clear();
            this.rubricPage.assignmentWeightBox.Clear();
        }

        #endregion
    }
}
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using CourseManagement.Models;
using CoursesManagementDesktop.Annotations;
using CoursesManagementDesktop.Controllers;

namespace CoursesManagementDesktop
{
    /// <summary>
    ///     Interaction logic for ManageRubricPage.xaml
    /// </summary>
    public partial class ManageRubricPage : Page, INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the controller.
        /// </summary>
        /// <value>
        ///     The controller.
        /// </value>
        public RubricController controller { get; set; }

        /// <summary>
        ///     Gets or sets the rubric items.
        /// </summary>
        /// <value>
        ///     The rubric items.
        /// </value>
        public ObservableCollection<RubricItem> rubricItems { get; set; }

        /// <summary>
        ///     Gets the original item.
        /// </summary>
        /// <value>
        ///     The original item.
        /// </value>
        public RubricItem originalItem { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ManageRubricPage" /> class.
        /// </summary>
        public ManageRubricPage()
        {
            this.InitializeComponent();
            this.controller = new RubricController(this);
            this.controller.PopulateComboBoxes();
            this.rubricItems = new ObservableCollection<RubricItem>();
            this.controller.LoadRubric();
            this.controller.SetWarningText();
            DataContext = this;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RubricDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.rubricDataGrid.SelectedItem is RubricItem rubricItem)
            {
                this.originalItem = rubricItem;
                this.assignmentTypeBox.Text = this.originalItem.AssignmentType;
                this.assignmentWeightBox.Text = this.originalItem.AssignmentWeight.ToString();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.controller.EditRubric();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            this.controller.InsertRubricItem();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            this.controller.DeleteRubricItem();
        }

        private void ViewGrades_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        #endregion
    }
}
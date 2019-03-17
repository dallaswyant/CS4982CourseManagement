using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public RubricController controller { get; set; }

        public CourseRubricDAL rubricDAL { get; set; }

        public ObservableCollection<RubricItem> rubricItems { get; set; }
        public ManageRubricPage()
        {
            InitializeComponent();
            this.controller = new RubricController(this);
            this.controller.populateComboBoxes();
            this.rubricDAL = new CourseRubricDAL();
            this.rubricItems = new ObservableCollection<RubricItem>();
            this.loadRubric();
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
        

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

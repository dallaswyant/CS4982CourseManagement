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
using CoursesManagementDesktop.Controllers;

namespace CoursesManagementDesktop
{
    /// <summary>
    /// Interaction logic for TeacherSummaryViewPage.xaml
    /// </summary>
    public partial class TeacherSummaryViewPage : Page
    {
        private SummaryViewController controller;
        public TeacherSummaryViewPage()
        {
            InitializeComponent();
            this.controller = new SummaryViewController(this);
            this.controller.populateComboBoxes();
         
            this.controller.LoadDataGrid();
        }

        private void SemesterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // throw new NotImplementedException();
        }
    }
}

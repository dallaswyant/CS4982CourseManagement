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
using System.Windows.Shapes;

namespace CoursesManagementDesktop
{
    /// <summary>
    /// Interaction logic for confirmationWindow.xaml
    /// </summary>
    public partial class confirmationWindow : Window
    {
        public confirmationWindow()
        {
            InitializeComponent();
        }

        private void OkayButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

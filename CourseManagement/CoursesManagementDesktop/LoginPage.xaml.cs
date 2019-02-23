using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;



namespace CoursesManagementDesktop
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private LoginController controller;
        public LoginPage()
        {
            InitializeComponent();
            this.controller = new LoginController(this);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            controller.handleLogin();
        }

        
    }
}

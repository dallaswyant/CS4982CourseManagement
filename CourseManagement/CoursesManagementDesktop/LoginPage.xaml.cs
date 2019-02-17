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
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CoursesManagementDesktop
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            handleLogin();
        }

        private void handleLogin()
        {
            
            UserDAL userDAL = new UserDAL();
            User user = userDAL.CheckLogin(this.usernameBox.Text, this.passwordBox.Password);
            if (user == null || string.IsNullOrWhiteSpace(user.UserId + user.Password + user.Role))
            {
                this.passwordBox.Password ="";
                this.errorText.Visibility = Visibility.Visible;
            }
            else
            {
                TeacherHomePAge page = new TeacherHomePAge(user.UserId);
                var navigationService = this.NavigationService;
                navigationService?.Navigate(page);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CourseManagement.App_Code;
using CourseManagement.DAL;
using CoursesManagementDesktop.Model;

namespace CoursesManagementDesktop
{
   public  class LoginController
   {

       private LoginPage page;
       private LoginHandler handler;


       public  LoginController(LoginPage page)
       {
           this.page = page;
            this.handler = new LoginHandler();
       }
        

        public  void handleLogin()
        {
            if (this.handler.IsInvalidCredentials(this.page.usernameBox.Text, this.page.passwordBox.Password ))
            {
                this.page.passwordBox.Password = "";
                this.page.errorText.Visibility = Visibility.Visible;
            }
            else
            {
                TeacherHomePAge page = new TeacherHomePAge(handler.user.UserId);
                var navigationService = this.page.NavigationService;
                navigationService?.Navigate(page);
            }
           
 
                
           
        }
    }
}

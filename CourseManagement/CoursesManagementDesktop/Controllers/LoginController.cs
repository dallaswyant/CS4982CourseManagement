using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CoursesManagementDesktop.Model;

namespace CoursesManagementDesktop
{
    /// <summary>
    /// defines login controller class
    /// </summary>
   public  class LoginController
   {

       private LoginPage page;
       private LoginHandler handler;

        /// <summary>
        /// logincontoller constructor
        /// </summary>
        /// <param name="page">the login page</param>
        /// <precondition>
        /// page != null
        /// </precondition>
       public  LoginController(LoginPage page)
       {
           if (page == null)
           {
               throw new ArgumentException("page cannot be null");
           }
           this.page = page;
            this.handler = new LoginHandler();
       }
        
        /// <summary>
        /// allows user to login or displays error text if credentials didnt match
        /// </summary>
        public  void handleLogin()
        {
            if (this.handler.IsInvalidCredentials(this.page.usernameBox.Text, this.page.passwordBox.Password ))
            {
                this.page.passwordBox.Password = "";
                this.page.errorText.Visibility = Visibility.Visible;
            }
            else
            {
                CourseManagementTools.TeacherID = handler.CurrentUser.UserId;
                TeacherHomePAge page = new TeacherHomePAge();
                var navigationService = this.page.NavigationService;
                navigationService?.Navigate(page);
            }
           
 
                
           
        }
    }
}

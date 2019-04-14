using System;
using System.Windows;
using CoursesManagementDesktop.Model;

namespace CoursesManagementDesktop
{
    /// <summary>
    ///     defines login controller class
    /// </summary>
    public class LoginController
    {
        #region Data members

        private readonly LoginPage page;
        private readonly LoginHandler handler;

        #endregion

        #region Constructors

        /// <summary>
        ///     logincontoller constructor
        /// </summary>
        /// <param name="page">the login page</param>
        /// <precondition>
        ///     page != null
        /// </precondition>
        public LoginController(LoginPage page)
        {
            if (page == null)
            {
                throw new ArgumentException("page cannot be null");
            }

            this.page = page;
            this.handler = new LoginHandler();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     allows user to login or displays error text if credentials didnt match
        /// </summary>
        public void handleLogin()
        {
            if (this.handler.IsInvalidCredentials(this.page.usernameBox.Text, this.page.passwordBox.Password))
            {
                this.page.passwordBox.Password = "";
                this.page.errorText.Visibility = Visibility.Visible;
            }
            else
            {
                CourseManagementTools.TeacherID = this.handler.CurrentUser.UserId;
               // var page = new TeacherHomePAge();
                var page = new TeacherSummaryViewPage();
                var navigationService = this.page.NavigationService;
                navigationService?.Navigate(page);
            }
        }

        #endregion
    }
}
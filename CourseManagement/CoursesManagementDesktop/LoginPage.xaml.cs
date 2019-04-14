using System;
using System.Windows;
using System.Windows.Controls;
using CoursesManagementDesktop.DAL;

namespace CoursesManagementDesktop
{
    /// <summary>
    ///     Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        #region Data members

        private readonly LoginController controller;

        #endregion

        #region Constructors

        /// <summary>
        ///     the login page constructor
        /// </summary>
        public LoginPage()
        {
            this.InitializeComponent();
            this.controller = new LoginController(this);
        }

        #endregion

        #region Methods

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.controller.handleLogin();
        }


        #endregion

    }
}
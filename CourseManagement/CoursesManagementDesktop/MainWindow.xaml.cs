using System.Windows;

namespace CoursesManagementDesktop
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            var home = new LoginPage();

            this.myFrame.NavigationService.Navigate(home);
        }

        #endregion
    }
}
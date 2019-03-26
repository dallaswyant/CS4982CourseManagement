using System.Windows;

namespace CoursesManagementDesktop
{
    /// <summary>
    ///     Interaction logic for confirmationWindow.xaml
    /// </summary>
    public partial class confirmationWindow : Window
    {
        #region Constructors

        public confirmationWindow()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void OkayButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion
    }
}
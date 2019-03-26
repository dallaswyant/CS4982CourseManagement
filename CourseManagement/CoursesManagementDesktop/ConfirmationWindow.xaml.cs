using System.Windows;

namespace CoursesManagementDesktop
{
    /// <summary>
    ///     Interaction logic for confirmationWindow.xaml
    /// </summary>
    public partial class confirmationWindow : Window
    {
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="confirmationWindow"/> class.
        /// </summary>
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
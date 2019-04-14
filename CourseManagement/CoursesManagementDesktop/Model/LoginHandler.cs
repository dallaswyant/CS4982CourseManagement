using CourseManagement.DAL;
using CourseManagement.Models;

namespace CoursesManagementDesktop.Model
{
    public class LoginHandler
    {
        #region Properties

        /// <summary>
        ///     This is a public property for the user that is currently logged in
        /// </summary>
        public User CurrentUser { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Checks and handles if the credintials are a valid match
        /// </summary>
        /// <param name="username">the username</param>
        /// <param name="password">the password</param>
        /// <precondition>
        ///     username != null And username!= empty string
        ///     And password!= null And password != empty string
        /// </precondition>
        /// <returns>false is valid credits true other wise</returns>
        public bool IsInvalidCredentials(string username, string password)
        {
            //TODO handle here
            var userDAL = new UserDAL();
            this.CurrentUser = userDAL.CheckLogin(username, password);
            return this.CurrentUser == null ||
                   string.IsNullOrWhiteSpace(
                       this.CurrentUser.UserId + this.CurrentUser.Password + this.CurrentUser.Role);
        }

        #endregion
    }
}
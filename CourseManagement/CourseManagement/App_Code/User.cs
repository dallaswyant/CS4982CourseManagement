using System;

namespace CourseManagement.App_Code
{
    [Serializable]
    public class User
    {
        #region Properties
        /// <summary>
        /// gets the userID
        /// </summary>
        public string UserId { get; }

        /// <summary></summary>
        public int PersonalInfoID { get; }
        /// <summary>
        /// gets the password
        /// </summary>
        public string Password { get; }
        /// <summary>
        /// gets the role
        /// </summary>
        public string Role { get; }

        #endregion

        #region Constructors
        /// <summary>
        /// constructor for a user
        /// </summary>
        /// <param name="userID">the user ID</param>
        /// <param name="password">The password</param>
        /// <param name="role">the user role</param>
        /// <param name="personalInfoID">the personal Info ID</param>
        public User(string userID, string password, string role, int personalInfoID)
        {
            this.UserId = userID;
            this.Password = password;
            this.Role = role;
            this.PersonalInfoID = personalInfoID;
        }

        public User()
        {
        }

        #endregion
    }
}
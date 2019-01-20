namespace CourseManagement.App_Code
{
    public class User
    {
        #region Properties

        public string UserId { get; }
        public string Password { get; }
        public string Role { get; }

        #endregion

        #region Constructors

        public User(string userID, string password, string role)
        {
            this.UserId = userID;
            this.Password = password;
            this.Role = role;
        }

        public User()
        {
        }

        #endregion
    }
}
namespace CourseManagement.App_Code
{
    public class Teacher
    {
        #region Properties

        public string Location { get; }
        public string Name { get; }
        public string Email { get; }
        public bool IsEmailPublic { get; }
        public string PhoneNumber { get; }
        public Department PrimaryDepartment { get; private set; }
        public CourseCollection CoursesTaught { get; }

        #endregion

        #region Constructors

        public Teacher(string location, string name, string email, bool isEmailPublic, string phoneNumber,
            CourseCollection coursesTaught)
        {
            this.Location = location;
            this.Name = name;
            this.Email = email;
            this.IsEmailPublic = isEmailPublic;
            this.PhoneNumber = phoneNumber;
            this.CoursesTaught = coursesTaught;
        }

        #endregion
    }
}
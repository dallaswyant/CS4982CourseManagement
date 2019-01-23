namespace CourseManagement.App_Code
{
    public class Student
    {
        #region Properties

        public string name { get; }
        public string StudentUID { get; }
        public string Email { get; }
        public CourseCollection ActiveCourses { get; }
        public DegreeProgram Program { get; }

        #endregion

        #region Constructors

        public Student(string studentUID,string name, string email)
        {
            this.name = name;
            this.Email = email;
            this.StudentUID = studentUID;
        }

        #endregion

        public override string ToString()
        {
            return name;
        }
    }
}
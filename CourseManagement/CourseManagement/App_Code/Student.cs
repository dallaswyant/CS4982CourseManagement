namespace CourseManagement.App_Code
{
    public class Student
    {
        #region Properties

        public string Name { get; }
        public string StudentUID { get; }
        public string Email { get; }
        public CourseCollection ActiveCourses { get; private set; }
        public DegreeProgram Program { get; private set; }

        #endregion

        #region Constructors

        public Student(string studentUID,string name, string email)
        {
            this.Name = name;
            this.Email = email;
            this.StudentUID = studentUID;
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
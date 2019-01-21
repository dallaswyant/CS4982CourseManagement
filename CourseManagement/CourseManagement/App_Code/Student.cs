namespace CourseManagement.App_Code
{
    public class Student
    {
        #region Properties

        public string name { get; }
        public int StudentID { get; }
        public string Email { get; }
        public CourseCollection ActiveCourses { get; private set; }
        public DegreeProgram Program { get; private set; }

        #endregion

        #region Constructors

        public Student(int studentID,string name, string email)
        {
            this.name = name;
            this.Email = email;
            this.StudentID = studentID;
        }

        #endregion
    }
}
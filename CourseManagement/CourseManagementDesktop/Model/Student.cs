namespace CourseManagement.App_Code
{
    public class Student
    {
        #region Properties
        /// <summary>
        /// gets the name
        /// </summary>
        public string name { get; }
        /// <summary>
        /// gets the student ID
        /// </summary>
        public string StudentUID { get; }
        /// <summary>
        /// gets the email
        /// </summary>
        public string Email { get; }
        /// <summary>
        /// gets the active courses
        /// </summary>
        public CourseCollection ActiveCourses { get; }
        /// <summary>
        /// gets the degree program
        /// </summary>
        public DegreeProgram Program { get; }

        #endregion

        #region Constructors
        /// <summary>
        /// constructor for student 
        /// </summary>
        /// <param name="studentUID">the student ID</param>
        /// <param name="name">The student Name</param>
        /// <param name="email">the email of the student</param>
        public Student(string studentUID,string name, string email)
        {
            this.name = name;
            this.Email = email;
            this.StudentUID = studentUID;
        }

        #endregion
        /// <summary>
        /// to string method
        /// </summary>
        /// <returns>the string representation of the student</returns>
        public override string ToString()
        {
            return name;
        }
    }
}
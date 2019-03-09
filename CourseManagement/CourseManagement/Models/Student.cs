


using CourseManagement.DAL;

namespace CourseManagement.Models
{
    public class Student
    {
        #region Properties
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


        /// <summary>
        ///   Gets the classification.
        /// </summary>
        /// <value>
        ///   The classification.
        /// </value>
        public string Classification { get; }

        public string Name
        {
            get
            {
                PersonallnfoDAL infoGetter = new PersonallnfoDAL();
                PersonalStuff info = infoGetter.GetPersonalInfoFromUserID(this.StudentUID);
                return info.FName + " " + info.LName;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// constructor for student
        /// </summary>
        /// <param name="studentUID">the student ID</param>
        /// <param name="email">the email of the student</param>
        /// <param name="classification">The classification.</param>
        public Student(string studentUID, string email, string classification)
        {
            this.StudentUID = studentUID;
            this.Email = email;
            this.Classification = classification;
        }

        #endregion
    }
}
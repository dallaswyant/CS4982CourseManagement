


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

        public string Name
        {
            get
            {
                PersonallnfoDAL infoGetter = new PersonallnfoDAL();
                PersonalInformation info = infoGetter.GetPersonalInfoFromUserID(this.StudentUID);
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
        public Student(string studentUID, string email)
        {
            this.StudentUID = studentUID;
            this.Email = email;
        }

        #endregion
    }
}
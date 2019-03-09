using System.Collections.Generic;
using CourseManagement.DAL;
using CourseManagements;

namespace CourseManagement.Models
{
    public class Teacher
    {
        #region Properties
        /// <summary>
        /// gets the teacher User ID
        /// </summary>
        public string TeacherUID { get; }
        /// <summary>
        /// gets the location
        /// </summary>
        public string Location { get; }
        /// <summary>
        /// gets the email
        /// </summary>
        public string Email { get; }
        /// <summary>
        /// gets if the email is public
        /// </summary>
        public bool IsEmailPublic { get; }
        /// <summary>
        /// gets the phonenumber
        /// </summary>
        public string PhoneNumber { get; }
        /// <summary>
        /// gets the primary department
        /// </summary>
        public Department PrimaryDepartment { get; }
        /// <summary>
        /// gets the course taught
        /// </summary>
        public List<Course> CoursesTaught { get; }
        public string Name
        {
            get
            {
                PersonallnfoDAL infoGetter = new PersonallnfoDAL();
                PersonalStuff info = infoGetter.GetPersonalInfoFromUserID(this.TeacherUID);
                return info.FName + " " + info.LName;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// constructor for a teacher
        /// </summary>
        /// <param name="location">the office location</param>
        /// <param name="name">the teachers name</param>
        /// <param name="email">the teachers email</param>
        /// <param name="isEmailPublic">if the email is public</param>
        /// <param name="phoneNumber">teachers phone number</param>
        /// <param name="coursesTaught">the course they teach</param>
        /// <param name="teacherUID">the teachers user id</param>
        public Teacher(string location, string email, bool isEmailPublic, string phoneNumber,
            List<Course> coursesTaught, string teacherUID)
        {
            this.Location = location;
            this.Email = email;
            this.IsEmailPublic = isEmailPublic;
            this.PhoneNumber = phoneNumber;
            this.CoursesTaught = coursesTaught;
            this.TeacherUID = teacherUID;
        }

        #endregion
    }
}
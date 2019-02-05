using System.Collections.Generic;

namespace CourseManagement.App_Code
{
    public class Department
    {
        #region Properties
        /// <summary>
        /// gets the chair
        /// </summary>
        public Teacher Chair { get; }
        /// <summary>
        /// gets the courses of the department
        /// </summary>
        public CourseCollection DeptCourses { get; }
        /// <summary>
        /// gets the department name
        /// </summary>
        public string DeptName { get; }
        /// <summary>
        /// gets the teachers of the department
        /// </summary>
        public List<Teacher> Teachers { get; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for department
        /// </summary>
        /// <param name="chair">the chair teacher</param>
        /// <param name="deptName">the department name</param>
        public Department(Teacher chair, string deptName)
        {
            this.Chair = chair;
            this.DeptName = deptName;
        }

        #endregion
    }
}
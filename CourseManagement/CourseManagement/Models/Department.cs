using System.Collections.Generic;
using CourseManagement.Models;

namespace CourseManagements
{
    public class Department
    {
        #region Properties
        /// <summary>
        /// gets the chair
        /// </summary>
        public Teacher Chair { get; }
        /// <summary>
        /// gets the department name
        /// </summary>
        public string Name { get; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for department
        /// </summary>
        /// <param name="chair">the chair teacher</param>
        /// <param name="name">the department name</param>
        public Department(Teacher chair, string name)
        {
            this.Chair = chair;
            this.Name = name;
        }

        #endregion
    }
}
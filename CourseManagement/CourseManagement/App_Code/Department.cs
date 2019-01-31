using System.Collections.Generic;

namespace CourseManagement.App_Code
{
    public class Department
    {
        #region Properties

        public Teacher Chair { get; }
        public CourseCollection DeptCourses { get; }
        public string DeptName { get; }
        public List<Teacher> Teachers { get; }

        #endregion

        #region Constructors

        public Department(Teacher chair, string deptName)
        {
            this.Chair = chair;
            this.DeptName = deptName;
        }

        #endregion
    }
}
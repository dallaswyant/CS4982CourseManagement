using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagement.App_Code
{
    public class Department
    {
        public Teacher Chair { get; private set; }
        public CourseCollection DeptCourses { get; private set; }
        public string DeptName { get; private set; }
        public List<Teacher> teachers { get; private set; }

        public Department(Teacher chair, CourseCollection deptCourses, string deptName, List<Teacher> teachers)
        {
            this.Chair = chair;
            this.DeptCourses = deptCourses;
            this.DeptName = deptName;
            this.teachers = teachers;
        }
    }
}
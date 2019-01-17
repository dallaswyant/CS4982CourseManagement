using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagement.App_Code
{
    public class Student
    {
        public string name { get; private set; }
        public string Email { get; private set; }
        public CourseCollection activeCourses { get; private set; }
        public DegreeProgram Program { get; private set; }
    }
}
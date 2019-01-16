using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagement.App_Code
{
    public class CourseCollection
    {

        public List<Course> Courses { get; private set; }

        public CourseCollection()
        {
            this.Courses = new List<Course>();
        }

        public void Add(Course course)
        {
            this.Courses.Add(course);

        }
    }
}
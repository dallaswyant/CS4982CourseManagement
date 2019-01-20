using System.Collections.Generic;

namespace CourseManagement.App_Code
{
    public class CourseCollection
    {
        #region Properties

        public List<Course> Courses { get; }

        #endregion

        #region Constructors

        public CourseCollection()
        {
            this.Courses = new List<Course>();
        }

        #endregion

        #region Methods

        public void Add(Course course)
        {
            this.Courses.Add(course);
        }

        #endregion
    }
}
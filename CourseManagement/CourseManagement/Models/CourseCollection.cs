using System.Collections.Generic;

namespace CourseManagement.Models
{
    public class CourseCollection
    {
        #region Properties
        /// <summary>
        /// gets courses
        /// </summary>
        public List<Course> Courses { get; }

        #endregion

        #region Constructors
        /// <summary>
        /// constructor for course collection
        /// </summary>
        public CourseCollection()
        {
            this.Courses = new List<Course>();
        }

        #endregion

        #region Methods
        /// <summary>
        /// adds a course to the course collection
        /// </summary>
        /// <param name="course">the course to add</param>
        public void Add(Course course)
        {
            this.Courses.Add(course);
        }

        #endregion
    }
}
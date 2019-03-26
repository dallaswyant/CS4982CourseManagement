using CourseManagement.DAL;

namespace CoursesManagementDesktop.Model
{
    internal static class CourseManagementTools
    {
        #region Properties        
        /// <summary>
        /// Gets or sets the teacher identifier.
        /// </summary>
        /// <value>
        /// The teacher identifier.
        /// </value>
        public static string TeacherID { get; set; }

        #endregion

        #region Methods        
        /// <summary>
        /// Finds the CRN.
        /// </summary>
        /// <param name="courseName">Name of the course.</param>
        /// <param name="semester">The semester.</param>
        /// <returns></returns>
        public static int findCrn(string courseName, string semester)
        {
            var courseDal = new CourseDAL();
            var crn = -1;

            var courses = courseDal.GetCoursesByTeacherAndSemester(TeacherID, semester);
            foreach (var course in courses)
            {
                if (course.Name.Equals(courseName))
                {
                    crn = course.CRN;
                }
            }

            return crn;
        }

        #endregion
    }
}
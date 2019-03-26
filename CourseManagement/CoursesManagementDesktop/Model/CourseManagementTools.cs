using CourseManagement.DAL;

namespace CoursesManagementDesktop.Model
{
    internal static class CourseManagementTools
    {
        #region Properties

        public static string TeacherID { get; set; }

        #endregion

        #region Methods

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
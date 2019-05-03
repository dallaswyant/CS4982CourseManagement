using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.DAL;

namespace CoursesManagementDesktop.Model
{
    static class CourseManagementTools
    {
        public static string TeacherID { get; set; }

        public static int findCrn(string courseName,string semester)
        {
            CourseDAL courseDal = new CourseDAL();
            var crn = -1;

            var courses = courseDal.GetCoursesByTeacherAndSemester(TeacherID,semester);
            foreach (var course in courses)
            {
                if (course.Name.Equals(courseName))
                {
                    crn = course.CRN;
                }
            }

            return crn;
        }

    }
}

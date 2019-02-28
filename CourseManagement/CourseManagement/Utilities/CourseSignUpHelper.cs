using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CourseManagement.Utilities
{
    public class CourseSignUpHelper
    {
        public bool CheckIfStudentCanSignUpForCourse(int crn, string studentID)
        {
            CourseDAL courses = new CourseDAL();
            var preReqs = courses.GetPrerequisiteCoursesForGivenCRN(crn);
            Course currentcourse = courses.GetCourseByCRN(crn);
            var grades = courses.GetGradesEarnedForCompletedCourse(currentcourse.Name, studentID);
            bool canAdd = false;
            foreach (var thing in preReqs)
            {
                if (this.getGradeValueFromChar(thing.Value) <= this.getGradeValueFromChar(grades[grades.Count]))
                {
                    canAdd = true;
                }
            }
            return canAdd;
        }

        private int getGradeValueFromChar(char value)
        {
            if (value == 'A')
            {
                return 5;
            }

            if (value == 'B')
            {
                return 4;
            }

            if (value == 'C')
            {
                return 3;
            }

            if (value == 'D')
            {
                return 2;
            }

            return 1;
        }
    }
}
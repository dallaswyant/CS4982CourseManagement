using System;
using System.Collections.Generic;
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
            if (preReqs.Count == 0)
            {
                return true;
            }
            List<string> stuffToCheck = new List<string>();
            foreach (var curr in preReqs)
            {
                stuffToCheck.Add(curr.Key);
            }
            Course currentcourse = courses.GetCourseByCRN(crn);
            var grades = courses.GetGradesEarnedForCompletedCourse(stuffToCheck[0], studentID);
            bool[] canAdd = new bool[preReqs.Count];
            int count = 0;
            if (grades.Count < preReqs.Count)
            {
                return false;
            }

            //if (grades[grades.Count-1] == null)
            //{
                //throw new Exception("This course is still in progress.");
            //}
            foreach (var thing in preReqs)
            {
                if (this.getGradeValueFromChar(thing.Value) <= this.getGradeValueFromChar(grades[grades.Count-1]))
                {
                    canAdd[count] = true;
                }

                count++;
            }

            foreach (var thing in canAdd)
            {
                if (!thing)
                {
                    return false;
                }
            }
            return true;
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
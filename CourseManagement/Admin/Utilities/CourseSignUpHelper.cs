﻿using System;
using System.Collections.Generic;
using Admin.DAL;
using Admin.Models;


namespace Admin.Utilities
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
            List<string> namesToCheck = new List<string>();
            List<char> gradesToCheck = new List<char>();
            foreach (var curr in preReqs)
            {
                namesToCheck.Add(curr.Key);
                gradesToCheck.Add(curr.Value);
            }
            int counter = 0;
            bool[] canAdd = new bool[preReqs.Count];
            foreach (var courseToCheck in namesToCheck)
            {
                var grades = courses.GetGradesEarnedForCompletedCourse(courseToCheck, studentID);
                if (grades.Count < 1)
                {
                    return false;
                }

                //if (grades[grades.Count-1] == null)
                //{
                //throw new Exception("This course is still in progress.");
                //}
                foreach (var currentGrade in grades)
                {
                    if (this.getGradeValueFromChar(gradesToCheck[counter]) <=
                        this.getGradeValueFromChar(currentGrade))
                    {
                        canAdd[counter] = true;
                    }
                }

                counter++;
            }
            foreach (var currentCanAddValue in canAdd)
            {
                if (!currentCanAddValue)
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

        public string GetPreReqsAndFormatForDisplay(int crn)
        {
            CourseDAL preReqChecker = new CourseDAL();
            var preReqs = preReqChecker.GetPrerequisiteCoursesForGivenCRN(crn);
            string output = String.Empty;
            foreach (var currentPreReq in preReqs)
            {
                output += "This course requires completing the course: " + currentPreReq.Key +
                          " with a grade of at least: " + currentPreReq.Value + Environment.NewLine;
            }

            return output;
        }

        public bool IsCourseContributingToDegreeProgram(Course courseCheck, string studentIDCheck)
        {
            DegreeProgramDAL degreeChecker = new DegreeProgramDAL();
            string degreeName = degreeChecker.GetDegreeProgramByStudentID(studentIDCheck);
            List<string> requiredCourses = degreeChecker.GetCourseNamesByDegreeProgram(degreeName);
            foreach (var currentCourse in requiredCourses)
            {
                if (currentCourse.Equals(courseCheck.Name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using CourseManagement.DAL;
using CourseManagement.Models;


namespace CourseManagement.Utilities
{
    public class CourseSignUpHelper
    {
        public bool CheckIfStudentCanSignUpForCourseBasedOnPreReqs(int crn, string studentID)
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
            Course currentcourse = courses.GetCourseByCRN(crn);
            int counter = 0;
            bool[] canAdd = new bool[preReqs.Count];
            foreach (var courseToCheck in namesToCheck)
            {
                var grades = courses.GetGradesEarnedForCompletedCourse(courseToCheck, studentID);
                if (grades.Count < 1)
                {
                    return false;
                }

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

        public bool CheckIfCanSignUpForCourseBasedOnTimes(int crn, string studentID, string semesterID)
        {
            //get all courses that a student is currently signed up for
            //check if those courses time's directly conflict with the new course's time
            bool canSignUp = true;
            CourseDAL courseGetter = new CourseDAL();
            List<Course> currentCourses = courseGetter.GetCoursesByStudentIDAndSemester(studentID, semesterID);
            CourseTimeDAL timeChecker = new CourseTimeDAL();
            CourseTime desiredTime = timeChecker.GetCourseTimeByCRN(crn);
            foreach (var currentCourse in currentCourses)
            {
                CourseTime currentTime = timeChecker.GetCourseTimeByCRN(currentCourse.CRN);
                foreach (var currentDay in currentTime.CourseDays.ToCharArray())
                {
                    foreach (var desiredDays in desiredTime.CourseDays.ToCharArray())
                    {
                        if (desiredDays == currentDay)
                        {
                            if (desiredTime.CourseStart.TimeOfDay > currentTime.CourseStart.TimeOfDay &&
                                desiredTime.CourseStart.TimeOfDay < currentTime.CourseEnd.TimeOfDay)
                            {
                                canSignUp = false;
                                return canSignUp;
                            }
                        }
                    }
                }
            }

            return canSignUp;
        }

        public bool CheckIfCourseContributesToMajor(int crn, string studentID)
        {
            DegreeProgramDAL degreeChecker = new DegreeProgramDAL();
            string degree = degreeChecker.GetDegreeProgramByStudentID(studentID);
            List<string> degreeCourses = degreeChecker.GetCourseNamesByDegreeProgram(degree);
            CourseDAL courseChecker = new CourseDAL();
            Course currentCourse = courseChecker.GetCourseByCRN(crn);
            bool isContributing = false;
            foreach (var names in degreeCourses)
            {
                if (names.Equals(currentCourse.Name))
                {
                    isContributing = true;
                }
            }

            return isContributing;
        }
    }
}
﻿using System;
using System.Collections.Generic;

namespace CourseManagement.App_Code
{
    public class Course
    {
        #region Properties

        public List<GradedItem> GradeItems { get; }

        public List<string> LectureNotes { get; } //TODO discuss being file paths
        public CourseInfo CourseInfo { get; }

        public Department Department { get; }
        public DateTime DropDeadline { get; }
        public int MaxSeats { get; }
        public List<Student> EnrolledStudents { get; }

        #endregion

        #region Constructors

        public Course(List<GradedItem> gradeItems, List<string> lectureNotes, CourseInfo courseInfo,
            Department department, DateTime dropDeadline, int maxSeats, List<Student> enrolledStudents)
        {
            this.GradeItems = gradeItems;
            this.LectureNotes = lectureNotes;
            this.CourseInfo = courseInfo;
            this.Department = department;
            this.DropDeadline = dropDeadline;
            this.MaxSeats = maxSeats;
            this.EnrolledStudents = enrolledStudents;
        }

        #endregion
    }
}
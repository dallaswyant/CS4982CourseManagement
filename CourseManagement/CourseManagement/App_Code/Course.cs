using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CourseManagement.App_Code
{
    public class Course
    {
        public List<GradedItem> GradeItems { get; private set; }

        public List<string> LectureNotes { get; private set; }//TODO discuss being file paths
        public CourseInfo CourseInfo { get; private set; }

        public Department Department { get; private set; }
        public DateTime DropDeadline { get; private set; }
        public int MaxSeats { get; private set; }
        public List<Student> EnrolledStudents { get; private set; }

        public Course(List<GradedItem> gradeItems, List<string> lectureNotes, CourseInfo courseInfo, Department department, DateTime dropDeadline, int maxSeats, List<Student> enrolledStudents)
        {
            GradeItems = gradeItems;
            LectureNotes = lectureNotes;
            CourseInfo = courseInfo;
            Department = department;
            DropDeadline = dropDeadline;
            MaxSeats = maxSeats;
            EnrolledStudents = enrolledStudents;
        }
    }
}
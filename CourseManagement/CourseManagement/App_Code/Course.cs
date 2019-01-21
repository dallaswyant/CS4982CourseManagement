using System;
using System.Collections.Generic;

namespace CourseManagement.App_Code
{
    //commment
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

        public CourseRubric CourseRubric { get; }
        #endregion

        #region Constructors

        public Course(List<GradedItem> gradeItems, CourseInfo courseInfo, int maxSeats)
        {
            this.GradeItems = gradeItems;
            this.CourseInfo = courseInfo;
            this.MaxSeats = maxSeats;
        }

        public int CountRemainingSeats()
        {
            return this.MaxSeats - this.EnrolledStudents.Count;

        }

        #endregion
    }
}
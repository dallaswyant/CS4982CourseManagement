using System;
using System.Collections.Generic;

namespace CourseManagementLibrary.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Course
    {
        #region Properties
        /// <summary>
        /// gets grade Item
        /// </summary>
        public List<GradedItem> GradeItems { get; }
        /// <summary>
        /// Gets lecture Notes
        /// </summary>
        public List<string> LectureNotes { get; } //TODO discuss being file paths
        /// <summary>
        /// Gets courseInfo
        /// </summary>
        public CourseInfo CourseInfo { get; }
        /// <summary>
        /// Gets the Department
        /// </summary>
        public Department Department { get; }
        /// <summary>
        /// Gets the Drop dead line
        /// </summary>
        public DateTime DropDeadline { get; }
        /// <summary>
        /// Gets the Max seats
        /// </summary>
        public int MaxSeats { get; }
        /// <summary>
        /// Gets the Enrolled Students
        /// </summary>
        public List<Student> EnrolledStudents { get; }
        /// <summary>
        /// Gets course Rubric
        /// </summary>
        public CourseRubric CourseRubric { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for course
        /// </summary>
        /// <param name="gradeItems">the grade items</param>
        /// <param name="courseInfo"> the course info</param>
        /// <param name="maxSeats">the maximum seats</param>
        public Course(List<GradedItem> gradeItems, CourseInfo courseInfo, int maxSeats)
        {
            this.GradeItems = gradeItems;
            this.CourseInfo = courseInfo;
            this.MaxSeats = maxSeats;
        }
        /// <summary>
        /// counts the remaining seats and returns them
        /// </summary>
        /// <returns>the number of remaining seats</returns>
        public int CountRemainingSeats()
        {
            return this.MaxSeats - this.EnrolledStudents.Count;

        }
        /// <summary>
        /// auto-generated to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return CourseInfo.Name;
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using CourseManagement.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    /// <summary>
    /// Class for unit testing the Course class
    /// </summary>
    [TestClass]
    public class CourseTest
    {
        /// <summary>
        /// Tests the course constructor.
        /// </summary>
        [TestMethod]
        public void TestCourseConstructor()
        {
            /**
            CourseInfo courseInfo = new CourseInfo("Psychology", "Psychology Building", 4, 1, "Section 1");
            List<GradedItem> gradesForCourse = new List<GradedItem>();
            Course course = new Course(gradesForCourse,courseInfo,50);
            Assert.AreEqual(course.CourseInfo,courseInfo);
            Assert.AreEqual(course.GradeItems,gradesForCourse);
            Assert.AreEqual(course.MaxSeats,50);
            **/
        }

        /// <summary>
        /// Tests the course properties.
        /// </summary>
        [TestMethod]
        public void TestCourseProperties()
        {
            /**
            CourseInfo courseInfo = new CourseInfo("Psychology", "Psychology Building", 4, 1, "Section 1");
            List<GradedItem> gradesForCourse = new List<GradedItem>();
            Course course = new Course(gradesForCourse, courseInfo, 50);
            Assert.AreEqual(course.CourseRubric, null);
            Assert.AreEqual(course.Department, null);
            Assert.AreEqual(course.DropDeadline, DateTime.MinValue);
            Assert.AreEqual(course.LectureNotes,null);
            **/
        }

        /// <summary>
        /// Tests the course count remaining seats throws null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void TestCourseCountRemainingSeatsThrowsNull()
        {
            /**
            CourseInfo courseInfo = new CourseInfo("Psychology", "Psychology Building", 4, 1, "Section 1");
            List<GradedItem> gradesForCourse = new List<GradedItem>();
            Course course = new Course(gradesForCourse, courseInfo, 50);
            course.CountRemainingSeats();
            **/

        }

        /// <summary>
        /// Tests the course to string.
        /// </summary>
        [TestMethod]
        public void TestCourseToString()
        {
            /**
            CourseInfo courseInfo = new CourseInfo("Psychology", "Psychology Building", 4, 1, "Section 1");
            List<GradedItem> gradesForCourse = new List<GradedItem>();
            Course course = new Course(gradesForCourse, courseInfo, 50);
            Assert.AreEqual(course.ToString(), courseInfo.Name);
            **/
        }
    }
}

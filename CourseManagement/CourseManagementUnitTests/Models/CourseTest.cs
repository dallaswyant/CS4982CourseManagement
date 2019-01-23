using System;
using System.Collections.Generic;
using CourseManagement.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    [TestClass]
    public class CourseTest
    {
        [TestMethod]
        public void TestCourseConstructor()
        {
            CourseInfo courseInfo = new CourseInfo("Psychology", "Psychology Building", 4, 1, "Section 1");
            List<GradedItem> gradesForCourse = new List<GradedItem>();
            Course course = new Course(gradesForCourse,courseInfo,50);
            Assert.AreEqual(course.CourseInfo,courseInfo);
            Assert.AreEqual(course.GradeItems,gradesForCourse);
            Assert.AreEqual(course.MaxSeats,50);
        }

        [TestMethod]
        public void TestCourseProperties()
        {
            CourseInfo courseInfo = new CourseInfo("Psychology", "Psychology Building", 4, 1, "Section 1");
            List<GradedItem> gradesForCourse = new List<GradedItem>();
            Course course = new Course(gradesForCourse, courseInfo, 50);
            Assert.AreEqual(course.CourseRubric, null);
            Assert.AreEqual(course.Department, null);
            Assert.AreEqual(course.DropDeadline, DateTime.MinValue);
            Assert.AreEqual(course.LectureNotes,null);
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void TestCourseCountRemainingSeatsThrowsNull()
        {
            CourseInfo courseInfo = new CourseInfo("Psychology", "Psychology Building", 4, 1, "Section 1");
            List<GradedItem> gradesForCourse = new List<GradedItem>();
            Course course = new Course(gradesForCourse, courseInfo, 50);
            course.CountRemainingSeats();

        }

        [TestMethod]
        public void TestCourseToString()
        {
            CourseInfo courseInfo = new CourseInfo("Psychology", "Psychology Building", 4, 1, "Section 1");
            List<GradedItem> gradesForCourse = new List<GradedItem>();
            Course course = new Course(gradesForCourse, courseInfo, 50);
            Assert.AreEqual(course.ToString(), courseInfo.Name);
        }
    }
}

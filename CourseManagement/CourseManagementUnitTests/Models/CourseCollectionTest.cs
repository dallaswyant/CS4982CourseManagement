using System;
using System.Collections.Generic;
using CourseManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    //TODO not in use discuss removal
    /// <summary>
    /// Class for unit testing the Course Collection class
    /// </summary>
    [TestClass]
    public class CourseCollectionTest
    {
        /// <summary>
        /// Tests the course collection constructor.
        /// </summary>
        [TestMethod]
        public void TestCourseCollectionConstructor()
        { 
            CourseCollection collection = new CourseCollection();
            Assert.AreEqual(collection.Courses.Count, 0);
        }

        /// <summary>
        /// Tests the course collection adding courses.
        /// </summary>
        [TestMethod]
        public void TestCourseCollectionAddingCourses()
        {
            /**
            CourseCollection collection = new CourseCollection();
            Assert.AreEqual(collection.Courses.Count, 0);
            CourseInfo courseInfo = new CourseInfo("Psychology", "Psychology Building", 4, 1, "Section 1");
            List<GradedItem> gradesForCourse = new List<GradedItem>();
            Course course = new Course(gradesForCourse, courseInfo, 50);
            collection.Add(course);
            CourseInfo courseInfo1 = new CourseInfo("Psychology2", "Psychology Building", 4, 1, "Section 2");
            List<GradedItem> gradesForCourse1 = new List<GradedItem>();
            Course course1 = new Course(gradesForCourse1, courseInfo1, 50);
            collection.Add(course1);
            Assert.AreEqual(collection.Courses[0],course);
            Assert.AreEqual(collection.Courses[1],course1);
            Assert.AreEqual(collection.Courses.Count,2);
            **/
        }
    }
}

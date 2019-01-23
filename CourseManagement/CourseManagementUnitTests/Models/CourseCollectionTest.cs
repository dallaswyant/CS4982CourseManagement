using System;
using System.Collections.Generic;
using CourseManagement.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    [TestClass]
    public class CourseCollectionTest
    {
        [TestMethod]
        public void TestCourseCollectionConstructor()
        { 
            CourseCollection collection = new CourseCollection();
            Assert.AreEqual(collection.Courses.Count, 0);
        }

        [TestMethod]
        public void TestCourseCollectionAddingCourses()
        {
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
        }
    }
}

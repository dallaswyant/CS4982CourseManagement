using System;
using CourseManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    [TestClass]
    public class CourseTimeTest
    {
        [TestMethod]
        public void TestCourseTimeConstructor()
        {
            int courseTimeId = 1;
            DateTime courseStart = DateTime.Now;
            DateTime courseEnd = DateTime.Now.AddHours(1.5);
            string courseDays = "MWF";
            CourseTime courseTime = new CourseTime(courseTimeId, courseStart, courseEnd, courseDays);

            Assert.AreEqual(courseTime.CourseTimeID, courseTimeId);
            Assert.AreEqual(courseTime.CourseStart, courseStart);
            Assert.AreEqual(courseTime.CourseEnd, courseEnd);
            Assert.AreEqual(courseTime.CourseDays, courseDays);
        }
    }
}

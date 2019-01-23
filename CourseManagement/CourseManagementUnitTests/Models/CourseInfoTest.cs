using System;
using CourseManagement.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    [TestClass]
    public class CourseInfoTest
    {
        [TestMethod]
        public void TestCourseInfoConstructor()
        {
            CourseInfo courseInfo = new CourseInfo("Psychology","Psychology Building",4,1,"Section 1");
            Assert.AreEqual(courseInfo.Name,"Psychology");
            Assert.AreEqual(courseInfo.Location,"Psychology Building");
            Assert.AreEqual(courseInfo.CreditHours,4);
            Assert.AreEqual(courseInfo.CRN,1);
            Assert.AreEqual(courseInfo.SectionNumber,"Section 1");

        }

        [TestMethod]
        public void TestCourseInfoProperties()
        {
            CourseInfo courseInfo = new CourseInfo("Psychology", "Psychology Building", 4, 1, "Section 1");
            Assert.AreEqual(courseInfo.Teacher, null);
            Assert.AreEqual(courseInfo.PreReqClasses, null);
            Assert.AreEqual(courseInfo.Description, null);
        }
    }
}

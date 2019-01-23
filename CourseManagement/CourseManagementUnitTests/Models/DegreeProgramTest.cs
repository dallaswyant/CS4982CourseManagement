using System;
using CourseManagement.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    [TestClass]
    public class DegreeProgramTest
    {
        [TestMethod]
        public void TestDegreeProgramConstructor()
        {
            CourseManagement.App_Code.CourseCollection courses = new CourseManagement.App_Code.CourseCollection();
            DegreeProgram degree = new DegreeProgram("Bachelors of Science",courses);
            Assert.AreEqual(degree.name,"Bachelors of Science");
            Assert.AreEqual(degree.RequiredCourses,courses);
        }
    }
}

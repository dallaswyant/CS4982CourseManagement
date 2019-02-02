using System;
using CourseManagement.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    /// <summary>
    /// Class for unit testing the degree program class
    /// </summary>
    [TestClass]
    public class DegreeProgramTest
    {
        /// <summary>
        /// Tests the degree program constructor.
        /// </summary>
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

using System;
using CourseManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    /// <summary>
    /// Class for unit testing the student class
    /// </summary>
    [TestClass]
    public class StudentTest
    {
        /// <summary>
        /// Tests the student constructor.
        /// </summary>
        [TestMethod]
        public void TestStudentConstructor()
        {
            Student billy = new Student("student", "billy@billy.com");
            Assert.AreEqual(billy.Email,"billy@billy.com");
            Assert.AreEqual(billy.StudentUID,"student");
        }

    }
}

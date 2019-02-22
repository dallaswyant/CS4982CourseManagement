using System;
using CourseManagement.App_Code;
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
            Student billy = new Student("student", "Billy", "billy@billy.com");
            Assert.AreEqual(billy.Email,"billy@billy.com");
            Assert.AreEqual(billy.StudentUID,"student");
        }

        /// <summary>
        /// Tests the student to string.
        /// </summary>
        [TestMethod]
        public void TestStudentToString()
        {
            Student billy = new Student("student", "Billy", "billy@billy.com");

           
        }

        /// <summary>
        /// Tests the student properties.
        /// </summary>
        [TestMethod]
        public void TestStudentProperties()
        {
            Student billy = new Student("student", "Billy", "billy@billy.com");
            Assert.AreEqual(billy.ActiveCourses, null);
            Assert.AreEqual(billy.Program, null);
        }
    }
}

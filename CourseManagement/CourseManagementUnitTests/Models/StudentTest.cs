using System;
using CourseManagement.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    [TestClass]
    public class StudentTest
    {
        [TestMethod]
        public void TestStudentConstructor()
        {
            Student billy = new Student("student", "Billy", "billy@billy.com");
            Assert.AreEqual(billy.name,"Billy");
            Assert.AreEqual(billy.Email,"billy@billy.com");
            Assert.AreEqual(billy.StudentUID,"student");
        }

        [TestMethod]
        public void TestStudentToString()
        {
            Student billy = new Student("student", "Billy", "billy@billy.com");

            Assert.AreEqual(billy.ToString(), "Billy");
        }

        [TestMethod]
        public void TestStudentProperties()
        {
            Student billy = new Student("student", "Billy", "billy@billy.com");
            Assert.AreEqual(billy.ActiveCourses, null);
            Assert.AreEqual(billy.Program, null);
        }
    }
}

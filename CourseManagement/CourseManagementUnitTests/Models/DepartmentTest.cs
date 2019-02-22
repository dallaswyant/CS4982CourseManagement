using System;
using System.Collections.Generic;
using CourseManagement.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    /// <summary>
    /// Class for unit testing the department class
    /// </summary>
    [TestClass]
    public class DepartmentTest
    {
        /// <summary>
        /// Tests the department constructor.
        /// </summary>
        [TestMethod]
        public void TestDepartmentConstructor()
        {
            List<Course> coursesForBob = new List<Course>();
            Teacher chair = new Teacher("office", "bob", "bob@bob.com", true, "867-5309", coursesForBob, "teacher");
            Department department = new Department(chair,"Psychology");

            Assert.AreEqual(department.Chair, chair);
            Assert.AreEqual(department.Name,"Psychology");
        }
    }
}

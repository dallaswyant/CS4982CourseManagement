using System;
using System.Collections.Generic;
using CourseManagement.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    [TestClass]
    public class DepartmentTest
    {
        [TestMethod]
        public void TestDepartmentConstructor()
        {
            List<Course> coursesForBob = new List<Course>();
            Teacher chair = new Teacher("office", "bob", "bob@bob.com", true, "867-5309", coursesForBob, "teacher");
            CourseManagement.App_Code.CourseCollection departmentCourses = new CourseManagement.App_Code.CourseCollection();
            List<Teacher> departmentTeachers = new List<Teacher>();
            Department department = new Department(chair,departmentCourses,"Psychology",departmentTeachers);

            Assert.AreEqual(department.Chair, chair);
            Assert.AreEqual(department.DeptCourses,departmentCourses);
            Assert.AreEqual(department.DeptName,"Psychology");
            Assert.AreEqual(department.Teachers,departmentTeachers);
        }
    }
}

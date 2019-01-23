using System;
using System.Collections.Generic;
using CourseManagement.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    [TestClass]
    public class TeacherTest
    {
        [TestMethod]
        public void TestTeacherConstructor()
        {
            List<Course> coursesForBob = new List<Course>();
            Teacher teacher = new Teacher("office","bob","bob@bob.com",true,"867-5309",coursesForBob,"teacher");
            Assert.AreEqual(teacher.Location,"office");
            Assert.AreEqual(teacher.Name,"bob");
            Assert.AreEqual(teacher.Email,"bob@bob.com");
            Assert.AreEqual(teacher.IsEmailPublic,true);
            Assert.AreEqual(teacher.PhoneNumber,"867-5309");
            Assert.AreEqual(teacher.CoursesTaught,coursesForBob);
            Assert.AreEqual(teacher.TeacherUID,"teacher");
        }

        [TestMethod]
        public void TestTeacherProperties()
        {
            List<Course> coursesForBob = new List<Course>();
            Teacher teacher = new Teacher("office", "bob", "bob@bob.com", true, "867-5309", coursesForBob, "teacher");
            Assert.AreEqual(teacher.PrimaryDepartment, null);
    
        }
    }
}

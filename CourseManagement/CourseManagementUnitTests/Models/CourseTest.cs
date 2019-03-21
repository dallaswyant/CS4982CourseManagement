using System;
using System.Collections.Generic;
using CourseManagement.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    /// <summary>
    /// Class for unit testing the Course class
    /// </summary>
    [TestClass]
    public class CourseTest
    {
        /// <summary>
        /// Tests the course constructor.
        /// </summary>
        [TestMethod]
        public void TestCourseConstructor()
        {
            int crn = 1;
            string department = "Psychology";
            string name = "Psychology 101";
            string description = "Learn things";
            string section = "Section 2";
            int maxSeats = 50;
            string location = "Psychology Building";
            string semester = "SP15";
            int courseTimeId = 5;
            Course theCourse = new Course(crn, department, name, description, section, maxSeats,
                location, semester, courseTimeId);

            Assert.AreEqual(theCourse.CRN,crn);
            Assert.AreEqual(theCourse.DepartmentName,department);
            Assert.AreEqual(theCourse.Description,description);
            Assert.AreEqual(theCourse.SectionNumber,section);
            Assert.AreEqual(theCourse.MaxSeats, maxSeats);
            Assert.AreEqual(theCourse.Location,location);
            Assert.AreEqual(theCourse.SemesterID, semester);
            Assert.AreEqual(theCourse.CourseTimeID, courseTimeId);
            
        }
    }
}

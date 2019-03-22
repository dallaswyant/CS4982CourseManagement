using System;
using CourseManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    [TestClass]
    public class SemesterTest
    {
        [TestMethod]
        public void TestSemester()
        {
            string semesterID = "SP15";
            DateTime addDrop = DateTime.Today.AddDays(1);
            DateTime finalGrade = DateTime.Today.AddDays(8);
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(7);
            Semester testSemester = new Semester(semesterID, addDrop, finalGrade, startDate, endDate);

            Assert.AreEqual(testSemester.AddDropDeadline, addDrop);
            Assert.AreEqual(testSemester.FinalGradeDeadline, finalGrade);
            Assert.AreEqual(testSemester.StartDate, startDate);
            Assert.AreEqual(testSemester.EndDate, endDate);
            Assert.AreEqual(testSemester.SemesterID, semesterID);
        }
    }
}

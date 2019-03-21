using System;
using CourseManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    /// <summary>
    /// Class for unit testing the graded item class
    /// </summary>
    [TestClass]
    public class GradeItemTest
    {
        /// <summary>
        /// Tests the graded item constructor.
        /// </summary>
        [TestMethod]
        public void TestGradeItemConstructor()
        {
            Student student = new Student("student", "billy@billy.com");

            string name = "Homework 1";
            double gradeEarned = 30;
            string feedback = "You failed.";
            double gradePossible = 100;
            string gradeType = "homework";
            string description = "The first homework";
            int gradeID = 1;
            DateTime timeGraded = DateTime.Now;
            bool isPublic = true;
            
            GradeItem grade = new GradeItem(name, student, gradeEarned, feedback, gradePossible, gradeType, description, gradeID, isPublic, timeGraded);

            Assert.AreEqual(grade.Name, name);
            Assert.AreEqual(grade.Student, student);
            Assert.AreEqual(grade.Grade, gradeEarned);
            Assert.AreEqual(grade.Feedback, feedback);
            Assert.AreEqual(grade.PossiblePoints, gradePossible);
            Assert.AreEqual(grade.GradeType, gradeType);
            Assert.AreEqual(grade.Description, description);
            Assert.AreEqual(grade.GradeId, gradeID);
            Assert.AreEqual(grade.TimeGraded, timeGraded);
        }

        /// <summary>
        /// Tests the graded item default constructor.
        /// </summary>
        [TestMethod]
        public void TestGradeItemDefaultConstructor()
        {
            
            GradeItem grade = new GradeItem();
            Assert.AreEqual(grade.Name, null);
            Assert.AreEqual(grade.Student, null);
            Assert.AreEqual(grade.Grade, 0);
            Assert.AreEqual(grade.Feedback, null);
            Assert.AreEqual(grade.PossiblePoints, 0);
            Assert.AreEqual(grade.GradeType, null);
            Assert.AreEqual(grade.GradeId, 0);
            Assert.AreEqual(grade.Description,null);
            Assert.AreEqual(grade.TimeGraded, null);
            

        }

        /// <summary>
        /// Tests the graded item properties and setters.
        /// </summary>
        [TestMethod]
        public void TestGradeItemIsGraded()
        {
            Student student = new Student("student", "billy@billy.com");

            string name = "Homework 1";
            double gradeEarned = 30;
            string feedback = "You failed.";
            double gradePossible = 100;
            string gradeType = "homework";
            string description = "The first homework";
            int gradeID = 1;
            DateTime timeGraded = DateTime.Now;
            bool isPublic = true;

            GradeItem grade1 = new GradeItem(name, student, gradeEarned, feedback, gradePossible, gradeType, description, gradeID, isPublic, timeGraded);
            GradeItem grade2 = new GradeItem(name, student, gradeEarned, feedback, gradePossible, gradeType, description, gradeID, isPublic, null);

            Assert.AreEqual(grade1.IsGraded(), true);
            Assert.AreEqual(grade2.IsGraded(), false);
        }
    }
}

﻿using System;
using CourseManagement.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    /// <summary>
    /// Class for unit testing the graded item class
    /// </summary>
    [TestClass]
    public class GradedItemTest
    {
        /// <summary>
        /// Tests the graded item constructor.
        /// </summary>
        [TestMethod]
        public void TestGradedItemConstructor()
        {
            Student student = new Student("student", "billy", "billy@billy.com");
            GradedItem grade = new GradedItem("Homework 1", student, 30, "you failed", 100, "homework", 1, null, null);
            Assert.AreEqual(grade.Name, "Homework 1");
            Assert.AreEqual(grade.Student, student);
            Assert.AreEqual(grade.Grade, 30);
            Assert.AreEqual(grade.Feedback, "you failed");
            Assert.AreEqual(grade.PossiblePoints, 100);
            Assert.AreEqual(grade.GradeType, "homework");
            Assert.AreEqual(grade.GradeId, 1);

        }

        /// <summary>
        /// Tests the graded item default constructor.
        /// </summary>
        [TestMethod]
        public void TestGradedItemDefaultConstructor()
        {
            
            GradedItem grade = new GradedItem();
            Assert.AreEqual(grade.Name, null);
            Assert.AreEqual(grade.Student, null);
            Assert.AreEqual(grade.Grade, 0);
            Assert.AreEqual(grade.Feedback, null);
            Assert.AreEqual(grade.PossiblePoints, 0);
            Assert.AreEqual(grade.GradeType, null);
            Assert.AreEqual(grade.GradeId, 0);

        }

        /// <summary>
        /// Tests the graded item properties and setters.
        /// </summary>
        [TestMethod]
        public void TestGradedItemPropertiesAndSetters()
        {
            Student student = new Student("student", "billy", "billy@billy.com");
            GradedItem grade = new GradedItem("Homework 1", student, 30, "you failed", 100, "homework", 1, null, null);
            grade.Name = "Homework 2";
            grade.Grade = 90;
            grade.Feedback = "you passed";
            grade.GradeId = 2;
            Assert.AreEqual(grade.Name, "Homework 2");
            Assert.AreEqual(grade.Grade, 90);
            Assert.AreEqual(grade.Feedback, "you passed");
            Assert.AreEqual(grade.GradeId, 2);
        }
    }
}

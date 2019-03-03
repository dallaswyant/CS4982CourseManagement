using System;
using CourseManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    /// <summary>
    /// Class for unit testing the rubric item class
    /// </summary>
    [TestClass]
    public class RubricItemTest
    {
        /// <summary>
        /// Tests the rubric item constructor.
        /// </summary>
        [TestMethod]
        public void TestRubricItemConstructor()
        {
            RubricItem item = new RubricItem(1,"homework",25,1);
            Assert.AreEqual(item.CRN,1);
            Assert.AreEqual(item.AssignmentType,"homework");
            Assert.AreEqual(item.AssignmentWeight,25);
            Assert.AreEqual(item.Index, 1);
        }

        /// <summary>
        /// Tests the rubric item properties.
        /// </summary>
        [TestMethod]
        public void TestRubricItemProperties()
        {
            RubricItem item = new RubricItem(1, "homework", 25, 1);
            item.CRN = 3;
            item.AssignmentType = "projects";
            item.AssignmentWeight = 40;
            item.Index = 5;
            Assert.AreEqual(item.CRN, 3);
            Assert.AreEqual(item.AssignmentType, "projects");
            Assert.AreEqual(item.AssignmentWeight, 40);
            Assert.AreEqual(item.Index, 5);
        }
    }
}

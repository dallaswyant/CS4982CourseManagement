using System;
using CourseManagement.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    [TestClass]
    public class RubricItemTest
    {
        [TestMethod]
        public void TestRubricItemConstructor()
        {
            RubricItem item = new RubricItem(1,"homework",25,1);
            Assert.AreEqual(item.CRN,1);
            Assert.AreEqual(item.AssignmentType,"homework");
            Assert.AreEqual(item.AssignmentWeight,25);
            Assert.AreEqual(item.Index, 1);
        }

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

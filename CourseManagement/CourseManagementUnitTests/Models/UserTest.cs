using System;
using CourseManagement.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    /// <summary>
    /// Class for unit testing the teacher class
    /// </summary>
    [TestClass]
    public class UserTest
    {
        /// <summary>
        /// Tests the user constructor.
        /// </summary>
        [TestMethod]
        public void TestUserConstructor()
        {
            /**
            User theUser = new User("user","password","teacher");
            Assert.AreEqual(theUser.UserId,"user");
            Assert.AreEqual(theUser.Password,"password");
            Assert.AreEqual(theUser.Role,"teacher");
            **/
        }

        /// <summary>
        /// Tests the user default constructor.
        /// </summary>
        [TestMethod]
        public void TestUserDefaultConstructor()
        {
            User theUser = new User();
            Assert.AreEqual(theUser.UserId, null);
            Assert.AreEqual(theUser.Password, null);
            Assert.AreEqual(theUser.Role, null);
        }
    }
}

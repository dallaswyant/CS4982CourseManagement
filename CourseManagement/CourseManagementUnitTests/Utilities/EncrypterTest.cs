using System;
using CourseManagement.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Utilities
{
    [TestClass]
    public class EncrypterTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            String thing = Encrypter.Encrypt("password", "raspberryberet");
            String otherthing = Encrypter.Encrypt("password", "raspberryberet");
            String originalthing = Encrypter.Decrypt(thing, "raspberryberet");
            String testthing =
                Encrypter.Decrypt(
                    "fLibpTLVTaojqqqc4HbeTBhd7gOf+qS4h+y7/45IP3YGWuT2M7mdx7iNdnAYdpqI9VB0I6J0UlSPgfKSILh+SQKzUfJaiMIm+gM0GJtxJEiH5GIuhRg5Rf6iCbRP/Ha8",
                    "raspberryberet");
            Assert.AreEqual("password", originalthing);
        }
    }
}

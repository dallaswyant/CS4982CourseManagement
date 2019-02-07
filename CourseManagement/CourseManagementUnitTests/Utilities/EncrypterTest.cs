using System;
using CourseManagement.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Utilities
{
    /// <summary>
    /// This Class tests the encryption Utility class for encryption and decryption
    /// </summary>
    [TestClass]
    public class EncrypterTest
    {
        /// <summary>
        /// Tests the encryption against one password.
        /// </summary>
        [TestMethod]
        public void TestEncryptionAgainstOne()
        {
            String password = Encrypter.Encrypt("password", "raspberryberet");
            String originalpassword = Encrypter.Decrypt(password, "raspberryberet");
            Assert.AreEqual("password", originalpassword);
        }

        /// <summary>
        /// Tests the encryption against three of the same passwords,
        /// which are encrypted to three different strings.
        /// </summary>
        [TestMethod]
        public void TestEncryptionAgainstThree()
        {
            String password = Encrypter.Encrypt("password", "raspberryberet");
            String originalpassword = Encrypter.Decrypt(password, "raspberryberet");
            Assert.AreEqual("password",originalpassword);
            String password1 = Encrypter.Encrypt("password", "raspberryberet");
            String originalpassword1 = Encrypter.Decrypt(password1, "raspberryberet");
            Assert.AreEqual("password",originalpassword1);
            String password2 = Encrypter.Encrypt("password", "raspberryberet");
            String originalpassword2 = Encrypter.Decrypt(password2, "raspberryberet");
            Assert.AreEqual("password", originalpassword2);
        }
    }
}

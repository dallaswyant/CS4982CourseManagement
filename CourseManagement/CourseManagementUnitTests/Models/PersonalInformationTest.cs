using System;
using CourseManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseManagementUnitTests.Models
{
    [TestClass]
    public class PersonalInformationTest
    {
        [TestMethod]
        public void TestPersonalInformationConstructor()
        {
            string uid = "testID";
            string fname = "testFName";
            char minit = 'q';
            string lname = "testLName";
            int addressID = 1;
            string phoneNumber = "867-5309";
            string sex = "testSex";
            DateTime DOB = DateTime.Now;
            string race = "testRace";
            string email = "test@test.com";
            string ssn = "123456789";
            PersonalInformation testInformation = new PersonalInformation(uid, fname, minit, lname, addressID, phoneNumber, sex, DOB, race, email, ssn);

            Assert.AreEqual(testInformation.UID, uid);
            Assert.AreEqual(testInformation.FName, fname);
            Assert.AreEqual(testInformation.Minit, minit);
            Assert.AreEqual(testInformation.LName, lname);
            Assert.AreEqual(testInformation.AddrID, addressID);
            Assert.AreEqual(testInformation.PhoneNumber, phoneNumber);
            Assert.AreEqual(testInformation.Sex, sex);
            Assert.AreEqual(testInformation.DOB, DOB);
            Assert.AreEqual(testInformation.Race, race);
            Assert.AreEqual(testInformation.Email, email);
            Assert.AreEqual(testInformation.SSN, ssn);
        }
    }
}

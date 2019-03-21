using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace CourseManagement.Models
{
    public class PersonalInformation
    {
        /// <summary>
        /// The user ID
        /// </summary>
        public string UID;
        /// <summary>
        /// The first name
        /// </summary>
        public string FName;
        /// <summary>
        /// The middle initial
        /// </summary>
        public char Minit;
        /// <summary>
        /// The last name
        /// </summary>
        public string LName;
        /// <summary>
        /// The addr identifier
        /// </summary>
        public int AddrID;
        /// <summary>
        /// The phone number
        /// </summary>
        public string PhoneNumber;
        /// <summary>
        /// The sex
        /// </summary>
        public string Sex;
        /// <summary>
        /// The date of birth
        /// </summary>
        public DateTime DOB;
        /// <summary>
        /// The race
        /// </summary>
        public string Race;
        /// <summary>
        /// The email
        /// </summary>
        public string Email;
        /// <summary>
        /// The SSN
        /// </summary>
        public string SSN;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalInformation"/> class.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <param name="fname">The fname.</param>
        /// <param name="minit">The minit.</param>
        /// <param name="lname">The lname.</param>
        /// <param name="addrId">The addr identifier.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="sex">The sex.</param>
        /// <param name="dob">The dob.</param>
        /// <param name="race">The race.</param>
        /// <param name="email">The email.</param>
        /// <param name="ssn">The SSN.</param>
        public PersonalInformation(string uid, string fname, char minit, string lname, int addrId, string phoneNumber,
            string sex, DateTime dob,
            string race, string email, string ssn)
        {
            this.UID = uid;
            this.FName = fname;
            this.Minit = minit;
            this.LName = lname;
            this.AddrID = addrId;
            this.PhoneNumber = phoneNumber;
            this.Sex = sex;
            this.DOB = dob;
            this.Race = race;
            this.Email = email;
            this.SSN = ssn;

        }
    }
}
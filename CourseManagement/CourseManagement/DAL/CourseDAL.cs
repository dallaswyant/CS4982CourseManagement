using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class CourseDAL
    {
        /// <summary>
        /// Gets the person by identifier.
        /// </summary>
        /// <param name="personIDCheck">The person identifier check.</param>
        /// <returns>a person with the matching personID</returns>
        public CourseCollection GetCourseByTeacherID(int teacherIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            CourseCollection coursesTaught = new CourseCollection();
            using (conn)
            {

                conn.Open();
                var selectQuery = "select courses.* from courses, teacher_teaches_courses WHERE teacher_teaches_courses.courses_CRN = courses.CRN AND teacher_teaches_courses.teacher_id = @teacherID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@teacherID", teacherIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int personIdOrdinal = reader.GetOrdinal("personID");
                        int fnameOrdinal = reader.GetOrdinal("fname");
                        int minitOrdinal = reader.GetOrdinal("minit");
                        int lnameOrdinal = reader.GetOrdinal("lname");
                        int ssnOrdinal = reader.GetOrdinal("ssn");
                        int genderOrdinal = reader.GetOrdinal("gender");
                        int bdateOrdinal = reader.GetOrdinal("bdate");
                        int phoneOrdinal = reader.GetOrdinal("phonenumber");
                        int addr1Ordinal = reader.GetOrdinal("addr1");
                        int addr2Ordinal = reader.GetOrdinal("addr2");
                        int cityOrdinal = reader.GetOrdinal("city");
                        int stateOrdinal = reader.GetOrdinal("state");
                        int zipcodeOrdinal = reader.GetOrdinal("zipcode");
                        int countryOrdinal = reader.GetOrdinal("country");

                        while (reader.Read())
                        {
                            int personID = reader[personIdOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(personIdOrdinal);
                            string fname = reader[fnameOrdinal] == DBNull.Value ? default(string) : reader.GetString(fnameOrdinal);
                            char minit = reader[minitOrdinal] == DBNull.Value ? default(char) : reader.GetChar(minitOrdinal);
                            string lname = reader[lnameOrdinal] == DBNull.Value ? default(string) : reader.GetString(lnameOrdinal);
                            string ssn = reader[ssnOrdinal] == DBNull.Value ? default(string) : reader.GetString(ssnOrdinal);
                            string gender = reader[genderOrdinal] == DBNull.Value ? default(string) : reader.GetString(genderOrdinal);
                            DateTime bdate = reader[bdateOrdinal] == DBNull.Value ? default(DateTime) : reader.GetDateTime(bdateOrdinal);
                            string addr1 = reader[addr1Ordinal] == DBNull.Value ? default(string) : reader.GetString(addr1Ordinal);
                            string addr2 = reader[addr2Ordinal] == DBNull.Value ? default(string) : reader.GetString(addr2Ordinal);
                            string city = reader[cityOrdinal] == DBNull.Value ? default(string) : reader.GetString(cityOrdinal);
                            string state = reader[stateOrdinal] == DBNull.Value ? default(string) : reader.GetString(stateOrdinal);
                            string zipcode = reader[zipcodeOrdinal] == DBNull.Value ? default(string) : reader.GetString(zipcodeOrdinal);
                            string country = reader[countryOrdinal] == DBNull.Value ? default(string) : reader.GetString(countryOrdinal);
                            string phoneNumber = reader[phoneOrdinal] == DBNull.Value ? default(string) : reader.GetString(phoneOrdinal);
                            Address currAddress = new Address(addr1, addr2, city, state, zipcode, country);
                            Person currPerson = new Person(personID, fname, minit, lname, ssn, gender, phoneNumber, bdate, currAddress);
                            return currPerson;

                        }
                    }
                }
                conn.Close();
            }

            return null;
        }

        /// <summary>
        /// Gets the person by identifier.
        /// </summary>
        /// <param name="personIDCheck">The person identifier check.</param>
        /// <returns>a person with the matching personID</returns>
        public CourseCollection GetCoursesByStudentID(int studentIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            CourseCollection coursesTaught = new CourseCollection();
            using (conn)
            {

                conn.Open();
                var selectQuery = "select courses.* from courses, students, student_has_courses WHERE students.student_id = student_has_courses.student_id AND student_has_courses.courses_CRN = courses.CRN AND students.student_id = @studentID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@studentID", studentIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int personIdOrdinal = reader.GetOrdinal("personID");
                        int fnameOrdinal = reader.GetOrdinal("fname");
                        int minitOrdinal = reader.GetOrdinal("minit");
                        int lnameOrdinal = reader.GetOrdinal("lname");
                        int ssnOrdinal = reader.GetOrdinal("ssn");
                        int genderOrdinal = reader.GetOrdinal("gender");
                        int bdateOrdinal = reader.GetOrdinal("bdate");
                        int phoneOrdinal = reader.GetOrdinal("phonenumber");
                        int addr1Ordinal = reader.GetOrdinal("addr1");
                        int addr2Ordinal = reader.GetOrdinal("addr2");
                        int cityOrdinal = reader.GetOrdinal("city");
                        int stateOrdinal = reader.GetOrdinal("state");
                        int zipcodeOrdinal = reader.GetOrdinal("zipcode");
                        int countryOrdinal = reader.GetOrdinal("country");

                        while (reader.Read())
                        {
                            int personID = reader[personIdOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(personIdOrdinal);
                            string fname = reader[fnameOrdinal] == DBNull.Value ? default(string) : reader.GetString(fnameOrdinal);
                            char minit = reader[minitOrdinal] == DBNull.Value ? default(char) : reader.GetChar(minitOrdinal);
                            string lname = reader[lnameOrdinal] == DBNull.Value ? default(string) : reader.GetString(lnameOrdinal);
                            string ssn = reader[ssnOrdinal] == DBNull.Value ? default(string) : reader.GetString(ssnOrdinal);
                            string gender = reader[genderOrdinal] == DBNull.Value ? default(string) : reader.GetString(genderOrdinal);
                            DateTime bdate = reader[bdateOrdinal] == DBNull.Value ? default(DateTime) : reader.GetDateTime(bdateOrdinal);
                            string addr1 = reader[addr1Ordinal] == DBNull.Value ? default(string) : reader.GetString(addr1Ordinal);
                            string addr2 = reader[addr2Ordinal] == DBNull.Value ? default(string) : reader.GetString(addr2Ordinal);
                            string city = reader[cityOrdinal] == DBNull.Value ? default(string) : reader.GetString(cityOrdinal);
                            string state = reader[stateOrdinal] == DBNull.Value ? default(string) : reader.GetString(stateOrdinal);
                            string zipcode = reader[zipcodeOrdinal] == DBNull.Value ? default(string) : reader.GetString(zipcodeOrdinal);
                            string country = reader[countryOrdinal] == DBNull.Value ? default(string) : reader.GetString(countryOrdinal);
                            string phoneNumber = reader[phoneOrdinal] == DBNull.Value ? default(string) : reader.GetString(phoneOrdinal);
                            Address currAddress = new Address(addr1, addr2, city, state, zipcode, country);
                            Person currPerson = new Person(personID, fname, minit, lname, ssn, gender, phoneNumber, bdate, currAddress);
                            return currPerson;

                        }
                    }
                }
                conn.Close();
            }

            return null;
        }
    }
}
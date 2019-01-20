using System;
using CourseManagement.App_Code;

namespace CourseManagement.DAL
{
    public class CourseDAL
    {
        #region Methods

        /// <summary>
        ///     Gets the person by identifier.
        /// </summary>
        /// <param name="personIDCheck">The person identifier check.</param>
        /// <returns>a person with the matching personID</returns>
        public CourseCollection GetCourseByTeacherID(int teacherIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var coursesTaught = new CourseCollection();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "select courses.* from courses, teacher_teaches_courses WHERE teacher_teaches_courses.courses_CRN = courses.CRN AND teacher_teaches_courses.teacher_id = @teacherID";

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
                            var personID = reader[personIdOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(personIdOrdinal);
                            var fname = reader[fnameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(fnameOrdinal);
                            var minit = reader[minitOrdinal] == DBNull.Value
                                ? default(char)
                                : reader.GetChar(minitOrdinal);
                            var lname = reader[lnameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(lnameOrdinal);
                            var ssn = reader[ssnOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(ssnOrdinal);
                            var gender = reader[genderOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(genderOrdinal);
                            var bdate = reader[bdateOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(bdateOrdinal);
                            var addr1 = reader[addr1Ordinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(addr1Ordinal);
                            var addr2 = reader[addr2Ordinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(addr2Ordinal);
                            var city = reader[cityOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(cityOrdinal);
                            var state = reader[stateOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(stateOrdinal);
                            var zipcode = reader[zipcodeOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(zipcodeOrdinal);
                            var country = reader[countryOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(countryOrdinal);
                            var phoneNumber = reader[phoneOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(phoneOrdinal);
                            Address currAddress = new Address(addr1, addr2, city, state, zipcode, country);
                            Person currPerson = new Person(personID, fname, minit, lname, ssn, gender, phoneNumber,
                                bdate, currAddress);
                            return currPerson;
                        }
                    }
                }

                conn.Close();
            }

            return null;
        }

        /// <summary>
        ///     Gets the person by identifier.
        /// </summary>
        /// <param name="personIDCheck">The person identifier check.</param>
        /// <returns>a person with the matching personID</returns>
        public CourseCollection GetCoursesByStudentID(int studentIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var coursesTaught = new CourseCollection();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "select courses.* from courses, students, student_has_courses WHERE students.student_id = student_has_courses.student_id AND student_has_courses.courses_CRN = courses.CRN AND students.student_id = @studentID";

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
                            var personID = reader[personIdOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(personIdOrdinal);
                            var fname = reader[fnameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(fnameOrdinal);
                            var minit = reader[minitOrdinal] == DBNull.Value
                                ? default(char)
                                : reader.GetChar(minitOrdinal);
                            var lname = reader[lnameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(lnameOrdinal);
                            var ssn = reader[ssnOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(ssnOrdinal);
                            var gender = reader[genderOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(genderOrdinal);
                            var bdate = reader[bdateOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(bdateOrdinal);
                            var addr1 = reader[addr1Ordinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(addr1Ordinal);
                            var addr2 = reader[addr2Ordinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(addr2Ordinal);
                            var city = reader[cityOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(cityOrdinal);
                            var state = reader[stateOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(stateOrdinal);
                            var zipcode = reader[zipcodeOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(zipcodeOrdinal);
                            var country = reader[countryOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(countryOrdinal);
                            var phoneNumber = reader[phoneOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(phoneOrdinal);
                            Address currAddress = new Address(addr1, addr2, city, state, zipcode, country);
                            Person currPerson = new Person(personID, fname, minit, lname, ssn, gender, phoneNumber,
                                bdate, currAddress);
                            return currPerson;
                        }
                    }
                }

                conn.Close();
            }

            return null;
        }

        #endregion
    }
}
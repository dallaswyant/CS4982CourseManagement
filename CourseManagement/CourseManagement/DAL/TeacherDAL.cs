using System;
using CourseManagement.App_Code;

namespace CourseManagement.DAL
{
    public class TeacherDAL
    {
        #region Methods

        public Teacher GetAllTeachers()
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "select teachers.*, dept_employs_teachers.dept_name FROM teachers, dept_employs_teachers WHERE teachers.teacher_id = dept_employs_teachers.teacher_id";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int officeLocationOrdinal = reader.GetOrdinal("office_location");
                        int nameOrdinal = reader.GetOrdinal("name");
                        int emailOrdinal = reader.GetOrdinal("email");
                        int publicEmailOrdinal = reader.GetOrdinal("public_email");
                        int phoneOrdinal = reader.GetOrdinal("phone_number");
                        int departmentOrdinal = reader.GetOrdinal("gender");
                        int bdateOrdinal = reader.GetOrdinal("bdate");
                        //int phoneOrdinal = reader.GetOrdinal("phonenumber");
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
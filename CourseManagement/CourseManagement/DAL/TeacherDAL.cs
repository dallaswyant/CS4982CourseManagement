using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class TeacherDAL
    {
        public Teacher GetAllTeachers()
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {

                conn.Open();
                var selectQuery = "select teachers.*, dept_employs_teachers.dept_name FROM teachers, dept_employs_teachers WHERE teachers.teacher_id = dept_employs_teachers.teacher_id";

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
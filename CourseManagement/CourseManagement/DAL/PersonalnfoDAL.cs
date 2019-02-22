using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class PersonalnfoDAL
    {
        public PersonalInfo GetPersonalInfoFromUserID(string uid)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT * FROM personal_info WHERE uid = @UID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UID", uid);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int uidOrdinal = reader.GetOrdinal("uid");
                        int fnameOrdinal = reader.GetOrdinal("fname");
                        int minitOrdinal = reader.GetOrdinal("minit");
                        int lnameOrdinal = reader.GetOrdinal("lname");
                        int addrIdOrdinal = reader.GetOrdinal("addr_id");
                        int phoneNumberOrdinal = reader.GetOrdinal("phone_number");
                        int sexOrdinal = reader.GetOrdinal("sex");
                        int dobOrdinal = reader.GetOrdinal("dob");
                        int raceOrdinal = reader.GetOrdinal("race");
                        int emailOrdinal = reader.GetOrdinal("email");
                        int ssnOrdinal = reader.GetOrdinal("SSN");

                        while (reader.Read())
                        {
                            var foundusername = reader[uidOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(uidOrdinal);
                            var foundfname = reader[fnameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(fnameOrdinal);
                            var foundminit = reader[minitOrdinal] == DBNull.Value
                                ? default(char)
                                : reader.GetChar(minitOrdinal);
                            var foundlname = reader[lnameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(lnameOrdinal);
                            var foundAddrId = reader[addrIdOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(addrIdOrdinal);
                            var foundPhone = reader[phoneNumberOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(phoneNumberOrdinal);
                            var foundSex = reader[sexOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(sexOrdinal);
                            var foundDob = reader[dobOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(dobOrdinal);
                            var foundRace = reader[raceOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(raceOrdinal);
                            var foundEmail = reader[emailOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(emailOrdinal);
                            var foundSSN = reader[emailOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(emailOrdinal);

                            var foundInfo = new PersonalInfo(foundusername, foundfname, foundminit, foundlname,foundAddrId,foundPhone,foundSex,foundDob,foundRace,foundEmail,foundSSN);
                            return foundInfo;
                        }
                    }
                }

                conn.Close();
            }

            return null;
        }
    }
}
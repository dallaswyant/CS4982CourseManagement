using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;



namespace CourseManagement.DAL
{
    public class UserDAL
    {
        public User CheckLogin(string username, string password)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {

                conn.Open();
                var selectQuery = "SELECT `users`.`uid`, `users`.password, `roles`.role_name  FROM `users`, `user_has_role`, `roles` WHERE `users`.`uid` = `user_has_role`.user_uid AND `user_has_role`.`roles_role_id` = `roles`.role_id AND users.uid = @username AND users.password = @password";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int unameOrdinal = reader.GetOrdinal("uid");
                        int pwordOrdinal = reader.GetOrdinal("password");
                        int roleOrdinal = reader.GetOrdinal("role_name");

                        while (reader.Read())
                        {
                            string foundusername = reader[unameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(unameOrdinal);
                            string foundpassword = reader[pwordOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(pwordOrdinal);
                            string foundrole = reader[roleOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(roleOrdinal);
                            User foundUser = new User(foundusername, foundpassword, foundrole);
                            return foundUser;
                        }
                    }
                }

                conn.Close();

            }

            return null;
        }
    }
}
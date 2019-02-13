using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.App_Code;
using CourseManagement.DAL;
using CourseManagement.Utilities;
using MySql.Data.MySqlClient;

namespace Forms
{
    class UserDAL
    {
        #region Methods

        /// <summary>
        /// Checks the login of a user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>A user with the selected credentials, if one exists</returns>
        public User CheckLogin(string username, string password)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT `users`.`uid`, `users`.password, `roles`.role_name  FROM `users`, `user_has_role`, `roles` WHERE `users`.`uid` = `user_has_role`.user_uid AND `user_has_role`.`roles_role_id` = `roles`.role_id AND users.uid = @username";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int unameOrdinal = reader.GetOrdinal("uid");
                        int pwordOrdinal = reader.GetOrdinal("password");
                        int roleOrdinal = reader.GetOrdinal("role_name");

                        while (reader.Read())
                        {
                            var foundusername = reader[unameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(unameOrdinal);
                            var foundpassword = reader[pwordOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(pwordOrdinal);
                            var decryptedpassword = Encrypter.Decrypt(foundpassword, "raspberryberet");
                            var foundrole = reader[roleOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(roleOrdinal);
                            var foundUser = new User(foundusername, decryptedpassword, foundrole);
                            return foundUser;
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

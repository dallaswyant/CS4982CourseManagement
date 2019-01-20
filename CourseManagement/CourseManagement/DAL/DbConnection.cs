using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class DbConnection
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns> the connection for the db</returns>
        public static MySqlConnection GetConnection()
        {

            string conStr = "server=160.10.25.16; port=3306; uid=cs4982s19d;" +
                            "pwd=H0KQ5qCgSXKDNdp5; database=cs4982s19d;";

            MySqlConnection conn = new MySqlConnection(conStr);
            return conn;
        }
    }
}
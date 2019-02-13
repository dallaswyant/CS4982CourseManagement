using MySql.Data.MySqlClient;

namespace CourseManagementLibrary.DAL
{
    /// <summary>
    /// Class returns a static instance of the database connection
    /// </summary>
    public class DbConnection
    {
        #region Methods

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns> the connection for the database</returns>
        public static MySqlConnection GetConnection()
        {
            var conStr = "server=160.10.25.16; port=3306; uid=cs4982s19d;" +
                         "pwd=H0KQ5qCgSXKDNdp5; database=cs4982s19d;";

            MySqlConnection conn = new MySqlConnection(conStr);
            return conn;
        }

        #endregion
    }
}
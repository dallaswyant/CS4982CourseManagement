using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManagementDesktop.DAL
{
    class LocalDbConnection
    {
        public static SQLiteConnection GetConnection()
        {
            SQLiteConnection sqLiteConnection = new SQLiteConnection("Data Source=../../Data/MyDatabase.sqlite;Version=3;");
            return sqLiteConnection;
        }
    }
}

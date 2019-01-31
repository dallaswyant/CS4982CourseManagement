using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class DepartmentDAL
    {
        public List<Department> GetAllDepartments()
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "select * FROM departments";
                List<Department> departments = new List<Department>();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int departmentNameOrdinal = reader.GetOrdinal("name");
                        int chairOrdinal = reader.GetOrdinal("chair_uid");

                        while (reader.Read())
                        {
                            var departmentName = reader[departmentNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(departmentNameOrdinal);
                            var chairUID = reader[chairOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(chairOrdinal);
                            TeacherDAL teacherGetter = new TeacherDAL();
                            Teacher chair = teacherGetter.GetTeacherByTeacherID(chairUID);
                            Department dept = new Department(chair, departmentName);
                            departments.Add(dept);
                        }
                        return departments;
                    }
                }

                conn.Close();
            }

            return null;
        }
    }
}
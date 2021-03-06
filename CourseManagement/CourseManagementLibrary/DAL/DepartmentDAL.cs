﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CourseManagementLibrary.Model;
using MySql.Data.MySqlClient;

namespace CourseManagementLibrary.DAL
{
    /// <summary>
    /// This class defined a DepartmentDAL object for interacting with departments on the database
    /// </summary>
    public class DepartmentDAL
    {
        /// <summary>
        /// Gets a list of all departments.
        /// </summary>
        /// <returns>A list of all departments.</returns>
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
            }
        }
    }
}
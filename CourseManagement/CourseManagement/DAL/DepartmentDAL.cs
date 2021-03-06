﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CourseManagement.Models;
using CourseManagements;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
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
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Department> GetAllDepartments()
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();

            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "select * FROM departments";
                List<Department> departments = new List<Department>();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
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
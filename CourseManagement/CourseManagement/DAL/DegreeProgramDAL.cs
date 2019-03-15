using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.Models;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class DegreeProgramDAL
    {
        public List<string> GetAllDegreePrograms()
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();
            List<string> degrees = new List<string>();
            using (dbConnection)
            {

                dbConnection.Open();

                var selectQuery =
                    "select degree_programs.name from degree_programs";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    using (MySqlDataReader queryResultReader = cmd.ExecuteReader())
                    {

                        int nameOrdinal = queryResultReader.GetOrdinal("name");


                        while (queryResultReader.Read())
                        {
                            
                            string degreeName = queryResultReader[nameOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(nameOrdinal);
                           
                            degrees.Add(degreeName);

                        }

                        return degrees;
                    }
                }
            }
        }

        public List<string> GetCourseNamesByDegreeProgram(string degreeProgramName)
        {
            if (degreeProgramName == null)
            {
                throw new Exception("Degree name cannot be null");
            }
            MySqlConnection dbConnection = DbConnection.GetConnection();
            List<string> coursesRequired = new List<string>();
            using (dbConnection)
            {

                dbConnection.Open();

                var selectQuery =
                    "SELECT * FROM degree_requires_courses, degree_programs WHERE degree_programs.degree_id = degree_requires_courses.degree_id AND degree_programs.name = @degree_program_name";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@degree_program_name", degreeProgramName);
                    using (MySqlDataReader queryResultReader = cmd.ExecuteReader())
                    {
                        int nameOrdinal = queryResultReader.GetOrdinal("course_name");

                        while (queryResultReader.Read())
                        {
                            string degreeName = queryResultReader[nameOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(nameOrdinal);
                            
                            coursesRequired.Add(degreeName);

                        }

                        return coursesRequired;
                    }
                }
            }
        }

        public string GetDegreeProgramByStudentID(string studentIDCheck)
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();
            using (dbConnection)
            {

                dbConnection.Open();

                var selectQuery =
                    "SELECT students.degree_name FROM students WHERE students.uid = @studentIDCheck";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@studentIDCheck", studentIDCheck);
                    using (MySqlDataReader queryResultReader = cmd.ExecuteReader())
                    {

                        int nameOrdinal = queryResultReader.GetOrdinal("degree_name");


                        while (queryResultReader.Read())
                        {

                            string degreeName = queryResultReader[nameOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(nameOrdinal);

                            return degreeName;

                        }
                    }
                }
            }

            return null;
        }
    }
}
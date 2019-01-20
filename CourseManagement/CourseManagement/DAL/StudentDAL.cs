using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class StudentDAL
    {

        /// <summary>
        /// Gets the person by identifier.
        /// </summary>
        /// <param name="personIDCheck">The person identifier check.</param>
        /// <returns>a person with the matching personID</returns>
        public Student GetStudentByStudentID(int studentIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {

                conn.Open();
                var selectQuery = "SELECT * from students WHERE students.student_id = @studentID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@studentID", studentIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        int nameOrdinal = reader.GetOrdinal("name");
                        int emailOrdinal = reader.GetOrdinal("email");

                        while (reader.Read())
                        { 

                            string name = reader[nameOrdinal] == DBNull.Value ? default(string) : reader.GetString(nameOrdinal);
                            string email = reader[emailOrdinal] == DBNull.Value ? default(string) : reader.GetString(emailOrdinal);
                            
                            Student newStudent = new Student(name, email);
                            return newStudent;

                        }
                    }
                }
                conn.Close();
            }

            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class StudentDAL
    {
        #region Methods

        /// <summary>
        ///     Gets the person by identifier.
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
                        int studentIDOrdinal = reader.GetOrdinal("student_id");
                        int nameOrdinal = reader.GetOrdinal("name");
                        int emailOrdinal = reader.GetOrdinal("email");

                        while (reader.Read())
                        {
                            var name = reader[nameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(nameOrdinal);
                            var email = reader[emailOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(emailOrdinal);
                            var studentID = reader[studentIDOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(emailOrdinal);

                            var newStudent = new Student(studentID, name, email);
                            return newStudent;
                        }
                    }
                }

                conn.Close();
            }

            return null;
        }

        public List<Student> GetStudentsByCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            List<Student> studentsInCurrentClasses = new List<Student>();
            using (conn)
            {
                conn.Open();
                var selectQuery = "SELECT * from students WHERE students.student_id = @studentID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int nameOrdinal = reader.GetOrdinal("name");
                        int emailOrdinal = reader.GetOrdinal("email");
                        int studentIDOrdinal = reader.GetOrdinal("student_id");

                        while (reader.Read())
                        {
                            var name = reader[nameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(nameOrdinal);
                            var email = reader[emailOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(emailOrdinal);
                            var studentID = reader[studentIDOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(emailOrdinal);

                            var newStudent = new Student(studentID, name, email);
                            studentsInCurrentClasses.Add(newStudent);
                        }

                        return studentsInCurrentClasses;
                    }
                }

                conn.Close();
            }

            return null;
        }

        #endregion
    }
}
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
        public Student GetStudentByStudentID(string studentUIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                conn.Open();
                var selectQuery = "SELECT * from students WHERE students.uid = @studentUID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@studentUID", studentUIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentUIDOrdinal = reader.GetOrdinal("uid");
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
                            var studentUID = reader[studentUIDOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(emailOrdinal);

                            var newStudent = new Student(studentUID, name, email);
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
                var selectQuery = "select students.* from students, student_has_courses WHERE students.uid=student_has_courses.student_uid AND student_has_courses.courses_CRN=@CRNCheck";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int nameOrdinal = reader.GetOrdinal("name");
                        int emailOrdinal = reader.GetOrdinal("email");
                        int studentUIDOrdinal = reader.GetOrdinal("uid");

                        while (reader.Read())
                        {
                            var name = reader[nameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(nameOrdinal);
                            var email = reader[emailOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(emailOrdinal);
                            var studentUID = reader[studentUIDOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(studentUIDOrdinal);

                            var newStudent = new Student(studentUID, name, email);
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
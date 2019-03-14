using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;
using CourseManagement.Models;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class StudentDAL
    {
        #region Methods


        /// <summary>
        /// Gets the student by student id.
        /// </summary>
        /// <param name="studentUIDCheck">The student uid to check.</param>
        /// <returns>A student with the selected studentUID</returns>
        public Student GetStudentByStudentID(string studentUIDCheck)
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = "SELECT * from students WHERE students.uid = @studentUID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@studentUID", studentUIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentUIDOrdinal = reader.GetOrdinal("uid");
                        int emailOrdinal = reader.GetOrdinal("email");
                        

                        while (reader.Read())
                        {
                            var studentUID = reader[studentUIDOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(studentUIDOrdinal);
                            var email = reader[emailOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(emailOrdinal);


                            var newStudent = new Student(studentUID, email);
                            return newStudent;
                        }
                    }
                }

                dbConnection.Close();
            }

            return null;
        }

        /// <summary>
        /// Gets a list of students in the course with the given CRN.
        /// </summary>
        /// <param name="CRNCheck">The CRN to check.</param>
        /// <returns>A list of students in the selected course</returns>
        public List<Student> GetStudentsByCRN(int CRNCheck)
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();
            List<Student> studentsInCurrentClasses = new List<Student>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = "select students.* from students, student_has_courses WHERE students.uid=student_has_courses.student_uid AND student_has_courses.courses_CRN=@CRNCheck";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentUIDOrdinal = reader.GetOrdinal("uid");
                        int emailOrdinal = reader.GetOrdinal("email");


                        while (reader.Read())
                        {
                            var studentUID = reader[studentUIDOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(studentUIDOrdinal);
                            var email = reader[emailOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(emailOrdinal);

                            var newStudent = new Student(studentUID, email);
                            studentsInCurrentClasses.Add(newStudent);
                        }

                        return studentsInCurrentClasses;
                    }
                }
            }
        }
        /// <summary>
        /// Adds the course to the student's courses by CRN and studentUID
        /// </summary>
        /// <param name="CRN">The CRN.</param>
        /// <param name="studentUID">The student uid.</param>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void addCourseByCRNAndStudentUID(int CRN, string studentUID)
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();

            using (dbConnection)
            {
                dbConnection.Open();
                    var selectQuery =
                        "INSERT INTO student_has_courses (student_uid, courses_CRN) VALUES (@studentUID,@CRN)";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                    {
                        cmd.Parameters.AddWithValue("@studentUID", studentUID);
                        cmd.Parameters.AddWithValue("@CRN", CRN);
                        cmd.ExecuteNonQuery();
                    }
                }
                dbConnection.Close();
            

        }

        #endregion
    }
}
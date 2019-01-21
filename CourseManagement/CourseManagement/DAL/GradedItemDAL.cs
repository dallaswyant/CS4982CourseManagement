﻿using System;
using System.Collections.Generic;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class GradedItemDAL
    {
        #region Methods

        

        public List<GradedItem> GetGradedItemsByCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var coursesTaught = new CourseCollection();
            var grades = new List<GradedItem>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT grade_items.* from grade_items, grade_belongs_to_courses WHERE grade_items.grade_item_id = grade_belongs_to_courses.grade_item_id AND grade_belongs_to_courses.courses_CRN = @CRN";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRN", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentIdOrdinal = reader.GetOrdinal("student_uid");
                        int totalPointsOrdinal = reader.GetOrdinal("grade_total_points");
                        int gradeEarnedOrdinal = reader.GetOrdinal("grade_earned_points");
                        int gradeTypeOrdinal = reader.GetOrdinal("grade_type");
                        int gradeNameOrdinal = reader.GetOrdinal("grade_name");
                        int gradeFeedbackOrdinal = reader.GetOrdinal("grade_feedback");
                        int gradeItemIdOrdinal = reader.GetOrdinal("grade_item_id");

                        while (reader.Read())
                        {
                            var studentUID = reader[studentIdOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(studentIdOrdinal);
                            var totalPoints = reader[totalPointsOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(totalPointsOrdinal);
                            var gradeEarned = reader[gradeEarnedOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(gradeEarnedOrdinal);
                            var gradeType = reader[gradeTypeOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeTypeOrdinal);
                            var gradeName = reader[gradeNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeNameOrdinal);
                            var gradeFeedback = reader[gradeFeedbackOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeFeedbackOrdinal);
                            var gradeItemId = reader[gradeItemIdOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(gradeItemIdOrdinal);

                            var currStudent = studentGetter.GetStudentByStudentID(studentUID);
                            
                            var currGradedItem = new GradedItem(gradeName, currStudent, gradeEarned, gradeFeedback, totalPoints,
                                gradeType,gradeItemId);
                            grades.Add(currGradedItem);
                        }

                        return grades;
                    }
                }
            }
        }

        public Dictionary<string, string> GetUniqueGradedItemsByCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var coursesTaught = new CourseCollection();
            var grades = new Dictionary<string, string>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT DISTINCT grade_items.grade_name From grade_items, grade_belongs_to_courses WHERE grade_belongs_to_courses.courses_CRN = @CRN";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRN", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int gradeNameOrdinal = reader.GetOrdinal("grade_name");

                        while (reader.Read())
                        {
                           
                            var gradeName = reader[gradeNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeNameOrdinal);
                            
                            grades.Add(gradeName, gradeName);
                        }

                        return grades;
                    }
                }
            }
        }

        public List<GradedItem> GetGradedItemsByStudentId(string studentId, int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var coursesTaught = new CourseCollection();
            var grades = new List<GradedItem>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT grade_items.* from grade_items, grade_belongs_to_courses WHERE grade_items.grade_item_id = grade_belongs_to_courses.grade_item_id AND grade_belongs_to_courses.courses_CRN = @CRN AND student_uid = @studentId";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRN", CRNCheck);
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentUIDOrdinal = reader.GetOrdinal("student_uid");
                        int totalPointsOrdinal = reader.GetOrdinal("grade_total_points");
                        int gradeEarnedOrdinal = reader.GetOrdinal("grade_earned_points");
                        int gradeTypeOrdinal = reader.GetOrdinal("grade_type");
                        int gradeNameOrdinal = reader.GetOrdinal("grade_name");
                        int gradeFeedbackOrdinal = reader.GetOrdinal("grade_feedback");
                        int gradeItemIdOrdinal = reader.GetOrdinal("grade_item_id");

                        while (reader.Read())
                        {
                            var studentUID = reader[studentUIDOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(studentUIDOrdinal);
                            var totalPoints = reader[totalPointsOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(totalPointsOrdinal);
                            var gradeEarned = reader[gradeEarnedOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(gradeEarnedOrdinal);
                            var gradeType = reader[gradeTypeOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeTypeOrdinal);
                            var gradeName = reader[gradeNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeNameOrdinal);
                            var gradeFeedback = reader[gradeFeedbackOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeFeedbackOrdinal);
                            var gradeItemId = reader[gradeItemIdOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(gradeItemIdOrdinal);

                            var currStudent = studentGetter.GetStudentByStudentID(studentUID);
                            
                            var currGradedItem = new GradedItem(gradeName, currStudent, gradeEarned, gradeFeedback, totalPoints,
                                gradeType,gradeItemId);
                            grades.Add(currGradedItem);
                        }

                        return grades;
                    }
                }
            }
        }



        public void deleteGradedItem(GradedItem gradedItem)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var coursesTaught = new CourseCollection();
            var grades = new List<GradedItem>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "DELETE FROM `grade_items` WHERE grade_item_id = @gradeId";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@gradeId", gradedItem.GradeId);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void InsertNewGradedItemByCRN(GradedItem newItem, int CRN)
        {
            StudentDAL studentGetter = new StudentDAL();
            List<Student> students = studentGetter.GetStudentsByCRN(CRN);
            
                
                MySqlConnection conn = DbConnection.GetConnection();

                using (conn)
                {
                    conn.Open();
                    foreach (var t in students)
                    {
                        GradedItem grade = new GradedItem(newItem.Name, t, 0.0, null, newItem.PossiblePoints, null, 0);
                        var selectQuery =
                            "INSERT INTO grade_items(student_id, grade_total_points, grade_earned_points, grade_type, grade_name, grade_feedback) VALUES (@studentUID,@grade_total,@grade_points,@grade_type,@grade_name,@grade_feedback)";
                        using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@studentUID", newItem.Student.StudentUID);
                            cmd.Parameters.AddWithValue("@grade_total", newItem.Grade);
                            cmd.Parameters.AddWithValue("@grade_points", newItem.PossiblePoints);
                            cmd.Parameters.AddWithValue("@grade_type", newItem.GradeType);
                            cmd.Parameters.AddWithValue("@grade_name", newItem.Name);
                            cmd.Parameters.AddWithValue("@grade_feedback", newItem.Feedback);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }
            
        }

        public void UpdateGradeItem(GradedItem newItem,int CRN)
        {

            MySqlConnection conn = DbConnection.GetConnection();
            string sql = "UPDATE grade_items SET grade_total_points=@grade_total, grade_earned_points=@grade_points, grade_type=@grade_type, grade_name=@grade_name, grade_feedback=@grade_feedback WHERE student_uid = @studentUID";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@studentUID", newItem.Student.StudentUID);
            cmd.Parameters.AddWithValue("@grade_total", newItem.Grade);
            cmd.Parameters.AddWithValue("@grade_points", newItem.PossiblePoints);
            cmd.Parameters.AddWithValue("@grade_type", newItem.GradeType);
            cmd.Parameters.AddWithValue("@grade_name", newItem.Name);
            cmd.Parameters.AddWithValue("@grade_feedback", newItem.Feedback);
            cmd.Parameters.AddWithValue("@grade_item_id", newItem.GradeId);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public List<GradedItem> GetGradedItemsByCRNAndGradeName(int CRNCheck, string gradeName)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var coursesTaught = new CourseCollection();
            var grades = new List<GradedItem>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT * FROM grade_items,grade_belongs_to_courses WHERE grade_items.grade_item_id = grade_belongs_to_courses.grade_item_id AND grade_belongs_to_courses.courses_CRN = @CRNCheck AND grade_name = @grade_name";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    cmd.Parameters.AddWithValue("@grade_name", gradeName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentIdOrdinal = reader.GetOrdinal("student_uid");
                        int totalPointsOrdinal = reader.GetOrdinal("grade_total_points");
                        int gradeEarnedOrdinal = reader.GetOrdinal("grade_earned_points");
                        int gradeTypeOrdinal = reader.GetOrdinal("grade_type");
                        int gradeFeedbackOrdinal = reader.GetOrdinal("grade_feedback");
                        int gradeItemIdOrdinal = reader.GetOrdinal("grade_item_id");

                        while (reader.Read())
                        {
                            var studentUID = reader[studentIdOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(studentIdOrdinal);
                            var totalPoints = reader[totalPointsOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(totalPointsOrdinal);
                            var gradeEarned = reader[gradeEarnedOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(gradeEarnedOrdinal);
                            var gradeType = reader[gradeTypeOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeTypeOrdinal);
                            var gradeFeedback = reader[gradeFeedbackOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeFeedbackOrdinal);
                            var gradeItemId = reader[gradeItemIdOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(gradeItemIdOrdinal);

                            var currStudent = studentGetter.GetStudentByStudentID(studentUID);

                            var currGradedItem = new GradedItem(gradeName, currStudent, gradeEarned, gradeFeedback, totalPoints,
                                gradeType, gradeItemId);
                            grades.Add(currGradedItem);
                        }

                        return grades;
                    }
                }
            }
        }

        #endregion
    }
}
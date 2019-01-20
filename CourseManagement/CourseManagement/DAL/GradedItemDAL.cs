using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class GradedItemDAL
    {
        
        public List<GradedItem> GetGradedItemsByCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            CourseCollection coursesTaught = new CourseCollection();
            List<GradedItem> grades = new List<GradedItem>();
            using (conn)
            {
                conn.Open();
                var selectQuery = "SELECT grade_items.* from grade_items, grade_belongs_to_courses WHERE grade_items.grade_item_id = grade_belongs_to_courses.grade_item_id AND grade_belongs_to_courses.courses_CRN = @CRN";
                StudentDAL studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRN", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentIdOrdinal = reader.GetOrdinal("studentID");
                        int totalPointsOrdinal = reader.GetOrdinal("grade_total_points");
                        int gradeEarnedOrdinal = reader.GetOrdinal("grade_earned_points");
                        int gradeTypeOrdinal = reader.GetOrdinal("grade_type");
                        int gradeNameOrdinal = reader.GetOrdinal("grade_name");
                        int gradeFeedbackOrdinal = reader.GetOrdinal("grade_feedback");

                        while (reader.Read())
                        {
                            int studentID = reader[studentIdOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(studentIdOrdinal);
                            int totalPoints = reader[totalPointsOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(totalPointsOrdinal);
                            int gradeEarned = reader[gradeEarnedOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(gradeEarnedOrdinal);
                            string gradeType = reader[gradeTypeOrdinal] == DBNull.Value ? default(string) : reader.GetString(gradeTypeOrdinal);
                            string gradeName = reader[gradeNameOrdinal] == DBNull.Value ? default(string) : reader.GetString(gradeNameOrdinal);
                            string gradeFeedback = reader[gradeFeedbackOrdinal] == DBNull.Value ? default(string) : reader.GetString(gradeFeedbackOrdinal);
                            
                            Student currStudent = studentGetter.GetStudentByStudentID(studentID);
                            Dictionary<Student,Double> studentGrade = new Dictionary<Student, double>();
                            studentGrade.Add(currStudent,gradeEarned);
                            Dictionary<Student,String> studentFeedback = new Dictionary<Student, string>();
                            studentFeedback.Add(currStudent, gradeFeedback);
                            GradedItem currGradedItem = new GradedItem(gradeName,studentGrade,studentFeedback,totalPoints,gradeType);
                            grades.Add(currGradedItem);
                            

                        }

                        return grades;
                    }
                }   
            }
        }
    }
}
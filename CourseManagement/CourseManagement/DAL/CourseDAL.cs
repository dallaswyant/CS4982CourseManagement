﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    [DataObject(true)]
    public class CourseDAL
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public CourseCollection GetCourseByTeacherID(string teacherIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            CourseCollection coursesTaught = new CourseCollection();
            using (conn)
            {

                conn.Open();
                GradedItemDAL gradedStuff = new GradedItemDAL();
                
                var selectQuery = "select courses.*, rubrics.assignment_types, rubrics.weight_per_type from courses, teacher_teaches_courses, rubrics WHERE teacher_teaches_courses.courses_CRN = courses.CRN AND teacher_teaches_courses.teacher_uid = @teacherUID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@teacherUID", teacherIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");
                        int rubricIDOrdinal = reader.GetOrdinal("rubric_id");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
                            string courseName = reader[courseNameOrdinal] == DBNull.Value ? default(string) : reader.GetString(courseNameOrdinal);
                            string sectionNumber = reader[sectionNumberOrdinal] == DBNull.Value ? default(string) : reader.GetString(sectionNumberOrdinal);
                            int creditHours = reader[creditHoursOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(creditHoursOrdinal);
                            int maxSeats = reader[maxSeatsOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(maxSeatsOrdinal);
                            int rubricID = reader[rubricIDOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(rubricIDOrdinal);
                            string location = reader[locationOrdinal] == DBNull.Value ? default(string) : reader.GetString(locationOrdinal);
                            
                            List<GradedItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);
                            
                            CourseInfo currCourseInfo = new CourseInfo(courseName, currTeacher, location, creditHours, CRN, sectionNumber);
                            Course currentCourse = new Course(listOfGrades, currCourseInfo, maxSeats);
                            coursesTaught.Add(currentCourse);
                            
                        }
                    }
                    return coursesTaught;
                }
            }

            return null;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<CourseInfo> GetCoursesByStudentID(string studentUIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            List<Course> coursesTaken = new List<Course>();
            using (conn)
            {
                GradedItemDAL gradedStuff = new GradedItemDAL();
                conn.Open();
                var selectQuery = "select courses.* from courses, students, student_has_courses WHERE students.uid = student_has_courses.student_uid AND student_has_courses.courses_CRN = courses.CRN AND students.uid = @studentUID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@studentUID", studentUIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
                            string courseName = reader[courseNameOrdinal] == DBNull.Value ? default(string) : reader.GetString(courseNameOrdinal);
                            string sectionNumber = reader[sectionNumberOrdinal] == DBNull.Value ? default(string) : reader.GetString(sectionNumberOrdinal);
                            int creditHours = reader[creditHoursOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(creditHoursOrdinal);
                            int maxSeats = reader[maxSeatsOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(maxSeatsOrdinal);
                            string location = reader[locationOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(locationOrdinal);

                            List<GradedItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);
                            
                            
                            CourseInfo currCourseInfo = new CourseInfo(courseName, location, creditHours, CRN, sectionNumber);
                            Course currentCourse = new Course(listOfGrades, currCourseInfo, maxSeats);
                            coursesTaken.Add(currentCourse);

                        }
                    }
                    List<CourseInfo> courseBulletin = new List<CourseInfo>();
                    foreach (var courses in coursesTaken)
                    {
                        courseBulletin.Add(courses.CourseInfo);
                    }

                    return courseBulletin;
                }
                conn.Close();
            }

            return null;
        }
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AddCourseRubric(Course courseToAdd)
        {
            string assignment_types = "";
            string weight_per_types = "";
            for (int i = 0; i < courseToAdd.CourseRubric.GradeTypeWithWeights.Count; i++)
            {
                if (i == courseToAdd.CourseRubric.GradeTypeWithWeights.Count - 1)
                {
                    assignment_types += courseToAdd.CourseRubric.GradeTypeWithWeights.ElementAt(i).Key;
                    weight_per_types += courseToAdd.CourseRubric.GradeTypeWithWeights.ElementAt(i).Value;
                }
                else
                {
                    assignment_types += courseToAdd.CourseRubric.GradeTypeWithWeights.ElementAt(i).Key + "/";
                    weight_per_types += courseToAdd.CourseRubric.GradeTypeWithWeights.ElementAt(i).Value + "/";
                }
            }
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "INSERT INTO rubrics(assignment_types, weight_per_type) VALUES (@assignment_types,@weight_per_type)";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@rubric_id", courseToAdd.CourseRubric.RubricID);
                    cmd.Parameters.AddWithValue("@assignment_types", assignment_types);
                    cmd.Parameters.AddWithValue("@weight_per_type", weight_per_types);

                    cmd.ExecuteNonQuery();
                }
                selectQuery =
                    "UPDATE courses SET rubric_id = @rubric_id)";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@rubric_id", courseToAdd.CourseRubric.RubricID);
                    cmd.Parameters.AddWithValue("@assignment_types", assignment_types);
                    cmd.Parameters.AddWithValue("@weight_per_type", weight_per_types);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateCourseRubric(Course courseToAdd)
        {
            string assignment_types = "";
            string weight_per_types = "";
            for (int i = 0; i < courseToAdd.CourseRubric.GradeTypeWithWeights.Count; i++)
            {
                if (i == courseToAdd.CourseRubric.GradeTypeWithWeights.Count - 1)
                {
                    assignment_types += courseToAdd.CourseRubric.GradeTypeWithWeights.ElementAt(i).Key;
                    weight_per_types += courseToAdd.CourseRubric.GradeTypeWithWeights.ElementAt(i).Value;
                }
                else
                {
                    assignment_types += courseToAdd.CourseRubric.GradeTypeWithWeights.ElementAt(i).Key + "/";
                    weight_per_types += courseToAdd.CourseRubric.GradeTypeWithWeights.ElementAt(i).Value + "/";
                }
            }
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "UPDATE rubrics SET assignment_types=@assignment_types, weight_per_type=@weight_per_type WHERE rubrics.rubric_id = @rubric_id";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@assignment_types", assignment_types);
                    cmd.Parameters.AddWithValue("@weight_per_type", weight_per_types);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }


    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class CourseDAL
    {

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
                    cmd.Parameters.AddWithValue("@teacherID", teacherIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");
                        int assignmentTypesOrdinal = reader.GetOrdinal("assignment_types");
                        int weightPerTypeOrdinal = reader.GetOrdinal("weight_per_type");
                        int dropDateOrdinal = reader.GetOrdinal("add_drop_deadline");
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
                            string assignmentTypes = reader[assignmentTypesOrdinal] == DBNull.Value ? default(string) : reader.GetString(assignmentTypesOrdinal);
                            string weightPerType = reader[weightPerTypeOrdinal] == DBNull.Value ? default(string) : reader.GetString(weightPerTypeOrdinal);
                            DateTime dropDate = reader[dropDateOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(dropDateOrdinal);
                            Dictionary<string, int> rubricStuff = new Dictionary<string, int>();
                            int assingmentCount = assignmentTypes.Split('/').Length - 1;
                            int weightCount = weightPerType.Split('/').Length - 1;
                            String[] types = new String[assingmentCount];
                            String[] weights = new String[weightCount];
                            if (assignmentTypes != default(string))
                            {
                                types = assignmentTypes.Split('/');
                            }
                            if (weightPerType != default(string))
                            {
                                weights = weightPerType.Split('/');
                            }
                            for (int i = 0; i < types.Length; i++)
                            {
                                rubricStuff.Add(types[i],Convert.ToInt32(weights[i]));
                            }
                            CourseRubric rubric = new CourseRubric(rubricStuff, rubricID);
                            List<GradedItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);
                            TeacherDAL teacherGetter = new TeacherDAL();
                            Teacher currTeacher = teacherGetter.GetAllTeachers();
                            
                            CourseInfo currCourseInfo = new CourseInfo(courseName, currTeacher, location, creditHours, CRN, sectionNumber);
                            StudentDAL studentGetter = new StudentDAL();
                            List<Student> studentsInCourse = studentGetter.GetStudentsByCRN(CRN);
                            Course currentCourse = new Course(listOfGrades, currCourseInfo, dropDate, maxSeats, studentsInCourse);
                            coursesTaught.Add(currentCourse);
                            
                        }
                    }
                    return coursesTaught;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the person by identifier.
        /// </summary>
        /// <param name="personIDCheck">The person identifier check.</param>
        /// <returns>a person with the matching personID</returns>
        public CourseCollection GetCoursesByStudentID(string studentIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            CourseCollection coursesTaken = new CourseCollection();
            using (conn)
            {
                GradedItemDAL gradedStuff = new GradedItemDAL();
                conn.Open();
                var selectQuery = "select courses.* from courses, students, student_has_courses WHERE students.student_id = student_has_courses.student_id AND student_has_courses.courses_CRN = courses.CRN AND students.student_id = @studentID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@studentID", studentIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");
                        int assignmentTypesOrdinal = reader.GetOrdinal("assignment_types");
                        int weightPerTypeOrdinal = reader.GetOrdinal("weight_per_type");
                        int dropDateOrdinal = reader.GetOrdinal("add_drop_deadline");
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
                            string assignmentTypes = reader[assignmentTypesOrdinal] == DBNull.Value ? default(string) : reader.GetString(assignmentTypesOrdinal);
                            string weightPerType = reader[weightPerTypeOrdinal] == DBNull.Value ? default(string) : reader.GetString(weightPerTypeOrdinal);
                            DateTime dropDate = reader[dropDateOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(dropDateOrdinal);
                            Dictionary<string, int> rubricStuff = new Dictionary<string, int>();
                            int assingmentCount = assignmentTypes.Split('/').Length - 1;
                            int weightCount = weightPerType.Split('/').Length - 1;
                            String[] types = new String[assingmentCount];
                            String[] weights = new String[weightCount];
                            if (assignmentTypes != default(string))
                            {
                                types = assignmentTypes.Split('/');
                            }
                            if (weightPerType != default(string))
                            {
                                weights = weightPerType.Split('/');
                            }
                            for (int i = 0; i < types.Length; i++)
                            {
                                rubricStuff.Add(types[i], Convert.ToInt32(weights[i]));
                            }
                            CourseRubric rubric = new CourseRubric(rubricStuff, rubricID);
                            List<GradedItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);
                            TeacherDAL teacherGetter = new TeacherDAL();
                            Teacher currTeacher = teacherGetter.GetAllTeachers();
                            CourseInfo currCourseInfo = new CourseInfo(courseName, currTeacher, location, creditHours, CRN, sectionNumber);
                            StudentDAL studentGetter = new StudentDAL();
                            List<Student> studentsInCourse = studentGetter.GetStudentsByCRN(CRN);
                            Course currentCourse = new Course(listOfGrades, currCourseInfo, dropDate, maxSeats, studentsInCourse);
                            coursesTaken.Add(currentCourse);

                        }
                    }

                    return coursesTaken;
                }
                conn.Close();
            }

            return null;
        }

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
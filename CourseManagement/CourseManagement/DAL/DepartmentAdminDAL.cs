using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class DepartmentAdminDAL
    {
        //Add new course
        //INSERT INTO courses (dept_name, course_name, section_num, credit_hours, seats_max, location) VALUES (@dept_name, @course_name, @section_num, @credit_hours, @seats_max, @location)
        //THEN
        //INSERT INTO dept_offers_courses(dept_name, courses_CRN) VALUES (@department_name, @CRN)
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void InsertNewCourse(Course newCourse)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                    
                    
                    var selectQuery =
                        "INSERT INTO courses (dept_name, course_name, section_num, credit_hours, seats_max, location) VALUES (@dept_name, @course_name, @section_num, @credit_hours, @seats_max, @location)";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@dept_name",newCourse.Department.DeptName);
                        cmd.Parameters.AddWithValue("@course_name", newCourse.CourseInfo.Name);
                        cmd.Parameters.AddWithValue("@section_num", newCourse.CourseInfo.SectionNumber);
                        cmd.Parameters.AddWithValue("@credit_hours", newCourse.CourseInfo.CreditHours);
                        cmd.Parameters.AddWithValue("@seats_max", newCourse.MaxSeats);
                        cmd.Parameters.AddWithValue("@location", newCourse.CourseInfo.Location);
                        //cmd.Parameters.AddWithValue("@semester_id", newCourse.CourseInfo.);
                        cmd.ExecuteNonQuery();
                    }

                    var query =
                        "INSERT INTO dept_offers_courses(dept_name, courses_CRN) VALUES (@department_name, (SELECT courses.CRN FROM courses WHERE dept_name = @dept_name AND course_name = @course_name AND section_num = @section_num))";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@department_name", newCourse.Department.DeptName);
                        cmd.Parameters.AddWithValue("@course_name", newCourse.CourseInfo.Name);
                        cmd.Parameters.AddWithValue("@section_num", newCourse.CourseInfo.SectionNumber);
                        cmd.ExecuteNonQuery();
                    }
                
                conn.Close();
            }

        }

        //Delete course
        //DELETE FROM courses WHERE CRN = @CRN;
        //THEN
        //DELETE FROM dept_offers_courses WHERE dept_name = @dept_name AND courses_CRN = @CRN
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteCourseByDepartmentAndCRN(string departmentName, int CRN)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                conn.Open();
                var selectQuery = "DELETE FROM courses WHERE CRN = @CRN";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRN", CRN);
                    cmd.ExecuteNonQuery();
                }

                selectQuery = "DELETE FROM dept_offers_courses WHERE dept_name = @dept_name AND courses_CRN = @CRN";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRN", CRN);
                    cmd.Parameters.AddWithValue("@dept_name", departmentName);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

        }
        //Edit course
        //UPDATE courses SET (course_name=@course_name, section_num=@section_num, credit_hours=@credit_hours, seats_max=@seats_max, location=@location, semester_id=@semester_id) WHERE CRN = @CRN;
        public void UpdateCourse(Course updateCourse)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                    
                    var selectQuery =
                        "UPDATE courses SET (course_name=@course_name, section_num=@section_num, credit_hours=@credit_hours, seats_max=@seats_max, location=@location) WHERE CRN = @CRN";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@course_name", updateCourse.CourseInfo.Name);
                    cmd.Parameters.AddWithValue("@section_num", updateCourse.CourseInfo.SectionNumber);
                    cmd.Parameters.AddWithValue("@credit_hours", updateCourse.CourseInfo.CreditHours);
                    cmd.Parameters.AddWithValue("@seats_max", updateCourse.MaxSeats);
                    cmd.Parameters.AddWithValue("@location", updateCourse.CourseInfo.Location);
                    cmd.Parameters.AddWithValue("@CRN", updateCourse.CourseInfo.CRN);
                    //cmd.Parameters.AddWithValue("@semester_id", newCourse.CourseInfo.);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        //Assign teacher to course
        //INSERT INTO teacher_teaches_courses (teacher_uid, courses_CRN) VALUES (@teacher_UID, @CRN)
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AssignTeacherToCourse(Teacher teacher, int CRN)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();


                var selectQuery =
                    "INSERT INTO teacher_teaches_courses (teacher_uid, courses_CRN) VALUES (@teacher_UID, @CRN)";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@teacherUID", teacher.TeacherUID);
                    cmd.Parameters.AddWithValue("@CRN", CRN);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

        }
    }
}
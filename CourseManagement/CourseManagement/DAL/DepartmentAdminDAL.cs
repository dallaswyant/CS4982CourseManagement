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
        //INSERT INTO courses (dept_name, course_name, section_num, credit_hours, seats_max, location, semester_id) VALUES (@dept_name, @course_name, @section_num, @credit_hours, @seats_max, @location, @semester_id)
        //THEN
        //INSERT INTO dept_offers_courses(dept_name, courses_CRN) VALUES (@department_name, @CRN)

        //Delete course
        //DELETE FROM courses WHERE CRN = @CRN;
        //THEN
        //DELETE FROM dept_offers_courses WHERE dept_name = @dept_name AND courses_CRN = @CRN
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteCourseByDepartmentANDCRN(string departmentName, int CRN)
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

        //Assign teacher to course
        //INSERT INTO teacher_teaches_courses (teacher_uid, courses_CRN) VALUES (@teacher_UID, @CRN)
    }
}
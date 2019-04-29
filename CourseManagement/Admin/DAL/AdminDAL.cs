using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Admin.Models;
using Admin;
using Admin.Utilities;
using MySql.Data.MySqlClient;

namespace Admin.DAL
{
    /// <summary>
    /// Defines a DepartmentAdminDAL object for communication with the DB
    /// </summary>
    [DataObject(true)]
    public class AdminDAL
    {
        /// <summary>
        /// Updates the course.
        /// </summary>
        /// <param name="course">
        /// The course.
        /// </param>
        /// <preconditions>
        /// Course cannot be null
        /// </preconditions>
        /// <postcondition>
        /// The course has now been updated on the DB
        /// </postcondition>
        public void UpdateCourse(Course course)
        {
            if (course == null)
            {
                throw new Exception("Course cannot be null");
            }
            MySqlConnection dbConnection = DbConnection.GetConnection();

            using (dbConnection)
            {
                dbConnection.Open();
                    
                    var selectQuery =
                        "UPDATE courses SET course_name=@course_name, section_num=@section_num, " +
                        "course_desc = @course_description, seats_max=@seats_max, location=@location, " +
                        "semester_name = @semester_name, dept_name=@department, course_time_id = @course_time_id" +
                        " WHERE CRN = @CRN";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@course_name", course.Name);
                    cmd.Parameters.AddWithValue("@section_num", course.SectionNumber);
                    cmd.Parameters.AddWithValue("@course_description", course.Description);
                    cmd.Parameters.AddWithValue("@seats_max", course.MaxSeats);
                    cmd.Parameters.AddWithValue("@location", course.Location);
                    cmd.Parameters.AddWithValue("@CRN", course.CRN);
                    cmd.Parameters.AddWithValue("@semester_name", course.SemesterID);
                    cmd.Parameters.AddWithValue("@department", course.DepartmentName);
                    cmd.Parameters.AddWithValue("@course_time_id", course.CourseTimeID);
                    cmd.ExecuteNonQuery();
                }

                dbConnection.Close();
            }
        }

        /// <summary>
        /// Creates an address
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>The ID of the address that was inserted</returns>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public long CreateAddress(Address address)
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();
                long id = -1;

                var selectQuery =
                    "INSERT INTO `addresses` (`addr1`, `addr2`, `zip`, `city`, `state`, `country`) " +
                    "VALUES (@addressLine1, @addressLine2, @zip, @city, @state, @country)";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@addressLine1", address.AddressLine1);
                    cmd.Parameters.AddWithValue("@addressLine2", address.AddressLine2);
                    cmd.Parameters.AddWithValue("@zip", address.Zip);
                    cmd.Parameters.AddWithValue("@city", address.City);
                    cmd.Parameters.AddWithValue("@state", address.State);
                    cmd.Parameters.AddWithValue("@country", address.Country);
                    cmd.ExecuteNonQuery();
                    id = cmd.LastInsertedId;
                }
                dbConnection.Close();
                return id;
            }

        }

        public void CreateStudent(PersonalStuff info, string studentID, string degreeProgram)
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();
            var safePassword = Encrypter.Encrypt("password", "raspberryberet");
            using (dbConnection)
            {
                dbConnection.Open();
                createUser(studentID, dbConnection, safePassword);
                createStudent(info, studentID, degreeProgram, dbConnection);
                storeInfo(info, studentID, dbConnection);
                dbConnection.Close();
            }
        }

        public void CreateTeacher(PersonalStuff info, Teacher teacher, string department, string officeHours)
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();
            var safePassword = Encrypter.Encrypt("password", "raspberryberet");
            dbConnection.Open();
            using (dbConnection)
            {
                try
                {
                    createUser(teacher.TeacherUID, dbConnection, safePassword);
                    createTeacher(info, teacher, department, dbConnection, officeHours);
                    storeInfo(info, teacher.TeacherUID, dbConnection);
                }
                catch (Exception)
                {

                }
                

                dbConnection.Close();
            }
        }

        private void createTeacher(PersonalStuff info, Teacher teacher, string department, MySqlConnection dbConnection, string officeHours)
        {
            string selectQuery;
            selectQuery =
                "INSERT INTO `teachers` (`uid`, `email`, `public_email`, `phone_number`, `office_location`, `office_hours`) " +
                "VALUES (@user_id, @email, @isEmailPublic, @phone, @location, @hours)";
            using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
            {
                cmd.Parameters.AddWithValue("@user_id", teacher.TeacherUID);
                cmd.Parameters.AddWithValue("@email", info.Email);
                cmd.Parameters.AddWithValue("@isEmailPublic", teacher.IsEmailPublic);
                cmd.Parameters.AddWithValue("@phone", info.PhoneNumber);
                cmd.Parameters.AddWithValue("@location", teacher.Location);
                cmd.Parameters.AddWithValue("@hours", officeHours);
                cmd.ExecuteNonQuery();
            }
            selectQuery =
                "INSERT INTO `dept_employs_teachers` (`dept_name`, `teacher_uid`) " +
                "VALUES (@department, @user_id)";
            using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
            {
                cmd.Parameters.AddWithValue("@user_id", teacher.TeacherUID);
                cmd.Parameters.AddWithValue("@department", department);
                cmd.ExecuteNonQuery();
            }
        }

        private static void storeInfo(PersonalStuff info, string studentID, MySqlConnection dbConnection)
        {
            string selectQuery;
            string dateFormat = "yyyy-MM-dd HH:mm:ss";
            selectQuery =
                "INSERT INTO `personal_info` (`uid`, `fname`, `minit`, `lname`, " +
                "`addr_id`, `phone_number`, `sex`, `dob`, `race`, `email`, `SSN`) " +
                "VALUES (@user_id, @fname, @middleInit, @lname, @addressID, " +
                "@phone, @sex, @dob, @race, @email, @ssn)";
            using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
            {
                cmd.Parameters.AddWithValue("@user_id", studentID);
                cmd.Parameters.AddWithValue("@fname", info.FName);
                cmd.Parameters.AddWithValue("@middleInit", info.Minit);
                cmd.Parameters.AddWithValue("@lname", info.LName);

                cmd.Parameters.AddWithValue("@addressID", info.AddrID);
                cmd.Parameters.AddWithValue("@phone", info.PhoneNumber);
                cmd.Parameters.AddWithValue("@sex", info.Sex);
                cmd.Parameters.AddWithValue("@dob", info.DOB.ToString(dateFormat));
                cmd.Parameters.AddWithValue("@race", info.Race);
                cmd.Parameters.AddWithValue("@email", info.Email);
                cmd.Parameters.AddWithValue("@ssn", info.SSN);
                cmd.ExecuteNonQuery();
            }
        }

        private static void createStudent(PersonalStuff info, string studentID, string degreeProgram,
            MySqlConnection dbConnection)
        {
            string selectQuery;
            selectQuery =
                "INSERT INTO `students` (`uid`, `email`, `degree_name`) " +
                "VALUES (@user_id, @email, @degreeName)";
            using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
            {
                cmd.Parameters.AddWithValue("@user_id", studentID);
                cmd.Parameters.AddWithValue("@email", info.Email);
                cmd.Parameters.AddWithValue("@degreeName", degreeProgram);
                cmd.ExecuteNonQuery();
            }
        }

        private static void createUser(string id, MySqlConnection dbConnection, string safePassword)
        {
            var selectQuery =
                "INSERT INTO `users` (`uid`, `password`) " +
                "VALUES (@user_id, @password)";
            using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
            {
                cmd.Parameters.AddWithValue("@user_id", id);
                cmd.Parameters.AddWithValue("@password", safePassword);
                cmd.ExecuteNonQuery();
            }
        }

        public void CreateSemester(Semester semester)
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();
                string selectQuery =
                    "INSERT INTO `semesters` (`semester_name`, `start_date`, `end_date`, `final_grade_deadline`, `add_drop_deadline`) " +
                    "VALUES (@semesterName, @startDate, @endDate, @finalGradeDeadline, @addDropDeadline)";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@semesterName", semester.SemesterID);
                    cmd.Parameters.AddWithValue("@startDate", semester.StartDate);
                    cmd.Parameters.AddWithValue("@endDate", semester.EndDate);
                    cmd.Parameters.AddWithValue("@finalGradeDeadline", semester.FinalGradeDeadline);
                    cmd.Parameters.AddWithValue("@addDropDeadline", semester.AddDropDeadline);
                    cmd.ExecuteNonQuery();
                }
                dbConnection.Close();
            }
        }
    }
}
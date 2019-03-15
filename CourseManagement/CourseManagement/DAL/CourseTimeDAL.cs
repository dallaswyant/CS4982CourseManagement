using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.Models;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class CourseTimeDAL
    {
        public List<CourseTime> GetAllCourseTimes()
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();
            List<CourseTime> times = new List<CourseTime>();
            using (dbConnection)
            {

                dbConnection.Open();

                var selectQuery =
                    "select * from course_times";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    using (MySqlDataReader queryResultReader = cmd.ExecuteReader())
                    {

                        int courseTimeOrdinal = queryResultReader.GetOrdinal("course_time_id");
                        int courseStartOrdinal = queryResultReader.GetOrdinal("course_start");
                        int courseEndOrdinal = queryResultReader.GetOrdinal("course_end");
                        int courseDaysOrdinal = queryResultReader.GetOrdinal("course_days");


                        while (queryResultReader.Read())
                        {
                            int courseTimeID = queryResultReader[courseTimeOrdinal] == DBNull.Value
                                ? default(int)
                                : queryResultReader.GetInt32(courseTimeOrdinal);
                            DateTime courseStart = queryResultReader[courseStartOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : queryResultReader.GetDateTime(courseStartOrdinal);
                            DateTime courseEnd = queryResultReader[courseEndOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : queryResultReader.GetDateTime(courseEndOrdinal);
                            string courseDays = queryResultReader[courseDaysOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(courseDaysOrdinal);
                            CourseTime currentTime = new CourseTime(courseTimeID, courseStart, courseEnd, courseDays);
                            times.Add(currentTime);

                        }

                        return times;
                    }
                }
            }
        }

        //
        public CourseTime GetCourseTimeByCRN(int CRN)
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();
            List<CourseTime> times = new List<CourseTime>();
            using (dbConnection)
            {

                dbConnection.Open();

                var selectQuery =
                    "SELECT course_times.* FROM courses, course_times WHERE courses.course_time_id = course_times.course_time_id AND courses.CRN = @CRN";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@CRN", CRN);
                    using (MySqlDataReader queryResultReader = cmd.ExecuteReader())
                    {

                        int courseTimeOrdinal = queryResultReader.GetOrdinal("course_time_id");
                        int courseStartOrdinal = queryResultReader.GetOrdinal("course_start");
                        int courseEndOrdinal = queryResultReader.GetOrdinal("course_end");
                        int courseDaysOrdinal = queryResultReader.GetOrdinal("course_days");


                        while (queryResultReader.Read())
                        {
                            int courseTimeID = queryResultReader[courseTimeOrdinal] == DBNull.Value
                                ? default(int)
                                : queryResultReader.GetInt32(courseTimeOrdinal);
                            DateTime courseStart = queryResultReader[courseStartOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : queryResultReader.GetDateTime(courseStartOrdinal);
                            DateTime courseEnd = queryResultReader[courseEndOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : queryResultReader.GetDateTime(courseEndOrdinal);
                            string courseDays = queryResultReader[courseDaysOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(courseDaysOrdinal);
                            CourseTime currentTime = new CourseTime(courseTimeID, courseStart, courseEnd, courseDays);
                            return currentTime;
                        }




                    }

                    return null;
                }
            }
        }
    }
}
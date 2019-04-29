using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Admin.DAL;
using CourseManagement.Models;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    [DataObject(true)]
    public class CourseTimeDAL
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
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
    }
}
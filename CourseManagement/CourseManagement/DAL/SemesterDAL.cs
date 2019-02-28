using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    [DataObject(true)]
    public class SemesterDAL
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Semester> GetAllSemesters()
        {
            MySqlConnection conn = DbConnection.GetConnection();
            List<Semester> allSemesters = new List<Semester>();
            using (conn)
            {
                conn.Open();
                var selectQuery = "SELECT * FROM semesters ";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int semesterNameOrdinal = reader.GetOrdinal("semester_name");
                        int startDateOrdinal = reader.GetOrdinal("start_date");
                        int endDateOrdinal = reader.GetOrdinal("end_date");
                        int withdrawDeadlineOrdinal = reader.GetOrdinal("withdraw_deadline");
                        int addDropDeadlineOrdinal = reader.GetOrdinal("add_drop_deadline");

                        while (reader.Read())
                        {
                            string semesterName = reader[semesterNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(semesterNameOrdinal);
                            DateTime startDate = reader[startDateOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(startDateOrdinal);
                            DateTime endDate = reader[endDateOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(endDateOrdinal);
                            DateTime withdrawDeadline = reader[withdrawDeadlineOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(withdrawDeadlineOrdinal);
                            DateTime addDropDeadline = reader[addDropDeadlineOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(addDropDeadlineOrdinal);

                            Semester current = new Semester(semesterName, addDropDeadline, withdrawDeadline, startDate, endDate);
                            allSemesters.Add(current);
                        }

                        return allSemesters;
                    }
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public Semester GetSemesterBySemesterName(string semesterName)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                conn.Open();
                var selectQuery = "SELECT * FROM semesters WHERE semesters.semester_name = @semester_name";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@semester_name", semesterName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int semesterNameOrdinal = reader.GetOrdinal("semester_name");
                        int startDateOrdinal = reader.GetOrdinal("start_date");
                        int endDateOrdinal = reader.GetOrdinal("end_date");
                        int withdrawDeadlineOrdinal = reader.GetOrdinal("withdraw_deadline");
                        int addDropDeadlineOrdinal = reader.GetOrdinal("add_drop_deadline");

                        while (reader.Read())
                        {
                            semesterName = reader[semesterNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(semesterNameOrdinal);
                            DateTime startDate = reader[startDateOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(startDateOrdinal);
                            DateTime endDate = reader[endDateOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(endDateOrdinal);
                            DateTime withdrawDeadline = reader[withdrawDeadlineOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(withdrawDeadlineOrdinal);
                            DateTime addDropDeadline = reader[addDropDeadlineOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(addDropDeadlineOrdinal);

                            Semester current = new Semester(semesterName,addDropDeadline,withdrawDeadline,startDate,endDate);
                            return current;
                        }
                    }
                }
                conn.Close();
            }
            return null;
        }

        public bool CheckIfSemesterIsCompleted(string semesterID)
        {
            Semester current = this.GetSemesterBySemesterName(semesterID);
            return DateTime.Now < current.EndDate;
        }
    }
}
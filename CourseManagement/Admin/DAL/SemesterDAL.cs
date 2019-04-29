using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Admin.Models;
using MySql.Data.MySqlClient;

namespace Admin.DAL
{
    [DataObject(true)]
    public class SemesterDAL
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Semester> GetAllSemesters()
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();
            List<Semester> allSemesters = new List<Semester>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = "SELECT * FROM semesters ";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int semesterNameOrdinal = reader.GetOrdinal("semester_name");
                        int startDateOrdinal = reader.GetOrdinal("start_date");
                        int endDateOrdinal = reader.GetOrdinal("end_date");
                        int finalGradeDeadlineOrdinal = reader.GetOrdinal("final_grade_deadline");
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
                            DateTime finalGradeDeadline = reader[finalGradeDeadlineOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(finalGradeDeadlineOrdinal);
                            DateTime addDropDeadline = reader[addDropDeadlineOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(addDropDeadlineOrdinal);

                            Semester current = new Semester(semesterName, addDropDeadline, finalGradeDeadline, startDate, endDate);
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
            MySqlConnection dbConnection = DbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = "SELECT * FROM semesters WHERE semesters.semester_name = @semester_name";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
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
                dbConnection.Close();
            }
            return null;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Semester> GetCurrentAndFutureSemesters()
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();
            List<Semester> semesters = new List<Semester>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = "SELECT * FROM semesters WHERE semesters.end_date >= @today";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@today", DateTime.Now.ToString("yyyy-MM-dd"));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int semesterNameOrdinal = reader.GetOrdinal("semester_name");
                        int startDateOrdinal = reader.GetOrdinal("start_date");
                        int endDateOrdinal = reader.GetOrdinal("end_date");
                        int withdrawDeadlineOrdinal = reader.GetOrdinal("withdraw_deadline");
                        int addDropDeadlineOrdinal = reader.GetOrdinal("add_drop_deadline");

                        while (reader.Read())
                        {
                            var semesterName = reader[semesterNameOrdinal] == DBNull.Value
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
                            semesters.Add(current);
                        }

                        return semesters;
                    }
                }
                dbConnection.Close();
            }
            return null;
        }

        public bool CheckIfSemesterIsCompleted(string semesterID)
        {
            Semester current = this.GetSemesterBySemesterName(semesterID);
            return DateTime.Now <= current.EndDate;
        }

        public bool CheckIfAddDropHasPassed(string semesterID)
        {
            Semester current = this.GetSemesterBySemesterName(semesterID);
            return DateTime.Now <= current.AddDropDeadline;
        }

        public bool CheckIfWithdrawHasPassed(string semesterID)
        {
            Semester current = this.GetSemesterBySemesterName(semesterID);
            return DateTime.Now <= current.FinalGradeDeadline;
        }
    }
}
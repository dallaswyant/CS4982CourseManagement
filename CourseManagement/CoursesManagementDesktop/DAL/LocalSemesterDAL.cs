using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using CourseManagement.Models;
using CoursesManagementDesktop.DAL;
using MySql.Data.MySqlClient;

namespace CourseManagementDesktop.DAL
{
    [DataObject(true)]
    public class LocalSemesterDAL
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Semester> GetAllSemesters()
        {
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            List<Semester> allSemesters = new List<Semester>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = "SELECT * FROM semesters ";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
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
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = "SELECT * FROM semesters WHERE semesters.semester_name = @semester_name";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@semester_name", semesterName);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        int semesterNameOrdinal = reader.GetOrdinal("semester_name");
                        int startDateOrdinal = reader.GetOrdinal("start_date");
                        int endDateOrdinal = reader.GetOrdinal("end_date");
                        int finalGradeDeadlineOrdinal = reader.GetOrdinal("final_grade_deadline");
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
                            DateTime finalGradeDeadline = reader[finalGradeDeadlineOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(finalGradeDeadlineOrdinal);
                            DateTime addDropDeadline = reader[addDropDeadlineOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(addDropDeadlineOrdinal);

                            Semester current = new Semester(semesterName,addDropDeadline,finalGradeDeadline,startDate,endDate);
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
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            List<Semester> semesters = new List<Semester>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = "SELECT * FROM semesters WHERE semesters.end_date >= @today";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@today", DateTime.Now.ToString("yyyy-MM-dd"));
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        int semesterNameOrdinal = reader.GetOrdinal("semester_name");
                        int startDateOrdinal = reader.GetOrdinal("start_date");
                        int endDateOrdinal = reader.GetOrdinal("end_date");
                        int finalGradeDeadlineOrdinal = reader.GetOrdinal("final_grade_deadline");
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
                            DateTime finalGradeDeadline = reader[finalGradeDeadlineOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(finalGradeDeadlineOrdinal);
                            DateTime addDropDeadline = reader[addDropDeadlineOrdinal] == DBNull.Value
                                ? default(DateTime)
                                : reader.GetDateTime(addDropDeadlineOrdinal);

                            Semester current = new Semester(semesterName, addDropDeadline, finalGradeDeadline, startDate, endDate);
                            semesters.Add(current);
                        }

                        return semesters;
                    }
                }
            }
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

        public List<Semester> GetTermsInProgress()
        {
            List<Semester> semesters = GetCurrentAndFutureSemesters();
            List<Semester> semestersInProgress = new List<Semester>();
            DateTime today = DateTime.Now;
            foreach (var semester in semesters)
            {
                if (semester.StartDate < today && semester.EndDate > today)
                {
                    semestersInProgress.Add(semester);
                }
            }

            return semestersInProgress;
        }
    }
}
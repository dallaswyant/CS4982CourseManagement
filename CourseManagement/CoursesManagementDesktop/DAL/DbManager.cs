using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.Models;
using CourseManagementDesktop.DAL;

namespace CoursesManagementDesktop.DAL
{
    class DbManager
    {
        #region StudentDALs

        public List<Student> GetStudentsByCRN(int CRNCheck)
        {
            //if(isConnected){
            //  UserDAL userChecker = new UserDAL();
            //  User current = userChecker.CheckLogin();
            //  return current;
            //} else {
            //TODO add query to sql script
            LocalStudentDAL studentGetter = new LocalStudentDAL();
            List<Student> currentStudents = studentGetter.GetStudentsByCRN(CRNCheck);
            return currentStudents;
            //}
        }

        #endregion

        #region GradeItemDALs

        #endregion

        #region CourseDALs

        #endregion

        #region SemesterDALs

        #endregion

        #region RubricDALs

        #endregion

        #region UserDALs

        public User CheckLogin(string username, string password)
        {
            //if(isConnected){
            //  UserDAL userChecker = new UserDAL();
            //  User current = userChecker.CheckLogin();
            //  return current;
            //} else {
            LocalUserDAL userChecker = new LocalUserDAL();
            User current = userChecker.CheckLogin(username, password);
            return current;
            //}
        }

        #endregion
    }
}

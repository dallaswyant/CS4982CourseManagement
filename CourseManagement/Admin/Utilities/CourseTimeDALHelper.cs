using System.Collections.Generic;
using CourseManagement.DAL;
using CourseManagement.Models;

namespace Admin.Utilities
{
    public static class CourseTimeDALHelper
    {
        public static  List<CourseTime> GetCourseTimes()
        {
            CourseTimeDAL dal = new CourseTimeDAL();
            return dal.GetAllCourseTimes();
        }
    }
}
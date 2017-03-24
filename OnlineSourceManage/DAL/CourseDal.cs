using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle;

namespace DAL
{
    public class CourseDal
    {
        #region 查询课程

        /// <summary>
        /// 查询所有的课程
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCourse(bool isDesc)
        {
            var sql = isDesc
                ? "SELECT * FROM dbo.Course WHERE isDel = '0' ORDER BY cId DESC"
                : "SELECT * FROM dbo.Course WHERE isDel = '0'";
            DataTable dt = SqlHelper.SqlDataTable(sql);
            return dt;
        }

        /// <summary>
        /// 根据类型查询课程
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public DataTable GetCouseByTypes(string types)
        {
            string sql = string.Format("SELECT * FROM dbo.Course WHERE types ='{0}' AND isDel ='0'", types);
            DataTable dt = SqlHelper.SqlDataTable(sql);
            return dt;
        }

        /// <summary>
        /// 根据Id查找课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetCourseById(int id)
        {
            string sql = string.Format("SELECT * FROM [dbo].[Course] WHERE cId = '{0}' AND  isDel = '0'", id);
            return SqlHelper.SqlDataTable(sql);
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public DataTable GetCourseBySearch(string keys)
        {
            string sql = string.Format("SELECT * FROM dbo.Course WHERE cName LIKE '%{0}%' OR types LIKE '%{0}%' OR levelNum LIKE '%{0}%' AND isDel ='0'", keys);
            return SqlHelper.SqlDataTable(sql);

        }

        /// <summary>
        /// 根据不同方向或者难度统计课程
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public DataTable GetCourseByTypesOrLevel(string t) 
        {
            var sql = string.Format("SELECT {0} ,count(0) AS num FROM [dbo].[Course] WHERE isDel = '0' GROUP BY {0} ", t);
            return SqlHelper.SqlDataTable(sql);

        }

        #endregion


        #region 添加课程

        /// <summary>
        /// 添加课程
        /// </summary>
        public bool AddCourse(Course course)
        {
            string sql = string.Format("INSERT dbo.Course( cName ,levelNum   ,mark ,types) VALUES  ( N'{0}' ,{1} , N'{2}' ,  N'{3}')", course.cName, course.levelNum, course.mark, course.types);

            int count = SqlHelper.ExcuteNonQuery(sql);
            return count > 0;


        }

        #endregion

        #region 编辑课程

        public bool EditCourse(Course course)
        {
            var sql = string.Format("update [dbo].[Course] set cName = '{0}' , levelNum = '{1}' , types = '{2}' , mark = '{3}'  where cId = '{4}'", course.cName, course.levelNum, course.types, course.mark, course.cId);
            return SqlHelper.ExcuteNonQuery(sql) > 0;
        }
        #endregion

        #region 删除课程

        public bool DeleteCourse(int id)
        {
            string sql = String.Format("update [dbo].[Course] set isDel = 1 where cId = '{0}'", id);
            return SqlHelper.ExcuteNonQuery(sql) > 0;
        }
        #endregion

    }
}

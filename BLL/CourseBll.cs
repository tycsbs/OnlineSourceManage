using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Modle;

namespace BLL
{
    public class CourseBll
    {
        private readonly CourseDal _dal = new CourseDal();

        #region 查找课程

        /// <summary>
        /// 升序或者降序查找所有课程 默认降序
        /// </summary>
        /// <param name="isdesc"></param>
        /// <returns></returns>
        public List<Course> GetAllCourse(bool isdesc)
        {

            DataTable dt = _dal.GetAllCourse(isdesc);
            return (from DataRow dataRow in dt.Rows select DataRowToModel(dataRow)).ToList();
        }

        /// <summary>
        /// 根据类型查询课程
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public List<Course> GetCouseByTypes(string types)
        {
            DataTable dt = _dal.GetCouseByTypes(types);
            return (from DataRow dr in dt.Rows select DataRowToModel(dr)).ToList();
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<Course> GetCourseBySearch(string keys)
        {
            if (keys == "中级") keys = "1";
            if (keys == "初级") keys = "0";
            if (keys == "高级") keys = "2";

            DataTable dt = _dal.GetCourseBySearch(keys);
            return (from DataRow dataRow in dt.Rows select DataRowToModel(dataRow)).ToList();

        }

        /// <summary>
        /// 根据Id查找课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Course GetCourseById(int id)
        {
            DataTable dt = _dal.GetCourseById(id);
            DataRow dataRow = dt.Rows[0];
            return DataRowToModel(dataRow);
        }

        /// <summary>
        /// 根据不同方向或者难度统计课程 types / levelNum
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public DataTable GetCourseByTypesOrLevel(string t)
        {
            return _dal.GetCourseByTypesOrLevel(t);

        }
        #endregion

        #region 添加课程

        /// <summary>
        /// 添加课程
        /// </summary>
        public bool AddCourse(Course course)
        {
            return _dal.AddCourse(course);
        }

        #endregion

        #region 编辑课程
        /// <summary>
        /// 编辑课程
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public bool EditCourse(Course course)
        {
            return _dal.EditCourse(course);
        }
        #endregion

        #region 删除课程
        /// <summary>
        /// 删除课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteCourse(int id)
        {
            return _dal.DeleteCourse(id);
        }
        #endregion

        #region 关系转对象
        
       
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Course DataRowToModel(DataRow row)
        {
            Course model = new Course();
            if (row != null)
            {
                if (row["cId"] != null && row["cId"].ToString() != "")
                {
                    model.cId = int.Parse(row["cId"].ToString());
                }
                if (row["cName"] != null)
                {
                    model.cName = row["cName"].ToString();
                }
                if (row["levelNum"] != null && row["levelNum"].ToString() != "")
                {
                    model.levelNum = int.Parse(row["levelNum"].ToString());
                }
                if (row["starttime"] != null && row["starttime"].ToString() != "")
                {
                    model.startTime = row["starttime"].ToString();
                }
                if (row["isDel"] != null && row["isDel"].ToString() != "")
                {
                    model.isDel = int.Parse(row["isDel"].ToString());
                }
                if (row["mark"] != null)
                {
                    model.mark = row["mark"].ToString();
                }
                if (row["types"] != null)
                {
                    model.types = row["types"].ToString();
                }
            }
            return model;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BLL;
using Modle;

namespace OnlineSourceManage.Controllers
{
    public class CourseController : Controller
    {
        //
        // GET: /Course/
        private readonly CourseBll _bll = new CourseBll();

        public ActionResult Course()
        {
            return View();
        }

        /// <summary>
        /// 获取所有课程的信息 默认倒叙
        /// </summary>
        /// <param name="isdesc"></param>
        /// <returns></returns>
        public ActionResult GetAllCourse()
        {
            var isdesc = Convert.ToBoolean(Request["isdesc"]);
            List<Course> courses = _bll.GetAllCourse(isdesc);

            var pageIndex = int.Parse(Request["page"]); //当前页  
            var pageSize = int.Parse(Request["rows"]); //页面行数 
            IEnumerable<Course> courseList = courses.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return Json(new {total = courses.Count, rows = courseList}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 模糊查询课程 名称、难度、方向
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public ActionResult GetCourseBySearch(string keys)
        {
            List<Course> courses = _bll.GetCourseBySearch(keys);
            return Json(new {total = courses.Count, rows = courses}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 图表信息
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public ActionResult GetCourseForChart(string keys)
        {
            DataTable dt = _bll.GetCourseByTypesOrLevel(keys);
            List<int> intlist = new List<int>();
            List<string> strList = new List<string>();

            foreach (DataRow dataRow in dt.Rows)
            {
                intlist.Add(Convert.ToInt32(dataRow[1]));
                if (keys == "levelNum")
                {
                    string[] mapStrings = {"初级", "中级", "高级"};
                    strList.Add(mapStrings[Convert.ToInt32(dataRow[0])]);
                }
                else
                {
                    strList.Add(dataRow[0] as string);
                }
            }

            return Json(new {name = strList, num = intlist}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteCourse(int id)
        {
            bool isok = _bll.DeleteCourse(id);
            return Content(isok ? "ok" : "error");
        }

        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="cName"></param>
        /// <param name="types"></param>
        /// <param name="levelNum"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        public ActionResult AddCourse(string cName, string types, int levelNum, string mark)
        {
            var course = new Course
            {
                cName = cName,
                levelNum = levelNum,
                mark = mark,
                types = types
            };
            bool result = _bll.AddCourse(course);
            return Content(result ? "ok" : "error");
        }

        /// <summary>
        /// 根据ID获取课程的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetCourseById(int id)
        {
            var course = _bll.GetCourseById(id);
            return Json(new {rows = course}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cName"></param>
        /// <param name="types"></param>
        /// <param name="mark"></param>
        /// <param name="levelNum"></param>
        /// <returns></returns>
        public ActionResult EditCourse(int cId, string cName, string types, string mark, int levelNum)
        {
            var course = new Course
            {
                cId = cId,
                types = types,
                mark = mark,
                levelNum = levelNum,
                cName = cName
            };
            bool result = _bll.EditCourse(course);
            return Content(result ? "ok" : "error");
        }
    }
}
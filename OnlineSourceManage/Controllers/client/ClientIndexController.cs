using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Modle;

namespace OnlineSourceManage.Controllers.client
{
    public class ClientIndexController : Controller
    {
        //
        // GET: /ClientIndex/
        private readonly CourseBll _bll = new CourseBll();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCourseToNav()
        {
            DataTable dt = _bll.GetCourseByTypesOrLevel("types");
            List<int> ints = new List<int>();
            List<string> strList = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                strList.Add(row[0].ToString());
                ints.Add(Convert.ToInt32(row[1]));
            }
            return Json(new {types = strList, numlist = ints},JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有课程信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDefaultCart()
        {
           List<Course> list = _bll.GetAllCourse(true);
            return Json(new {total = list.Count, rows = list}, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查找不同类型的课程
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public ActionResult GetCartByTypes(string types)
        {
            List<Course> list = _bll.GetCouseByTypes(types);
            return Json(new { total = list.Count, rows = list }, JsonRequestBehavior.AllowGet);
        }





    }
}

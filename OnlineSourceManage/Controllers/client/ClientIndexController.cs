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

        public ActionResult RegisterPage()
        {
            return View("Register");
        }

        public ActionResult Register(string uName, string pwd, int sex, string home)
        {
            UsersBll bll = new UsersBll();
            Users user = new Users();
            user.uName = uName;
            user.pwd = pwd;
            user.sex = sex;
            user.home = home;
            user.role = 0;
            bool result = bll.AddUser(user);
            return Content(result ? "ok" : "err");
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ActionResult UserLogin(string name,string pwd)
        {
            
            UsersBll bll = new UsersBll();
            List<Users> user  = bll.UserLogin(name, pwd);//默认不存在
            Session["UserId"] = user[0].uId;
            Session["UserName"] = user[0].uName;
            
            return Json(new {total=user.Count, row=user}, JsonRequestBehavior.AllowGet);
        }







    }
}

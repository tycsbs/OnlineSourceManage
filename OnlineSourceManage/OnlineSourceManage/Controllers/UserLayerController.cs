using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Modle;

namespace OnlineSourceManage.Controllers
{
    public class UserLayerController : Controller
    {
        //
        // GET: /UserLayer/

        public ActionResult UserLayer() 
        {
            return View();
        }
        private readonly UsersBll _bll = new UsersBll();
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="pwd"></param>
        /// <param name="sex"></param>
        /// <param name="home"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult AddUser(string uName,string pwd,int sex,string home,int role )
        {
            Users user = new Users();
            user.uName = uName;
            user.pwd = pwd;
            user.sex = sex;
            user.home = home;
            user.role = role;
            bool result  = _bll.AddUser(user);
            return Content(result ? "ok" : "err");
        }
        /// <summary>
        /// 获取指定id用户
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        public ActionResult GetUserById(int uId)
        {
            Users u = _bll.GetUsersById(uId);
            return Json(new {rows = u},JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="pwd"></param>
        /// <param name="sex"></param>
        /// <param name="home"></param>
        /// <param name="role"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        public ActionResult UpdataUser(string uName, string pwd, int sex, string home, int role,int uId)
        {
            Users user = new Users();
            user.uName = uName;
            user.pwd = pwd;
            user.sex = sex;
            user.home = home;
            user.role = role;
            user.uId = uId;
            bool s = _bll.EditUser(user);
            return Content(s ? "ok" : "err");
        }
        /// <summary>
        /// 查找一级城市
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProvice()
        {
            List<string> li = _bll.GetProvice();
            return Json(new {total=li.Count, rows = li }, JsonRequestBehavior.AllowGet);

        }

    }
}

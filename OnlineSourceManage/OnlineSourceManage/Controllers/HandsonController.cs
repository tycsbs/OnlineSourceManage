using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Modle;

namespace OnlineSourceManage.Controllers
{
    public class HandsonController : Controller
    {
        //
        // GET: /Handson/

        public ActionResult Index()
        {
            return View();
        }

        private readonly UsersBll _bll = new UsersBll();
        public ActionResult GetData()
        {
            var list = _bll.GetAllUserList();

            return Json(new { total = list.Count, rows = list }, JsonRequestBehavior.AllowGet);
        }
        

    }
}

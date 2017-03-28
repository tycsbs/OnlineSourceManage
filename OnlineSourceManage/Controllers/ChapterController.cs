using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Modle;

namespace OnlineSourceManage.Controllers
{
    public class ChapterController : Controller
    {
        //
        // GET: /Chapter/

        private readonly ChapterBll _bll = new ChapterBll();
        public ActionResult ChapterPage()
        {
            return View("Chapter");
        }
        
        public ActionResult GetChapter()
        {
            List<Chapter> list = _bll.GetAllChapter();
            return Json(new{total=list.Count,rows=list},JsonRequestBehavior.AllowGet);
        }

    }
}

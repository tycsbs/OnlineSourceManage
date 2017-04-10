using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Modle;

namespace OnlineSourceManage.Controllers.client
{
    public class ClientCourseController : Controller
    {
        //
        // GET: /ClientCourse/

        public ActionResult ClientChapters()
        {
            return View();  
        }

        public ActionResult CheckLogin()
        {
            if (Session["UserId"] != null)
            {
                string userId = Session["UserId"].ToString();
                string userName = Session["UserName"].ToString();
                if (userId.Length <= 0)
                {
                    return RedirectToRoute("/ClientIndex/Index");
                }
                return Json(new {name = userName, id = userId}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { name = "-1", id = -1 }, JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult LogOut()
        {
            Session["UserId"] = null;
            Session["UserName"] = null;
            return Content("ok");
        }


        private ChapterBll _bll = new ChapterBll();
        public ActionResult GetChaptersById(int cId)
        {
            List<chapterBase> chapter = _bll.GetChapterListById(cId);
            return Json(new {total = chapter.Count, rows = chapter}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFileByChapter(int chId)
        {
           List<ChapterSource> chapters = _bll.GetChapterFileById(chId);
           return Json(new { total = chapters.Count, rows = chapters }, JsonRequestBehavior.AllowGet);
        }

    }
}

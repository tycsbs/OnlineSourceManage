﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Modle;

namespace OnlineSourceManage.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/

        public ActionResult AdminIndex()
        {
            return View("Index");
        }
    }
}

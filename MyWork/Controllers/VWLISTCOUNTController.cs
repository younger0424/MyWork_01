using MyWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class VWLISTCOUNTController : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        // GET: VWLISTCOUNT
        public ActionResult Index()
        {
            var all = db.VW_LISTCOUNT.AsQueryable();

            return View(all);
        }
    }
}
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
        Models.VW_LISTCOUNTRepository qry = RepositoryHelper.GetVW_LISTCOUNTRepository();
        客戶資料Repository repo;
        public VWLISTCOUNTController()
        {
            repo =  RepositoryHelper.Get客戶資料Repository(qry.UnitOfWork);
        }

        // GET: VWLISTCOUNT
        [宣告客戶分類的selectList物件Attribute]
        public ActionResult Index(string keyword , string type)
        { 
            //ViewBag.客戶分類 = new SelectList(repo.All().Distinct(), "客戶分類", "客戶分類");

            //var data = db.VW_LISTCOUNT.AsQueryable();
            var data = qry.All();

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(x => x.客戶名稱.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(type))
            {
                data = data.Where(x => x.客戶分類 == type);
            }

            return View(data);
        }
    }
}
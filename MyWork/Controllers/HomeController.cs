using MyWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyWork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM form)
        {
            if (checkLogin(form)) {

                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(form.UserName, false);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

    private bool checkLogin(LoginVM form)
    {
        if (form.UserName == "admin")
        {
            if (form.Password == "123")
            {
                return true;
            }
        } else{
            var repo = RepositoryHelper.Get客戶資料Repository();
            var curPW = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(form.Password, "SHA1");

                return repo.All().Any(p => p.帳號 == form.UserName && p.密碼 == curPW);
         }
        return false;
      }
    
    }
}
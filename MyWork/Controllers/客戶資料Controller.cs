using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyWork.Models;
using System.Data.Entity.Validation;

namespace MyWork.Controllers
{
    public class 客戶資料Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        //以下改為Repository
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        客戶聯絡人Repository repo客戶聯絡人;

        public  客戶資料Controller(){
            repo客戶聯絡人 = RepositoryHelper.Get客戶聯絡人Repository(repo.UnitOfWork);
        }


        // GET: 客戶資料
        public ActionResult Index(string keyword, string sort , bool? desc)
        {
            //var all = db.客戶資料.AsQueryable();
            //var data = all.Where(p => p.是否已刪除 == false);
            //var data = repo.All();

            //if (!String.IsNullOrEmpty(keyword))
            //{
            //    data = data.Where(p => p.客戶名稱.Contains(keyword));
            //}

            var data = repo.Get客戶資料(keyword);
            switch (sort){
                case "客戶名稱":
                    if(desc.HasValue &&  desc.Value)
                    {
                        data = data.OrderByDescending(p => p.客戶名稱);
                    }
                    else { 
                        data = data.OrderBy(p => p.客戶名稱);
                    }
                break;
                case "統一編號":
                    if (desc.HasValue && desc.Value)
                    {
                        data = data.OrderByDescending(p => p.統一編號);
                    }
                    else
                    {
                        data = data.OrderBy(p => p.統一編號);
                    }
                    break;
                default:
                    break;       
            }


            return View(data);
        }

        [HttpPost]
        public ActionResult BatchUpdateForContacts(int? id ,IList<客戶聯絡人批次更新ViewModel> data)
        {
            if (ModelState.IsValid)
            {
                    foreach (var item in data)
                {
                    var c1 = repo客戶聯絡人.Get單筆資料ById(item.Id);
                    if (c1 != null)
                    {
                        c1.職稱 = item.職稱;
                        c1.手機 = item.手機;
                        c1.電話 = item.電話;
                    }
                    repo客戶聯絡人.UnitOfWork.Commit();
                }
            }
                客戶資料 客戶資料 = repo.Get單筆資料ById(id.Value);
                if (客戶資料 == null)
                {
                    return HttpNotFound();
                }

            return View("Details", 客戶資料);
        }

        // GET: 客戶資料/Query
        //public ActionResult Query()
        //{
        //    var all = db.客戶資料.AsQueryable();
        //    var data = all.Where(p => p.是否已刪除 == false); 
        //    return View(data);
        //}

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Get單筆資料ById(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        [宣告客戶分類的selectList物件Attribute]
        public ActionResult Create()
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "廠商", Value = "廠商" });
            items.Add(new SelectListItem() { Text = "客戶", Value = "客戶" });
            ViewBag.客戶分類 = new SelectList(items, "Value", "Text");

            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [宣告客戶分類的selectList物件Attribute]
        [ValidateAntiForgeryToken]
       // public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        public ActionResult Create(客戶資料 客戶資料)
        {

            if (ModelState.IsValid)
            {
                //db.客戶資料.Add(客戶資料);
                客戶資料.對密碼進行雜湊運算();
                repo.Add(客戶資料);
                try
                {
                    //db.SaveChanges();
                    repo.UnitOfWork.Commit();
                }
               catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        [宣告客戶分類的selectList物件Attribute]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Get單筆資料ById(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            客戶資料.密碼 = "";
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [宣告客戶分類的selectList物件Attribute]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        public ActionResult Edit(int id , FormCollection form)
        {
            var 客戶資料 = repo.Get單筆資料ById(id);
            var orgPW = 客戶資料.密碼;
            if (TryUpdateModel(客戶資料))
            {
                //db.Entry(客戶資料).State = EntityState.Modified;
                //db.SaveChanges();
                if (!String.IsNullOrEmpty(客戶資料.密碼))
                {
                    客戶資料.對密碼進行雜湊運算();

                }else
                {
                    客戶資料.密碼 = orgPW;

                }
                repo.Update(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Get單筆資料ById(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            //客戶資料.是否已刪除 = true;
            客戶資料 客戶資料 = repo.Get單筆資料ById(id);
            //db.客戶資料.Remove(客戶資料);
            try
            {
                //db.SaveChanges();
                repo.Delete(客戶資料);
                repo.UnitOfWork.Commit();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
       
            return RedirectToAction("Index");
        }


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}

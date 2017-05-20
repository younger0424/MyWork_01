using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MyWork.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => p.是否已刪除 == false);
        }

        public IQueryable<客戶資料> All(bool showAll)
        { 
           if (showAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }

        public IQueryable<客戶資料> Get客戶資料所有資料(bool? 是否已刪除 = true, bool showAll = false)
        {
            IQueryable<客戶資料> all = this.All();
            if (showAll)
            {
                all = base.All();
            }
            return All().Where(p => p.是否已刪除 == 是否已刪除).OrderByDescending(p => p.Id).Take(10);
        }

        public IQueryable<客戶資料> Get客戶資料(string keyword)
        {
            IQueryable<客戶資料> data = this.All();
            if (!String.IsNullOrEmpty(keyword))
            {
                data = data.Where(p => p.客戶名稱.Contains(keyword));
            }
            return data;
        }

        public 客戶資料 Get單筆資料ById(int Id)
        {
            return All().FirstOrDefault(p => p.Id == Id);
        }

        public void Update(客戶資料 客戶資料)
        {
            this.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified; ;
        }

        public override void Delete(客戶資料 客戶資料)
        {
            // base.Delete(entity);)
            //關閉驗證
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            客戶資料.是否已刪除 = true;
        }


    }

public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}
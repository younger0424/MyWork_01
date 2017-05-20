using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MyWork.Models
{
    public class VW_LISTCOUNTRepository : EFRepository<VW_LISTCOUNT>, IVW_LISTCOUNTRepository
    {
        public override IQueryable<VW_LISTCOUNT> All()
        {
            return base.All();
        }
    }

	public  interface IVW_LISTCOUNTRepository : IRepository<VW_LISTCOUNT>
	{

	}
}
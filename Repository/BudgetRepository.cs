using DoAn1.Areas.Identity.Data;
using DoAn1.Data;
using DoAn1.Models;
using DoAn1.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DbContext = DoAn1.Data.DbContext;

namespace DoAn1.Repository
{
	public class BudgetRepository : Repository<Budget>, IBudgetRepository
	{
		private DbContext _context;
		public BudgetRepository(DbContext context) : base(context)
		{
			_context = context;
		}
		public void Update(Budget obj)
		{
			var objFromDb = _context.Budgets.FirstOrDefault(p=>p.Id == obj.Id);
			if (objFromDb != null)
			{
				objFromDb.CategoryId = obj.CategoryId;
				objFromDb.Amount = obj.Amount;
				objFromDb.Date = obj.Date;
 			}
		}
	}
}

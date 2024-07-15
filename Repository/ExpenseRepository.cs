using DoAn1.Areas.Identity.Data;
using DoAn1.Data;
using DoAn1.Models;
using DoAn1.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DbContext = DoAn1.Data.DbContext;

namespace DoAn1.Repository
{
	public class ExpenseRepository : Repository<Expense>, IExpenseRepository
	{
		private DbContext _context;
		public ExpenseRepository(DbContext context) : base(context)
		{
			_context = context;
		}
		public void Update(Expense obj)
		{
			var objFromDb = _context.Expenses.FirstOrDefault(p=>p.Id == obj.Id);
			if (objFromDb != null)
			{
				objFromDb.CategoryId = obj.CategoryId;
				objFromDb.Amount = obj.Amount;
				objFromDb.Description = obj.Description;
				objFromDb.Date = obj.Date;
 			}
		}
	}
}

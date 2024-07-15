using DoAn1.Areas.Identity.Data;
using DoAn1.Data;
using DoAn1.Models;
using DoAn1.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DbContext = DoAn1.Data.DbContext;

namespace DoAn1.Repository
{
	public class IncomeRepository : Repository<Income>, IIncomeRepository
	{
		private DbContext _context;
		public IncomeRepository(DbContext context) : base(context)
		{
			_context = context;
		}
		public void Update(Income obj)
		{
			var objFromDb = _context.Incomes.FirstOrDefault(p=>p.Id == obj.Id);
			if (objFromDb != null)
			{
				objFromDb.Amount = obj.Amount;
				objFromDb.Description = obj.Description;
				objFromDb.Date = obj.Date;
 			}
		}
	}
}

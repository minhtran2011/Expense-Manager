using DoAn1.Data;
using DoAn1.Models;
using DoAn1.Repository;
using DoAn1.Repository.IRepository;

namespace DoAn1.Repository
{
    public class UnitOfWork : IUnitOfWork
	{
		private DbContext _dbcontext;
		public ICategoryRepository Category { get; private set; }
		public IExpenseRepository Expense { get; private set; }
		public IBudgetRepository Budget {  get; private set; }
		public IIncomeRepository Income { get; private set; }
		public UnitOfWork(DbContext dbcontext)
		{
			_dbcontext = dbcontext;
			Category = new CategoryRepository(_dbcontext);
			Expense = new ExpenseRepository(_dbcontext);
			Budget = new BudgetRepository(_dbcontext);
			Income = new IncomeRepository(_dbcontext);
		}
		public void Save()
		{
			_dbcontext.SaveChanges();
		}
	}
}

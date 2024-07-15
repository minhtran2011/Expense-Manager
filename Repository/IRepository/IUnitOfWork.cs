using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryRepository Category { get; }
		IExpenseRepository Expense { get; }
		IBudgetRepository Budget { get; }
		IIncomeRepository Income { get; }
		void Save();

	}
}

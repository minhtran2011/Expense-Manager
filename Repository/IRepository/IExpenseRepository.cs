using DoAn1.Models;

namespace DoAn1.Repository.IRepository
{
	public interface IExpenseRepository : IRepository<Expense>
	{
		void Update(Expense obj);
	}
}

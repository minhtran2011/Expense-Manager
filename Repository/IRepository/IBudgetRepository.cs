using DoAn1.Models;

namespace DoAn1.Repository.IRepository
{
	public interface IBudgetRepository : IRepository<Budget>
	{
		void Update(Budget obj);
	}
}

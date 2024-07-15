using DoAn1.Models;

namespace DoAn1.Repository.IRepository
{
	public interface IIncomeRepository : IRepository<Income>
	{
		void Update(Income obj);
	}
}

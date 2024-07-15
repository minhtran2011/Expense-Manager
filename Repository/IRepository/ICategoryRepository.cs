using DoAn1.Models;

namespace DoAn1.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
	{
		void Update(Category category);
	}
}

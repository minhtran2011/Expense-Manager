using DoAn1.Data;
using DoAn1.Models;
using DoAn1.Repository.IRepository;

namespace DoAn1.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private DbContext _dbcontext;
		public CategoryRepository(DbContext dbcontext): base(dbcontext)
		{
			_dbcontext = dbcontext;
		}
		public void Update(Category obj)
		{
			_dbcontext.Categories.Update(obj);
		}
	}
}

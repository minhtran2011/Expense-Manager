using DoAn1.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DoAn1.Repository
{
    public class Repository<T> : IRepository<T> where T : class
	{
		private readonly DbContext _dbcontext;
		internal DbSet<T> dbSet;
		public Repository(DbContext dbcontext)
		{
			_dbcontext = dbcontext;
			this.dbSet = _dbcontext.Set<T>();
		}
		public void Add(T entity)
		{
			_dbcontext.Add(entity);
		}

		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if(!string.IsNullOrEmpty(includeProperties))
			{
				foreach(var includeProp in includeProperties.Split(new char[] { ',' }, 
					StringSplitOptions.RemoveEmptyEntries)) {
						query = query.Include(includeProp);
				}
			}	
			return query.ToList();
		}

		public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			query  = query.Where(filter);
			if (!string.IsNullOrEmpty(includeProperties))
			{
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
				}
			}
			return query.FirstOrDefault();
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}
	}
}

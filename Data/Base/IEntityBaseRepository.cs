using System;
using System.Linq.Expressions;

namespace eTicketing.Data.Base
{
	public interface IEntityBaseRepository<T> where T: class, IEntityBase, new()
	{
		Task<IEnumerable<T>> GetAllASync();

		Task<IEnumerable<T>> GetAllASync(params Expression<Func<T, object>>[] includeProperties);

		Task<T> GetByIdASync(int id);

		Task AddASync(T entity);

		Task UpdateASync(int id, T entity);

		Task DeleteASync(int id);
	}
}


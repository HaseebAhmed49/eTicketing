using System;
namespace eTicketing.Data.Base
{
	public interface IEntityBaseRepository<T> where T: class, IEntityBase, new()
	{
		Task<IEnumerable<T>> GetAllASync();


		Task<T> GetByIdASync(int id);

		Task AddASync(T entity);

		Task UpdateASync(int id, T entity);

		Task DeleteASync(int id);
	}
}


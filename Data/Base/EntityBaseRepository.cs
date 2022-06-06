using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eTicketing.Data.Base
{
	public class EntityBaseRepository<T>: IEntityBaseRepository<T> where T:class, IEntityBase, new()
	{
        private readonly AppDbContext _context;

		public EntityBaseRepository(AppDbContext context)
		{
            _context = context;
		}

        public async Task AddASync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteASync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(a => a.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<T>> GetAllASync() => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllASync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdASync(int id) => await _context.Set<T>().FirstOrDefaultAsync(a => a.Id == id);

        public async Task UpdateASync(int id, T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}


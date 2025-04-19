using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CoreEntities;
using Microsoft.EntityFrameworkCore;
using Repository.InterFace;

namespace Repository.RepositryPattern
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbcontext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbcontext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllByExpressionAsync(
                  Expression<Func<T, bool>>? filter = null,
                  int pageNumber = 1,
                  int pageSize = 5)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

             int skip = (pageNumber - 1) * pageSize;
             query = query.Skip(skip).Take(pageSize);
             return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}

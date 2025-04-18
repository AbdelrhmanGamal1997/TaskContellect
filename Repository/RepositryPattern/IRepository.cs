using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.InterFace
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllByExpressionAsync(Expression<Func<T, bool>>? filter = null, int pageNumber = 1,int pageSize = 5);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

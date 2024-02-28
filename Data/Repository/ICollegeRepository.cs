using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project1.Data.Repository
{
    public interface ICollegeRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, bool UseNoTracking = false);
        Task<T> GetByNameAsync(Expression<Func<T, bool>> filter);
        Task<T> create(T dbRecord);

        Task<T> updateAsync(T dbRecord);

        Task<bool> DeleteAsync(T dbRecord);
    }
}
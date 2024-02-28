using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project1.Data.Repository
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly CollegeDbContext _collegeDbContext;
        private DbSet<T> _dbSet;
        public CollegeRepository(CollegeDbContext collegeDbContext)
        {
            collegeDbContext = _collegeDbContext;
            _dbSet = _collegeDbContext.Set<T>();
        }

        public async Task<T> create(T dbRecord)
        {
            await _dbSet.AddAsync(dbRecord);
            await _collegeDbContext.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(T dbRecord)
        {

            _dbSet.Remove(dbRecord);
            await _collegeDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>>filter,bool UseNoTracking = false)
        {
            if (UseNoTracking)
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _dbSet.Where(filter).FirstOrDefaultAsync();

        }

        public async Task<T> GetByNameAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).FirstOrDefaultAsync();
        }


        public async Task<T> updateAsync(T dbRecord)
        {
            _collegeDbContext.Update(dbRecord);
            await _collegeDbContext.SaveChangesAsync();
            return dbRecord;
        }


    }
}
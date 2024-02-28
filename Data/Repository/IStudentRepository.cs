using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Data.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAll();
        Task<Student> GetByIdAsync(int id,bool UseNoTracking=false);
        Task<Student> GetByNameAsync(string name);
        Task<int> create(Student student);

        Task<int> updateAsync(Student student);

        Task<bool> DeleteAsync(Student student);
    }
}
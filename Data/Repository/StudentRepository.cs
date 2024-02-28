using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project1.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        // private readonly CollegeDbContext _collegeDbContext;
        private readonly CollegeDbContext _collegeDbContext;

        public StudentRepository(CollegeDbContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }
        public async Task<int> create(Student student)
        {
            await _collegeDbContext.Students.AddAsync(student);
            await _collegeDbContext.SaveChangesAsync();
            return student.Id;
        }

        public async Task<bool> DeleteAsync(Student student)
        {
          
            _collegeDbContext.Students.Remove(student);
            await _collegeDbContext.SaveChangesAsync();

            return true;
        }


        public async Task<List<Student>> GetAll()
        {
            return await _collegeDbContext.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id, bool UseNoTracking = false)
        {
            if (UseNoTracking)
                return await _collegeDbContext.Students.AsNoTracking().Where(n => n.Id == id).FirstOrDefaultAsync();
            else
                return await _collegeDbContext.Students.Where(n => n.Id == id).FirstOrDefaultAsync();

        }

        public async Task<Student> GetByNameAsync(string name)
        {
            return await _collegeDbContext.Students.Where(n => n.StudentName.ToLower().Equals(name.ToLower())).FirstOrDefaultAsync();
        }

        public async Task<int> updateAsync(Student student)
        {
            var studentToUpdate = await _collegeDbContext.Students.Where(n => n.Id == student.Id).FirstOrDefaultAsync();
            if (student == null)
            {
                throw new ArgumentNullException($"no student found with this id : {student.Id}");
            }
            studentToUpdate.StudentName = student.StudentName;
            studentToUpdate.Address = student.Address;
            studentToUpdate.Email = student.Email;
            studentToUpdate.DOB = student.DOB;

            await _collegeDbContext.SaveChangesAsync();
            return student.Id;
        }

    }
}
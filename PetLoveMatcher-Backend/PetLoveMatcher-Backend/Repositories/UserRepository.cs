using DtoNetProject.Models;
using Microsoft.EntityFrameworkCore;
using PetLoveMatcher_Backend.Data;
using PetLoveMatcher_Backend.Models;

namespace DtoNetProject.Repositories
{
    public class UserRepository : IDisposable
    {

        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users
                //.Include(user => user.Classes) //FETCH EAGER
                .Include(user => user.School)
                .ToList();
        }

        public User? GetUserByID(string id)
        {
            //return _context.Users.Find(id);
            var temp = (from s in _context.Users.Include(user => user.School)
                        where s.Id.Equals(id)
                        select s).FirstOrDefault<User>();
            return temp;
        }

        public User? GetUserByUserName(String username)
        {
            //return _context.Users.Find(id);
            var temp = (from s in _context.Users.Include(user => user.School)
                        where s.UserName == username
                        select s).FirstOrDefault<User>();
            return temp;
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }


        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

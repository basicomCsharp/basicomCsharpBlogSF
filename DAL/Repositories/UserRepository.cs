using BlogSF.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSF.DAL.Repositories
{
    public class UserRepository : IUserRepositories //: IBaseRepositories<User>
    {
        private readonly AppContext db;
        public UserRepository(AppContext context)
        {
            this.db = context;
        }
        public async Task Create(User value)
        {
            var entry = db.Entry(value);
            if (entry.State == EntityState.Detached)
                db.Users.AddAsync(value);
            await db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var _user = db.Users.Find(id);
            if (_user != null) 
                db.Users.Remove(_user);
            await db.SaveChangesAsync();
        }

        public async Task<User> Get(Guid id)
        {

            return await db.Users.Where(u => u.Id == id).Include(u => u.Role).FirstOrDefaultAsync();

        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.Include(u => u.Role).ToListAsync();
            //return await db.Users.ToListAsync();
        }

        public async Task<User> GetByLogin(string _login)
        {
            return await db.Users.Where(u => u.Login == _login).FirstOrDefaultAsync();
        }

        public async Task Update(User value)
        {
            db.Entry(value).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}

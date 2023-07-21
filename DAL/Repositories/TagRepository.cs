using BlogSF.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlogSF.DAL.Repositories
{
    public class TagRepository : ITagRepositories//IBaseRepositories<Tag>
    {
        private readonly AppContext db;
        public TagRepository(AppContext contex)
        {
            this.db = contex;
        }
        public async Task Create(Tag value)
        {
            var entry = db.Entry(value);
            if (entry.State == EntityState.Detached)
                db.Tags.AddAsync(value);
            await db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Tag _tag = db.Tags.Find(id);
            if (_tag != null)
                db.Tags.Remove(_tag);
            await db.SaveChangesAsync();
        }

        public async Task<Tag> Get(Guid id)
        {
            return await db.Tags.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await db.Tags.ToListAsync();
        }

        public async Task Update(Tag value)
        {
            db.Entry(value).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}


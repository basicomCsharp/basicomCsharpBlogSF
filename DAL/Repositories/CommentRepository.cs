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
    public class CommentRepository : ICommentRepositories//IBaseRepositories<Comment>
    {
        private readonly AppContext db;
        public CommentRepository(AppContext contex)
        {
            this.db = contex;
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await db.Comments.ToListAsync();
        }

        public async Task<Comment> Get(Guid id)
        {
            return await db.Comments.Where(u => u.Id == id).FirstOrDefaultAsync();            
        }


        public async Task Create(Comment comment)
        {
            var entry = db.Entry(comment);
            if (entry.State == EntityState.Detached)
               _= db.Comments.AddAsync(comment);
            await db.SaveChangesAsync();
        }
        public async Task Update(Comment value)
        {
            db.Entry(value).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            Comment _comment = db.Comments.Find(id);
            if (_comment != null)
                db.Comments.Remove(_comment);
            await db.SaveChangesAsync();
        }

    }
}

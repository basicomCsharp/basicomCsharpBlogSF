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
    public class BookRepository : IBookRepositories//IBaseRepositories<Book>
    {
        private readonly AppContext db;
        public BookRepository(AppContext contex)
        {
            this.db = contex;
        }

        public  async Task<IEnumerable<Book>> GetAll()
        {
            return await db.Books.ToListAsync();
        }

        public async Task<Book> Get(Guid id)
        {
            return await db.Books.Where(u => u.Id == id).FirstOrDefaultAsync();            
        }

        public async Task Create(Book book)
        {
            var entry = db.Entry(book);
            if (entry.State == EntityState.Detached)
              _=  db.Books.AddAsync(book);
            await db.SaveChangesAsync();
        }
 
        public async Task Update(Book value)
        {
            db.Entry(value).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            Book book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
            await db.SaveChangesAsync();
        }
    }
}

namespace BlogSF.DAL.Repositories
{
    public interface IBookRepositories
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> Get(Guid id);
        Task Create(Book value);
        Task Update(Book value);
        Task Delete(Guid id);
    }
}

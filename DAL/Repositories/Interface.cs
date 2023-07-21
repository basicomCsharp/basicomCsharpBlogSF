namespace BlogSF.DAL.Repositories
{
    public interface IBaseRepositories<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Guid id);
        Task Create(T value);        
        Task Update(T value);
        Task Delete(Guid id);
    }
}

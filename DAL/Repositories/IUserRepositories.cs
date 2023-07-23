using NuGet.Protocol.Plugins;

namespace BlogSF.DAL.Repositories
{
    public interface IUserRepositories
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(Guid id);
        Task Create(User value);
        Task Update(User value);
        Task Delete(Guid id);
        Task<User> GetByLogin(string login);
    }
}

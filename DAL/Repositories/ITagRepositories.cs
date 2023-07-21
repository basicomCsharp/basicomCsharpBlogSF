namespace BlogSF.DAL.Repositories
{
    public interface ITagRepositories
    {
        Task<IEnumerable<Tag>> GetAll();
        Task<Tag> Get(Guid id);
        Task Create(Tag value);
        Task Update(Tag value);
        Task Delete(Guid id);
    }
}

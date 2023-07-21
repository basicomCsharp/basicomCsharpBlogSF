namespace BlogSF.DAL.Repositories
{
    public interface ICommentRepositories
    {
        Task<IEnumerable<Comment>> GetAll();
        Task<Comment> Get(Guid id);
        Task Create(Comment value);
        Task Update(Comment value);
        Task Delete(Guid id);
    }
}

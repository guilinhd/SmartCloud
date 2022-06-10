using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.Users
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<List<User>> GetListAsync(QueryEnum query, string name);
    }
}

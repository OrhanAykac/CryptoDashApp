using RiseX.Entities.Concrete;

namespace RiseX.DataAccess.Abstract;
public interface IUserDAL
{
    Task<User> GetUserByEmailAsync(string email);
    Task<int> InsertAsync(User model);
}
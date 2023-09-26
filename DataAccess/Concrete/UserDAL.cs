using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Data;

namespace DataAccess.Concrete;
public class UserDAL : IUserDAL
{
    private readonly IDbConnection _dbConnection;

    public UserDAL(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> InsertAsync(User model)
    {
        var addedEntityId = await _dbConnection.InsertAsync(model);
        return addedEntityId;
    }
    public async Task<User> GetUserByEmailAsync(string email)
    {
        string query = "SELECT * FROM Users WITH(NOLOCK) WHERE Email=@Email";

        var user = await _dbConnection.QuerySingleOrDefaultAsync<User>(query, new { Email = email });
        return user;
    }
}

using Entities.Concrete;
using Entities.Dto;
using Shared.Results;

namespace Business.Abstract;
public interface IAuthService
{
    Task<BaseDataResponse<User>> RegisterAsync(UserForRegisterDto model);
    Task<BaseDataResponse<User>> LoginAsync(UserForLoginDto model);
}

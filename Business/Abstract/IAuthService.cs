using RiseX.Entities.Concrete;
using RiseX.Entities.Dto;
using RiseX.Shared.Results;

namespace RiseX.Business.Abstract;
public interface IAuthService
{
    Task<BaseDataResponse<User>> RegisterAsync(UserForRegisterDto model);
    Task<BaseDataResponse<User>> LoginAsync(UserForLoginDto model);
}

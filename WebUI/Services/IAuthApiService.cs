using Entities.Dto;
using Shared.Results;

namespace WebUI.Services;
public interface IAuthApiService
{
    Task<BaseDataResponse<string>> LoginAsync(UserForLoginDto userForLogin);
    Task<BaseDataResponse<string>> RegisterAsync(UserForRegisterDto userForRegister);
}
using Entities.Dto;
using Shared.Results;

namespace WebUI.Services;

public class AuthApiService : IAuthApiService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<BaseDataResponse<string>> LoginAsync(UserForLoginDto userForLogin)
    {
        var http = _httpClientFactory.CreateClient("AuthApi");
        var result = await http.PostAsJsonAsync("auth/login", userForLogin);

        if (result.IsSuccessStatusCode == true)
        {
            var successContent = await result.Content.ReadFromJsonAsync<BaseDataResponse<string>>();
            return successContent;
        }
        else
        {
            var errorContent = await result.Content.ReadFromJsonAsync<BaseResponse>();
            return new BaseDataResponse<string>("",false,errorContent.Message);
        }
    }
    public async Task<BaseDataResponse<string>> RegisterAsync(UserForRegisterDto userForRegister)
    {
        var http = _httpClientFactory.CreateClient("AuthApi");
        var result = await http.PostAsJsonAsync(requestUri: "auth/register", userForRegister);

        if (result.IsSuccessStatusCode == true)
        {
            var successContent = await result.Content.ReadFromJsonAsync<BaseDataResponse<string>>();
            return successContent;
        }
        else
        {
            var errorContent = await result.Content.ReadFromJsonAsync<BaseResponse>();
            return new BaseDataResponse<string>("",false,errorContent.Message);
        }
    }
}

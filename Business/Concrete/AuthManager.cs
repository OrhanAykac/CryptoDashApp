using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.Extensions.Logging;
using Shared.Results;
using Shared.Utilities.Hashing;

namespace Business.Concrete;
public class AuthManager : IAuthService
{
    private readonly IUserDAL _userDAL;
    private readonly ILogger<AuthManager> _logger;

    public AuthManager(IUserDAL userDAL, ILogger<AuthManager> logger)
    {
        _userDAL = userDAL;
        _logger = logger;
    }

    public async Task<BaseDataResponse<User>> LoginAsync(UserForLoginDto model)
    {
        try
        {
            var userEntity = await _userDAL.GetUserByEmailAsync(model.Email);

            if (userEntity is null)
                return new BaseDataResponse<User>(default, false, "E-posta yada şifre hatalı.");

            var isPasswordMatched = HashingHelper.VerifyPasswordHash(model.Password, userEntity.PasswordHash, userEntity.PasswordSalt);

            if (isPasswordMatched == false)
                return new BaseDataResponse<User>(default, false, "E-posta yada şifre hatalı.");

            return new BaseDataResponse<User>(userEntity, true, "Giriş başarılı.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseDataResponse<User>(default, false, "İşlenemeyen bir hata oluştu.");
        }
    }
    public async Task<BaseDataResponse<User>> RegisterAsync(UserForRegisterDto model)
    {
        try
        {
            var userEntity = await _userDAL.GetUserByEmailAsync(model.Email);

            if (userEntity is not null)
                return new BaseDataResponse<User>(default, false, "Bu E-posta adresi ile daha önce kayıt oluşturulmuş.");

            HashingHelper.CreatePasswordHash(model.Password, out var passwordHash, out var passwordSalt);

            userEntity = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            userEntity.Id = await _userDAL.InsertAsync(userEntity);

            return new BaseDataResponse<User>(userEntity, true, "Kayıt başarılı.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return new BaseDataResponse<User>(default, false, "İşlenemeyen bir hata oluştu.");
        }
    }
}

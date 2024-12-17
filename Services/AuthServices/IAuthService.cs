using HET_BACKEND.EntityModel;
using HET_BACKEND.Models.Auth;

namespace HET_BACKEND.Services.AuthServices
{
    public interface IAuthService
    {
        Task<UserEntityModel> GetUser(LoginModel loginModel);
        Task RegisterUser(UserRegisterModel userRegisterModel);
        Task<UserEntityModel> UpdateUserNameAndEmail(UserRegisterModel userRegisterModel);
    }
}

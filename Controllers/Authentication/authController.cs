using HET_BACKEND.Helper;
using HET_BACKEND.Models.Auth;
using HET_BACKEND.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HET_BACKEND.Services.AuthServices;

namespace HET_BACKEND.Controllers.Authentication
{
    [Route("HET/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly JWTHelper _jwthelper;
        private readonly IAuthService _authService;
        public AuthController(JWTHelper jWTHelper,IAuthService authService)
        {
            _jwthelper = jWTHelper;
            _authService = authService;
        }
        [HttpPost]
        public async Task<JsonResult> Login([FromBody]LoginModel loginModel)
        {
            var user = await _authService.GetUser(loginModel);
            if(user!=null)
            {
                bool isValid = PasswordHash.VerifyPassword(loginModel.Password ?? "Password",user.password ?? "Password",Convert.FromHexString(user.salt ?? "salt"));
                if(isValid)
                {
                    var token = _jwthelper.GenerateJwtToken(user);
                    return Json(new { Message = "Login SuccessFully",Token=token,UserId=user.userId });
                }
                else
                {
                    throw new KeyNotFoundException("Password Not Found");
                }
            }
            else
            {
                throw new KeyNotFoundException("UserName Not Found");
            }
        }

        [HttpPost]
        public async Task<JsonResult> Register([FromBody] UserRegisterModel userRegisterModel)
        {
            await _authService.RegisterUser(userRegisterModel);
            return Json(new { message = "User registered Successfully" });
        }

        [HttpPut]
        [Authorize]
        public async Task<JsonResult> UpdateUserNameAndEmail([FromBody] UserRegisterModel userRegisterModel)
        {
           var existingUser = await _authService.UpdateUserNameAndEmail(userRegisterModel);
            return Json(new { Result = "Updated Successfully", Message = existingUser });
        }
    }
}

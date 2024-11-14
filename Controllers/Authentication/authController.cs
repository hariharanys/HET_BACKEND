using HET_BACKEND.EntityModel;
using HET_BACKEND.Helper;
using HET_BACKEND.Models.Auth;
using HET_BACKEND.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HET_BACKEND.Controllers.Authentication
{
    [Route("HET/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly HETDbContext _context;
        private readonly JWTHelper _jwthelper;
        public AuthController(HETDbContext context,JWTHelper jWTHelper)
        {
            _context = context;
            _jwthelper = jWTHelper;
        }
        [HttpPost]
        public async Task<JsonResult> Login([FromBody]LoginModel loginModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.userName == loginModel.UserName);
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
            var existingUser = await _context.Users.FirstOrDefaultAsync(u=>u.userName==userRegisterModel.userName);
            if (existingUser != null)
            {
                throw new DbUpdateException("UserName already exists");
            }
            (string password, string salt) = PasswordHash.HashPassword(userRegisterModel.password ?? "password");
            var user = new UserEntityModel
            {
                userName = userRegisterModel.userName,
                email = userRegisterModel.email,
                password = password,
                phoneNumber = userRegisterModel.phoneNumber,
                salt = salt,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Json(new { message = "User registered Successfully" });
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> UpdateUserNameAndEmail([FromBody] UserRegisterModel userRegisterModel)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u=>u.userName == userRegisterModel.userName);
            if(existingUser==null)
            {
                throw new KeyNotFoundException("The given Username is not exist.");
            }
            existingUser.userName = userRegisterModel.userName;
            existingUser.email = userRegisterModel.email;
            existingUser.phoneNumber = userRegisterModel.phoneNumber;
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return Json(new { Resykt = "Updated Successfully", Message = existingUser });
        }
    }
}

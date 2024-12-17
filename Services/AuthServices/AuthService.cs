using HET_BACKEND.EntityModel;
using HET_BACKEND.Models.Auth;
using HET_BACKEND.Utilities;
using Microsoft.EntityFrameworkCore;

namespace HET_BACKEND.Services.AuthServices
{
    public class AuthService:IAuthService
    {
        private readonly HETDbContext _dbContext;

        public AuthService(HETDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserEntityModel> GetUser(LoginModel login)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.userName == login.UserName);
            if (user == null) { 
                throw new KeyNotFoundException("UserName Not Found");
            }
            return user;
        }

        public async Task RegisterUser(UserRegisterModel userRegisterModel)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u=>u.userName==userRegisterModel.userName);
            if (existingUser != null) {
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
                CreatedDate = userRegisterModel.CreatedDate,
                ModifiedDate = userRegisterModel.ModifiedDate
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserEntityModel> UpdateUserNameAndEmail(UserRegisterModel userRegisterModel)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync<UserEntityModel>(u => u.userName == userRegisterModel.userName);
            if(existingUser == null)
            {
                throw new KeyNotFoundException("The given Username is not exist. ");
            }
            existingUser.userName = userRegisterModel.userName;
            existingUser.email = userRegisterModel.email;
            existingUser.phoneNumber = userRegisterModel.phoneNumber;
            existingUser.ModifiedDate = userRegisterModel.ModifiedDate;
            _dbContext.Users.Update(existingUser);
            await _dbContext.SaveChangesAsync();
            return existingUser;
        }
    }
}

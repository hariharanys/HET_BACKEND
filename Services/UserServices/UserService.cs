using HET_BACKEND.EntityModel;
using HET_BACKEND.Models.UserProfile;
using HET_BACKEND.ServiceModels;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HET_BACKEND.Services.UserServices
{
    public class UserService:IUserService
    {
      private readonly HETDbContext _context;
        public UserService(HETDbContext context)
        {
            _context = context;
        }

        //Get Userdetails for particular Id
        public async Task<List<GetUserDetailsModel>> GetUserDetails(string profileId)
        {
            try
            {
                if (string.IsNullOrEmpty(profileId))
                {
                    throw new ArgumentNullException("Enter Valid Profile Id");
                }
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.userId == Convert.ToInt32(profileId));
                if (existingUser is null)
                {
                    throw new KeyNotFoundException("No User have found");
                }
                var UserData =  _context.UserDetails.Include(x => x.User).Where(x=>x.UserId== Convert.ToInt32(profileId)).Select(x => new GetUserDetailsModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User!.userName ?? "",
                    UserEmail = x.User!.email ?? "",
                    PhoneNumber = x.User!.phoneNumber ?? "0",
                    FullName = x.FullName! ?? "",
                    Address = x.Address! ?? "",
                    City = x.City! ?? "",
                    Country = x.Country! ?? "",
                    PostalCode = x.PostalCode! ?? "0",
                    State = x.State! ?? "",
                    FamilyName = x.FamilyName! ?? ""
                }).ToList();
                return UserData;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        //Insert Profile Details
        public async Task<UserDetailsEntityModel> AddProfileDetails(UserProfileModel userProfileModel)
        {
            try
            {
                bool IsUserExits = await _context.Users.AnyAsync(u => u.userId == userProfileModel.userId);
                if (!IsUserExits)
                {
                    throw new KeyNotFoundException("No User have found");
                }
                var existingUser = await _context.UserDetails.FirstOrDefaultAsync(u => u.UserId == userProfileModel.userId);
                if(existingUser is null)
                {
                    var userDetails = new UserDetailsEntityModel
                    {
                        UserId = userProfileModel.userId,
                        FullName = userProfileModel.FullName,
                        Address = userProfileModel.Address,
                        City = userProfileModel.City,
                        State = userProfileModel.State,
                        Country = userProfileModel.Country,
                        PostalCode = userProfileModel.PostalCode,
                        FamilyName = userProfileModel.FamilyName,
                    };
                    _context.UserDetails.Add(userDetails);
                    await _context.SaveChangesAsync();
                    return userDetails;
                }
                else
                {
                  var userDetails =  await UpdateProfileDetails(existingUser, userProfileModel);
                    await _context.SaveChangesAsync();
                    return userDetails;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        //Update Profile Details
        public async Task<UserDetailsEntityModel> UpdateProfileDetails(UserDetailsEntityModel existingUserDetails,UserProfileModel userProfileModel)
        {
            try
            {
                existingUserDetails.FullName = userProfileModel.FullName;
                existingUserDetails.Address = userProfileModel.Address;
                existingUserDetails.City = userProfileModel.City;
                existingUserDetails.State = userProfileModel.State;
                existingUserDetails.Country = userProfileModel.Country;
                existingUserDetails.PostalCode = userProfileModel.PostalCode;
                existingUserDetails.FamilyName = userProfileModel.FamilyName;
                _context.UserDetails.Update(existingUserDetails);
                await _context.SaveChangesAsync();
                return existingUserDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

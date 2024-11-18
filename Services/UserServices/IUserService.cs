using HET_BACKEND.EntityModel;
using HET_BACKEND.Models.UserProfile;
using HET_BACKEND.ServiceModels;

namespace HET_BACKEND.Services.UserServices
{
    public interface IUserService
    {
        Task<List<GetUserDetailsModel>> GetUserDetails(string profileId);
        Task<UserDetailsEntityModel> AddProfileDetails(UserProfileModel userProfileModel);
        Task<UserDetailsEntityModel> UpdateProfileDetails(UserDetailsEntityModel existingUserDetails, UserProfileModel userProfileModel);
    }
}

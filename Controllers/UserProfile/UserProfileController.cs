using HET_BACKEND.EntityModel;
using HET_BACKEND.Models.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace HET_BACKEND.Controllers.UserProfile
{
    [Route("HET/[controller]/[action]")]
    public class UserProfileController : Controller
    {
        private readonly HETDbContext _context;
        public UserProfileController(HETDbContext hETDbContext)
        {
            _context = hETDbContext;
        }
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> AddProfileDetails([FromBody]UserProfileModel userProfileModel)
        {
            var exisitingUser = await _context.UserDetails.FirstOrDefaultAsync(u=>u.UserId == userProfileModel.userId);
            if(exisitingUser != null) {
                return await UpdateProfileDetails(exisitingUser,userProfileModel);
            }
            var userDetails = new UserDetails
            {
                UserId = userProfileModel.userId,
                FullName = userProfileModel.FullName,
                Address = userProfileModel.Address,
                City = userProfileModel.City,
                State = userProfileModel.State,
                Country = userProfileModel.Country,
                PostalCode = userProfileModel.PostalCode,
                FamilyName = userProfileModel.FamilyName
            };
            _context.UserDetails.Add(userDetails);
            await _context.SaveChangesAsync();
            return Json(new { Result = "Saved Successfully", Message = userDetails });
        }

        [Authorize]
        private async Task<JsonResult> UpdateProfileDetails(UserDetails existingUserDetails,UserProfileModel userProfileModel)
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
            return Json(new {Result = "Updated Successfully",Message = existingUserDetails });
        }
    }
}

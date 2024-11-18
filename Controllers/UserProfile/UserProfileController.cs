using HET_BACKEND.EntityModel;
using HET_BACKEND.Models.UserProfile;
using HET_BACKEND.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HET_BACKEND.Controllers.UserProfile
{
    [Route("HET/[controller]/[action]")]
    public class UserProfileController : Controller
    {
        private readonly IUserService _userService;
        public UserProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetProfileDetails([FromQuery] string profileId)
        {
            var userData = await _userService.GetUserDetails(profileId);
            return Json(new { Result = "Success", Message = userData });
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> AddProfileDetails([FromBody]UserProfileModel userProfileModel)
        {
            var userDetails = await _userService.AddProfileDetails(userProfileModel);
            return Json(new { Result = "Saved Successfully", Message = userDetails });
        }
    }
}

using HET_BACKEND.EntityModel;
using HET_BACKEND.ServiceModels;
using Microsoft.EntityFrameworkCore;

namespace HET_BACKEND.Services.UserServices
{
    public class UserService:IUserService
    {
      private readonly HETDbContext _context;
        public UserService(HETDbContext context)
        {
            _context = context;
        }

        public List<GetUserDetailsModel> GetUserDetails(long id)
        {
            try
            {
                var UserData = _context.UserDetails.Include(x => x.User).Select(x => new GetUserDetailsModel
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
                throw new Exception("An error occurred while performing [specific action].", ex);
            }
        }
    }
}

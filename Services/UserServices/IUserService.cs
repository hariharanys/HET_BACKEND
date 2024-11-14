using HET_BACKEND.EntityModel;
using HET_BACKEND.ServiceModels;

namespace HET_BACKEND.Services.UserServices
{
    public interface IUserService
    {
        List<GetUserDetailsModel> GetUserDetails(long id);
    }
}

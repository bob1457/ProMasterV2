using ProMaster.Infrastructure.UserProfile.ViewModels;

namespace ProMaster.DAL.Account
{
    public interface IAccountRepository
    {
        CreateProfileViewModel CreateUserProfile();
    }
}

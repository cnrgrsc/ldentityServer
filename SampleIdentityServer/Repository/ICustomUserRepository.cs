using SampleIdentityServer.UI.Models;

namespace SampleIdentityServer.UI.Services
{
    public interface ICustomUserRepository
    {
        Task<bool> Validate(string email,string password);

        Task<CustomUser> FindById(int id);
        Task<CustomUser> FindByEmail(string email);
    }
}

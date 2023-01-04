using Microsoft.EntityFrameworkCore;
using SampleIdentityServer.UI.Models;
using SampleIdentityServer.UI.Services;

namespace SampleIdentityServer.UI.Repository
{
    public class CustomUserRepository : ICustomUserRepository
    {
        private readonly MyDbContext _mydbcontext;

        public CustomUserRepository(MyDbContext mydbcontext)
        {
            _mydbcontext = mydbcontext;
        }

        public async Task<CustomUser> FindByEmail(string email)
        {
            return await _mydbcontext.customUsers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<CustomUser> FindById(int id)
        {
            return await _mydbcontext.customUsers.FindAsync(id);
        }

        public async Task<bool> Validate(string email, string password)
        {
            return await _mydbcontext.customUsers.AnyAsync(x=>x.Email== email && x.Password == password);
        }
    }
}

using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SampleIdentityServer.UI.Services
{
    public class CustomProfileService : IProfileService
    {

        private readonly ICustomUserRepository _customUser;

        public CustomProfileService(ICustomUserRepository customUser)
        {
            _customUser = customUser;
        }
        /// <summary>
        /// idendityy içersindei userinfo methodu çalıştığında bu rası tetiklenecek 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _customUser.FindById(int.Parse(sub));
            var claims = new List<Claim>()
            { //bu alam içerisinde herşeyi eklemeye gerek yok sadece ihtiyaçlarımız kadırını eklemek daha iyi hatta veri tabanına gitmeden alabilcekelerimiz dahada iyi olur.
               new Claim(JwtRegisteredClaimNames.Email, user.Email),
               new Claim("name", user.UserName),
               new Claim("city", user.City)
            };

            if (user.Id==1)
            {
                claims.Add(new Claim("role", "admin"));
            }
            else
            {
                claims.Add(new Claim("role", "customer"));
            }

            context.AddRequestedClaims(claims);
            //context.IssuedClaims= claims; //bu property bu claimler verilir ise json web token gözükür fakat bu uygun olan  değildir.

        }
        /// <summary>
        /// user ıd kontrol edecek. veri tabanında subjectid=1 durumuna bakacak
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userid = context.Subject.GetSubjectId();
            var user = await _customUser.FindById(int.Parse(userid));

            context.IsActive=user!= null? true:false;
        }
    }
}

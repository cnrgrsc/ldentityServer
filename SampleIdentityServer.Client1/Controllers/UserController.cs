using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;

namespace SampleIdentityServer.Client1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {


            return View();
        }
        public async Task LogOut()
        {


            await HttpContext.SignOutAsync("Cookies"); // opt.DefaultScheme = "Cookies"; //bu şemalar uygulamada birden fazla cookie mekanizması olabilir. örneğpik bir kullanıcı bir bayi gibi. bunları ayırmak içinn default schme kullanılır. bunları startp tarafındn alıyoruz
            await HttpContext.SignOutAsync("oidc");
        }

        /// <summary>
        /// Refresh tokeni almak için ıdentity freamwork kullanıalrak yazıldı. bu yöntem ile kod tarafından refresh token yenileinir.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetRefreshToken()
        {

            HttpClient httpClient = new HttpClient();
            var discovry = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001"); //Discover Endpointe istek attık bütün url getirdik.

            if (discovry.IsError) //eğerki hata verirse url gidemezse burada log la
            {
                // discovry.Error.ToString(); // gelen hatayı discovery erro nesnesi üzerindne yakalarız 
            }

            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken); //cookie içerisindeki refresh token almak istediğimiz zaman bu yöntemi kullanırız. openıdconnectparamaternames ise bizim sabitlerimizi verir 

            RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest(); //isntance aldık
            refreshTokenRequest.ClientId = _configuration["Client1Mvc:ClientId"]; //appsttings json dan okuduk
            refreshTokenRequest.ClientSecret = _configuration["Client1Mvc:ClientSecret"];  
            refreshTokenRequest.RefreshToken = refreshToken; //cookie içerisindeki refresh tokeni aldık
            refreshTokenRequest.Address = discovry.TokenEndpoint; //discovery üzerin den tokenendpointe gittik

            var token=await httpClient.RequestRefreshTokenAsync(refreshTokenRequest); //refrehs token bir grant typ'inde gönderiri .biz bunu manuel yaptığımızda beliritriz bu methodu kullandığımızda belirmeyiz çünkü granty type ni kendi refresh token olarak gönderir.

            if (token.IsError)
            {
                //hata sayfasına yönlendirme
            }

            var tokens = new List<AuthenticationToken>()
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.IdToken,
                    Value=token.IdentityToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value=token.AccessToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.RefreshToken,
                    Value=token.RefreshToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.ExpiresIn,
                    Value=DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("O",CultureInfo.InvariantCulture),
                }
            };

            var authenticationResult = await HttpContext.AuthenticateAsync();

            var properties = authenticationResult.Properties;

            properties.StoreTokens(tokens);

            await HttpContext.SignInAsync("Cookies",authenticationResult.Principal,properties);

            return RedirectToAction("Index");
        }

        [Authorize(Roles ="admin")]
        public IActionResult AdminAction()
        {
            return View();
        }

        [Authorize(Roles = "admin,customer")]
        public IActionResult CustomerAction()
        {
            return View();
        }
    }



}

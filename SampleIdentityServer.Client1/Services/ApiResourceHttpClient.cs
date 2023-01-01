using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http;

namespace SampleIdentityServer.Client1.Services
{
    public class ApiResourceHttpClient : IApiResourceHttpClient
    {
        private readonly IHttpContextAccessor _httpContext;
        private HttpClient _client;

        public ApiResourceHttpClient(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _client= new HttpClient();
        }


        public async Task<HttpClient> GetHttpClient()
        {
            var accessToken = await _httpContext.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken); //cookie içerisindeki refresh token almak istediğimiz zaman bu yöntemi kullanırız. openıdconnectparamaternames ise bizim sabitlerimizi verir

            _client.SetBearerToken(accessToken);

            return _client;
        }
    }
}

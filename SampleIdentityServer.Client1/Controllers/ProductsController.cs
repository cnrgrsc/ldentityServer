using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using SampleIdentityServer.Client1.Models;
using SampleIdentityServer.Client1.Services;

namespace SampleIdentityServer.Client1.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApiResourceHttpClient _apiResourceHttp;

        public ProductsController(IConfiguration configuration, IApiResourceHttpClient apiResourceHttp)
        {
            _configuration = configuration;
            _apiResourceHttp = apiResourceHttp;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = await _apiResourceHttp.GetHttpClient();
            List <Product> products= new List<Product>();   
            

            //var discovry = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001"); //Discover Endpointe istek attık bütün url getirdik.

            //if (discovry.IsError) //eğerki hata verirse url gidemezse burada log la
            //{
            //   // discovry.Error.ToString(); // gelen hatayı discovery erro nesnesi üzerindne yakalarız 
            //}
            //ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest(); //ClientCredentialsTokenRequest bir nesne oluşturduk

            //clientCredentialsTokenRequest.ClientId = _configuration["Client:ClientId"]; //request nesnesine gitt appsettings.json içerisinden Client-Clientıd al gel
            //clientCredentialsTokenRequest.ClientSecret= _configuration["Client:ClientSecret"]; //request nesnesine gitt appsettings.json içerisinden Client-Secreti al gel
            //clientCredentialsTokenRequest.Address = discovry.TokenEndpoint; // diso nesnesşnde gideceğin token endpoint yolunu al ve bilgileri ver dedik

            //var token =  await httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken); //cookie içerisindeki refresh token almak istediğimiz zaman bu yöntemi kullanırız. openıdconnectparamaternames ise bizim sabitlerimizi verir 



            //if (token.IsError)
            //{

            //}
            //https://localhost:5001

           


            var response = await client.GetAsync("https://localhost:5016/api/product/getproduct"); //yukarıda tek tek topladığımız kullanıcı adı şifre gideceği endpoint ve acces tokenı artık burada istek atarak veriyoruz. Bu bilgileri al bu url git bak senin kullanıcı adın bu şifren bu gelen token bu artık bana product nesneleri içindeki bilgiyi ver diyoruz.

            if (response.IsSuccessStatusCode) //bu işlemler tamam ve 200 status kodu veriyorsan 
            {
                var content =await response.Content.ReadAsStringAsync();


                products = JsonConvert.DeserializeObject<List<Product>>(content); 

            }
            else //eğerki işler kötü gitti ve sen bana 200 statu kodu vermediysen o zaman hatayı logla bakalım bi reis 
            {
                //hata kodları loglanacak
            }

            return View(products);
        }
    }
}

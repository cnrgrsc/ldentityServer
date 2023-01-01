using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace SampleIdentityServer.UI
{
    public static class Config
    {
        /// <summary>
        /// bu method içerisinde biz kulllandığımız apilere izin veriyoruz.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResource()
        {
            return new List<ApiResource>() //cast işlemi
            {
                new ApiResource("resource_api1")
                {
                    Scopes=
                    { "api1.read", "api1.write", "api1.update" },
                    ApiSecrets= new[]{new Secret("secretapi1".Sha256()) }
                },
                new ApiResource("resource_api2")
                {
                    Scopes=
                    {"api2.read","api2.write","api2.update"}, //IdentityServer hangi api için hangi izin var öğrnemiş oldu bu kod satırı ile. Scopes Icollectin<string> aldığı için string olarak verildi.
                    ApiSecrets= new[]{new Secret("secretapi2".Sha256()) }
                }
            };
        }

        /// <summary>
        /// bu method içerisinde biz kullandığımız apilerin yapması gereken işlemler için izin veriyoruz.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("api1.read","API 1 için okuma izni verildi"),
                new ApiScope("api1.write","API 1 için yazma izni verildi"),
                new ApiScope("api1.update","API 1 için güncelleme izni verildi"),
                 new ApiScope("api2.read","API 2 için okuma izni verildi"),
                new ApiScope("api2.write","API 2 için yazma izni verildi"),
                new ApiScope("api2.update","API 2 için güncelleme izni verildi"),
            };
        }

        /// <summary>
        /// kullanıcıların bilgilerini aldığımız method
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), //open ıd bu token kimin hakkında. Kullanıcı işin içine girdiği zaman mecbur openıd olmak zorunda.
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name="CountryAndCity",
                    DisplayName="Country And City",
                    Description="Kullanıcın ülke ve şehir bilgisi",
                    UserClaims= new[] {"country","city"}
                },
                new IdentityResource()
                {
                    Name="Roles",
                    DisplayName="Roles",
                    Description="Kullanıcı Rolleri",
                    UserClaims= new[] {"role"}
                }
            };
        }
        /// <summary>
        /// test kullanıcılarını tutan method
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser() {SubjectId="1",Username="cnrgrsc",Password="password",Claims=new List<Claim>()
                    {
                        new Claim("given_name","Caner"),
                        new Claim("family_name","Güreşci"),
                        new Claim("country","Türkiye"),
                        new Claim("city","İstanbul"),
                        new Claim("role","admin")

                    }
                },
                new TestUser() {SubjectId="2",Username="ogzgrsc",Password="password",Claims=new List<Claim>()
                    {
                        new Claim("given_name","Oğuz"),
                        new Claim("family_name","Güreşci"),
                        new Claim("country","Türkiye"),
                        new Claim("city","İstanbul"),
                        new Claim("role","customer")
                    }
                }
            };
        }

        /// <summary>
        /// client uygulamalrımız tutan method
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId="Client1",
                    ClientName="Client app uygulaması",
                    ClientSecrets=new[]{new Secret("secret".Sha256())}, //sifeyi verip onu daha sonra sha256 ile hashledik. ICollectıon oldugu için dizin ile içine girdik
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes={"api1.read" } //bu properpty ile ben api bire bağlanayım ama okuma iznimde olsun veya 2 bağlanayım ama yazma iznimde olsun
                },
                new Client()
                {
                    ClientId="Client2",
                    ClientName="Client app uygulaması",
                    ClientSecrets=new[]{new Secret("secret".Sha256())}, //şifreyi secret verip sha256 ile hacledik geriye dönülmez diye dönmüyoruz. 
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes={"api1.read","api1.update","api2.write","api2.update" } //bu properpty ile ben api bire bağlanayım ama okuma iznimde olsun veya 2 bağlanayım ama yazma iznimde olsun
                },
                new Client()
                {
                    ClientId="Client1-Mvc",
                    RequirePkce=false,
                    ClientName="Client1-Mvc app uygulaması",
                    ClientSecrets=new[]{new Secret("secret".Sha256())}, //şifreyi secret verip sha256 ile hacledik geriye dönülmez diye dönmüyoruz.
                    AllowedGrantTypes=GrantTypes.Hybrid,
                    RedirectUris=new List<string>{ "https://localhost:5006/signin-oidc" }, //bu url token alma için işe yarar. Autheriztion server bu url döner ve otamatik döner identity server üzerinden token alınır. 
                    PostLogoutRedirectUris=new List<string>{ "https://localhost:5006/signout-callback-oidc" },
                    AllowedScopes={IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,"api1.read",IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"}, //bu starıda biz artık ıdnetity resources gidelim ve artık oradaki ıd ve porfile bilgilerini alalım diyoruz. ayrıca uzun bir şekilde sabiptler yani constlar üzerinden gitmeden "openid" yazsakda olur ama daha hatasız oolması için bu yöntemi kullanmak daha sağlıklı.
                    AccessTokenLifetime=2*60*60, //access token ömrünü verdik defaul oalrak 1 saat alır biz 2 saat verdik
                    AllowOfflineAccess=true, //truya set edildiğinde bir refresh token elde edilir
                    RefreshTokenUsage=TokenUsage.ReUse, //onetime olursa bir kez kullana bilirsin. Reuse olursa sürekli kullanırsın reuse oldugunda aynı refresh tokeni da gönderir
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds, //absult sabit zaman değeri verir defaul olarak 30 gün gelir biz 60 verdik 

                    RequireConsent=false, //onay sayfasını aktif hala getiriyoruz
                },
                new Client()
                {
                    ClientId="Client2-Mvc",
                    RequirePkce=false,
                    ClientName="Client2-Mvc app uygulaması",
                    ClientSecrets=new[]{new Secret("secret".Sha256())}, //şifreyi secret verip sha256 ile hacledik geriye dönülmez diye dönmüyoruz.
                    AllowedGrantTypes=GrantTypes.Hybrid,
                    RedirectUris=new List<string>{ "https://localhost:5011/signin-oidc" }, //bu url token alma için işe yarar. Autheriztion server bu url döner ve otamatik döner identity server üzerinden token alınır. 
                    PostLogoutRedirectUris=new List<string>{ "https://localhost:5011/signout-callback-oidc" },
                    AllowedScopes={IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,"api1.read","api2.read",IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"}, //bu starıda biz artık ıdnetity resources gidelim ve artık oradaki ıd ve porfile bilgilerini alalım diyoruz. ayrıca uzun bir şekilde sabiptler yani constlar üzerinden gitmeden "openid" yazsakda olur ama daha hatasız oolması için bu yöntemi kullanmak daha sağlıklı.
                    AccessTokenLifetime=2*60*60, //access token ömrünü verdik defaul oalrak 1 saat alır biz 2 saat verdik
                    AllowOfflineAccess=true, //truya set edildiğinde bir refresh token elde edilir
                    RefreshTokenUsage=TokenUsage.ReUse, //onetime olursa bir kez kullana bilirsin. Reuse olursa sürekli kullanırsın reuse oldugunda aynı refresh tokeni da gönderir
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds, //absult sabit zaman değeri verir defaul olarak 30 gün gelir biz 60 verdik 

                    RequireConsent=false, //onay sayfasını aktif hala getiriyoruz
                }
            };
        }
    }
}

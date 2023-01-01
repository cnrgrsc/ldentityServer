using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleIdentityServer.API2.Models;

namespace SampleIdentityServer.API2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        [HttpGet,Authorize]
        public IActionResult GetPictures()
        {
            var pictureLit = new List<Picture>()
            {
                new Picture { Id=1,Name="Doğa Resmi",Url="dogaresmi.jpg"},
                new Picture { Id=1,Name="Manzara Resmi",Url="dogaresmi.jpg"},
                new Picture { Id=1,Name="Deniz Resmi",Url="dogaresmi.jpg"},
                new Picture { Id=1,Name="Araba Resmi",Url="dogaresmi.jpg"},
                new Picture { Id=1,Name="Suluboya Resmi",Url="dogaresmi.jpg"},
            };
            return Ok(pictureLit);
        }
    }
}

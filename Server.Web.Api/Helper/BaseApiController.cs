using Microsoft.AspNetCore.Mvc;

namespace Server.Web.Api.Helper;

[Route("api/[controller]/[action]")]
[ApiController]
public class BaseApiController : Controller
{
    public string BaseUrlSite => $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
}
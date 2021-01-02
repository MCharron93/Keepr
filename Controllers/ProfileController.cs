using Keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Mvc;
using Keepr.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("[controller]")]

  public class ProfilesController : ControllerBase
  {

    private readonly ProfileService _ps;

    public ProfilesController(ProfileService ps)
    {
      _ps = ps;
    }

    [HttpGet]
    [Authorize]
    // NOTE this authorization will get back a profile, adds that layer of security on the requests.
    public async Task<ActionResult<Profile>> Get()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_ps.GetOrCreateProfile(userInfo));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    // Will need to GetProfileById for FE viewing of other's public profiles

    // [HttpGet("{id}/blogs")]
    // [Authorize]
    // // NOTE this authorization will get back a profile, adds that layer of security on the requests.
    // public async Task<ActionResult<Profile>> GetBlogsByProfile(string id)
    // {
    //   try
    //   {
    //     Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
    //     return Ok(_ps.GetBlogsByProfile(id, userInfo?.Id));
    //   }
    //   catch (System.Exception err)
    //   {
    //     return BadRequest(err.Message);
    //   }
    // }
  }
}
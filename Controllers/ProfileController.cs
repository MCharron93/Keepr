using Keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Mvc;
using Keepr.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class ProfilesController : ControllerBase
  {

    private readonly ProfileService _ps;
    private readonly KeepsService _ks;
    private readonly VaultService _vs;

    public ProfilesController(ProfileService ps, KeepsService ks, VaultService vs)
    {
      _ps = ps;
      _ks = ks;
      _vs = vs;
    }

    [HttpGet]
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

    [HttpGet("{creatorId}/keeps")]
    public ActionResult<IEnumerable<Keep>> GetKeepsByProfileId(string creatorId)
    {
      try
      {
        return Ok(_ks.GetKeepsByProfileId(creatorId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{creatorId}/vaults")]
    public ActionResult<IEnumerable<Vault>> GetVaultsByProfileId(string creatorId)
    {
      try
      {
        return Ok(_vs.GetVaultsByProfileId(creatorId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // Will need to GetProfileById for FE viewing of other's public profiles

    [HttpGet("{id}")]
    public ActionResult<Profile> GetProfileById(string id)
    {
      try
      {
        return Ok(_ps.GetProfileById(id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}
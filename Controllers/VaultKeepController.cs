using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    private readonly KeepsService _ks;

    public VaultKeepsController(VaultKeepsService vks, KeepsService ks)
    {
      _vks = vks;
      _ks = ks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVKS)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVKS.CreatorId = userInfo.Id;
        _ks.updateKeepCount(newVKS.KeepId);
        VaultKeep created = _vks.Create(newVKS);
        return Ok(created);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    //  Delete will go in here, get by Id will got somewhere else
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        _ks.decreaseKeepCount(id);
        return Ok(_vks.Delete(id, userInfo.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> GetVaultKeepById(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_vks.GetVaultKeepById(id, userInfo));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }

}
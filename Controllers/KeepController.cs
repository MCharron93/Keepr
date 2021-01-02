using System.Collections.Generic;
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

  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    private readonly ProfileService _ps;

    public KeepsController(KeepsService ks, ProfileService ps)
    {
      _ks = ks;
      _ps = ps;
    }

    // Basic Get to display all keeps, not required to be logged in
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> GetAllKeeps()
    {
      try
      {
        return Ok(_ks.GetAllKeeps());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // GetById to display details about product
    [HttpGet("{id}")]
    public ActionResult<Keep> GetSingleKeep(int id)
    {
      try
      {
        return Ok(_ks.GetSingleKeep(id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // Post used to create new Keep
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep newKeep)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newKeep.CreatorId = userInfo.Id;
        _ps.updateKeepCount(userInfo);
        Keep created = _ks.CreateKeep(newKeep);
        return Ok(created);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // Put used to increase view/keep count when added to vaults
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> EditKeep(int id, [FromBody] Keep editData)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        editData.Id = id;
        return Ok(_ks.Edit(editData, userInfo));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // Delete for single Keep, checks to see if you are the user
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        _ps.deleteKeepCount(userInfo);
        return Ok(_ks.Delete(id, userInfo.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // NOTE will need to get KeepsByProfileId
  }
}
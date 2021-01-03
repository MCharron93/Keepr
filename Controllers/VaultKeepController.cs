// using System.Threading.Tasks;
// using Keepr.Models;
// using Keepr.Services;
// using CodeWorks.Auth0Provider;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace Keepr.Controllers
// {
//   [ApiController]
//   [Route("api/[controller]")]
//   public class VaultKeepsController : ControllerBase
//   {
//     private readonly VaultKeepsService _vks;

//     public VaultKeepsController(VaultKeepsService vks)
//     {
//       _vks = vks;
//     }

//     [HttpPost]
//     [Authorize]
//     public async Task<ActionResult<VaultKeeps>> Create([FromBody] VaultKeeps newVKS)
//     {
//       try
//       {
//         Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
//         newVKS.CreatorId = userInfo.Id;
//         VaultKeeps created = _vks.Create(newVKS);
//         return Ok(created);
//       }
//       catch (System.Exception e)
//       {
//         return BadRequest(e.Message);
//       }
//     }

//  Delete will go in here, get by Id will got somewhere else

//   }
// }
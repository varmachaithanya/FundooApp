using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBusiness _business;

        public CollabController(ICollabBusiness business)
        {
            _business = business;
        }

        [Authorize]
        [HttpPost]
        [Route("Addcollaborator")]

        public IActionResult Addcollaborator(long Noteid, string collaboratorEmail) 
        { 
            var userid=long.Parse(User.Claims.Where(x=>x.Type== "UserId").FirstOrDefault().Value);

            var result=_business.AddCollobrator(userid,Noteid,collaboratorEmail);
            if (result != null)
            {
                return Ok("Collaborator Added...");
            }
            return BadRequest("Collaborator Not Added...");
        }

        [Authorize]
        [HttpDelete]
        [Route("Deletecolaborator")]

        public IActionResult Deletecollaborator(long Noteid,long ColaboratorId) 
        {
            var delete = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result=_business.Deletecolaborator(delete,Noteid,ColaboratorId);
            if (result != null) 
                {
                return Ok("Collaborator Deleted");
                }
            return BadRequest("Collaborator Not Found...");

        }

        [Authorize]
        [HttpGet]
        [Route("GetCollaboratorbyid")]

        public IActionResult GetcollaboratorbyId(long Noteid) 
        {
            var get = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result=_business.GetCollaboratorById(get,Noteid);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Collaborators Not Found...");
        }

        [Authorize]
        [HttpGet]
        [Route("GetCollaborator")]

        public IActionResult Getcollaborator(long userid)
        {
            var result = _business.GetCollaborators(userid);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Collaborators Not Found...");
        }
    }
}

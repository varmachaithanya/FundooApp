using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Entities;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBusiness business;

        public NotesController(INotesBusiness business)
        {
            this.business = business;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateNotes")]
        public IActionResult CreateNote([FromForm]NotesModel request)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            //var userid=HttpContext.Session.GetInt32("UserId").Value;
            //long user = (long)userid;

            business.CreateNotes(request,userid);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteNote")]

        public IActionResult DeleteNote(long userid,long noteid) 
        {
            var delete=business.DeleteNote(userid,noteid);
            if (delete)
            {
                return Ok("Note Deleted Sucessfully...");
            }

            return BadRequest("User Not Found...");
        }

        [HttpPut]
        [Route("UpdateNote")]

        public IActionResult UpdateNote(long userid,long noteid, [FromForm] NotesModel request)
        {
            var update=business.UpdateNote(userid,noteid,request);
            if (update)
            {
                return Ok("Note Updated Sucessfully...");
            }
            return BadRequest("User Not Found...");
        }

        [HttpGet]
        [Route("GetAllNotes")]

        public IActionResult GetNote()
        {
            var getNotes = business.GetAllNotes();
            if (getNotes != null)
            {
                return Ok(getNotes);
            }
            return BadRequest("Not Found...");
        }

        [HttpGet]
        [Route("GetByDate")]

        public IActionResult GetBydate(long userid,DateTime date)
        {
            var result = business.GetByDate(userid, date);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest("Not Found...");

        }

        [Authorize]
        [HttpPut]
        [Route("Toggeltrash")]
        public IActionResult ToggelTrash(long noteid) 
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result=business.ToggelTrash(userid, noteid);
            if(result != null)
            {
                return Ok("Toggled");
            }
            return BadRequest("Not Toggled, Note Not Found..!");
        }
        [Authorize]
        [HttpPost]
        [Route("AddColour")]

        public IActionResult Addcolor(long noteid,string color)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            //// Retrieve the value from the session
            //var userIdNullable = HttpContext.Session.GetInt32("UserId");

            //// Parse the value as a long, providing a default value of 0 if it's null
            //var userId = userIdNullable.HasValue ? (long)userIdNullable.Value : 1;

            var note =business.AddColor(userid, noteid,color);
            if (note != null)
            {
                return Ok("Color Updated");
            }
            return BadRequest("Color Not Updated..!");
        }

        [Authorize]
        [HttpPost]
        [Route("Toggelpin")]

        public IActionResult Togglepin(long noteid)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var pin =business.ToggelPin(userid, noteid);
            if(pin != null)
            {
                return Ok("Pinned...");
            }
            return BadRequest("Something Went Wrong...");
        }

        [Authorize]
        [HttpPost]
        [Route("Toggelarchive")]
        public IActionResult ToggleArchive(long noteid)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var archive =business.ToggelArchive(userid, noteid);

            if(archive != null)
            {
                return Ok("Archived...");
            }
            return BadRequest("Something Went Wrong..!");
        }
       
    }
}

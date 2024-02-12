using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RabbitMQ.Client;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        private readonly IGetBusiness busines;

        private readonly ILogger<GetController> logger;


        public GetController(IGetBusiness busines, ILogger<GetController> logger)
        {
            this.busines = busines;
            this.logger = logger;
        }

        [HttpPut]
        [Route("CreateOrUpdate")]

        public IActionResult Getdata(int id,UserModel model) 
        {
            logger.LogInformation("Get data From User");
            var data=busines.GetNote(id,model);
            if (data != null)
            {
                logger.LogWarning("User data Not Found");
                logger.LogError("user not found");

                return Ok(data);

            }
            return BadRequest();
        }

        [HttpGet("GetByCharacter")]

        public IActionResult getChar(string character)
        {
            var charget=busines.getByChar(character);

            if(charget != null)
            {
                return Ok(charget);
            }

            return BadRequest();
        }
        [HttpGet]
        [Route("GetByTitle")]
        public IActionResult Getdata(string title)
        {
            var data = busines.GetNoteByTitle(title);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("Title Not Found");
        }

        



    }
}

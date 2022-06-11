using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ResponseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        //GET /api/response/100
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult GetAResponse(int id)
        {
            var rnd = new Random();
            if (rnd.Next(1, 101) >= id)
            {
                Console.WriteLine("--> Failure - Generate a HTTP 500");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Console.WriteLine("--> Successfull - Generate a HTTP 200");
            return Ok();
        }
    }
}
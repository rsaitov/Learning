using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        //private readonly ClientPolicy _clientPolicy;
        private readonly IHttpClientFactory _clientFactory;

        public RequestController(
            //ClientPolicy clientPolicy, 
            IHttpClientFactory clientFactory)
        {
            //_clientPolicy = clientPolicy;
            _clientFactory = clientFactory;
        }
        // GET /api/request        
        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            // -= HttpClientFactory section =-

            var client = _clientFactory.CreateClient("TestPolicy");

            var response = await client.GetAsync("https://localhost:7035/api/response/25");

            // -=============================-

            // -= new HttpClient() section =-

            // var client = new HttpClient();
            // no polly request
            //var response = await client.GetAsync("https://localhost:7035/api/response/25");

            // immediate request
            // var response = await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(
            //     () => client.GetAsync("https://localhost:7035/api/response/25")
            // );

            // response after fixed delay
            // var response = await _clientPolicy.LinearHttpRetry.ExecuteAsync(
            //     () => client.GetAsync("https://localhost:7035/api/response/25")
            // );

            // response after delay with exponential delay
            // var response = await _clientPolicy.ExponentialHttpRetry.ExecuteAsync(
            //     () => client.GetAsync("https://localhost:7035/api/response/25")
            // );

            // -=============================-

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> ResponseService returned SUCCESS");
                return Ok();
            }

            Console.WriteLine("--> ResponseService returned FAILURE");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
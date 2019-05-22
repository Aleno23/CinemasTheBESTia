using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CinemasTheBESTia.CinemaBooking.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ICinemasFunctionsService _cinemaFunctionsService;

        public BookingController(ICinemasFunctionsService cinemaFuctionsService)
        {
            _cinemaFunctionsService = cinemaFuctionsService;
        }

        // GET api/originalTitle
        [HttpGet("{originalTitle}")]
        public async Task<IActionResult> Get(string originalTitle)
        {
            if (string.IsNullOrWhiteSpace(originalTitle))
            {
                return BadRequest();
            }
            return Ok(await _cinemaFunctionsService.GetFuctions(originalTitle));
        }



        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemasTheBESTia.Entities.CinemaFunctions;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemasTheBESTia.CinemaBooking.API.Controllers
{
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

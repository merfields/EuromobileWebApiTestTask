using EuromobileTestTaks.Domain.Models;
using EuromobileTestTask.WebApi.DTOs;
using EuromobileTestTask.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EuromobileTestTask.WebApi.Controllers
{
    [Route("api/coordinates")]
    [ApiController]
    public class CoordinatesController : ControllerBase
    {
        private readonly ICoordinatesRepository _coordinatesRepository;

        public CoordinatesController(ICoordinatesRepository coordinatesRepository)
        {
            _coordinatesRepository = coordinatesRepository;
        }


        // GET: api/coordinates?count=<int>
        [HttpGet]
        public ActionResult<List<CoordinatesDTO>> Get(int count)
        {
            if (count < 1)
            {
                return BadRequest("Number of coordinates should more than 0");
            }

            Coordinate[] generatedCoordinates = new Coordinate[count];
            CoordinatesDTO[] coordinatesDTOs = new CoordinatesDTO[count];

            for (int i = 0; i < count; i++)
            {
                generatedCoordinates[i] = _coordinatesRepository.GenerateCoordinates();
                coordinatesDTOs[i] = MapCoordinatesToDTO(generatedCoordinates[i]);
            }

            return Ok(coordinatesDTOs);
        }

        private CoordinatesDTO MapCoordinatesToDTO(Coordinate coordinate)
        {
            return new CoordinatesDTO { Latitude = coordinate.Latitude, Longitude = coordinate.Longitude };
        }

        // POST api/coordinates
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}

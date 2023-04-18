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

        /// <summary>
        /// Generates pairs of random coordinates
        /// </summary>
        /// <param name="count"></param>
        /// <returns>Returns an array of coordinates</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If user entered a number less than 1</response>

        // GET: api/coordinates?count=<int>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CoordinatesDTO[]> GetRandomCoordinates(int count)
        {
            if (count < 1)
            {
                return BadRequest("Number of coordinates should be more than 0");
            }

            Coordinate[] generatedCoordinates = new Coordinate[count];
            CoordinatesDTO[] coordinatesDTOs = new CoordinatesDTO[count];

            for (int i = 0; i < count; i++)
            {
                generatedCoordinates[i] = _coordinatesRepository.GenerateCoordinates();
                coordinatesDTOs[i] = MapCoordinateModelToDTO(generatedCoordinates[i]);
            }

            return Ok(coordinatesDTOs);
        }

        /// <summary>
        /// Calculates the total distance between all coordinates from the array
        /// </summary>
        /// <param name="coordinateDTOs"></param>
        /// <returns>Returns a DTO with total distance between coordinates in meters </returns>
        /// <response code="200">Success</response>

        // POST api/coordinates
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<TotalDistanceDTO> GetTotalDistanceBetweenCoordinates(CoordinatesDTO[] coordinateDTOs)
        {
            if (coordinateDTOs.Length < 2)
            {
                return new TotalDistanceDTO { Meters = 0, Miles = 0 };
            }

            Coordinate[] coordinates = new Coordinate[coordinateDTOs.Length];
            for (int i = 0; i < coordinateDTOs.Length; i++)
            {
                coordinates[i] = MapCoordinateDTOToModel(coordinateDTOs[i]);
            }

            TotalDistance totalDistance = new TotalDistance();
            for (int i = 0; i < coordinates.Length - 1; i++)
            {
                totalDistance.Meters += _coordinatesRepository.CalculateDistanceBetweenTwoCoordinatesInMeteres(coordinates[i], coordinates[i + 1]);
            }
            totalDistance.Miles = _coordinatesRepository.ConvertMetersToMiles(totalDistance.Meters);

            TotalDistanceDTO totalDistanceDTO = MapTotalDistanceModelToDTO(totalDistance);
            return Ok(totalDistanceDTO);
        }


        private CoordinatesDTO MapCoordinateModelToDTO(Coordinate coordinate)
        {
            return new CoordinatesDTO { Latitude = coordinate.Latitude, Longitude = coordinate.Longitude };
        }
        private Coordinate MapCoordinateDTOToModel(CoordinatesDTO coordinateDTO)
        {
            return new Coordinate { Latitude = coordinateDTO.Latitude, Longitude = coordinateDTO.Longitude };
        }
        private TotalDistanceDTO MapTotalDistanceModelToDTO(TotalDistance totalDistance)
        {
            return new TotalDistanceDTO { Meters = totalDistance.Meters, Miles = totalDistance.Miles };
        }
    }
}

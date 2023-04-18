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
        public ActionResult<List<CoordinatesDTO>> GetRandomCoordinates(int count)
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
                coordinatesDTOs[i] = MapCoordinateModelToDTO(generatedCoordinates[i]);
            }

            return Ok(coordinatesDTOs);
        }


        // POST api/coordinates
        [HttpPost]
        public ActionResult<SumDistanceDTO> GetSumDistanceBetweenCoordinates(CoordinatesDTO[] coordinateDTOs)
        {
            if (coordinateDTOs.Length < 2)
            {
                return new SumDistanceDTO { Metres = 0, Miles = 0 };
            }

            Coordinate[] coordinates = new Coordinate[coordinateDTOs.Length];
            for (int i = 0; i < coordinateDTOs.Length; i++)
            {
                coordinates[i] = MapCoordinateDTOToModel(coordinateDTOs[i]);
            }

            SumDistance sumDistance = new SumDistance();
            for (int i = 0; i < coordinates.Length - 1; i++)
            {
                sumDistance.Metres += _coordinatesRepository.CalculateDistanceBetweenTwoCoordinatesInMetres(coordinates[i], coordinates[i + 1]);
            }
            sumDistance.Miles = _coordinatesRepository.ConvertMetresToMiles(sumDistance.Metres);

            SumDistanceDTO sumDistanceDTO = MapSumDistanceModelToDTO(sumDistance);
            return Ok(sumDistanceDTO);
        }


        private CoordinatesDTO MapCoordinateModelToDTO(Coordinate coordinate)
        {
            return new CoordinatesDTO { Latitude = coordinate.Latitude, Longitude = coordinate.Longitude };
        }
        private Coordinate MapCoordinateDTOToModel(CoordinatesDTO coordinateDTO)
        {
            return new Coordinate { Latitude = coordinateDTO.Latitude, Longitude = coordinateDTO.Longitude };
        }
        private SumDistanceDTO MapSumDistanceModelToDTO(SumDistance sumDistance)
        {
            return new SumDistanceDTO { Metres = sumDistance.Metres, Miles = sumDistance.Miles };
        }
    }
}

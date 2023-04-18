using EuromobileTestTaks.Domain.Models;
using EuromobileTestTask.WebApi.DTOs;
using EuromobileTestTask.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace EuromobileTestTask.WebApi.Controllers;

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
    /// <response code="400">User entered a number less than 1</response>

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
    public ActionResult<TotalDistanceDTO> GetTotalDistanceBetweenCoordinates(CoordinatesDTO[] coordinatesDTOs)
    {
        if (coordinatesDTOs.Length < 2)
        {
            return new TotalDistanceDTO { Meters = 0, Miles = 0 };
        }

        Coordinate[] coordinates = new Coordinate[coordinatesDTOs.Length];
        for (int i = 0; i < coordinatesDTOs.Length; i++)
        {
            coordinates[i] = MapCoordinateDTOToModel(coordinatesDTOs[i]);
        }

        TotalDistance totalDistance = _coordinatesRepository.CalculateTotalDistanceBetweenCoordinates(coordinates);

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


﻿using System.ComponentModel.DataAnnotations;

namespace EuromobileTestTask.WebApi.DTOs;

public class CoordinatesDTO
{
    [Range(-90, 90)]
    public double Latitude { get; set; }

    [Range(-180, 180)]
    public double Longitude { get; set; }
}


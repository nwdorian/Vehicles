﻿using System.ComponentModel.DataAnnotations;

namespace Vehicles.Model;
public class Vehicle
{
    public Guid Id { get; set; }
    public Guid? MakeId { get; set; }
    public string? Model { get; set; }
    [Required]
    public string? Color { get; set; }
    public DateTime Year { get; set; }
    [Required]
    public bool ForSale { get; set; }
}
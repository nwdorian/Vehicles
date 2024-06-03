using System.ComponentModel.DataAnnotations;

namespace Vehicles.WebApi.Models;

public class Vehicle
{
    public Guid Id { get; set; }
    public string? Model { get; set; }
    [Required]
    public string? Colour { get; set; }
    public DateTime Year { get; set; }
    public Guid MakeId { get; set; }
    public Make Make { get; set; }
}

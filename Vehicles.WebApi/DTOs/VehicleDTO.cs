namespace Vehicles.WebApi.Models;

public class VehicleDTO
{
    public Guid Id { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public DateTime Year { get; set; }
    public bool ForSale { get; set; }
    public MakeDTO? Make { get; set; }
}

namespace Vehicles.WebApi.Models;

public class VehicleGetRest
{
    public string? Model { get; set; }
    public string? Color { get; set; }
    public DateTime Year { get; set; }
    public bool ForSale { get; set; }
    public string? Make { get; set; }
}

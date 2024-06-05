namespace Vehicles.Common;
public class Filtering
{
    public Guid? MakeId { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? ForSale { get; set; }
    public string? SearchQuery { get; set; }
}

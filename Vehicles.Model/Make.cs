using System.ComponentModel.DataAnnotations;

namespace Vehicles.Model;
public class Make
{
    public Guid Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public List<Vehicle> Vehicles { get; set; }
}

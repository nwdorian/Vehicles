using System.ComponentModel.DataAnnotations;

namespace Vehicles.Model;
public class Make
{
    public Guid Id { get; set; }
    [Required]
    public string? Name { get; set; }
}

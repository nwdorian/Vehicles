namespace Vehicles.Common;
public class Sorting
{
    public string? OrderBy { get; set; }
    public string? SortOrder { get; set; }
    public Sorting(string orderBy, string sortOrder)
    {
        OrderBy = orderBy;
        SortOrder = sortOrder;
    }
}

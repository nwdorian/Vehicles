namespace Vehicles.Common;
public class Paging
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public Paging(int pageSize, int pageNumber)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
}
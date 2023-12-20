namespace Comp2001.DTOs
{
    public class LocationReadDTO
    {
        public int LocationId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class LocationCreateDTO
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}

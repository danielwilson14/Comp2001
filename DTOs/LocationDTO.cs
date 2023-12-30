namespace Comp2001.DTOs
{
    public class LocationReadDTO
    {
        public int LocationId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();

    }

    public class LocationCreateDTO
    {
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class LocationUpdateDTO
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}

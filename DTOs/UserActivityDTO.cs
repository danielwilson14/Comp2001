namespace Comp2001.DTOs
{
    public class UserActivityReadDTO
    {
        public int UserActivityId { get; set; }
        public int UserId { get; set; }
        public string ActivityName { get; set; }
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();

    }

    public class UserActivityCreateDTO
    {
        public int UserId { get; set; }
        public string ActivityName { get; set; }
    }
    public class UserActivityUpdateDTO
    {
        public int UserId { get; set; }
        public string ActivityName { get; set; }
    }

}

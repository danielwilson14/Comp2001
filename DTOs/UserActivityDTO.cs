namespace Comp2001.DTOs
{
    public class UserActivityReadDTO
    {
        public int UserActivityId { get; set; }
        public int UserId { get; set; }
        public string Activity { get; set; }
    }

    public class UserActivityCreateDTO
    {
        public int UserId { get; set; }
        public string Activity { get; set; }
    }

}

namespace Comp2001.DTOs
{
    public class UserPreferencesReadDTO
    {
        public int UserId { get; set; }
        public string Units { get; set; }
        public string ActivityTimePreference { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string MarketingLanguage { get; set; }
    }

    public class UserPreferencesCreateDTO
    {
        public int UserId { get; set; }
        public string Units { get; set; }
        public string ActivityTimePreference { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string MarketingLanguage { get; set; }
    }
    public class UserPreferencesUpdateDTO
    {
        public string Units { get; set; }
        public string ActivityTimePreference { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string MarketingLanguage { get; set; }
    }

}

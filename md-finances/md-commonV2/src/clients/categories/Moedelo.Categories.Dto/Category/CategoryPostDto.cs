namespace Moedelo.Categories.Dto.Category
{
    public class CategoryPostDto
    {
        public int AccountId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsEnabled { get; set; }
        
        // public TaskCategoryTimeType WorkTo { get; set; }
        // public int StartInMinutes { get; set; }
        // public int AdditionalWorkMinutes { get; set; }
        
        public CategoryDeadlineType Deadline { get; set; }
        public int WorkDays { get; set; }
        public int WorkHours { get; set; }
        public int WorkMinutes { get; set; }
        public bool IsWorkTime { get; set; }
    }
}

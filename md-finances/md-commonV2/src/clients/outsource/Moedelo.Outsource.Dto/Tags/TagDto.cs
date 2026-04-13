using Moedelo.Outsource.Dto.Tags.Enums;

namespace Moedelo.Outsource.Dto.Tags
{
    public class TagDto
    {
        public int Id { get; set; }
        
        public int AccountId { get; set; }
        
        public string Name { get; set; }
        
        public TagColor Color { get; set; }
        
        public string CreateDate { get; set; }
        
        public string ModifyDate { get; set; }
    }
}
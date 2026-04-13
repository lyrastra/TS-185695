namespace Moedelo.Outsource.Dto.PageAccess
{
    public class GroupSettingPostDto
    {
        public int AccountId { get; set; }
        
        public int GroupId { get; set; }
        
        public GroupSettingItemPostDto[] Settings { get; set; }
    }
}
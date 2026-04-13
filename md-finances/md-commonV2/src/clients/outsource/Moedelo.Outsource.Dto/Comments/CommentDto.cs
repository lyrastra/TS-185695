namespace Moedelo.Outsource.Dto.Comments
{
    public class CommentDto
    {
        public int Id { get; set; }
        
        public int EmployeeId { get; set; }

        public int AccountId { get; set; }
        
        public int GroupId { get; set; }
        
        public string Text { get; set; }

        public string Color { get; set; }
        
        public int ChangesCount { get; set; }
        
        public int[] TagsIds { get; set; }

        public string CreateDate { get; set; }
        
        public string ModifyDate { get; set; }
    }
}
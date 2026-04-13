namespace Moedelo.Workflow.Dto.Users
{
    public class WorkflowUserDto
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public int AccountId { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }

        public bool IsDeleted { get; set; }
    }
}
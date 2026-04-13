namespace Moedelo.Workflow.Dto.Accounts
{
    public class AccountDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public int? ContextUserId { get; set; }
    }
}

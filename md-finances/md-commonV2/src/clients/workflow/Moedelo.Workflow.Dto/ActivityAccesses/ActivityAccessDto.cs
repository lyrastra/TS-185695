using System;

namespace Moedelo.Workflow.Dto.ActivityAccesses
{
    public class ActivityAccessDto
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public string ActivityKey { get; set; }

        public DateTime CreateDate { get; set; }
    }
}

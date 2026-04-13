using System;

namespace Moedelo.HomeV2.Dto.Operator
{
    public class OperatorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? Date { get; set; }

        public bool IsFired { get; set; }

        public string Email { get; set; }

        public string Post { get; set; }

        public int GroupId { get; set; }

        public int DepartmentId { get; set; }

        public bool IsActive { get; set; }
    }
}
using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs
{
    public class EfsWorkerDto
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
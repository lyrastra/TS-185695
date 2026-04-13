namespace Moedelo.SuiteCrm.Dto.Tasks
{
    public class CreateTaskNoTimeDto
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Fio { get; set; }
    }
}
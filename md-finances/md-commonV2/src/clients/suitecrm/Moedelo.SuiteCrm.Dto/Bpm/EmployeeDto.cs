namespace Moedelo.SuiteCrm.Dto.Bpm
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string FullName
        {
            get => $"{Name} {Surname}".Trim();
        }

        public string Login { get; set; }
    }
}

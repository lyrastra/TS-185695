namespace Moedelo.Outsource.Dto.Employees
{
    public class EmployeePostDto
    {
        public int AccountId { get; set; }
        
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Login { get; set; }

        public int PositionId { get; set; }

        public int DepartmentId { get; set; }

        public int? HeadId { get; set; }

        public string Phone { get; set; }

        public int RoleId { get; set; }

        public string Email { get; set; }

        public int? PhotoId { get; set; }

        /// <summary>
        /// Список доступных значений (на момент создания класса):
        /// https://github.com/moedelo/md-outsource-shared/blob/13248397df0517532488c25a00a8d9e423de6563/src/Moedelo.Outsource.Enums/Employees/EmployeeStatusType.cs#L3
        /// </summary>
        public string Status { get; set; }

        public int? AdditionalNumber { get; set; }
    }
}
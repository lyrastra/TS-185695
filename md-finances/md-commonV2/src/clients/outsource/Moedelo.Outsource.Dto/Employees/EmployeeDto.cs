namespace Moedelo.Outsource.Dto.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        
        public int AccountId { get; set; }
        
        public int UserId { get; set; }
        
        public string Login { get; set; }

        public int PositionId { get; set; }
        
        public int DepartmentId { get; set; }
        
        public int? HeadId { get; set; }

        public int RoleId { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
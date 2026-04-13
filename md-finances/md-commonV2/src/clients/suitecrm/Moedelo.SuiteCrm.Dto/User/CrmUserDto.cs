namespace Moedelo.SuiteCrm.Dto.User
{
    public class CrmUserDto
    {
        public string Id { get; set; }

        public string Login { get; set; }

        public int? MdOperatorId { get; set; }

        public string Department { get; set; }

        public int? CallerId { get; set; }
    }
}

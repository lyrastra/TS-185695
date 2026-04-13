namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class FirmShortInfoDto
    {
        public int Id { get; set; }

        public bool IsOoo { get; set; }

        public string Inn { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Phone { get; set; }

        public bool IsInternal { get; set; }

        public int LegalUserId { get; set; }

        public int OperatorId { get; set; }

        public int PartnerId { get; set; }
    }
}

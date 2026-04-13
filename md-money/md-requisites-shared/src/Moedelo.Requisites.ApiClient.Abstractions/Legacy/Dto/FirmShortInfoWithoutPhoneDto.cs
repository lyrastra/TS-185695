namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public sealed class FirmShortInfoWithoutPhoneDto
    {
        public int Id { get; set; }

        public bool IsOoo { get; set; }

        public string Inn { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public bool IsInternal { get; set; }
    }
}

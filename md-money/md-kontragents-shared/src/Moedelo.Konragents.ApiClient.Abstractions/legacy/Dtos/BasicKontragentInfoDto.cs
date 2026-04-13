using Moedelo.Konragents.Enums;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos
{
    public class BasicKontragentInfoDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public KontragentForm? Form { get; set; }

        public long? SubcontoId { get; set; }
    }
}
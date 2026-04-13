using Moedelo.Konragents.Enums;

namespace Moedelo.Money.Business.Kontragents
{
    internal sealed class Kontragent
    {
        public int Id { get; set; }
        public KontragentForm? Form { get; set; }
        public long? SubcontoId { get; set; }
    }
}
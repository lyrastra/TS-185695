namespace Moedelo.Money.Providing.Business.Abstractions.Models
{
    public class SetReserveRequest
    {
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Резерв (данная величина уменьшает сумму, покрываемую первичными документами)
        /// </summary>
        public decimal? ReserveSum { get; set; }
    }
}
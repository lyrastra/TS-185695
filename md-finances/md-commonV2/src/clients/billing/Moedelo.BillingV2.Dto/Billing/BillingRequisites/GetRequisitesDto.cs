namespace Moedelo.BillingV2.Dto.Billing.BillingRequisites
{
    public class GetRequisitesDto
    {
        /// 1-МоёДело, 2-ГлавУчет
        public int RequisitesId { get; set; } 
    }

    public static class MoedeloRequisitesIds
    {
        public static readonly int MoeDelo = 1;
        public static readonly int GlavUchet = 2;
    }
}
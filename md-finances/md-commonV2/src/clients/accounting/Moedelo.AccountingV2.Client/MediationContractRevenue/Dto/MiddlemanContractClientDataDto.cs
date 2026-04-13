
namespace Moedelo.AccountingV2.Client.MediationContractRevenue.Dto
{
    public class MiddlemanContractClientDataDto
    {
        /// <summary>
        /// Id договора посредничества
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// DocumentBaseId договора посредничества
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Id контрагента в договоре поредничества
        /// </summary>
        public int MiddlemanId { get; set; }

        /// <summary>
        /// Имя контрагента в договоре поредничества
        /// </summary>
        public string MiddlemanName { get; set; }

        /// <summary>
        /// Номер договора посредничества
        /// </summary>
        public string ContractNumber { get; set; }
    }
}

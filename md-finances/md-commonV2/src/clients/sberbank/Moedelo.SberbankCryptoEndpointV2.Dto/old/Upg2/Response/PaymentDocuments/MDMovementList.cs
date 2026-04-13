using System.Collections.Generic;
using Newtonsoft.Json;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Response.PaymentDocuments
{
    public class MDMovementList
    {
        /// <summary> Выписка в 1с </summary>
        public string Content1C { get; set; }

        /// <summary> Расчётный счёт </summary>
        public string SettlementAccount { get; set; }

        /// <summary> Банк из которого пришла выписка </summary>
        public string BankName { get; set; }

        /// <summary> Банк из которого пришла выписка </summary>
        public string BankBik { get; set; }

        /// <summary> Начальный остаток денежных средств </summary>
        [JsonProperty("StartBalance", NullValueHandling = NullValueHandling.Ignore)]
        public string StartBalance { get; set; }

        /// <summary> Конечный остаток денежных средств </summary>
        public string Balance { get; set; }

        /// <summary> Дата начала выписки </summary>
        [JsonProperty("StartDate", NullValueHandling = NullValueHandling.Ignore)]
        public string StartDate { get; set; }

        /// <summary> Дата конца выписки </summary>
        public string EndDate { get; set; }

        /// <summary> Секция документов </summary>
        public List<DocumentSection> Documents { get; set; }

        public MDMovementList()
        {
            SettlementAccount = string.Empty;
            Documents = new List<DocumentSection>();
        }
    }
}
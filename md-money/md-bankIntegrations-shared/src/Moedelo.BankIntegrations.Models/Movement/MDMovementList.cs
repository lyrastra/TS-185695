using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Moedelo.BankIntegrations.Models.Movement
{
    /// <summary> 
    /// Секция платежного документа из описания форамата 1с первоначально только 
    /// те поля которые помечены как необходимые каждый может добавить те поля, которые считает 
    /// необходимыми для себя все зависит от того с кем интегрируемся Так же смотрим на то, какие 
    /// данные поддерживает наша система в коментариях в скобках, указано название в формате 1с документа 
    /// </summary>
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

        public MDMovementList(string settlement, IEnumerable<DocumentSection> docSections, string startBalance, string balance, string startDate, string endDate)
            : this()
        {
            SettlementAccount = settlement;
            StartBalance = startBalance;
            Balance = balance;
            StartDate = startDate;
            EndDate = endDate;

            if (docSections != null)
            {
                Documents.AddRange(docSections.Where(order => order.PayerAccount == SettlementAccount || order.ContractorAccount == SettlementAccount));
            }
        }

        public MDMovementList(string settlementAccount, string bankName, string startBalance, string balance, string startDate, string endDate)
            : this()
        {
            SettlementAccount = settlementAccount;
            BankName = bankName;
            StartBalance = startBalance;
            Balance = balance;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void AddDocuments(IEnumerable<DocumentSection> documentSections)
        {
            Documents.AddRange(documentSections);
        }

        public MDMovementList GetShortMovementList(int i, int batchSize)
        {
            var sectionAmount = GetCountForCopy(i, batchSize);
            var partOfList = new MDMovementList(SettlementAccount, BankName, StartBalance, Balance, StartDate, EndDate);
            partOfList.Documents.AddRange(Documents.GetRange(i, sectionAmount));

            return partOfList;
        }

        public int GetCountForCopy(int index, int batchSize)
        {
            var sectionLeft = Documents.Count - index;
            return sectionLeft > batchSize ? batchSize : sectionLeft;
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Moedelo.BankIntegrationsV2.Dto.IntegratedFile
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
        public string StartBalance { get; set; }

        /// <summary> Конечный остаток денежных средств </summary>
        public string Balance { get; set; }

        /// <summary> Дата начала выписки </summary>
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
            StartBalance = startBalance;
            BankName = bankName;
            Balance = balance;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void AddDocuments(IEnumerable<DocumentSection> documentSections)
        {
            Documents.AddRange(documentSections);
        }

        public MDMovementList GetShortMovementList(int i)
        {
            var sectionAmount = GetCountForCopy(i);
            var partOfList = new MDMovementList(SettlementAccount, BankName, StartBalance, Balance, StartDate, EndDate);
            partOfList.Documents.AddRange(Documents.GetRange(i, sectionAmount));

            return partOfList;
        }

        public int GetCountForCopy(int index)
        {
            const int maxSectionsCountForSave = 150;

            var sectionLeft = Documents.Count - index;
            return sectionLeft > maxSectionsCountForSave ? maxSectionsCountForSave : sectionLeft;
        }
    }
}
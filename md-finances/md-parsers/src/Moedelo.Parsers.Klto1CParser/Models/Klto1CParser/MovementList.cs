using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Parsers.Klto1CParser.Models.Klto1CParser
{
    public class MovementList
    {
        private const int MaxSettlementLength = 20;

        private string settlementAccount;

        public MovementList()
        {
            settlementAccount = string.Empty;
            Balances = new List<Balance>();
            Documents = new List<Document>();
        }

        public MovementList(IEnumerable<Document> documents)
            : this()
        {
            if (documents == null)
            {
                return;
            }
            Documents.AddRange(documents);
        }

        /// <summary> Расчётный счёт </summary>
        public string SettlementAccount
        {
            get { return settlementAccount; }
            set
            {
                settlementAccount = value?.Length > MaxSettlementLength
                    ? value.Substring(0, MaxSettlementLength)
                    : value;
            }
        }
        
        /// <summary> БИК </summary>
        public string Bik
        {
            get
            {
                var document = Documents.FirstOrDefault();
                
                if (document == null)
                {
                    return string.Empty;
                }
                
                return document.PayerAccount == SettlementAccount ? document.PayerBik : document.ContractorBik;
            }
        }
        
        /// <summary> Признак, что счет в иностранной валюте </summary>
        public bool IsCurrency
        {
            get
            {
                var rubCodes = new List<string>{"643", "810"};
                var currency = SettlementAccount.Substring(5, 3);
                return !rubCodes.Contains(currency);
            }
        }

        /// <summary> Дата начала выписки </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Дата конца выписки </summary>
        public DateTime EndDate { get; set; }

        /// <summary> Остатки по расчетному счету </summary>
        public List<Balance> Balances { get; set; }

        /// <summary> Начальный остаток денежных средств </summary>
        public decimal? StartBalance => Balances.FirstOrDefault().StartBalance;

        /// <summary> Конечный остаток денежных средств </summary>
        public decimal? EndBalance => Balances.LastOrDefault().EndBalance;

        /// <summary> Всего поступило </summary>
        public decimal IncomingBalance => Balances.Sum(x => x.IncomingBalance ?? 0m);

        /// <summary> Всего списано </summary>
        public decimal OutgoingBalance => Balances.Sum(x => x.OutgoingBalance ?? 0m);

        /// <summary> Секция документов </summary>
        public List<Document> Documents { get; set; }

        /// <summary> Секция в текстовом виде </summary>
        public string RawSection { get; set; }

        public string ErrorMessage { get; set; }
    }
}
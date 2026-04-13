using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Common.Enums.Extensions
{
    public static class BankDocTypeExtention
    {
        private static readonly Dictionary<BankDocType, string> DocTypeDigitalValues = new Dictionary<BankDocType, string>
        {
          {
            BankDocType.NotDefine, 
            "00"
          },
          {
            BankDocType.PaymentOrder,
            "01"
          },
          {
            BankDocType.MemorialWarrant,
            "09"
          },
          {
            BankDocType.IncomingFromCash,
            "04"
          },
          {
            BankDocType.PaymentRequest,
            "02"
          },
          {
            BankDocType.MoneyOrder,
            "03"
          },
          {
            BankDocType.CollectionOrder,
            "06"
          },
          {
            BankDocType.SettlementCheck,
            "07"
          },
          {
            BankDocType.OpeningOfLetterOfCredit,
            "08"
          },
          {
            BankDocType.PaymentWarrant,
            "16"
          },
          {
            BankDocType.BankService,
            "17"
          },
          {
            BankDocType.TransferValuesWarrant,
            "18"
          }
        };

        private static readonly Dictionary<RealBankDocumentType, BankDocType> BankDocumentTypeConverter = new Dictionary<RealBankDocumentType, BankDocType>
        {
            {RealBankDocumentType.NotDefine, BankDocType.NotDefine},
            {RealBankDocumentType.PaymentOrder, BankDocType.PaymentOrder},
            {RealBankDocumentType.PaymentRequest, BankDocType.PaymentRequest},
            {RealBankDocumentType.MoneyOrder, BankDocType.MoneyOrder},
            {RealBankDocumentType.IncomingFromCash, BankDocType.IncomingFromCash},
            {RealBankDocumentType.CollectionOrder, BankDocType.CollectionOrder},
            {RealBankDocumentType.SettlementCheck, BankDocType.SettlementCheck},
            {RealBankDocumentType.OpeningOfLetterOfCredit, BankDocType.OpeningOfLetterOfCredit},
            {RealBankDocumentType.MemorialWarrant, BankDocType.MemorialWarrant},
            {RealBankDocumentType.PaymentWarrant, BankDocType.PaymentWarrant },
            {RealBankDocumentType.BankService, BankDocType.BankService },
            {RealBankDocumentType.TransferValuesWarrant, BankDocType.TransferValuesWarrant},
        };

        public static string ToDigitalEquivalent(this BankDocType bankDocType)
        {
            if (!BankDocTypeExtention.DocTypeDigitalValues.ContainsKey(bankDocType))
                return string.Empty;
            return BankDocTypeExtention.DocTypeDigitalValues[bankDocType];
        }

        public static BankDocType GetDocTypeByCode(string code)
        {
            return BankDocTypeExtention.DocTypeDigitalValues.FirstOrDefault<KeyValuePair<BankDocType, string>>((Func<KeyValuePair<BankDocType, string>, bool>)(type => type.Value == code)).Key;
        }

        public static bool IsMemorialWarrant(this BankDocType bankDocType)
        {
            if (bankDocType != BankDocType.MemorialWarrant && bankDocType != BankDocType.IncomingFromCash)
                return bankDocType == BankDocType.BankService;
            return true;
        }

        public static BankDocType GetAccountingOrderType(this BankDocType bankDocType)
        {
            if (bankDocType.IsMemorialWarrant())
                return BankDocType.MemorialWarrant;
            if (bankDocType == BankDocType.BudgetaryPayment)
                return bankDocType;
            return BankDocType.PaymentOrder;
        }

        public static BankDocType ConvertToBankDocType(this RealBankDocumentType bankDocType)
        {
            return BankDocumentTypeConverter.ContainsKey(bankDocType)
                ? BankDocumentTypeConverter[bankDocType]
                : BankDocType.NotDefine;
        }
    }
}
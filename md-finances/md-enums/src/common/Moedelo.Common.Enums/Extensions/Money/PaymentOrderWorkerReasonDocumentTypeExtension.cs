using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Money;

namespace Moedelo.Common.Enums.Extensions.Money
{
    public static class PaymentOrderWorkerReasonDocumentTypeExtensions
    {
        static PaymentOrderWorkerReasonDocumentTypeExtensions()
        {
            SalaryProjectDocumentTypes = new List<PaymentOrderWorkerReasonDocumentType>
            {
                PaymentOrderWorkerReasonDocumentType.SalaryProject,
                PaymentOrderWorkerReasonDocumentType.DividendsBySalaryProject,
                PaymentOrderWorkerReasonDocumentType.GpdBySalaryProject
            };
        }

        public static List<PaymentOrderWorkerReasonDocumentType> SalaryProjectDocumentTypes { get; }

        public static bool IsSalaryProjectDocumentType(this PaymentOrderWorkerReasonDocumentType type)
        {
            return SalaryProjectDocumentTypes.Contains(type);
        }
    }
}

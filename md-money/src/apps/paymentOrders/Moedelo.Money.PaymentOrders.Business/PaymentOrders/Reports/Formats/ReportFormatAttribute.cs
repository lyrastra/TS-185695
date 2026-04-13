using Moedelo.Money.Common.Domain.Models.Reports;
using System;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Reports.Formats
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class ReportFormatAttribute : Attribute
    {
        public ReportFormatAttribute(ReportFormat format)
        {
            Format = format;
        }

        public ReportFormat Format { get; }
    }
}

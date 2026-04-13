using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums;

namespace Moedelo.AccountingV2.Client.Kontragents.Dto
{
    public class ReconcillationStatementRequestDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int AccountCode { get; set; }

        public bool FillForKontragent { get; set; }

        public DocumentFormat DocFormat { get; set; }

        public List<int> KontragentIds { get; set; }

        public bool UseStampAndSign { get; set; }
    }
}
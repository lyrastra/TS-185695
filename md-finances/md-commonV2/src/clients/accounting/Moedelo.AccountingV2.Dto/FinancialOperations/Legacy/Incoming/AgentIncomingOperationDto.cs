using System;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class AgentIncomingOperationDto : IncomingOperationDto
    {
        public double ProfitSum { get; set; }
        public string AgentSettlement { get; set; }
        public int? PrincipalId { get; set; }
        public DateTime? AgentDate { get; set; }
        public string AgentOrderNumber { get; set; }

        public override string Name => FinancialOperationNames.AgentIncomingOperation;
    }
}

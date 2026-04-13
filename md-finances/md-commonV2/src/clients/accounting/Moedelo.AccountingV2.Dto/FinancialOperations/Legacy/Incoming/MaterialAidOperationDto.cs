using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class MaterialAidOperationDto : IncomingOperationDto
    {
        public string FioParent { get; set; }
        public string ProjectNumber { get; set; }
        public int? WorkerId { get; set; }

        public override string Name => FinancialOperationNames.MaterialAidOperation;
    }
}

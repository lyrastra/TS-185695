using System;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class PurchaseOfFixedAssetsOperationDto : OutgoingOperationDto
    {
        public DateTime? DateOfConfirmDocument { get; set; }
        public string NumberOfConfirmDocument { get; set; }
        public double SumOfConfirmDocument { get; set; }
        public string ConfirmDocumentType { get; set; }
        public long InventoryNumber { get; set; }
        public DateTime? StartUpDate { get; set; }
        public int? WorkerId { get; set; }
        public string UsefullLife { get; set; }
        public BasicAssetsType BasicAssetsType { get; set; }
        public string BasicAssetsName { get; set; }
        public DateTime? RegistrationDateOfBasicAssets { get; set; }

        public override string Name => FinancialOperationNames.PurchaseOfFixedAssetsOperation;
    }
}

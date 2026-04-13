using System;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.FinancialOperations
{
    public class FinancialOperationDto
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public string Des { get; set; }

        public string Number { get; set; }

        public DateTime OperationDate { get; set; }

        public long? DocumentBaseId { get; set; }

        public int? Order { get; set; }

        public int? ParentFinancialOperationId { get; set; }

        public string Name { get; set; }

        public FinancialOperationDirection Direction { get; set; }
        
        //BudgetaryPaymentOperationDto
        public string Kbk { get; set; }

        public string Okato { get; set; }

        public string Oktmo { get; set; }

        public int? EnvdTaxAdministrationId { get; set; }

        public BudgetaryPaymentSubtype BudgetaryPaymentSubtype { get; set; }

        public BudgetaryPaymentFoundation BudgetaryPaymentFoundation { get; set; }

        public string TypeDescription { get; set; }

        public string RecipientName { get; set; }

        public string RecipientInn { get; set; }

        public string RecipientKpp { get; set; }

        public string RecipientSettlement { get; set; }

        public int RecipientBankId { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DocumentDate { get; set; }

        public DateTime? PeriodDate { get; set; }

        public int YearPeriod { get; set; }

        public int QuarterPeriod { get; set; }

        public string TypePeriod { get; set; }

        public string DebitAccount { get; set; }

        public string CreditAccount { get; set; }

        public string CodeUin { get; set; }

        public BudgetaryPaymentType BudgetaryPaymentType { get; set; }

        public BudgetaryPaymentType BudgetaryPaymentPeniType { get; set; }

        public BudgetaryPaymentTurn BudgetaryPaymentTurn { get; set; }
        
        //MoneyTransferOperationDto
        public Decimal Sum { get; set; }

        public int? ProjectId { get; set; }

        public int? KontragentId { get; set; }

        public MoneyBayType MoneyBayType { get; set; }

        public WriteOff WriteOffBy { get; set; }

        public Decimal UsnSum { get; set; }

        public Decimal EnvdSum { get; set; }

        public string DestanitionDescription { get; set; }

        public string NumberOfDocument { get; set; }

        public int? SettlementAccountId { get; set; }

        public virtual int? PurseId { get; set; }
        
        //OutgoingOperationDto
        public string BillNumber { get; set; }

        public long? KontragentSettlementAccountId { get; set; }
    }
}
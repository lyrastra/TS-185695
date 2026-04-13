using System;

namespace Moedelo.AccountingV2.Dto.Money
{
    public class MoneyTransferDto
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public int? ProjectId { get; set; }
        public int? BillId { get; set; }
        public int? PurseId { get; set; }

        public int? KontragentId { get; set; }
        public string KontragentName { get; set; }
        public string KontragentInn { get; set; }
        public string KontragentSettlement { get; set; }

        public string PaymentNumber { get; set; }
        public string Type { get; set; }
        public string TransferType { get; set; }
        public double IncomingSumm { get; set; }
        public double OutgoingSumm { get; set; }
        public double SummForUsn { get; set; }
        public DateTime? Date { get; set; }
        public string DestinationDescription { get; set; }
        public string SettlementAccount { get; set; }
        public string SettlementAccount2 { get; set; }
        public int MoneyBayType { get; set; }
        public bool IsCash { get; set; }
        public string Kbk { get; set; }
        public string TaxPeriod { get; set; }
        public string Okato { get; set; }
        public string Oktmo { get; set; }
        public int? WorkerId { get; set; }
        public string WorkerSettlement { get; set; }
        public string TypeDescription { get; set; }

        public int YearPeriod { get; set; }
        public int QuarterPeriod { get; set; }

        public string RecipientInn { get; set; }
        public string RecipientKpp { get; set; }
        public string RecipientSettlement { get; set; }

        /// <summary>
        /// Группа выплат сотрудникам и выплат НДФЛ
        /// not null для операций сформированных на основе действий в зарплате 
        /// </summary>
        public long WorkerSalaryGroupId { get; set; }

        /// <summary>
        /// Группа выплат в фонды(ПФР, ФСС)
        /// not null для операций сформированных на основе действий в зарплате 
        /// </summary>
        public long FundSalaryGroupId { get; set; }

        /// <summary>
        /// операция зависит от зарплатного приложения 
        /// </summary>
        public bool IsSalaryOperation { get; set; }

        /// <summary>
        /// тип операции - discriminator
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// Тип фонда для платежей в фонды(НДФЛ, ПФР, ФСС)
        /// </summary>
        public int /*BudgetaryPaymentTypes*/ BudgetaryPaymentType { get; set; }
    }
}

using Moedelo.Changelog.Shared.Domain.Definitions.Money.Common;
using Moedelo.Changelog.Shared.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing
{
    public class BudgetaryPaymentStateDefinition
        : OutgoingPaymentOrderStateDefinition<
            BudgetaryPaymentStateDefinition,
            BudgetaryPaymentStateDefinition.State>
    {
        public override ChangeLogEntityType EntityType { get; } = ChangeLogEntityType.Money_PaymentOrders_Outgoing_BudgetaryPayment;

        public sealed class State
        {
            public long DocumentBaseId { get; set; }

            [Display(Name = "Номер")]
            public string Number { get; set; }

            [Display(Name = "Дата")]
            public DateTime Date { get; set; }

            // сохраняем идентификатор расчётного счёта на всякий случай
            public int SettlementAccountId { get; set; }

            [Display(Name = "Расчетный счет")]
            public string SettlementAccountNumber { get; set; }

            [Display(Name = "Тип бюджетного платежа")]
            public BudgetaryAccountType AccountType { get; set; }

            [Display(Name = "Тип налога/взноса")]
            public string AccountCode { get; set; }

            [Display(Name = "Период (107)")]
            public string Period { get; set; }

            // на всякий случай сохраняем технический идентификатор
            public int? KbkId { get; set; }

            [Display(Name = "КБК (104)")]
            public string KbkNumber { get; set; }

            [Display(Name = "Сумма (7)")]
            public MoneySum Sum { get; set; }

            [Display(Name = "Назначение (24)")]
            public string Description { get; set; }

            /// <summary>
            /// Признак: пользователь заполнил НУ вручную
            /// </summary>
            [Display(Name = "Проведено вручную в налоговом учете")]
            public bool IsManualTaxPostings { get; set; }

            /// <summary>
            /// Признак: учитывать в БУ
            /// </summary>
            [Display(Name = "Учитывать в БУ")]
            public bool ProvideInAccounting { get; set; }

            [Display(Name = "Тип платежа (КБК)")]
            public KbkPaymentTypeEnum KbkPaymentType { get; set; }

            [Display(Name = "Оплачено")]
            public bool IsPaid { get; set; }

            [Display(Name = "Статус плательщика (101)")]
            public string PayerStatus { get; set; }

            [Display(Name = "Основание платежа (106)")]
            public string PaymentBase { get; set; }

            [Display(Name = "№ документа (108)")]
            public string DocumentNumber { get; set; }

            [Display(Name = "Дата документа (109)")]
            public string DocumentDate { get; set; }

            [Display(Name = "Код (22)")]
            public string Uin { get; set; }

            [Display(Name = "Получатель")]
            public PaymentRecipient Recipient { get; set; }

            [Display(Name = "Связанные документы")]
            public LinkedDocumentInfo[] CurrencyInvoicesLinks { get; set; }

            /// <summary>
            /// Тип платежа (КБК)
            /// </summary>
            public enum KbkPaymentTypeEnum
            {
                /// <summary>
                /// Налоги
                /// </summary>
                [Display(Name = "Налоги")]
                PaymentTaxes = 100,

                /// <summary>
                /// Взносы
                /// </summary>
                [Display(Name = "Взносы")]
                PaymentFees = 101,

                /// <summary>
                /// Пени
                /// </summary>
                [Display(Name = "Пени")]
                Surcharge = 2,

                /// <summary>
                /// Штрафы
                /// </summary>
                [Display(Name = "Штрафы")]
                Forfeit = 3
            }

            public enum BudgetaryAccountType
            {
                [Display(Name = "Налоги")]
                Taxes = 0,
                [Display(Name = "Взносы")]
                Fees = 1
            }

            /// <summary>
            /// Получатель бюджетного платежа
            /// </summary>
            public sealed class PaymentRecipient
            {
                [Display(Name = "Получатель")]
                public string Name { get; set; }

                [Display(Name = "ИНН")]
                public string Inn { get; set; }

                [Display(Name = "КПП")]
                public string Kpp { get; set; }

                [Display(Name = "Казначейский счет")]
                public string SettlementAccount { get; set; }

                [Display(Name = "Банк получателя")]
                public string BankName { get; set; }

                public string BankBik { get; set; }

                [Display(Name = "Единый казначейский счет")]
                public string BankCorrespondentAccount { get; set; }

                public string Okato { get; set; }

                [Display(Name = "ОКТМО")]
                public string Oktmo { get; set; }
            }
        }

        public override long GetEntityId(State state)
        {
            return state.DocumentBaseId;
        }

        protected override string GetEntityName(State state)
        {
            return $"{EntityTypeName} №{state.Number} от {state.Date:dd.MM.yyyy}";
        }
    }
}

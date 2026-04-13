using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.CommonV2.EventBus.Backoffice
{
    public class BackofficeBlUpload1CEvent
    {
        public PaymentImportHistoryDto History { get; set; }

        public MovementListDto MovementList { get; set; }

        public int ImportFileType { get; set; }

        public UserContextDto UserContext { get; set; }

        public bool EncodedToBase64 { get; set; }

        public string Base64Body { get; set; }

        public class PaymentImportHistoryDto
        {
            public int Id { get; set; }

            public int UserId { get; set; }

            public DateTime ImportDate { get; set; }

            public string ImportFile { get; set; }

            public int ImportStatus { get; set; }

            public string ImportStatusDetail { get; set; }

            public string UserLogin { get; set; }

            public bool IsOutsource { get; set; }

            public int ImportFileType { get; set; }

            public decimal? ImportFileBalance { get; set; }

            public DateTime? ImportFileDate { get; set; }

            public DateTime? StartDate { get; set; }

            public DateTime? EndDate { get; set; }
        }

        public class MovementListDto
        {
            public int MaxSettlementLength { get; set; }

            public string settlementAccount { get; set; }

            /// <summary> Дата начала выписки </summary>
            public DateTime StartDate { get; set; }

            /// <summary> Дата конца выписки </summary>
            public DateTime EndDate { get; set; }

            /// <summary> Остатки по расчетному счету </summary>
            public List<BalanceDto> Balances { get; set; }

            /// <summary> Секция документов </summary>
            public List<DocumentDto> Documents { get; set; }

            /// <summary> Секция в текстовом виде </summary>
            public string RawSection { get; set; }

            public string ErrorMessage { get; set; }
        }

        public class BalanceDto
        {
            public int MaxSettlementLength { get; set; }

            public string settlementAccount { get; set; }

            /// <summary> Начальный остаток денежных средств </summary>
            public decimal? StartBalance { get; set; }

            /// <summary> Всего поступило </summary>
            public decimal? IncomingBalance { get; set; }

            /// <summary> Всего списано </summary>
            public decimal? OutgoingBalance { get; set; }

            /// <summary> Конечный остаток денежных средств </summary>
            public decimal? EndBalance { get; set; }

            /// <summary> Дата начала </summary>
            public DateTime StartDate { get; set; }

            /// <summary> Дата конца </summary>
            public DateTime? EndDate { get; set; }

            /// <summary> Секция в текстовом виде </summary>
            public string RawSection { get; set; }
        }

        public class DocumentDto
        {
            /// <summary> Тип операции </summary>
            public int Type { get; set; }

            #region Common

            /// <summary> Тип документа </summary>
            public string SectionName { get; set; }

            /// <summary> Дата документа </summary>
            public DateTime? DocDate { get; set; }

            /// <summary> Дата выполнения операции поступления </summary>
            public DateTime? IncomingDate { get; set; }

            /// <summary> Дата выполенения операции списания </summary>
            public DateTime? OutgoingDate { get; set; }

            /// <summary> Номер документа </summary>
            public string DocumentNumber { get; set; }

            /// <summary> Сумма платежа </summary>
            public decimal Summa { get; set; }

            /// <summary> Назначение платежа </summary>
            public string PaymentPurpose { get; set; }

            #endregion

            #region Payer

            /// <summary> Плательщик (Плательщик) </summary>
            public string Payer { get; set; }

            /// <summary> Расчетный счет плательщика (ПлательщикСчет) </summary>
            public string PayerAccount { get; set; }

            /// <summary> Расчетный счет плательщика (ПлательщикРасчСчет) </summary>
            public string PayerSettlementAccount { get; set; }

            /// <summary> ИНН плательщика (ПлательщикИНН) </summary>
            public string PayerInn { get; set; }

            /// <summary> КПП  плательщика (ПлательщикКПП). Поле не обязательное, но часто используемое </summary>
            public string PayerKpp { get; set; }

            /// <summary> БИК плательщика </summary>
            public string PayerBik { get; set; }

            /// <summary> Название банка плательщика </summary>
            public string PayerBankName { get; set; }

            #endregion

            #region Contractor

            /// <summary> Получатель (Получатель) </summary>
            public string Contractor { get; set; }

            /// <summary> Расчетный счет контрагента (ПолучательСчет) </summary>
            public string ContractorAccount { get; set; }

            /// <summary> ИНН получателя (ПолучательИНН) </summary>
            public string ContractorInn { get; set; }

            /// <summary> КПП контрагента (ПолучательКПП). Поле не обязательное, но часто используемое </summary>
            public string ContractorKpp { get; set; }

            /// <summary> БИК контрагента </summary>
            public string ContractorBik { get; set; }

            /// <summary> Название банка контрагента </summary>
            public string ContractorBankName { get; set; }

            #endregion

            #region Budgetary payment

            /// <summary> КБК (ПоказательКБК) </summary>
            public string IndicatorKbk { get; set; }

            /// <summary>Показатель периода </summary>
            public string Period { get; set; }

            /// <summary> Окато </summary>
            public string Okato { get; set; }

            /// <summary> Статус составителя </summary>
            public string PayerStatus { get; set; }

            /// <summary> Тип платежа </summary>
            public string PaymentType { get; set; }

            /// <summary> Основание платежа </summary>
            public string PaymentFoundation { get; set; }

            /// <summary> Дата платежа </summary>
            public DateTime? PaymentDate { get; set; }

            /// <summary> Номер платежа </summary>
            public string PaymentNumber { get; set; }

            /// <summary> Очередность платежа </summary>
            public string PaymentTurn { get; set; }

            /// <summary> Вид оплаты </summary>
            public string PaymentKind { get; set; }

            /// <summary> Уникальный идентификатор начисления "Код" (22)</summary>
            public string CodeUin { get; set; }

            #endregion

            /// <summary> Секция в текстовом виде </summary>
            public string RawSection { get; set; }

            /// <summary> Свободная строка, используется для передачи чего то, что может понадобится</summary>
            public string ReservedString { get; set; }
        }

        public class UserContextDto
        {
            public int UserId { get; set; }

            public string Login { get; set; }

            public int FirmId { get; set; }
        }
    }
}
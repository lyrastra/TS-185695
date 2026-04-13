using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.Models.Movement
{
    /// <summary> Секция документ в формате 1С </summary>
    public class DocumentSection
    {
        public DocumentSection()
        {
            SectionName = string.Empty;
            OperationType = OperationType.UnknownOperation;
            IncomingType = IncomingTransferTypes.NotDefined;
            OutgoingType = OutgoingTransferTypes.NotDefined;
            MovementType = MovementTransferTypesForImport1C.NotDefined;
            IndicatorKbk = string.Empty;
            DocumentNumber = string.Empty;
            DocDate = string.Empty;
            Period = string.Empty;
            IncomingDate = string.Empty;
            OutgoingDate = string.Empty;
            PaymentPurpose = string.Empty;
            PaymentPurposeCode = string.Empty;
            PayerAccount = string.Empty;
            Payer = string.Empty;
            PayerInn = string.Empty;
            PayerKpp = string.Empty;
            PayerBik = string.Empty;
            ContractorAccount = string.Empty;
            Contractor = string.Empty;
            ContractorInn = string.Empty;
            ContractorBankName = string.Empty;
            ProjectNumber = string.Empty;
            ReservedString = string.Empty;
            CodeUin = string.Empty;
            PaymentType = "0";
        }

        /// <summary>
        /// Тип документа
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// Статус составителя
        /// </summary>
        public string PayerStatus { get; set; }

        /// <summary> Тип операции </summary>
        public OperationType OperationType { get; set; }

        /// <summary> Тип входящей операции </summary>
        public IncomingTransferTypes IncomingType { get; set; }

        /// <summary> Тип исходящей операции </summary>
        public OutgoingTransferTypes OutgoingType { get; set; }

        /// <summary> Тип движения денежной операции </summary>
        public MovementTransferTypesForImport1C MovementType { get; set; }

        /// <summary> Номер документа </summary>
        public string DocumentNumber { get; set; }

        /// <summary> Дата документа </summary>
        public string DocDate { get; set; }

        /// <summary>Показатель периода </summary>
        public string Period { get; set; }

        /// <summary>Id договора (Если имеется)</summary>
        public int ProjectId { get; set; }

        /// <summary> Дата выполнения операции поступления </summary>
        public string IncomingDate { get; set; }

        /// <summary> Дата выполенения операции списания </summary>
        public string OutgoingDate { get; set; }

        /// <summary> Сумма платежа </summary>
        public decimal Summa { get; set; }

        /// <summary> Назначение платежа </summary>
        public string PaymentPurpose { get; set; }

        /// <summary> Назначение платежа кодовое </summary>
        public string PaymentPurposeCode { get; set; }

        /// <summary> КБК (ПоказательКБК) </summary>
        public string IndicatorKbk { get; set; }

        /// <remarks> РЕКВИЗИТЫ ПЛАТЕЛЬЩИКА </remarks>
        /// <summary> Расчетный счет плательщика (ПлательщикСчет) </summary>
        public string PayerAccount { get; set; }

        /// <summary> Плательщик (Плательщик) </summary>
        public string Payer { get; set; }

        /// <summary> ИНН плательщика (ПлательщикИНН) </summary>
        public string PayerInn { get; set; }

        /// <summary> КПП  плательщика (ПлательщикКПП). Поле не обязательное, но часто используемое </summary>
        public string PayerKpp { get; set; }

        /// <summary> БИК плательщика </summary>
        public string PayerBik { get; set; }

        /// <summary> Название банка плательщика </summary>
        public string PayerBankName { get; set; }

        /// <remarks> РЕКВИЗИТЫ КОНТРАГЕНТА </remarks>
        /// <summary> Расчетный счет контрагента (ПолучательСчет) </summary>
        public string ContractorAccount { get; set; }

        /// <summary> Получатель (Получатель) </summary>
        public string Contractor { get; set; }

        /// <summary> ИНН получателя (ПолучательИНН) </summary>
        public string ContractorInn { get; set; }

        /// <summary> КПП контрагента (ПолучательКПП). Поле не обязательное, но часто используемое </summary>
        public string ContractorKpp { get; set; }

        /// <summary> БИК контрагента </summary>
        public string ContractorBik { get; set; }

        /// <summary> Название банка контрагента </summary>
        public string ContractorBankName { get; set; }

        /// <summary> привязанный договор () </summary>
        public string ProjectNumber { get; set; }

        /// <summary> Свободная строка, используется для передачи чего то, что может понадобится</summary>
        public string ReservedString { get; set; }

        /// <summary> Окато </summary>
        public string Okato { get; set; }

        /// <summary> Тип платежа </summary>
        public string PaymentType { get; set; }

        /// <summary> Основание платежа </summary>
        public string PaymentFoundation { get; set; }

        /// <summary> Дата платежа </summary>
        public string PaymentDate { get; set; }

        /// <summary> Номер платежа </summary>
        public string PaymentNumber { get; set; }

        /// <summary> Очередность платежа </summary>
        public string PaymentTurn { get; set; }

        /// <summary> Вид оплаты </summary>
        public string PaymentKind { get; set; }

        /// <summary> Уникальный идентификатор начисления "Код" (22)</summary>
        public string CodeUin { get; set; }
    }
}

using System;
using Moedelo.Parsers.Klto1CParser.Enums;

namespace Moedelo.Parsers.Klto1CParser.Models
{
    public class Document
    {
        public Document()
        {
            SectionName = string.Empty;
            Type = TransferType.NotDefined;
            IndicatorKbk = string.Empty;
            DocumentNumber = string.Empty;
            Period = string.Empty;
            PaymentPurpose = string.Empty;
            PayerAccount = string.Empty;
            Payer = string.Empty;
            PayerInn = string.Empty;
            PayerKpp = string.Empty;
            PayerBik = string.Empty;
            PayerSettlementAccount = string.Empty;
            ContractorAccount = string.Empty;
            Contractor = string.Empty;
            PayerInn = string.Empty;
            ContractorInn = string.Empty;
            ContractorBankName = string.Empty;
            ReservedString = string.Empty;
            CodeUin = string.Empty;
        }

        /// <summary> Тип операции </summary>
        public TransferType Type { get; set; }

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

        /// <summary> ИНН плательщика (ПлательщикИНН) </summary>
        public string PayerInn { get; set; }

        /// <summary> КПП  плательщика (ПлательщикКПП). Поле не обязательное, но часто используемое </summary>
        public string PayerKpp { get; set; }

        /// <summary> БИК плательщика </summary>
        public string PayerBik { get; set; }

        /// <summary> Расчетный счет плательщика (ПлательщикРасчСчет) </summary>
        public string PayerSettlementAccount { get; set; }

        /// <summary> Название банка плательщика </summary>
        public string PayerBankName { get; set; }

        /// <summary> Корсчет банка плательщика </summary>
        public string PayerBankCorrespondentAccount { get; set; }

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

        /// <summary> Корсчет банка контрагента </summary>
        public string ContractorBankCorrespondentAccount { get; set; }

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

        /// <summary> Начало секции в выписке </summary>
        public int RawPosition { get; set; }

        /// <summary> Секция в текстовом виде </summary>
        public string RawSection { get; set; }

        /// <summary> Свободная строка, используется для передачи чего то, что может понадобится</summary>
        public string ReservedString { get; set; }
    }
}
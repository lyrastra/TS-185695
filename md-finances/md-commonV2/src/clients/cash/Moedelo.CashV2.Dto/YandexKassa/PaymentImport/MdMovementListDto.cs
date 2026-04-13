using System.Collections.Generic;

namespace Moedelo.CashV2.Dto.YandexKassa.PaymentImport
{
    // Копия Moedelo.PaymentOrderImport.WebApp.Business.Data.MdMovementList

    /// <summary>
    ///     Секция платежного документа из описания форамата 1с первоначально только
    ///     те поля которые помечены как необходимые каждый может добавить те поля, которые считает
    ///     необходимыми для себя все зависит от того с кем интегрируемся Так же смотрим на то, какие
    ///     данные поддерживает наша система в коментариях в скобках, указано название в формате 1с документа
    /// </summary>
    public class MdMovementListDto
    {
        /// <remarks> ОБЩИЕ ДАННЫЕ </remarks>
        /// <summary> Расчётный счёт </summary>
        public string SettlementAccount { get; set; }
        
        /// <summary> Банк из которого пришла выписка </summary>
        public string BankName { get; set; }

        /// <summary> Банк из которого пришла выписка </summary>
        public string BankBik { get; set; }

        /// <summary> Конечный остаток денежных средств </summary>
        public string Balance { get; set; }

        /// <summary> Дата конца выписки </summary>
        public string EndDate { get; set; }

        /// <summary> Секция документов </summary>
        public List<DocumentSectionDto> Documents { get; set; }

        /// <summary> 
        /// Список платежек
        /// </summary>
        public List<PaymentOrderDto> Payments { get; set; }
    }
}
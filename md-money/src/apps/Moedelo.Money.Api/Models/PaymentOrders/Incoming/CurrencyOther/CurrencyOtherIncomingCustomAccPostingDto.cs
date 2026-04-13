using Moedelo.AccPostings.Enums;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using System.Collections.Generic;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencyOther
{
    /// <summary>
    /// Бухгалтерский учёт (вручную)
    /// </summary>
    public class CurrencyOtherIncomingCustomAccPostingDto
    {
        /// <summary>
        /// Сумма
        /// </summary>
        [SumValue]
        [RequiredValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Идентификатор объекта учета (дебет)
        /// Поле "SubcontoId"
        /// Объект <a href="#!/Расчётные_счета/SettlementAccount_GetAsync">Расчетный счет</a>
        /// </summary>
        [RequiredValue]
        public long DebitSubconto { get; set; }

        /// <summary>
        /// Код учета (кредит)
        /// <a href="#!/Бухучёт_-_Субсчета/SyntheticAccount_Get">справочник счетов</a>
        /// </summary>
        [RequiredValue]
        [EnumValue(EnumType = typeof(SyntheticAccountCode))]
        public SyntheticAccountCode CreditCode { get; set; }

        /// <summary>
        /// Идентификаторы объектов учета (кредит)
        /// Поле "SubcontoId"
        /// Объекты:
        /// * <a href="#!/Договоры/Contract_Get">Договор</a>
        /// * <a href="#!/Контрагенты/Kontragent_Get_0">Контрагент</a>
        /// * <a href="#!/Зарплата_-_Сотрудники/Employee_Get">Сотрудник</a>
        /// * ...
        /// </summary>
        public IReadOnlyCollection<SubcontoDto> CreditSubconto { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [AccPostingDescription]
        [ValidateXss]
        public string Description { get; set; }
    }
}

using System.Collections.Generic;
using Moedelo.AccPostings.Enums;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.Deduction
{
    /// <summary>
    /// Бухгалтерский учёт (вручную)
    /// </summary>
    public class DeductionCustomAccPostingDto
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
        /// * <a href="#!/Договоры/Contract_Get">Договор</a>
        /// * <a href="#!/Контрагенты/Kontragent_Get_0">Контрагент</a>
        /// * <a href="#!/Зарплата_-_Сотрудники/Employee_Get">Сотрудник</a>
        /// </summary>
        public IReadOnlyCollection<SubcontoDto> DebitSubconto { get; set; }

        /// <summary>
        /// Код учета (дебет)
        /// <a href="#!/Бухучёт_-_Субсчета/SyntheticAccount_Get">справочник счетов</a>
        /// </summary>
        [RequiredValue]
        [EnumValue(EnumType = typeof(SyntheticAccountCode))]
        public SyntheticAccountCode DebitCode { get; set; }

        /// <summary>
        /// Идентификаторы объектов учета (кредит)
        /// Поле "SubcontoId"
        /// Объекты:
        /// Объект <a href="#!/Расчётные_счета/SettlementAccount_GetAsync">Расчетный счет</a>
        /// * ...
        /// </summary>
        [RequiredValue]
        public long CreditSubconto { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [AccPostingDescription]
        [ValidateXss]
        public string Description { get; set; }
    }
}

namespace Moedelo.Common.Enums.Enums.ClosingPeriod
{
    public enum ClosingPeriodBlockReason
    {
        /// <summary>
        /// Введены некорректные остатки
        /// </summary>
        IncorrectBalance = -1,

        /// <summary>
        /// Есть непроведённые УПД
        /// </summary>
        HasNotProvidedUpd = -2,

        /// <summary>
        /// Есть отрицательные остатки на складе
        /// </summary>
        HasStockNegativeBalances = -3,

        /// <summary>
        /// Есть непроведённые Возвраты розничной продажи
        /// </summary>
        HasNotProvidedRetailRefund = -4,

        /// <summary>
        /// Уставный капитал не начислен. Надо ввести через "Уставный капитал"
        /// </summary>
        MustInputAuthorizedCapitalAsIs = -5,

        /// <summary>
        /// Уставный капитал не начислен. Надо ввести через остатки
        /// </summary>
        MustInputAuthorizedCapitalInRemains = -6,

        /// <summary>
        /// Уставный капитал не начислен. Надо ввести через бухсправку
        /// </summary>
        MustInputAuthorizedCapitalByAccountingStatement = -7,

        /// <summary>
        /// Пользователь имеет материалы оприходованные на счет 10.01
        /// </summary>
        HasSoldRawMaterials = -8,

        /// <summary>
        /// Есть отрицательные остатки на складе при производстве готовой продукции
        /// </summary>
        HasStockNegativeBalanceUsingMaterials = -9,

        /// <summary>
        /// Имеются неучтенные в бухгалтерском учете документы
        /// </summary>
        HasNotProvideDocumentsInAccountingAfterSpecialDate = -10,
        
        /// <summary>
        /// Не начислен НДС в бюджет
        /// </summary>
        HasNdsForAccrual = -11,

        /// <summary>
        /// Временно заблокирован, 
        /// к примеру, когда фича не доделана и нельзя проходить мастер во избежание ошибок
        /// </summary>
        TemporaryBlocked = -12,

        /// <summary>
        /// Есть приходы без документов, в которых используются комплекты
        /// </summary>
        HasDebitWithoutDocsWithBundling = -13,

        /// <summary>
        /// Нужно закрыть УСН декларацию перед началом действия ФИФО
        /// </summary>
        NeedCloseUsnDeclarationBeforeFifo = -14
    }
}

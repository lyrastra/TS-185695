(function(cash) {
    cash.OperationType = {
        Incoming: {
            AnotherCash: 45, // Перемещение с другой кассы
            SettlementAccount: 46, // Поступление с расчетного счета
            RetailRevenue: 47, // Розничная выручка
            PaymentForGoods: 49, // Поступление в оплату продажи товаров/материалов/работ/услуг
            PrePaymentForGoods: 51,
            ReturnFromSupplier: 52, 
            AccountablePerson: 53, // Возврат от подотчетного лица в кассе
            OtherIncome: 54
        },
        
        Outgoing: {
            SettlementAccount: 55, // Зачисление на расчетный счет
            AnotherCash: 65, // Перевод в другую кассу
            PaymentForGoods: 62, // Оплата поставщику в кассе
            PrePaymentForGoods: 57,
            AccountablePerson: 59, // Выдача подотчетному лицу в кассе
            CollectionOfMoney: 64, // Инкассация денег
            AgencyContract: 117, // Выплата по агентскому договору
            OtherOutgoing: 60
        }
    };

    cash.NdsTypes = {
        None: -1,
        Nds0: 0,
        Nds10: 1,
        Nds18: 2,
        Nds10To110: 3,
        Nds18To118: 4
    };
})(CashEnums);
import InvoiceStatusCodeEnum from '../enums/InvoiceStatusCodeEnum';

const InvoiceStatusCodeNotificationResource = {
    [InvoiceStatusCodeEnum.None]: {
        message: ``,
        type: `error`
    },
    [InvoiceStatusCodeEnum.AccessError]: {
        message: `Функция выполнения прямого платежа недоступна. Для открытия данной возможности необходимо 
        переподписать согласие на стороне СББОЛ и переподключить интеграцию с банком.`,
        type: `error`
    },
    [InvoiceStatusCodeEnum.RequisiteError]: {
        message: `Документ не прошел проверку на стороне СББОЛ. Возможные причины:
            1. Документ с такими реквизитами и номером уже существует в СББОЛ. Измените номер платежного поручения.
            2. Ошибка в заполнении. Проверьте корректность внесенных данных на стороне СББОЛ.`,
        type: `error`
    },
    [InvoiceStatusCodeEnum.InvalidSettlementAccount]: {
        message: `Нет подходящих счетов для оплаты по СМС. ` +
            `Для оплаты платежей данным способом необходимо выбрать актуальные счета и переподписать оферту на стороне СББОЛ.`,
        type: `error`
    },
    [InvoiceStatusCodeEnum.ValidationError]: {
        message: `Отправка сквозного (прямого) платежа по данным реквизитам невозможна. Отправьте черновик платежного поручения в банк и перейдите в СББОЛ для его подписания и исполнения.`,
        type: `error`
    }
};

export default InvoiceStatusCodeNotificationResource;

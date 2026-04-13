import * as validationMethods from '../../../validation/validationMethods';

export default {
    Number: [
        {
            fn: validationMethods.validationNumberNotEmpty,
            message: `Введите номер`
        },
        {
            fn: validationMethods.validationNumberMaxLength,
            message: `Длина номера не должна превышать 6 символов`
        }
    ],
    Date: [
        {
            fn: validationMethods.validateDate,
            message: `Заполните дату`
        },
        {
            fn: validationMethods.validateDateFormat,
            message: `Неверный формат даты`
        },
        {
            fn: validationMethods.isOutOfLastClosedDate,
            message: `Нельзя создавать документы в закрытом периоде`,
            needRequisites: true
        },
        {
            fn: validationMethods.isOutOfRegistrationDate,
            message: `Дата не может быть меньше даты регистрации компании`,
            needRequisites: true
        },
        {
            fn: validationMethods.isOutOfBalanceDate,
            message: `Нельзя создавать с датой ранее ввода остатков`,
            needRequisites: true
        },
        {
            fn: validationMethods.isLessThan2013Year,
            message: `Нельзя создавать с датой ранее 1 января 2013 года`
        }
    ],
    Sum: [
        {
            fn: validationMethods.validateSum,
            message: `Заполните сумму`
        },
        {
            fn: validationMethods.sumIsPositive,
            message: `Сумма не может быть равна 0`
        }
    ],
    Kontragent: [
        {
            fn: validationMethods.validateKontragent,
            message: `Укажите получателя`
        }
    ],
    KontragentSettlementAccount: [
        {
            fn: validationMethods.validateKontragentOnlyResidentSettlementAccount,
            message: `Расчетный счет состоит из 20 символов`
        }
    ],
    KontragentInn: [
        {
            fn: validationMethods.validateKontragentInn,
            message: `ИНН должен содержать 8, 10 или 12 символов`
        },
        {
            fn: validationMethods.validateKontragentInnFormat,
            message: `ИНН не прошел форматный контроль`,
            needRequisites: true
        }
    ],
    KontragentKpp: [
        {
            fn: validationMethods.validateKontragentKpp,
            message: `Введите значение длиной 9 символов или равное 0`
        }
    ],
    Contract: [
        {
            fn: validationMethods.validateContract,
            message: `Укажите договор`
        }
    ],
    Description: [
        {
            fn: validationMethods.validateDescription,
            message: `Не более 210 символов`
        },
        {
            fn: validationMethods.notEmptyDescription,
            message: `Поле является обязательным к заполнению`
        }
    ]
};

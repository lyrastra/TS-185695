import * as validationMethods from '../../../validation/validationMethods';

export default {
    ToSettlementAccountId: [
        {
            fn: validationMethods.validationToSettlementAccountIdNotEmpty,
            message: `Выберите счет`
        }
    ],
    Number: [
        {
            fn: validationMethods.validationNumberNotEmpty,
            message: `Введите номер`
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
    ExchangeRate: [
        {
            fn: validationMethods.validateExchangeRate,
            message: `Заполните курс`
        },
        {
            fn: validationMethods.exchangeRateIsPositive,
            message: `Курс не может быть равен 0`
        }
    ],
    Description: [
        {
            fn: validationMethods.notEmptyDescription,
            message: `Поле является обязательным к заполнению`
        },
        {
            fn: validationMethods.validateDescription,
            message: `Не более 210 символов`
        }
    ]
};

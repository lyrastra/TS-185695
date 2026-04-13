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
    NdsSum: [
        {
            fn: validationMethods.validationNdsSum,
            message: `Заполните сумму НДС`
        },
        {
            fn: validationMethods.validateNdsSumLessTotalSum,
            message: `Неверная сумма НДС`
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
    TotalSum: [
        {
            fn: validationMethods.validateTotalSum,
            message: `Укажите сумму`
        }
    ],
    Kontragent: [
        {
            fn: (model) => {
                return model && (
                    model.Kontragent.KontragentId ||
                    model.Kontragent.KontragentSettlementAccount
                );
            },
            message: `Укажите плательщика`
        }
    ],
    KontragentSettlementAccount: [
        {
            fn: validationMethods.validateKontragentSettlementAccount,
            message: `Расчетный счет состоит из 20 символов`
        }
    ],
    Contract: [
        {
            fn: validationMethods.validateContract,
            message: `Укажите договор`
        }
    ],
    Period: [
        {
            fn: validationMethods.validatePeriod,
            message: `Укажите период`
        }
    ],
    SumPeriod: [
        {
            fn: validationMethods.validateSumPeriod,
            message: `Укажите сумму`
        }
    ],
    DefaultSumPeriod: [
        {
            fn: validationMethods.validateDefaultSumPeriod,
            message: `Превышает неоплаченную часть суммы за период`
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
    ],
    FixedAsset: [
        {
            fn: validationMethods.notEmptyFixedAsset,
            message: `Укажите основное средство`
        }
    ]
};

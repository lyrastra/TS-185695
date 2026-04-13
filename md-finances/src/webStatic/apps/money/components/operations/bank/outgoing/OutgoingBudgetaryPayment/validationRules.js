import * as validationMethods from '../../../validation/validationMethods';
import * as localValidationMethods from './validationMethods';
import {
    validateKbk,
    validatePeriodDate,
    validateUin
} from '../../../validation/validationMethodsNewBackend';

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
    DocumentDate: [
        {
            fn: localValidationMethods.validateDocumentDate,
            message: `Укажите дату документа`
        }
    ],
    DocumentNumber: [
        {
            fn: localValidationMethods.validateDocumentNumber,
            message: `Укажите номер документа`
        },
        {
            fn: localValidationMethods.validateDocumentNumberLength,
            message: `Длина номера должна быть до 13 цифр`
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
        },
        {
            fn: localValidationMethods.unifiedTaxPaymentSumLess,
            message: `Распределенная сумма по налогам/взносам меньше общей суммы`
        },
        {
            fn: localValidationMethods.unifiedTaxPaymentSumMore,
            message: `Распределенная сумма по налогам/взносам больше общей суммы`
        }
    ],
    Recipient: [
        {
            fn: localValidationMethods.validateRecipientName,
            message: `Укажите получателя`
        }
    ],
    BankName: [
        {
            fn: localValidationMethods.validateRecipientBankName,
            message: `Укажите название банка`
        }
    ],
    SettlementAccount: [
        {
            fn: localValidationMethods.validateRecipientSettlementAccountExists,
            message: `Введите казначейский счет`
        },
        {
            fn: localValidationMethods.validateRecipientSettlementAccount,
            message: `Казначейский счет состоит из 20 символов`
        },
        {
            fn: localValidationMethods.validateMiddleRecepientSettlementAccount,
            message: `Казначейский счет должен начинаться с цифр 03, 40204 или 40101`
        },
        {
            fn: localValidationMethods.validateNewRecepientSettlementAccount,
            message: `Казначейский счет должен начинаться с цифр 03, 40204`
        }
    ],
    BankCorrespondentAccount: [
        {
            fn: localValidationMethods.validateRecipientBankCorrespondentAccountExists,
            message: `Введите единый казначейский счет`
        },
        {
            fn: localValidationMethods.validateRecipientBankCorrespondentAccount,
            message: `Единый казначейский счет счет состоит из 20 символов`
        },
        {
            fn: localValidationMethods.validateRecepientBankCorrespondentAccountCommonFormat,
            message: `Единый казначейский счет должен начинаться с цифр 40102`
        },
        {
            fn: localValidationMethods.validateRecepientBankCorrespondentAccountBaikonurFormat,
            message: `Единый казначейский счет должен начинаться с цифр 00000`
        }
    ],
    Inn: [
        {
            fn: localValidationMethods.validateRecipientInn,
            message: `Требуется значение длиной 10 или 12 символов`
        },
        {
            fn: localValidationMethods.validateRecipientInnFormat,
            message: `ИНН не прошел форматный контроль`,
            needRequisites: true
        }
    ],
    Kpp: [
        {
            fn: localValidationMethods.validateRecipientKpp,
            message: `Требуется значение длиной 9 символов или пустое значение`
        }
    ],
    Okato: [
        {
            fn: localValidationMethods.validateRecipientOkato,
            message: `Ожидается 11 цифр, нельзя указывать все нули подряд`
        }
    ],
    Oktmo: [
        {
            fn: localValidationMethods.validateRecipientOktmo,
            message: `Ожидается 8 цифр, нельзя указывать все нули подряд`
        },
        {
            fn: localValidationMethods.validateUnifiedBudgetaryPaymentRecipientOktmo,
            message: `Ожидается 0 или 8 цифр, нельзя указывать все нули подряд`
        }
    ],
    DocumentsSum: [
        {
            fn: validationMethods.validateDocumentsSum,
            message: `Сумма оплаты по документам не может превышать сумму платежного поручения`
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
    ],
    Uin: [
        {
            fn: validationMethods.validateUin,
            message: `Заполните код`
        },
        {
            fn: validateUin,
            message: `Ожидается 20 или 25 цифр или '0', нельзя указывать все нули подряд`
        }
    ],
    KBK: [
        {
            fn: validateKbk,
            message: `Ожидается 20 цифр`
        }
    ],
    Period: [
        {
            fn: validatePeriodDate,
            message: `Укажите период`
        }
    ]
};

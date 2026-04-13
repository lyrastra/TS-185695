import { toInt } from '@moedelo/frontend-core-v2/helpers/converter';
import * as validationMethods from '../../../validation/validationMethods';
import { isBailiffPayment } from './helpers/payerStatusHelper';
import PayerStatus from './enums/PayerStatus';

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
            fn: model => {
                return model &&
                    (
                        model.Kontragent.KontragentId || model.Kontragent.SalaryWorkerId ||
                        (!model.Kontragent.KontragentId && !model.Kontragent.SalaryWorkerId && model.Kontragent.KontragentSettlementAccount)
                    );
            },
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
    Kbk: [
        {
            fn: model => {
                return !model.IsBudgetaryDebt || !model.Kbk?.length || model.Kbk?.length === 20;
            },
            message: `Ожидается 20 цифр`
        }
    ],
    Uin: [
        {
            fn: model => {
                return model.Uin === `0`
                    || ((model.Uin?.length === 20 || model.Uin?.length === 25) && toInt(model.Uin) !== 0);
            },
            message: `Ожидается 20 или 25 цифр или '0', нельзя указывать все нули подряд`
        }
    ],
    DeductionWorkerFio: [
        {
            fn: model => {
                return !model.IsBudgetaryDebt || !!model.DeductionWorkerFio;
            },
            message: `Выберите сотрудника-плательщика удержаний`
        }
    ],
    DeductionWorkerDocumentNumber: [
        {
            fn: model => {
                return !model.IsBudgetaryDebt || model.DeductionWorkerDocumentNumber || model.DefaultDocumentNumber || model.DeductionWorkerInn;
            },
            message: `Заполните СНИЛС или паспорт в реквизитах сотрудника`
        },
        {
            fn: model => {
                return !model.IsBudgetaryDebt || model.DeductionWorkerDocumentNumber || model.DeductionWorkerInn;
            },
            message: `Поле обязательное для заполнения`
        }
    ],
    Oktmo: [
        {
            fn: model => {
                return !model.IsBudgetaryDebt || (model.Oktmo?.length === 8 && toInt(model.Oktmo) !== 0);
            },
            message: `Ожидается 8 цифр, нельзя указывать все нули подряд`
        }
    ],
    PayerStatus: [
        {
            fn: model => {
                return !(isBailiffPayment(model.Kontragent?.KontragentSettlementAccount) && model.PayerStatus !== PayerStatus.BailiffPayment);
            },
            message: `Укажите статус 31, так как счет получателя начинается с «03212»`
        }
    ]
};

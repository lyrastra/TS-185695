import * as validationMethods from '../../../validation/validationMethods';
import * as validationMethodsNewBackend from '../../../validation/validationMethodsNewBackend';

export default {
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
    NdsSum: [
        {
            fn: validationMethods.validationNdsSum,
            message: `Заполните сумму НДС`
        },
        {
            fn: validationMethods.validateNdsSumLessSum,
            message: `Неверная сумма НДС`
        }
    ],
    MediationCommissionNdsSum: [
        {
            fn: validationMethods.validationMediationCommissionNdsSum,
            message: `Заполните сумму НДС`
        },
        {
            fn: validationMethods.validateMediationNdsSumLessMediationCommission,
            message: `Неверная сумма НДС`
        }
    ],
    Kontragent: [
        {
            fn: validationMethods.validateKontragent,
            message: `Укажите плательщика`
        }
    ],
    MediationCommission: [
        {
            fn: validationMethods.validateMediationCommission,
            message: `Нельзя указать больше, чем в поле Поступило`
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
    DocumentsSum: [
        {
            fn: validationMethods.validateDocumentsSum,
            message: `Сумма оплаты по документам не может превышать сумму платежного поручения`
        }
    ],
    BillsSum: [
        {
            fn: validationMethods.validateBillsSum,
            message: `Нельзя указать сумму более суммы операции`
        }
    ],
    Description: [
        {
            fn: validationMethods.validateDescription,
            message: `Не более 210 символов`
        }
    ],
    TaxationSystemType: [
        {
            fn: validationMethodsNewBackend.hasActivePatents,
            message: `Патент не действует на дату платежа`
        }
    ],
    Patent: [
        {
            fn: validationMethodsNewBackend.isCurrentPatentActive,
            message: `Патент не действует на дату платежа`
        }
    ],
    ReserveSum: [
        {
            fn: validationMethods.reserveSumIsPositive,
            message: `Сумма резерва не может быть равна 0`
        },
        {
            fn: validationMethods.validateOnDoNotExceedSum,
            message: `Сумма оплаты по документам и резервам не может превышать сумму платежного поручения`
        }
    ]
};

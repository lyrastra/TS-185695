import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
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
            fn: model => {
                if (toFloat(model.AcquiringCommission) <= 0) {
                    return validationMethods.validateSum(model);
                }

                return true;
            },
            message: `Заполните сумму`
        },
        {
            fn: model => {
                if (toFloat(model.AcquiringCommission) <= 0) {
                    return !(toFloat(model.Sum) === 0);
                }

                return true;
            },
            message: `Сумма не может быть равна 0`
        }
    ],
    NdsSum: [
        {
            fn: validationMethods.validationNdsSum,
            message: `Заполните сумму НДС`
        },
        {
            fn: validationMethods.validateNdsSumLessAcquiringCommission,
            message: `Неверная сумма НДС`
        }
    ],
    AcquiringCommissionDate: [
        {
            fn: validationMethods.validateAcquiringCommissionDate,
            message: `Заполните дату`
        },
        {
            fn: validationMethods.validateAcquiringCommissionDateFormat,
            message: `Неверный формат даты`
        },
        {
            fn: validationMethods.isOutOfLastClosedAcquiringCommissionDate,
            message: `Нельзя указать дату из закрытого периода`,
            needRequisites: true
        },
        {
            fn: validationMethods.isOutOfRegistrationAcquiringCommissionDate,
            message: `Дата не может быть меньше даты регистрации компании`,
            needRequisites: true
        },
        {
            fn: validationMethods.isOutOfBalanceAcquiringCommissionDate,
            message: `Нельзя указать дату ранее ввода остатков`,
            needRequisites: true
        },
        {
            fn: validationMethods.isLessThan2013YearAcquiringCommissionDate,
            message: `Нельзя указать дату ранее 1 января 2013 года`
        }
    ],
    Description: [
        {
            fn: validationMethods.validateDescription,
            message: `Не более 210 символов`
        }
    ],
    SaleDate: [
        {
            fn: validationMethodsNewBackend.validateSaleDateFormat,
            message: `Неверный формат даты`
        },
        {
            fn: validationMethodsNewBackend.isOutOfLastClosedSaleDate,
            message: `Нельзя указать дату из закрытого периода`,
            needRequisites: true
        },
        {
            fn: validationMethodsNewBackend.isOutOfRegistrationSaleDate,
            message: `Дата не может быть меньше даты регистрации компании`,
            needRequisites: true
        },
        {
            fn: validationMethodsNewBackend.isOutOfBalanceSaleDate,
            message: `Нельзя указать дату ранее ввода остатков`,
            needRequisites: true
        },
        {
            fn: validationMethodsNewBackend.isLessThan2013YearSaleDate,
            message: `Нельзя указать дату ранее 1 января 2013 года`
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
    ]
};

import * as localValidationMethods from './validationMethods';

export default {
    KBK: [
        {
            fn: localValidationMethods.validateKbk,
            message: `Выберите КБК`
        }
    ],
    AccountCode: [
        {
            fn: localValidationMethods.validateAccountCode,
            message: `Выберите вид налога/взноса`
        }
    ],
    Sum: [
        {
            fn: localValidationMethods.validateSum,
            message: `Введите сумму`
        },
        {
            fn: localValidationMethods.compareWithMainSum,
            message: `Сумма налога/взноса не должна превышать общую сумму платежа`
        }
    ],
    TradingObject: [
        {
            fn: localValidationMethods.validateTradingObject,
            message: `Выберите торговый объект`
        }
    ],
    Patent: [
        {
            fn: localValidationMethods.validatePatent,
            message: `Выберите патент`
        }
    ]
};

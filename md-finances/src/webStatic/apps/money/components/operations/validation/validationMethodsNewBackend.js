import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import CalendarTypesEnum from '../../../../../enums/newMoney/budgetaryPayment/BudgetaryCalendarTypesEnum';
import { VALID_KBK_LENGTH } from '../../../../../constants/requisitesConstants';

/**
 * Проверка валидности КБК
 * @param {object} model денежная операция
 * @param {object} model.Kbk КБК
 * @param {string} model.Kbk.Number Номер КБК (length 20)
 * @returns {boolean}
 */
export function validateKbk({ Kbk }, req, data = {}) {
    const { isUnifiedBudgetaryPayment } = data;

    if (isUnifiedBudgetaryPayment) {
        return true;
    }

    return /[0-9]{20}/.test(Kbk?.Number) && Kbk?.Number.length <= VALID_KBK_LENGTH;
}

/**
 * Проверка периода с типом Date
 * @param {object} model денежная операция
 * @param {object} model.Period
 * @param {number} model.Period.Type
 * @param {string} model.Period.Date
 * @returns {boolean}
 */
export function validatePeriodDate({ Period }) {
    if (Period?.Type === CalendarTypesEnum.Date) {
        const date = dateHelper(Period?.Date, `DD.MM.YYYY`);

        return date.isValid();
    }

    return true;
}

/**
 * Проверка валидности УИН(уникальный идентификатор начисления/платежа). Поле Код (22) в БП
 * @param  {object} model денежная операция
 * @param model.Uin {string|number} УИН (length 20|25)
 * @returns {boolean}
 */
export function validateUin({ Uin }) {
    if (Uin?.length && Uin !== `0`) {
        return !/^(0{20}|0{25})$/.test(Uin) && /^([0-9]{20}|[0-9]{25})$/.test(Uin);
    }

    return true;
}

/**
 * Проверка валидности поля "В т.ч. проценты" не больше поля "Сумма"
 * @param {object} model денежная операция
 * @param {number} model.LoanInterestSum - В т.ч. проценты
 * @param {number} model.Sum - Сумма
 * @returns {boolean}
 */
export function validateLoanInterestSum({ LoanInterestSum, Sum }) {
    const loanInterestSum = toFloat(LoanInterestSum) || 0;
    const sum = toFloat(Sum) || 0;

    return loanInterestSum <= sum;
}

/**
 * Проверка валидности формата даты отгрузки/реализации услуг
 * @param {object} model денежная операция
 * @param {string} model.SaleDate Дата отгрузки/реализации услуг
 * @returns {boolean}
 */
export function validateSaleDateFormat(model) {
    return dateHelper(model.SaleDate, `DD.MM.YYYY`, true).isValid();
}

/**
 * Проверка того, что дата отгрузки/реализации услуг должна быть позже последнего закрытого периода
 * @param {object} model денежная операция
 * @param {string} model.SaleDate - Дата отгрузки/реализации услуг
 * @param {string} FinancialResultLastClosedPeriod - Дата последнего закрытого периода
 * @returns {boolean}
 */
export function isOutOfLastClosedSaleDate(model, { FinancialResultLastClosedPeriod }) {
    if (model.SaleDate && FinancialResultLastClosedPeriod) {
        return dateHelper(model.SaleDate, `DD.MM.YYYY`, true).isAfter(dateHelper(FinancialResultLastClosedPeriod, `DD.MM.YYYY`, true));
    }

    return true;
}

/**
 * Проверка того, что дата отгрузки/реализации услуг должна быть позже даты гос. регистрации компании
 * @param {object} model денежная операция
 * @param {string} model.SaleDate Дата отгрузки/реализации услуг
 * @param {string} RegistrationDate Дата гос. регистрации компании
 * @returns {boolean}
 */
export function isOutOfRegistrationSaleDate(model, { RegistrationDate }) {
    if (model.SaleDate && RegistrationDate) {
        return dateHelper(model.SaleDate, `DD.MM.YYYY`, true).isSameOrAfter(dateHelper(RegistrationDate, `DD.MM.YYYY`, true));
    }

    return true;
}

/**
 * Проверка того, что дата отгрузки/реализации услуг должна быть позже даты ввода остатков
 * @param {object} model денежная операция
 * @param {string} model.SaleDate Дата отгрузки/реализации услуг
 * @param {string} BalanceDate Дата ввода остатков
 * @returns {boolean}
 */
export function isOutOfBalanceSaleDate(model, { BalanceDate }) {
    if (model.SaleDate && BalanceDate) {
        const balanceDate = dateHelper(BalanceDate, `DD.MM.YYYY`, true).startOf(`year`).subtract(1, `days`);

        return dateHelper(model.SaleDate, `DD.MM.YYYY`, true).isSameOrAfter(balanceDate);
    }

    return true;
}

/**
 * Проверка того, что дата отгрузки/реализации услуг должна быть позже 2013 года
 * @param {object} model денежная операция
 * @param {string} model.SaleDate Дата отгрузки/реализации услуг
 * @returns {boolean}
 */
export function isLessThan2013YearSaleDate(model) {
    if (model.SaleDate) {
        return dateHelper(model.SaleDate, `DD.MM.YYYY`, true).year() >= 2013;
    }

    return true;
}

/**
 * Проверяем есть ли у операции активные патенты перед сохранением операции если СНО ПСН
 * @param {object} model денежная операция
 * @returns {boolean}
 */
export function hasActivePatents(model) {
    if (model.TaxationSystemType === TaxationSystemType.Patent) {
        return !!model.CurrentActivePatents.length;
    }

    return true;
}

/**
 * Проверяем активен ли текущий патент перед сохранением операции если СНО ПСН
 * @param {object} model денежная операция
 * @returns {boolean}
 */
export function isCurrentPatentActive(model) {
    if (model.TaxationSystemType === TaxationSystemType.Patent) {
        if (model.PatentId !== null) {
            return model.CurrentActivePatents?.some(patent => patent.Id === model.PatentId);
        }
    }

    return true;
}

export default {
    validateKbk,
    validatePeriodDate,
    validateUin,
    validateLoanInterestSum,
    validateSaleDateFormat,
    isOutOfLastClosedSaleDate,
    isOutOfRegistrationSaleDate,
    isOutOfBalanceSaleDate,
    isLessThan2013YearSaleDate
};

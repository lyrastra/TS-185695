import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import allOperationTypes, {
    paymentOrderOperationResources,
    cashOrderOperationResources,
    purseOperationResources
} from '../resources/MoneyOperationTypeResources';
import contractTypes
    from '../enums/newMoney/ContractTypesEnum';
import MoneySourceType from '../enums/MoneySourceType';
import LegalType from '../enums/LegalTypeEnum';
import { taxationSystemsForDropdown } from '../apps/money/components/MassTaxSystemChangeModal/metaData';

export const availableCashOperations = [
    cashOrderOperationResources.CashOrderOutgoingReturnToBuyer.value,
    cashOrderOperationResources.CashOrderOutgoingPaymentSuppliersForGoods.value,
    cashOrderOperationResources.CashOrderOutgoingOther.value,
    cashOrderOperationResources.CashOrderIncomingPaymentForGoods.value,
    cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.value,
    cashOrderOperationResources.CashOrderIncomingMediationFee.value,
    cashOrderOperationResources.CashOrderIncomingOther.value,
    cashOrderOperationResources.CashOrderIncomingMiddlemanRetailRevenue.value
];

const operationsWithBills = [
    paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value,
    paymentOrderOperationResources.PaymentOrderIncomingMediationFee.value,
    paymentOrderOperationResources.PaymentOrderIncomingOther.value,
    cashOrderOperationResources.CashOrderIncomingPaymentForGoods.value,
    cashOrderOperationResources.CashOrderIncomingMediationFee.value,
    cashOrderOperationResources.CashOrderIncomingOther.value,
    purseOperationResources.PurseOperationOtherOutgoing.value,
    purseOperationResources.PurseOperationIncome.value

];

const mediationFeeOperation = [
    paymentOrderOperationResources.PaymentOrderIncomingMediationFee.value,
    cashOrderOperationResources.CashOrderIncomingMediationFee.value
];

class MoneyOperationHelper {
    static getDefaultIncludeNdsForCash(model) {
        const isEdit = model.get(`isCopy`) || model.get(`Id`);
        const operationType = parseInt(model.get(`OperationType`), 10);

        if (availableCashOperations.includes(operationType)) {
            return _getDefaultIncludeNds(model, isEdit);
        }

        return Promise.resolve(false);
    }

    static getDefaultIncludeNds(model) {
        const operationType = parseInt(model.get(`OperationType`), 10);

        if (availableCashOperations.includes(operationType)) {
            return _getDefaultIncludeNds(model, false);
        }

        return Promise.resolve(false);
    }

    static getDefaultIncludeMediationNdsForCash(model) {
        const isEdit = model.get(`isCopy`) || model.get(`Id`);
        const operationType = parseInt(model.get(`OperationType`), 10);

        if (operationType === cashOrderOperationResources.CashOrderIncomingPaymentForGoods.value || operationType === cashOrderOperationResources.CashOrderIncomingMiddlemanRetailRevenue.value) {
            return _getDefaultIncludeMediationNds(model, isEdit);
        }

        return Promise.resolve(false);
    }

    static canOperationUseBills(operationType) {
        return operationsWithBills.includes(operationType);
    }

    static isMediation(operationType) {
        return mediationFeeOperation.includes(operationType);
    }
}

function _getDefaultIncludeNds(model, isEdit) {
    const date = model.get(`Date`);

    return taxationSystemService.getTaxSystem(date)
        .then(taxObj => {
            return _getIncludeNds({ model, taxObj, isEdit });
        });
}

function _getDefaultIncludeMediationNds(model, isEdit) {
    const date = model.get(`Date`);

    return taxationSystemService.getTaxSystem(date)
        .then(taxObj => {
            return _getIncludeMediationNds({ model, taxObj, isEdit });
        });
}

function _getIncludeNds({ model, taxObj, isEdit }) {
    const isOsno = taxObj.IsOsno;
    const includeNds = model.get(`IncludeNds`);
    const isUsn = taxObj.IsUsn;
    const ndsUsn2025Date = dateHelper(`2025-01-01`);
    const documentDate = dateHelper(model.get(`Date`), `DD.MM.YYYY`);

    if (!isEdit && isOsno) {
        return true;
    }

    if (!isEdit && isUsn && documentDate.isSameOrAfter(ndsUsn2025Date)) {
        return true;
    }

    return includeNds;
}

function _getIncludeMediationNds({ model, taxObj, isEdit }) {
    const isUsn = taxObj.IsUsn;
    const includeNds = model.get(`IncludeMediationNds`);
    const ndsUsn2025Date = dateHelper(`2025-01-01`);
    const documentDate = dateHelper(model.get(`Date`), `DD.MM.YYYY`);

    if (!isEdit && isUsn && documentDate.isSameOrAfter(ndsUsn2025Date)) {
        return true;
    }

    return includeNds;
}

export function isSalaryProject(underContractType) {
    const contractType = parseInt(underContractType, 10);

    return contractType === contractTypes.SalaryProject.value ||
        contractType === contractTypes.GPDBySalaryProject.value ||
        contractType === contractTypes.DividendsBySalaryProject.value;
}

export function getOperationTypeByDirectionAndLegalTypeAndSource(direction, legalType, sourceType, hide) {
    const operationTypes = sourceType === MoneySourceType.SettlementAccount ? paymentOrderOperationResources : cashOrderOperationResources;

    return Object.values(operationTypes).filter(item => {
        return item.Direction === direction
            && (item.Available === LegalType.All || item.Available === legalType)
            && (!hide?.includes(item));
    }) || [];
}

export function getOperationTypeByDirectionAndLegalType(direction, legalType, isCurrencyAvailable = false) {
    let filteredOperationTypes = Object.values(allOperationTypes).filter(value => {
        const { Direction: operationTypeDirection, Available } = value;

        return operationTypeDirection === direction && (Available === LegalType.All || Available === legalType);
    }) || {};

    const result = [];

    if (!isCurrencyAvailable) {
        filteredOperationTypes = filteredOperationTypes.filter(operation => !isCurrency(operation.value));
    }

    filteredOperationTypes = filteredOperationTypes.reduce((acc, val) => {
        const key = val.text;

        if (!acc[key]) {
            acc[key] = [];
        }

        acc[key].push(val);

        return acc;
    }, {});

    Object.entries(filteredOperationTypes).forEach(([key, value]) => {
        const values = [];

        value.forEach(item => {
            values.push(item.value);
        });

        result.push({ value: values, text: key });
    });

    return result;
}

export function getDefaultBankOperationType(options = {}) {
    if (options.OperationType) {
        return parseInt(options.OperationType, 10);
    }

    return options.Direction === Direction.Incoming ?
        paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value :
        paymentOrderOperationResources.PaymentOrderPaymentToSupplier.value;
}

export function getOperationType(operationType) {
    return Object.values(allOperationTypes).find(({ value }) => value === operationType) || {};
}

export function isOutgoingByType(operationType) {
    const operation = getOperationType(operationType);

    if (operation) {
        return operation.Direction === Direction.Outgoing;
    }

    return false;
}

export function isSettlement(operationType) {
    const ot = Object.values(paymentOrderOperationResources).find(({ value }) => value === operationType) || {};

    return ot.Source === MoneySourceType.SettlementAccount;
}

export function isCash(operationType) {
    const ot = Object.values(cashOrderOperationResources).find(({ value }) => value === operationType) || {};

    return ot.Source === MoneySourceType.Cash;
}

export function isPurse(operationType) {
    const ot = Object.values(purseOperationResources).find(({ value }) => value === operationType) || {};

    return ot.Source === MoneySourceType.Purse;
}

export function isSalary(operationType) {
    const salaryTypes = [
        paymentOrderOperationResources.PaymentOrderOutgoingForTransferSalary.value,
        cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value
    ];

    return salaryTypes.includes(operationType);
}

export function isMemorial(operationType) {
    const memorialList = [
        paymentOrderOperationResources.BankFee.value,
        paymentOrderOperationResources.MemorialWarrantCreditingCollectedFunds.value,
        paymentOrderOperationResources.MemorialWarrantReceiptFromCash.value,
        paymentOrderOperationResources.MemorialWarrantReceiptGoodsPaidCreditCard.value,
        paymentOrderOperationResources.WithdrawalFromAccount.value,
        paymentOrderOperationResources.MemorialWarrantAccrualOfInterest.value
    ];

    return memorialList.includes(operationType);
}

/**
 * Проверяет является ли операция валютной
 * @param operationType Тип операции (.value в ресурсе)
 * @return {boolean} Возвращает true eсли операция является валютной
 */
export function isCurrency(operationType) {
    const currencyOperationsList = [
        paymentOrderOperationResources.PaymentOrderIncomingCurrencyPurchase.value,
        paymentOrderOperationResources.PaymentOrderIncomingCurrencySale.value,
        paymentOrderOperationResources.PaymentOrderIncomingCurrencyOther.value,
        paymentOrderOperationResources.PaymentOrderOutgoingCurrencyPurchase.value,
        paymentOrderOperationResources.PaymentOrderOutgoingCurrencySale.value,
        paymentOrderOperationResources.PaymentOrderOutgoingCurrencyPaymentSuppliersForGoods.value,
        paymentOrderOperationResources.PaymentOrderOutgoingCurrencyOther.value,
        paymentOrderOperationResources.PaymentOrderIncomingCurrencyPaymentFromBuyer.value,
        paymentOrderOperationResources.PaymentOrderOutgoingCurrencyTransferToAccount.value,
        paymentOrderOperationResources.PaymentOrderIncomingCurrencyFromAnotherAccount.value,
        paymentOrderOperationResources.PaymentOrderOutgoingCurrencyBankFee.value
    ];

    return currencyOperationsList.includes(operationType);
}

/**
 * Проверяет, есть в операции функционал "Резерв"
 * @param operationType Тип операции (.value в ресурсе)
 * @return {boolean} Возвращает значение признака
 */
export function hasReserveFeature(operationType) {
    const currencyOperationsList = [
        paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value,
        paymentOrderOperationResources.PaymentOrderPaymentToSupplier.value
    ];

    return currencyOperationsList.includes(operationType);
}

/**
 * Проверяет является ли операция комиссионерской
 * @param operationType Тип операции (.value в ресурсе)
 * @return {boolean} Возвращает true eсли операция является комиссионерской
 */
export function hasCommissionAgents(operationType) {
    const commissionAgentsOperationsList = [
        paymentOrderOperationResources.PaymentOrderIncomingIncomeFromCommissionAgent.value
    ];

    return commissionAgentsOperationsList.includes(operationType);
}

/**
 * Проверяет можно ли копировать операцию с данным типом
 * @param operationType Тип операции (.value в ресурсе)
 * @returns {boolean} Возвращает true если операцию можно копировать
 */
function cantCopy(operationType) {
    return operationType === allOperationTypes.PaymentOrderIncomingFromAnotherAccount.value
        || operationType === allOperationTypes.CashOrderIncomingFromAnotherCash.value;
}

export function canCopyOperation({ OperationType, Date, enpStartDate }) {
    return !cantCopy(OperationType)
        && canCopyCashBudgetaryPayment({ OperationType, Date, enpStartDate })
        && canCopyBudgetaryPayment({ OperationType, Date, enpStartDate });
}

function canCopyBudgetaryPayment({ OperationType, Date, enpStartDate }) {
    if (OperationType !== allOperationTypes.BudgetaryPayment.value) {
        return true;
    }

    if (dateHelper().isBefore(enpStartDate)) {
        return true;
    }

    if (dateHelper(Date).isSameOrAfter(enpStartDate)) {
        return true;
    }

    return false;
}

function canCopyCashBudgetaryPayment({ OperationType, Date, enpStartDate }) {
    if (OperationType !== allOperationTypes.CashOrderBudgetaryPayment.value) {
        return true;
    }

    if (dateHelper().isBefore(enpStartDate)) {
        return true;
    }

    if (dateHelper(Date).isSameOrAfter(enpStartDate)) {
        return true;
    }

    return false;
}

/**
 * Возвращает тип операции в строковом формате
 * @param operationType Тип операции (.value в ресурсе)
 * @returns {string} возвращает тип операции в строковом формате либо пустую строку если типа нет
 */
export function description(operationType) {
    const ot = getOperationType(operationType);

    return ot.text || ``;
}

/**
 * Проверяет поддерживает ли операция учёт в сдвоенной системе налогообложения
 * @param operationType Тип операции (.value в ресурсе)
 * @returns {boolean} возвращает true если операция поддерживает сдвоенную систему налогообложения
 */
export function isSupportDoubleTaxationSystemAccounting(operationType) {
    return [
        paymentOrderOperationResources.BankFee.value,
        purseOperationResources.PurseOperationComission.value,
        purseOperationResources.PurseOperationComission.purseOperationType
    ].includes(operationType);
}

/**
 * Проверяет возможность смены налогообложения у операции
 * @param operationType Тип операции (.value в ресурсе)
 * @returns {boolean} возвращает true если у операции можно сменить систему налогообложения
 */
export function availableForChangeTaxSystem(operationType, taxationSystem) {
    let availableOperationTypesForPatent = [
        paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value,
        paymentOrderOperationResources.MemorialWarrantReceiptGoodsPaidCreditCard.value,
        paymentOrderOperationResources.BankFee.value,
        cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.value,
        purseOperationResources.PurseOperationIncome.value,
        purseOperationResources.PurseOperationComission.value
    ];

    if (taxationSystem !== TaxationSystemType.Patent) {
        availableOperationTypesForPatent = [
            ...availableOperationTypesForPatent,
            paymentOrderOperationResources.MemorialWarrantAccrualOfInterest.value,
            purseOperationResources.PurseOperationComission.value];
    }

    return availableOperationTypesForPatent.includes(operationType);
}

export function parseOperationsForChangeTaxStructure(operationList = [], lastClosedPeriod = ``, taxationSystemsForAllYears = [], patentsForAllYears = [], newTaxationSystem) {
    const momentLastClosedPeriod = dateHelper(lastClosedPeriod);
    const listLength = operationList.length;
    const validList = [];
    let invalidCount = 0;
    let i = 0;

    for (i; i < listLength; i += 1) {
        const { OperationType, Date } = operationList[i];
        const momentDate = dateHelper(Date);
        const operationYear = momentDate.year();
        const { IsUsn, IsOsno, IsEnvd } = taxationSystemsForAllYears.find(({ StartYear, EndYear }) => {
            return StartYear <= operationYear && (EndYear === null || EndYear > operationYear);
        });

        const hasPatent = patentsForAllYears.some(({ StartDate, EndDate }) => {
            const patentEndDate = dateHelper(EndDate, `DD.MM.YYYY`);
            const patentStartDate = dateHelper(StartDate, `DD.MM.YYYY`);

            const isSameOrBefore = patentStartDate.isSameOrBefore(momentDate);
            const isSameOrAfter = patentEndDate.isSameOrAfter(momentDate);

            return isSameOrBefore && isSameOrAfter;
        });

        const isDoubleTaxSystemOnDate = [IsUsn, IsOsno, IsEnvd, hasPatent].filter(taxSystem => taxSystem).length > 1;
        const isAvailableForChangeTaxSystem = availableForChangeTaxSystem(OperationType, newTaxationSystem);
        const isAfterLastClosedPeriod = momentDate.isAfter(momentLastClosedPeriod);
        const patentValidityDate = newTaxationSystem === TaxationSystemType.Patent ? hasPatent : true;

        if (isAvailableForChangeTaxSystem && isAfterLastClosedPeriod && isDoubleTaxSystemOnDate && patentValidityDate) {
            validList.push(operationList[i]);
        } else {
            invalidCount += 1;
        }
    }

    return {
        validList,
        invalidCount
    };
}

export function getTaxationSystemsForChange(operationsYears = [], taxationSystemsForAllYears = [], patentsForAllYears = []) {
    const taxationSystem = new Set();

    operationsYears.forEach(year => {
        const { IsUsn, IsOsno, IsEnvd } = taxationSystemsForAllYears.find(({ StartYear, EndYear }) => {
            return StartYear <= year && (EndYear === null || EndYear > year);
        });

        const hasPatent = patentsForAllYears.some(({ StartDate }) => {
            const patentStartYear = dateHelper(StartDate, `DD.MM.YYYY`).year();

            return year === patentStartYear;
        });

        if (IsUsn) {
            taxationSystem.add(taxationSystemsForDropdown.filter(x => x.value === TaxationSystemType.Usn)[0]);
        }

        if (IsOsno) {
            taxationSystem.add(taxationSystemsForDropdown.filter(x => x.value === TaxationSystemType.Osno)[0]);
        }

        if (IsEnvd) {
            taxationSystem.add(taxationSystemsForDropdown.filter(x => x.value === TaxationSystemType.Envd)[0]);
        }

        if (hasPatent) {
            taxationSystem.add(taxationSystemsForDropdown.filter(x => x.value === TaxationSystemType.Patent)[0]);
        }
    });

    return Array.from(taxationSystem);
}

export function isDifferenceAvailableInTax(operationType) {
    return [
        paymentOrderOperationResources.PaymentOrderIncomingCurrencyPaymentFromBuyer.value,
        paymentOrderOperationResources.PaymentOrderOutgoingCurrencyPaymentSuppliersForGoods.value
    ].includes(operationType);
}

export default MoneyOperationHelper;

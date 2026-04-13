import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import innValidator from '@moedelo/frontend-common-v2/apps/requisites/validations/InnValidator';
import KontragentsFormEnum from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import { round } from '../../../../../helpers/numberConverter';
import NdsTypesEnum from '../../../../../enums/newMoney/NdsTypesEnum';
import {
    VALID_INN_LENGTH_LIST,
    VALID_KPP_LENGTH,
    VALID_RU_SETTLEMENT_NUMBER_LENGTH
} from '../../../../../constants/requisitesConstants';
import {
    VALID_OPERATION_NUMBER_LENGTH,
    VALID_OPERATION_LINKED_DOCUMENTS_COUNT
} from '../../../../../constants/commonOperationConstants';

function validationNumberNotEmpty(model) {
    return model && model.Number && `${model.Number}`.length > 0;
}

function validationNumberMaxLength(model) {
    // при редактировании пп из импорта не проверяем длину номера
    if (model && model.Id && model.IsFromImport) {
        return true;
    }

    return model && model.Number && `${model.Number}`.length < VALID_OPERATION_NUMBER_LENGTH;
}

function validateDate(model) {
    return model && model.Date;
}

function validateUin(model) {
    const u = model?.Uin;

    return u === 0 || !!u;
}

function validateDateFormat(model) {
    return dateHelper(model.Date, `DD.MM.YYYY`, true).isValid();
}

function isOutOfLastClosedDate(model, { FinancialResultLastClosedPeriod }) {
    if (model.Date && FinancialResultLastClosedPeriod) {
        return dateHelper(model.Date, `DD.MM.YYYY`, true).isAfter(dateHelper(FinancialResultLastClosedPeriod, `DD.MM.YYYY`, true));
    }

    return true;
}

function isOutOfRegistrationDate(model, { RegistrationDate }) {
    if (model.Date && RegistrationDate) {
        return dateHelper(model.Date, `DD.MM.YYYY`, true).isSameOrAfter(dateHelper(RegistrationDate, `DD.MM.YYYY`, true));
    }

    return true;
}

function isOutOfBalanceDate(model, { BalanceDate }) {
    if (model.Date && BalanceDate) {
        /* Если введены остатки на начало квартала, то сдвигаем дату остатков на начало года,
           чтобы пользователь мог отразить операции по зарплате */
        const balanceDate = dateHelper(BalanceDate, `DD.MM.YYYY`).startOf(`year`).subtract(1, `days`);

        return dateHelper(model.Date, `DD.MM.YYYY`, true).isSameOrAfter(balanceDate);
    }

    return true;
}

function isLessThan2013Year(model) {
    if (model.Date) {
        return dateHelper(model.Date, `DD.MM.YYYY`, true).year() >= 2013;
    }

    return true;
}

function validateAcquiringCommissionDate({ AcquiringCommission, AcquiringCommissionDate }) {
    if (!toFloat(AcquiringCommission)) {
        return true;
    }

    return AcquiringCommissionDate;
}

function validateAcquiringCommissionDateFormat({ AcquiringCommissionDate, AcquiringCommission }) {
    if (!toFloat(AcquiringCommission)) {
        return true;
    }

    return dateHelper(AcquiringCommissionDate, `DD.MM.YYYY`, true).isValid();
}

function isOutOfLastClosedAcquiringCommissionDate({ AcquiringCommissionDate, AcquiringCommission }, { FinancialResultLastClosedPeriod }) {
    if (!toFloat(AcquiringCommission)) {
        return true;
    }

    if (AcquiringCommissionDate && FinancialResultLastClosedPeriod) {
        return dateHelper(AcquiringCommissionDate, `DD.MM.YYYY`, true).isAfter(dateHelper(FinancialResultLastClosedPeriod, `DD.MM.YYYY`, true));
    }

    return true;
}

function isOutOfRegistrationAcquiringCommissionDate({ AcquiringCommissionDate, AcquiringCommission }, { RegistrationDate }) {
    if (!toFloat(AcquiringCommission)) {
        return true;
    }

    if (AcquiringCommissionDate && RegistrationDate) {
        return dateHelper(AcquiringCommissionDate, `DD.MM.YYYY`, true).isSameOrAfter(dateHelper(RegistrationDate, `DD.MM.YYYY`, true));
    }

    return true;
}

function isOutOfBalanceAcquiringCommissionDate({ AcquiringCommissionDate, AcquiringCommission }, { BalanceDate }) {
    if (!toFloat(AcquiringCommission)) {
        return true;
    }

    if (AcquiringCommissionDate && BalanceDate) {
        const balanceDate = dateHelper(BalanceDate, `DD.MM.YYYY`, true).startOf(`year`).subtract(1, `days`);

        return dateHelper(AcquiringCommissionDate, `DD.MM.YYYY`, true).isSameOrAfter(balanceDate);
    }

    return true;
}

function isLessThan2013YearAcquiringCommissionDate({ AcquiringCommissionDate, AcquiringCommission }) {
    if (!toFloat(AcquiringCommission)) {
        return true;
    }

    if (AcquiringCommissionDate) {
        return dateHelper(AcquiringCommissionDate, `DD.MM.YYYY`, true).year() >= 2013;
    }

    return true;
}

function validateTotalSum(model) {
    return model && (!toFloat(model.TotalSum) || model.TotalSum > 0);
}

function validateSum(model) {
    return model && (model.Sum || model.Sum === 0);
}

function sumIsPositive(model) {
    return model.Sum > 0;
}

function validateExchangeRate(model) {
    return model && (model.ExchangeRate || model.ExchangeRate === 0);
}

function exchangeRateIsPositive(model) {
    return model.ExchangeRate > 0;
}

function validateKontragent(model) {
    return model && model.Kontragent.KontragentId;
}

function validateContract(model) {
    return model && model.Contract.ContractBaseId;
}

function validatePeriod(model) {
    const empties = model.Periods.filter(period => period.Description === ``);

    return model && model.Periods && model.Periods.length > 0 && empties.length === 0;
}

function validateSumPeriod(model) {
    const empties = model.Periods.filter(period => !toFloat(period.Sum) || period.Sum <= 0);

    return model && model.Periods && model.Periods.length > 0 && empties.length === 0;
}

function validateDefaultSumPeriod(model) {
    const invalid = model.Periods.filter(period => {
        if (model.FixedAsset?.IsRentRemains && period.Id === model.FirstPeriodId) {
            return false;
        }

        return period.Sum > round(period.DefaultSum + period.PaymentRequiredSum);
    });

    return invalid.length === 0;
}

function validateMediationCommission(model) {
    return toFloat(model.Sum) >= toFloat(model.MediationCommission);
}

function validateNdsSumLessSum(model) {
    if (!model.Sum) {
        return true;
    }

    return toFloat(model.Sum) > toFloat(model.NdsSum);
}

function validateNdsSumLessTotalSum(model) {
    if (!model.TotalSum) {
        return true;
    }

    return toFloat(model.TotalSum) > toFloat(model.NdsSum);
}

function validateNdsSumLessAcquiringCommission(model) {
    if (!model.AcquiringCommission) {
        return true;
    }

    return toFloat(model.AcquiringCommission) > toFloat(model.NdsSum);
}

function validateKontragentSettlementAccount(model) {
    if (model && model.Kontragent.KontragentSettlementAccount) {
        return model.Kontragent.KontragentSettlementAccount.length === VALID_RU_SETTLEMENT_NUMBER_LENGTH;
    }

    return true;
}

/** пока что только для Оплаты от покупателя. Если контрагент нерезидент, в поле Счет даем вводить что угодно */
function validateKontragentOnlyResidentSettlementAccount(model) {
    const { KontragentSettlementAccount, KontragentForm } = model.Kontragent;

    if (model && KontragentSettlementAccount && (!KontragentForm || KontragentForm !== KontragentsFormEnum.NR)) {
        return (!KontragentForm || KontragentForm !== KontragentsFormEnum.NR) &&
            model.Kontragent.KontragentSettlementAccount.length === VALID_RU_SETTLEMENT_NUMBER_LENGTH;
    }

    return true;
}

function validateKontragentInn(model) {
    if (model && model.Kontragent.KontragentForm !== KontragentsFormEnum.NR && model.Kontragent.KontragentINN && model.Kontragent.KontragentINN.length) {
        return VALID_INN_LENGTH_LIST.includes(model.Kontragent.KontragentINN.length);
    }

    return true;
}

function validateKontragentInnFormat(model) {
    if (model && model.Kontragent.KontragentForm !== KontragentsFormEnum.NR && model.Kontragent.KontragentINN && model.Kontragent.KontragentINN.length) {
        return innValidator.isValid(model.Kontragent.KontragentINN, model.Kontragent.KontragentForm === KontragentsFormEnum.UL);
    }

    return true;
}

function validateKontragentKpp(model) {
    if (model && model.Kontragent.KontragentKPP && model.Kontragent.KontragentKPP.length) {
        const kpp = model.Kontragent.KontragentKPP;

        return kpp === `0` || kpp.length === VALID_KPP_LENGTH;
    }

    return true;
}

function validateDocumentsSum(model) {
    if (model && model.Sum && model.Documents) {
        const documentsSum = model.Documents.reduce((sum, doc) => sum + toFloat(doc.Sum), 0);

        return round(documentsSum) <= round(toFloat(model.Sum));
    }

    return true;
}

function validateDocumentsCount(model) {
    if (model && model.Sum && model.Documents) {
        return model.Documents.length <= VALID_OPERATION_LINKED_DOCUMENTS_COUNT;
    }

    return true;
}

function validateBillsSum(model) {
    if (model && model.Sum && model.Bills) {
        const documentsSum = model.Bills.reduce((sum, doc) => sum + toFloat(doc.Sum), 0);

        return round(documentsSum) <= round(toFloat(model.Sum));
    }

    return true;
}

function validateDescription(model) {
    if (model && model.Description) {
        return model.Description.length <= 210;
    }

    return true;
}

function notEmptyDescription(model) {
    return model && model.Description && model.Description.length;
}

function validateWorker(model) {
    return !!model.WorkerName;
}

function validateMiddlemanContract(model) {
    const { ContractNumber } = model.MiddlemanContract || {};

    return !!ContractNumber;
}

function validationToSettlementAccountIdNotEmpty(model) {
    return model && model.ToSettlementAccountId;
}

function validationFromSettlementAccountIdNotEmpty(model) {
    return model && model.FromSettlementAccountId;
}

function validationNdsSum(model) {
    if (model.IncludeNds && model.NdsType !== NdsTypesEnum.Nds0 && model.NdsType !== NdsTypesEnum.None && !model.NdsSum) {
        return false;
    }

    return true;
}

function validateMediationNdsSumLessMediationCommission(model) {
    if (model.IsMediation && model.IncludeMediationCommissionNds && model.MediationCommission) {
        return toFloat(model.MediationCommission) > toFloat(model.MediationCommissionNdsSum);
    }

    return true;
}

function validationMediationCommissionNdsSum(model) {
    if (model.IsMediation
        && model.IncludeMediationCommissionNds
        && model.MediationCommissionNdsType !== NdsTypesEnum.Nds0
        && model.MediationCommissionNdsType !== NdsTypesEnum.None
        && !model.MediationCommissionNdsSum) {
        return false;
    }

    return true;
}

function notEmptyFixedAsset(model) {
    return model?.FixedAsset?.Id > 0;
}

const primitiveMethods = {
    maxLength(value, length) {
        if (typeof value === `string`) {
            return value.length < length + 1;
        }

        if (value.toString) {
            return value.toString().length < length + 1;
        }

        return true;
    }
};

const validateOnDoNotExceedSum = (model, req, data) => {
    if (!model) {
        return true;
    }

    if (!data.reserve.opened) {
        return true;
    }

    if (model.ReserveSum && model.Documents) {
        const documentsSum = model.Documents.reduce((sum, doc) => sum + toFloat(doc.Sum), 0);

        return round(documentsSum + toFloat(model.ReserveSum)) <= round(toFloat(model.Sum));
    }

    return true;
};

function reserveSumIsPositive(model, req, data) {
    if (!model) {
        return true;
    }

    if (!data.reserve.opened) {
        return true;
    }

    if (model.ReserveSum == null) {
        return true;
    }

    return model.ReserveSum > 0;
}

function unifiedTaxPaymentSum(model, req, data) {
    if (!data) {
        return true;
    }

    const { Sum } = model;
    const { subPaymentsTotalSum } = data;

    if ([Sum, subPaymentsTotalSum].includes(0)) {
        return true;
    }

    return Sum === subPaymentsTotalSum;
}

export {
    validationNumberNotEmpty,
    validationNumberMaxLength,
    validateSum,
    validateTotalSum,
    sumIsPositive,
    validateDate,
    validateDateFormat,
    isOutOfLastClosedDate,
    isOutOfRegistrationDate,
    isOutOfBalanceDate,
    validateKontragent,
    validateContract,
    validateMediationCommission,
    validateKontragentSettlementAccount,
    validateKontragentOnlyResidentSettlementAccount,
    validateKontragentInn,
    validateKontragentInnFormat,
    validateKontragentKpp,
    validateDocumentsSum,
    validateDocumentsCount,
    validateDescription,
    notEmptyDescription,
    validateBillsSum,
    primitiveMethods,
    isLessThan2013Year,
    validateWorker,
    validateMiddlemanContract,
    validateAcquiringCommissionDate,
    validateAcquiringCommissionDateFormat,
    isOutOfLastClosedAcquiringCommissionDate,
    isOutOfRegistrationAcquiringCommissionDate,
    isOutOfBalanceAcquiringCommissionDate,
    isLessThan2013YearAcquiringCommissionDate,
    validateExchangeRate,
    exchangeRateIsPositive,
    validationToSettlementAccountIdNotEmpty,
    validationFromSettlementAccountIdNotEmpty,
    validationNdsSum,
    validationMediationCommissionNdsSum,
    validatePeriod,
    validateSumPeriod,
    validateDefaultSumPeriod,
    notEmptyFixedAsset,
    validateOnDoNotExceedSum,
    reserveSumIsPositive,
    unifiedTaxPaymentSum,
    validateNdsSumLessSum,
    validateNdsSumLessTotalSum,
    validateMediationNdsSumLessMediationCommission,
    validateNdsSumLessAcquiringCommission,
    validateUin
};

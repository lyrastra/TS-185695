import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import subcontoTypeResource from '../../../../../resources/newMoney/subcontoTypeResource';
import SubcontoTypeEnum from '../../../../../enums/newMoney/SubcontoTypeEnum';
import { VALID_RU_SETTLEMENT_NUMBER_LENGTH } from '../../../../../constants/requisitesConstants';

const getSumValidation = posting => {
    if (!toFloat(posting.Sum)) {
        return `Укажите сумму`;
    }

    return ``;
};

const getValidatedBySum = posting => {
    return {
        ...posting,
        SumError: getSumValidation(posting)
    };
};

const getCreditValidation = posting => {
    if (!posting.Credit || !posting.Credit.Code) {
        return `Укажите кредит`;
    }

    return ``;
};

const getValidatedByCredit = posting => {
    return {
        ...posting,
        CreditError: getCreditValidation(posting),
        SubcontoCreditError: ``
    };
};

const getSubcontoValidation = subconto => {
    /** до полного переезда, потом оставить Type */
    const getType = ({ SubcontoType, Type }) => {
        return !Number.isNaN(parseInt(SubcontoType, 10)) ? SubcontoType : Type;
    };

    const getId = ({ SubcontoId, Id }) => {
        return !Number.isNaN(parseInt(SubcontoId, 10)) ? SubcontoId : Id;
    };

    const customSubconto = subconto && subconto.length && subconto.find(({ Subconto, Type }) => {
        if (!Subconto || Type === SubcontoTypeEnum.AppointmentOfTrustFunds) {
            return false;
        }

        const type = getType(Subconto);

        return Subconto && type &&
            (type === SubcontoTypeEnum.SpecialSettlementAccount ||
                type === SubcontoTypeEnum.SpecialSettlementAccountForDigitalRuble) &&
            Subconto.Name.length !== VALID_RU_SETTLEMENT_NUMBER_LENGTH;
    });

    if (customSubconto) {
        return `Ожидается 20 цифр`;
    }

    const defaultSubconto = subconto && subconto.length && subconto.find(({ Subconto }) => {
        if (!Subconto) {
            return true;
        }

        const type = getType(Subconto);
        const id = getId(Subconto);

        return !id && id !== 0 && (!type || (type !== SubcontoTypeEnum.SpecialSettlementAccount || type === SubcontoTypeEnum.SpecialSettlementAccountForDigitalRuble));
    });

    if (defaultSubconto) {
        return `Укажите субконто "${subcontoTypeResource[defaultSubconto.Type]}"`;
    }

    return ``;
};

const getCreditSubcontoValidation = posting => {
    return getSubcontoValidation(posting.SubcontoCredit);
};

const getValidatedByCreditSubconto = posting => {
    return {
        ...posting,
        SubcontoCreditError: getCreditSubcontoValidation(posting)
    };
};

const getDebitValidation = posting => {
    if (!posting.Debit || !posting.Debit.Code) {
        return `Укажите дебет`;
    }

    return ``;
};

const getValidatedByDebit = posting => {
    return {
        ...posting,
        DebitError: getDebitValidation(posting),
        SubcontoDebitError: ``
    };
};

const getDebitSubcontoValidation = posting => {
    return getSubcontoValidation(posting.SubcontoDebit);
};

const getValidatedByDebitSubconto = posting => {
    return {
        ...posting,
        SubcontoDebitError: getDebitSubcontoValidation(posting)
    };
};

const getValidatedList = postings => {
    return postings && postings.map(posting => ({
        ...posting,
        CreditError: getCreditValidation(posting),
        SumError: getSumValidation(posting),
        SubcontoCreditError: getCreditSubcontoValidation(posting),
        DebitError: getDebitValidation(posting),
        SubcontoDebitError: getDebitSubcontoValidation(posting)
    }));
};

const getAllSumValidation = (postings, { Sum }) => {
    if (!postings) {
        return ``;
    }

    const sum = postings.reduce((agr, posting) => {
        const add = (posting.Sum !== null && !posting.ReadOnly) ? posting.Sum : 0;

        return agr + add;
    }, 0);

    if (sum > Sum) {
        return `Сумма проводок не может быть больше суммы операции`;
    }

    return ``;
};

const isPostingValid = posting => {
    return !posting.CreditError
        && !posting.SubcontoCreditError
        && !posting.DebitError
        && !posting.SubcontoDebitError
        && !posting.SumError;
};

const isListValid = postings => {
    return !postings || postings.every(posting => isPostingValid(posting));
};

const isValid = (postings, { Sum, needAllSumValidation }) => {
    const isValidSum = needAllSumValidation ? !getAllSumValidation(postings, { Sum }) : true;

    return isListValid(postings) && isValidSum;
};

export default {
    getValidatedList,
    getValidatedBySum,
    getValidatedByCredit,
    getValidatedByCreditSubconto,
    getValidatedByDebit,
    getValidatedByDebitSubconto,
    getAllSumValidation,
    isValid
};


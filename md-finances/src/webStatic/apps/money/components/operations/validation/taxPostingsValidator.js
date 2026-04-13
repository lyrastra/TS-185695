/* eslint-disable padding-line-between-statements */
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

const getSumValidation = posting => {
    if (posting.Description && toFloat(posting.Sum) === false) {
        return `Укажите сумму`;
    }

    return ``;
};

const getSumIsNotZero = posting => {
    const sum = toFloat(posting.Sum);

    if (sum === 0) {
        return `Укажите сумму не равную 0`;
    }

    return ``;
};

const getNegativeSumMessage = posting => {
    const isPositiveSum = (toFloat(posting.Sum) || 0) >= 0;

    if (posting.Description && isPositiveSum && posting.Direction === Direction.Outgoing) {
        return `Укажите отрицательный доход`;
    }

    return ``;
};

const getDescriptionValidation = posting => {
    if (toFloat(posting.Sum) !== false && !posting.Description) {
        return `Укажите описание`;
    }

    return ``;
};

const getValidatedIpOsnoPosting = posting => {
    return {
        ...posting,
        SumError: getSumValidation(posting) || getSumIsNotZero(posting)
    };
};

const getValidatedUsnPosting = posting => {
    return {
        ...posting,
        DescriptionError: getDescriptionValidation(posting),
        SumError: getSumValidation(posting) || getSumIsNotZero(posting)
    };
};

const getValidatedUnifiedBudgetaryPaymentIpOsnoPosting = posting => {
    return {
        ...posting,
        SumError: getSumValidation(posting)
    };
};

const getValidatedUnifiedBudgetaryPaymentUsnPosting = posting => {
    return {
        ...posting,
        DescriptionError: getDescriptionValidation(posting),
        SumError: getSumValidation(posting)
    };
};

const getValidatedNegativeUsnPosting = posting => {
    return {
        ...posting,
        DescriptionError: getDescriptionValidation(posting),
        SumError: getSumValidation(posting) || getNegativeSumMessage(posting)
    };
};

const getValidatedOsnoPosting = posting => {
    return posting;
};

const getValidatedIpOsnoList = postings => {
    return postings && postings.map(posting => {
        return getValidatedIpOsnoPosting(posting);
    });
};

const getValidatedUsnList = postings => {
    return postings && postings.map(posting => {
        return getValidatedUsnPosting(posting);
    });
};

const getValidatedNegativeUsnList = postings => {
    return postings && postings.map(posting => {
        return getValidatedNegativeUsnPosting(posting);
    });
};

const getValidatedList = (postings, { isOsno, isIp, negativeUsn } = {}) => {
    if (isOsno && isIp) {
        return getValidatedIpOsnoList(postings);
    }

    if (isOsno) {
        return postings;
    }

    if (negativeUsn) {
        return getValidatedNegativeUsnList(postings);
    }

    return getValidatedUsnList(postings);
};

const isUsnListValid = postings => {
    return !postings || postings.every(posting => {
        return !posting.DescriptionError && !posting.SumError;
    });
};

const getAllSumValidation = (postings, { Sum }) => {
    if (!postings) {
        return ``;
    }
    const sum = postings.reduce((agr, posting) => {
        return agr + (posting.Sum !== null ? Math.abs(posting.Sum) : 0);
    }, 0);

    if (sum.toFixed(2) > Sum) {
        return `Сумма проводок не может быть больше общей суммы операции`;
    }

    return ``;
};

const getUnifiedBudgetarySubPaymentValidation = (postings, { Sum }) => {
    if (!postings) {
        return ``;
    }
    const sum = postings.reduce((agr, posting) => {
        return agr + (posting.Sum !== null ? Math.abs(toFloat(posting.Sum, 2)) : 0);
    }, 0);

    if (sum.toFixed(2) > Sum) {
        return `Сумма проводки не может быть больше суммы вида налога/взноса`;
    }

    return ``;
};

const getAllTotalSumValidation = (postings, { TotalSum, isDifferenceAllowable }) => {
    if (!postings) {
        return ``;
    }

    const sum = postings.reduce((agr, posting) => {
        return agr + (posting.Sum !== null ? posting.Sum : 0);
    }, 0);

    const allowableAmount = Number(0.01);
    const difference = sum.toFixed(2) - TotalSum;
    const differenceIsAllowable = isDifferenceAllowable && difference.toFixed(2) <= allowableAmount;

    if (sum.toFixed(2) > TotalSum && !differenceIsAllowable) {
        return `Сумма проводок не может быть больше общей суммы операции`;
    }

    return ``;
};

const getAllLoanInterestSumValidation = (postings, { LoanInterestSum }) => {
    if (!postings) {
        return ``;
    }
    const sum = postings.reduce((agr, posting) => {
        return agr + (posting.Sum !== null ? posting.Sum : 0);
    }, 0);

    if (sum > LoanInterestSum) {
        return `Сумма проводок не может быть больше суммы процентов`;
    }

    return ``;
};

const getAllExchangeRateDiffValidation = (postings, { ExchangeRateDiff }) => {
    if (!postings) {
        return ``;
    }
    const sum = postings.reduce((agr, posting) => {
        return agr + (posting.Sum !== null ? posting.Sum : 0);
    }, 0);

    if (sum > 0 && sum > ExchangeRateDiff) {
        return `Сумма проводок не может быть больше курсовой разницы`;
    }

    return ``;
};

const isValid = (postings, {
    Sum, TotalSum, isOsno, LoanInterestSum, ExchangeRateDiff, needAllSumValidation, needAllTotalSumValidation,
    isDifferenceAllowable
}) => {
    const validList = isOsno ? true : isUsnListValid(postings);
    const isValidSum = needAllSumValidation ? !getAllSumValidation(postings, { Sum }) : true;
    const isValidTotalSum = needAllTotalSumValidation ? !getAllTotalSumValidation(postings, { TotalSum, isDifferenceAllowable }) : true;
    const isValidLoanInterestSum = LoanInterestSum && LoanInterestSum > 0 ? !getAllLoanInterestSumValidation(postings, { LoanInterestSum }) : true;
    const isValidExchangeRateDiff = ExchangeRateDiff && ExchangeRateDiff > 0 ? !getAllExchangeRateDiffValidation(postings, { ExchangeRateDiff }) : true;

    return validList && isValidSum && isValidTotalSum && isValidLoanInterestSum && isValidExchangeRateDiff;
};

export default {
    getValidatedUsnPosting,
    getValidatedIpOsnoPosting,
    getValidatedOsnoPosting,
    getValidatedList,
    isUsnListValid,
    getValidatedNegativeUsnList,
    getAllSumValidation,
    getAllTotalSumValidation,
    getAllLoanInterestSumValidation,
    getAllExchangeRateDiffValidation,
    getValidatedNegativeUsnPosting,
    isValid,
    getSumIsNotZero,
    getUnifiedBudgetarySubPaymentValidation,
    getValidatedUnifiedBudgetaryPaymentIpOsnoPosting,
    getValidatedUnifiedBudgetaryPaymentUsnPosting
};


import KontragentsFormEnum from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import innValidator from '@moedelo/frontend-common-v2/apps/requisites/validations/InnValidator';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { VALID_RU_SETTLEMENT_NUMBER_LENGTH } from '../../../../../../../constants/requisitesConstants';

function validateRecipientName(model) {
    return model && model.Recipient.Name?.length > 0;
}

function validateRecipientBankName(model) {
    return model?.Recipient.BankName?.length > 0;
}

function validateRecipientSettlementAccount(model) {
    const { SettlementAccount } = model?.Recipient;

    return SettlementAccount ? SettlementAccount.length === VALID_RU_SETTLEMENT_NUMBER_LENGTH : true;
}

function validateRecipientSettlementAccountExists(model) {
    const { SettlementAccount } = model?.Recipient;

    return SettlementAccount?.length > 0;
}

function validateRecipientBankCorrespondentAccountExists(model) {
    const { Date } = model;
    const { SettlementAccount, BankCorrespondentAccount } = model?.Recipient;

    if (dateHelper(Date).isAfter(`31.12.2020`) && /^(03|40204)\d*/.test(SettlementAccount)) {
        return BankCorrespondentAccount?.length > 0;
    }

    return true;
}

function validateRecipientBankCorrespondentAccount(model) {
    const { BankCorrespondentAccount } = model?.Recipient;

    return BankCorrespondentAccount ? BankCorrespondentAccount.length === VALID_RU_SETTLEMENT_NUMBER_LENGTH : true;
}

function validateRecipientInn(model, req, data) {
    const { isUnifiedBudgetaryPayment } = data;

    if (isUnifiedBudgetaryPayment) {
        return true;
    }

    const { Form, Inn } = model.Recipient;

    if (model && Form !== KontragentsFormEnum.NR && Inn?.length) {
        const { length } = Inn;

        return length === 8 || length === 10 || length === 12;
    }

    return true;
}

function validateRecipientInnFormat(model, req, data) {
    const { isUnifiedBudgetaryPayment } = data;

    if (isUnifiedBudgetaryPayment) {
        return true;
    }

    if (model && model.Recipient.Form !== KontragentsFormEnum.NR && model.Recipient.Inn?.length) {
        return innValidator.isValid(model.Recipient.Inn, model.Recipient.Form === KontragentsFormEnum.UL);
    }

    return true;
}

function validateRecipientKpp(model) {
    if (model?.Recipient.Kpp?.length) {
        const kpp = model.Recipient.Kpp;

        return kpp === `0` || kpp.length === 9;
    }

    return true;
}

function validateRecipientOkato(model) {
    const momentDate = dateHelper(model.Date, `DD.MM.YYYY`, true);
    const okato = model.Recipient.Okato;

    if (momentDate.isValid() && momentDate.isBefore(`2014-01-01`) && okato?.length) {
        return !/0{11}/.test(okato) && /[0-9]{11}/.test(okato);
    }

    return true;
}

function validateRecipientOktmo(model, req, data) {
    const { isUnifiedBudgetaryPayment } = data;
    const momentDate = dateHelper(model.Date, `DD.MM.YYYY`, true);
    const oktmo = model.Recipient.Oktmo;

    if (model && momentDate.isValid() && oktmo?.length && !isUnifiedBudgetaryPayment) {
        return !/0{8}/.test(oktmo) && /^[0-9]{8}$/.test(oktmo);
    }

    return true;
}

function validateUnifiedBudgetaryPaymentRecipientOktmo(model, req, data) {
    const { isUnifiedBudgetaryPayment } = data;
    const momentDate = dateHelper(model.Date, `DD.MM.YYYY`, true);
    const oktmo = model.Recipient.Oktmo;

    if (model && momentDate.isValid() && oktmo?.length && isUnifiedBudgetaryPayment) {
        return (!/0{8}/.test(oktmo) && /^[0-9]{8}$/.test(oktmo)) || (oktmo.length === 1 && parseInt(oktmo, 10) === 0);
    }

    return true;
}

function validateMiddleRecepientSettlementAccount(model) {
    const { Date } = model;
    const { SettlementAccount } = model.Recipient;

    if (SettlementAccount.length !== VALID_RU_SETTLEMENT_NUMBER_LENGTH) {
        return true;
    }

    if (dateHelper(Date).isBetween(`01.01.2021`, `01.05.2021`, undefined, `[)`)) {
        return /^(40101|03|40204)\d*/.test(SettlementAccount);
    }

    return true;
}

function validateNewRecepientSettlementAccount(model) {
    const { Date } = model;
    const { SettlementAccount } = model.Recipient;

    if (SettlementAccount.length !== VALID_RU_SETTLEMENT_NUMBER_LENGTH) {
        return true;
    }

    if (dateHelper(Date).isAfter(`30.04.2021`)) {
        return /^(03|40204)\d*/.test(SettlementAccount);
    }

    return true;
}

function validateRecepientBankCorrespondentAccountCommonFormat(model) {
    const { Date } = model;
    const { SettlementAccount, BankCorrespondentAccount } = model.Recipient;

    if (dateHelper(Date).isBefore(`01.01.2021`)) {
        return true;
    }

    if (/^(03)\d*/.test(SettlementAccount)) {
        return /^(40102)/.test(BankCorrespondentAccount);
    }

    return true;
}

function validateRecepientBankCorrespondentAccountBaikonurFormat(model) {
    const { Date } = model;
    const { SettlementAccount, BankCorrespondentAccount } = model.Recipient;

    if (dateHelper(Date).isBefore(`01.01.2021`)) {
        return true;
    }

    if (/^(40204)\d*/.test(SettlementAccount)) {
        return /^(00000)/.test(BankCorrespondentAccount);
    }

    return true;
}

function validateDocumentDate(model) {
    if (!model.isComplexDocumentNumber) {
        return true;
    }

    return !(model.complexNumber.literalCode !== 0 && !dateHelper(model.DocumentDate, `DD.MM.YYYY`, true).isValid());
}

function validateDocumentNumber(model) {
    if (!model.isComplexDocumentNumber) {
        return true;
    }

    const { DocumentDate, complexNumber: { literalCode, value } } = model;
    const isDateValid = dateHelper(DocumentDate, `DD.MM.YYYY`, true).isValid();
    const isLiteralCodeSelected = literalCode !== 0;
    const isNoNumber = !value || value === `0`;

    if (isLiteralCodeSelected && isNoNumber) {
        return false;
    }

    return !(!isLiteralCodeSelected && isNoNumber && isDateValid);
}

function validateDocumentNumberLength(model) {
    const { isComplexDocumentNumber, complexNumber: { literalCode, value } } = model;

    return !(isComplexDocumentNumber && !!literalCode && value.length > 13);
}

function unifiedTaxPaymentSumMore(model, req, data) {
    if (!data?.subPaymentsTotalSum) {
        return true;
    }

    const { Sum } = model;
    const { subPaymentsTotalSum } = data;

    if ([Sum, subPaymentsTotalSum].includes(0)) {
        return true;
    }

    return Sum >= subPaymentsTotalSum;
}

function unifiedTaxPaymentSumLess(model, req, data) {
    if (!data?.subPaymentsTotalSum) {
        return true;
    }

    const { Sum } = model;
    const { subPaymentsTotalSum } = data;

    if ([Sum, subPaymentsTotalSum].includes(0)) {
        return true;
    }

    return Sum <= subPaymentsTotalSum;
}

export {
    validateRecipientName,
    validateRecipientBankName,
    validateRecipientSettlementAccount,
    validateMiddleRecepientSettlementAccount,
    validateNewRecepientSettlementAccount,
    validateRecipientSettlementAccountExists,
    validateRecipientBankCorrespondentAccount,
    validateRecipientBankCorrespondentAccountExists,
    validateRecepientBankCorrespondentAccountCommonFormat,
    validateRecepientBankCorrespondentAccountBaikonurFormat,
    validateRecipientInn,
    validateRecipientInnFormat,
    validateRecipientKpp,
    validateRecipientOkato,
    validateRecipientOktmo,
    validateUnifiedBudgetaryPaymentRecipientOktmo,
    validateDocumentDate,
    validateDocumentNumber,
    validateDocumentNumberLength,
    unifiedTaxPaymentSumMore,
    unifiedTaxPaymentSumLess
};

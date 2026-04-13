import SyntheticAccountCodesEnum from '../../../../../../../../../../enums/SyntheticAccountCodesEnum';

function validateKbk(model) {
    return model.Kbk.Id !== null;
}

function validateAccountCode(model) {
    return model.AccountCode !== null;
}

function validateSum(model) {
    const { Sum } = model;

    return Sum > 0;
}

function compareWithMainSum(model, requisites, data) {
    return model.Sum <= data.budgetaryPaymentModel.Sum;
}

function validateTradingObject(model) {
    if (model.AccountCode !== SyntheticAccountCodesEnum._68_09) {
        return true;
    }

    return !!model.TradingObjectId;
}

function validatePatent(model) {
    if (model.AccountCode !== SyntheticAccountCodesEnum._68_14) {
        return true;
    }

    return !!model.PatentId;
}

export {
    validateKbk,
    validateAccountCode,
    validateSum,
    validateTradingObject,
    validatePatent,
    compareWithMainSum
};

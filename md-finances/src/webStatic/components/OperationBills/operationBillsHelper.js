import OperationBillsStore from './store/OperationBillsStore';
import MoneyOperationHelper from '../../helpers/MoneyOperationHelper';

function getContractBaseId(model) {
    let contractBaseId;

    if (MoneyOperationHelper.isMediation(parseInt(model.get(`OperationType`), 10))) {
        const middleManContract = model.get(`MiddlemanContract`) || {};

        if (middleManContract.get && middleManContract.get(`ContractNumber`) > 0) {
            contractBaseId = parseInt(middleManContract.get(`DocumentBaseId`), 10);
        }
    } else {
        const contractId = parseInt(model.get(`ContractBaseId`), 10);

        if (contractId) {
            contractBaseId = contractId;
        }
    }

    return contractBaseId;
}

function getStore(model = {}) {
    try {
        const isNew = model.get(`action`) === `new`;
        const billsArray = model.get(`Bills`);
        const operationSum = model.get(`Sum`);
        const kontragentId = model.get(`KontragentId`);
        const baseDocumentId = model.get(`BaseDocumentId`);
        const contractBaseId = getContractBaseId(model);
        const handlers = {};

        if (model.setKontragent) {
            handlers.setKontragentFromBill = model.setKontragent.bind(model);
        }

        const options = {
            billsArray,
            kontragentId,
            baseDocumentId,
            operationSum,
            contractBaseId,
            isNew,
            handlers
        };

        return new OperationBillsStore(options);
    } catch (e) {
        throw new Error(e);
    }
}

const operationBillsHelper = {
    initBillsStore: (model = {}) => {
        const currentModel = model;

        try {
            const operationType = parseInt(currentModel.get(`OperationType`) || currentModel.get(`PurseOperationType`), 10);

            if (MoneyOperationHelper.canOperationUseBills(operationType) && !currentModel.operationBillsStore) {
                currentModel.operationBillsStore = getStore(currentModel);
            }
        } catch (e) {
            throw new Error(e);
        }
    },
    validateModelAndBillsSum: (model = {}) => {
        try {
            const operationType = parseInt(model.get(`OperationType`) || model.get(`PurseOperationType`), 10);

            if (MoneyOperationHelper.canOperationUseBills(operationType) && model.get(`BillsSum`) > model.get(`Sum`)) {
                return `Сумма поступления не может быть меньше общей суммы привязанных счетов`;
            }
        } catch (e) {
            throw new Error(e);
        }

        return null;
    },
    getContractBaseId
};

export default operationBillsHelper;

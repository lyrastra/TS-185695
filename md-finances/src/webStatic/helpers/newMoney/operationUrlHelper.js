import DirectionEnum from '@moedelo/frontend-enums/mdEnums/Direction';
import {
    isSettlement, isCash, isPurse, getOperationType
} from '../../helpers/MoneyOperationHelper';
import { paymentOrderOperationResources } from '../../resources/MoneyOperationTypeResources';

export function getEditUrlHash(operation) {
    if (isSettlement(operation.OperationType)) {
        return `edit/settlement/${operation.DocumentBaseId}/${operation.OperationType}`;
    }

    if (isCash(operation.OperationType)) {
        return `edit/cash/${operation.DocumentBaseId}`;
    }

    if (isPurse(operation.OperationType)) {
        return `edit/purse/${operation.DocumentBaseId}`;
    }

    return false;
}

export function getCopyUrlHash(operation) {
    if (isSettlement(operation.OperationType)) {
        return `copy/settlement/${operation.DocumentBaseId}/${operation.OperationType}`;
    }

    if (isCash(operation.OperationType)) {
        return `copy/cash/${operation.DocumentBaseId}`;
    }

    if (isPurse(operation.OperationType)) {
        return `copy/purse/${operation.DocumentBaseId}`;
    }

    return false;
}

export function getRestMoneyPath({ operationType }) {
    return `/Money/api/v1/PaymentOrders/${getDirectionAndOperationTypePath(operationType)}`;
}

export function getRestTaxPostingsMoneyPath({ operationType }) {
    switch (operationType) {
        case paymentOrderOperationResources.PaymentOrderIncomingCurrencyPaymentFromBuyer.value:
            return `/Providing/api/v1/Money/PaymentOrders/${getDirectionAndOperationTypePath(operationType)}/TaxPostings/Generate`;
        default:
            return `/TaxPostingsMoney/api/v1/PaymentOrders/${getDirectionAndOperationTypePath(operationType)}`;
    }
}

export function getRestAccPostingsMoneyPath({ operationType }) {
    switch (operationType) {
        case paymentOrderOperationResources.PaymentOrderIncomingCurrencyPaymentFromBuyer.value:
            return `/Providing/api/v1/Money/PaymentOrders/${getDirectionAndOperationTypePath(operationType)}/AccPostings/Generate`;
        default:
            return `/AccPostingsMoney/api/v1/PaymentOrders/${getDirectionAndOperationTypePath(operationType)}`;
    }
}

function getDirectionAndOperationTypePath(operationType) {
    const operationTypeObj = getOperationType(parseInt(operationType, 10));

    return operationTypeObj.Direction === DirectionEnum.Incoming ? `Incoming/${operationTypeObj.RestPath}` : `Outgoing/${operationTypeObj.RestPath}`;
}

export default {
    getEditUrlHash, getCopyUrlHash, getRestMoneyPath, getRestTaxPostingsMoneyPath, getRestAccPostingsMoneyPath
};

import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { getOperationTypeByDirectionAndLegalType } from '../helpers/MoneyOperationHelper';

function getIncomingOperationTypeByLegalType(legalType, isCurrencyAvailable = true) {
    return {
        title: `Поступления`,
        data: getOperationTypeByDirectionAndLegalType(Direction.Incoming, legalType, isCurrencyAvailable)
    };
}

function getOutgoingOperationTypeByLegalType(legalType, isCurrencyAvailable = true) {
    return {
        title: `Списания`,
        data: getOperationTypeByDirectionAndLegalType(Direction.Outgoing, legalType, isCurrencyAvailable)
    };
}

function getAllOperationTypeByLegalType(legalType, isCurrencyAvailable = true) {
    return [
        [{
            value: [0],
            text: `Все`
        }],
        getOutgoingOperationTypeByLegalType(legalType, isCurrencyAvailable),
        getIncomingOperationTypeByLegalType(legalType, isCurrencyAvailable)
    ];
}

export default getAllOperationTypeByLegalType;
export { getIncomingOperationTypeByLegalType, getOutgoingOperationTypeByLegalType, getAllOperationTypeByLegalType };

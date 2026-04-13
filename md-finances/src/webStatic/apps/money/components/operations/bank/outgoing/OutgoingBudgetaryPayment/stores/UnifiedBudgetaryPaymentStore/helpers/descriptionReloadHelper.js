import { isNumber } from '@moedelo/frontend-core-v2/helpers/typeCheckHelper';
import OperationStateEnum from '../../../../../../../../../../enums/newMoney/OperationStateEnum';

export function isAvailableOperationStateToReloadDescription(operationState) {
    const unavailableStates = [
        OperationStateEnum.Imported,
        OperationStateEnum.Duplicate,
        OperationStateEnum.MissingKontragent,
        OperationStateEnum.MissingWorker,
        OperationStateEnum.ImportProcessing,
        OperationStateEnum.DuplicateProcessing,
        OperationStateEnum.MissingKontragentProcessing,
        OperationStateEnum.MissingWorkerProcessing,
        OperationStateEnum.Invalid,
        OperationStateEnum.NoSubPayments,
        OperationStateEnum.MissingExchangeRate,
        OperationStateEnum.MissingCurrencySettlementAccount,
        OperationStateEnum.MissingContract,
        OperationStateEnum.MissingCommissionAgent
    ];

    return isNumber(operationState) && !unavailableStates.includes(operationState);
}

export default {};


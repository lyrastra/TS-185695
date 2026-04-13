/* eslint-disable no-plusplus,no-await-in-loop */
import MoneyOperationService from '../../services/newMoney/moneyOperationService';
import BankIntegrationStatusCodeEnum from '../../enums/BankIntegrationStatusCodeEnum';
import {
    isMemorial, isCash, isPurse
} from '../../helpers/MoneyOperationHelper';
import { actionEnum } from '../../resources/newMoney/saveButtonResource';
import { putApproveById } from '../../services/approvedService';
import notificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';

export const isDownloadable = operation => {
    const canDownload = typeof operation.CanDownload === `boolean` ? operation.CanDownload : true;

    return !isMemorial(operation.OperationType) && !isPurse(operation.OperationType) && canDownload;
};

export const is1CDisabled = operation => isCash(operation.OperationType);

export const isAnyDownloadable = operationList => operationList.some(isDownloadable);

export const isAllCanBeSent = operationList => operationList.every(operation => operation.CanSendToBank);

export const isAny1CEnabled = operationList => operationList.some(operation => isCash(operation.OperationType));

export const downloadOperations = async (operationList = [], format = `1c`, isDownloadOneFile = false) => {
    if (isDownloadOneFile) { // Загрузка нескольких РКО склеенных в один файл
        await MoneyOperationService.downloadJoinedByDocumentBaseIds(operationList.map(({ DocumentBaseId }) => DocumentBaseId));

        return;
    }

    for (let i = 0; operationList.length > i; i += 1) {
        const operation = operationList[i];

        if (isDownloadable(operation)) {
            await MoneyOperationService.downloadFile(operation.DocumentBaseId, format, operation.OperationType);
        }
    }
};

const parseSentOperationsResponse = response => {
    const {
        StatusCode, List, PhoneMask, Message, ErrorCode
    } = response;
    const data = {
        StatusCode,
        ErrorCode,
        PhoneMask,
        Message,
        List
    };

    if (StatusCode === BankIntegrationStatusCodeEnum.Error) {
        const successCount = List.filter(operation => {
            return operation.IsSuccess;
        }).length;
        const errorsCount = List.filter(operation => {
            return !operation.IsSuccess;
        }).length;

        if (successCount && errorsCount) {
            data.StatusCode = BankIntegrationStatusCodeEnum.Partially;
            data.report = {
                errorsCount,
                successCount
            };
        }
    }

    return data;
};

export const sendToBank = (operationList = []) => {
    const operationIds = operationList.map(operation => {
        return operation.DocumentBaseId;
    });

    return MoneyOperationService.sendToBank(operationIds)
        .then(response => {
            return new Promise(resolve => {
                resolve(parseSentOperationsResponse(response));
            });
        });
};

export const onApproveOperation = async ({ Id }) => {
    try {
        await putApproveById({ Id, isApproved: true });
        
        return true;
    } catch (error) {
        notificationManager.show({
            message: `При одобрении операции возникла ошибка`,
            type: `error`,
            duration: 5000
        });

        return false;
    }
};

export const isValidDownloadOperationActionType = additionalActionValue => {
    return Object.values(actionEnum).some(action => action === additionalActionValue);
};

export default {
    isDownloadable,
    is1CDisabled,
    isAnyDownloadable,
    isAllCanBeSent,
    isAny1CEnabled,
    downloadOperations,
    sendToBank
};

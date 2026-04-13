import sessionStorageHelper from '@moedelo/frontend-core-v2/helpers/sessionStorageHelper';
import confirmModalHelper from '@moedelo/frontend-core-react/helpers/confirmModalHelper';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import SendPaymentErrorCodeEnum from '../../enums/newMoney/SendPaymentErrorCodeEnum';
import SendToBankErrorsResource from '../../resources/newMoney/SendToBankErrorsResource';

const splitSymbol = `|`;
const storageErrorPrefix = `sendToBankErrorCode_`;

export const showErrorModal = options => {
    const {
        header,
        message,
        onAction,
        onActionText,
        onCloseText
    } = options;

    confirmModalHelper.showModal({
        header,
        children: message,
        onConfirm: onAction,
        confirmButtonText: onActionText,
        cancelButtonText: onCloseText
    });
};

export const setErrorAfterOperationCreated = options => {
    const { DocumentBaseId, ErrorCode, Message } = options;

    sessionStorageHelper.set(`${storageErrorPrefix}${DocumentBaseId}`, `${ErrorCode}${splitSymbol}${Message}`);
};

export const getErrorAfterOperationCreated = options => {
    const { DocumentBaseId } = options;
    const value = sessionStorageHelper.get(`${storageErrorPrefix}${DocumentBaseId}`);

    if (!value) {
        return null;
    }

    const [ErrorCode, Message] = value.split(splitSymbol);

    return {
        ErrorCode: JSON.parse(ErrorCode),
        Message
    };
};

export const clearErrorAfterOperationCreated = options => {
    const { DocumentBaseId } = options;

    sessionStorageHelper.remove(`${storageErrorPrefix}${DocumentBaseId}`);
};

export const handleSendPaymentError = options => {
    const {
        DocumentBaseId, Message, ErrorCode, callback
    } = options;
    const { Common } = SendPaymentErrorCodeEnum;
    const needToRedirectOnEditPage = DocumentBaseId && !window.location.hash.includes(`edit`);

    if (needToRedirectOnEditPage) {
        setErrorAfterOperationCreated({ DocumentBaseId, ErrorCode, Message });
        NavigateHelper.replace(`edit/settlement/${DocumentBaseId}`);

        return;
    }

    if (ErrorCode === null) {
        setTimeout(() => NotificationManager.show({
            message: `${Message}`,
            type: `error`,
            duration: 5000
        }), 250);

        return;
    }

    if (ErrorCode === Common) {
        showErrorModal(SendToBankErrorsResource[Common]);

        return;
    }

    callback && callback();
};

import { isString } from '@moedelo/frontend-core-v2/helpers/typeCheckHelper';
import { paymentOrderOperationResources } from '../../resources/MoneyOperationTypeResources';

export function needToClearContract({ OperationType }) {
    const operationTypesList = [
        paymentOrderOperationResources.PaymentOrderIncomingLoanObtaining.value,
        paymentOrderOperationResources.PaymentOrderOutgoingLoanRepayment.value,
        paymentOrderOperationResources.PaymentOrderIncomingLoanReturn.value,
        paymentOrderOperationResources.PaymentOrderOutgoingLoanIssue.value,
        paymentOrderOperationResources.PaymentOrderOutgoingPaymentAgencyContract.value
    ];

    return operationTypesList.some(type => type === OperationType);
}

/* бывает, пользователи копипастят в назначение платежа строку с управляющими символами и возникают проблемы при отправке в банк. зачищаем. */
export function clearDescriptionControlCharacters(model) {
    if (!isString(model?.Description)) {
        return model;
    }

    // eslint-disable-next-line no-control-regex
    const controlCharactersRegexp = /[\u0000-\u001F\u007F-\u009F]/g;

    return Object.assign(model, { Description: model.Description.replace(controlCharactersRegexp, ``) });
}

export default { needToClearContract };

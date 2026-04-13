/* global _ */

import { paymentOrderOperationResources, cashOrderOperationResources, purseOperationResources } from '../resources/MoneyOperationTypeResources';

function isPostingsOfTypeOtherForIp(operationType) {
    const { IsOoo } = window._preloading; // new Common.FirmRequisites().get('IsOoo');
    const isOperationTypeOther = _.contains([
        paymentOrderOperationResources.PaymentOrderIncomingOther.value,
        paymentOrderOperationResources.PaymentOrderOutgoingOther.value,
        cashOrderOperationResources.CashOrderIncomingOther.value,
        cashOrderOperationResources.CashOrderOutgoingOther.value,
        purseOperationResources.PurseOperationOtherOutgoing.value
    ], parseInt(operationType, 10));

    return !IsOoo && isOperationTypeOther;
}

export default isPostingsOfTypeOtherForIp;

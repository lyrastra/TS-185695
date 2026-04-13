import cn from 'classnames';
import { availablePassThruStatuses, paymentStatusResource } from './PaymentStatusResource';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';

import style from './style.m.less';
import PassThruPaymentStatusEnum from './PassThruPaymentStatusEnum';

function isPaidWithinMd(model) {
    return model.PaidStatus === DocumentStatusEnum.Payed;
}

function hasPassThruPayment(model) {
    const { PassThruPaymentState } = model;

    if (!PassThruPaymentState) {
        return false;
    }

    return availablePassThruStatuses.includes(PassThruPaymentState.Status);
}

function getStatusText(options) {
    if (!options.hasPassThru) {
        return paymentStatusResource[DocumentStatusEnum.NotPayed].text;
    }

    return paymentStatusResource[options.model.PassThruPaymentState.Status].text;
}

function getStatusIcon(options) {
    if (!options.hasPassThru) {
        return paymentStatusResource[DocumentStatusEnum.NotPayed].icon;
    }

    return paymentStatusResource[options.model.PassThruPaymentState.Status].icon;
}

function getIconClassName(options) {
    const colorClassName = !options.hasPassThru ? style.orange : style.red;

    return cn(
        style.icon,
        colorClassName
    );
}

function getTooltipText(options) {
    if (!options.hasPassThru) {
        return ``;
    }

    return options.model.PassThruPaymentState.Message;
}

function hidePaymentStatus(options) {
    const { isPaidWithin, hasPassThru, model } = options;
    const isPaidLocally = isPaidWithin && !hasPassThru;
    const isPaidPassThru = hasPassThru && model.PassThruPaymentState.Status === PassThruPaymentStatusEnum.Processed;

    return isPaidLocally || isPaidPassThru;
}

function getOptions(model) {
    return {
        hasPassThru: hasPassThruPayment(model),
        isPaidWithin: isPaidWithinMd(model),
        model
    };
}

export function getPaymentState(model) {
    const options = getOptions(model);

    if (hidePaymentStatus(options)) {
        return null;
    }

    return {
        text: getStatusText(options),
        icon: getStatusIcon(options),
        iconClassName: getIconClassName(options),
        tooltipText: getTooltipText(options)
    };
}

export default {
    getPaymentState
};

import errorIcon from '@moedelo/frontend-core-react/icons/error.m.svg';
import infoIcon from '@moedelo/frontend-core-react/icons/info.m.svg';
import warningIcon from '@moedelo/frontend-core-react/icons/warning.m.svg';

import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import PassThruPaymentStatusEnum from './PassThruPaymentStatusEnum';

export const availablePassThruStatuses = [PassThruPaymentStatusEnum.Canceled, PassThruPaymentStatusEnum.Failed];

export const paymentStatusResource = {
    [PassThruPaymentStatusEnum.Canceled]: {
        text: `Отменен`,
        icon: errorIcon
    },
    [PassThruPaymentStatusEnum.Failed]: {
        text: `Ошибка`,
        icon: infoIcon
    },
    [DocumentStatusEnum.NotPayed]: {
        text: `Не оплачен`,
        icon: warningIcon
    }
};


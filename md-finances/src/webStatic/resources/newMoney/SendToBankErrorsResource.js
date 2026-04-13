import React from 'react';
import Link from '@moedelo/frontend-core-react/components/Link';
import chatHelper from '@moedelo/chatbot/src/Helpers/ChatHelper';
import SendPaymentErrorCodeEnum from '../../enums/newMoney/SendPaymentErrorCodeEnum';

export default {
    [SendPaymentErrorCodeEnum.Common]: {
        header: `Произошёл непредвиденный сбой`,
        message: <span>Скорее всего это временные неполадки, попробуйте повторить операцию позднее. Если ошибка будет повторяться, напишите о проблеме в чат технической поддержки</span>,
        type: `error`,
        onAction: chatHelper.showChat,
        onClose: () => {},
        onActionText: `Перейти в чат`,
        onCloseText: `Закрыть`
    },
    [SendPaymentErrorCodeEnum.SberDocumentAlreadyExists]: {
        header: ``,
        message: <span>Документ не прошел проверку на стороне СББОЛ. Возможные причины:<br />
            1. Документ с такими реквизитами и номером уже существует в СББОЛ. Измените номер платежного поручения.<br />
            2. Ошибка в заполнении. Проверьте корректность внесенных данных на стороне СББОЛ.</span>,
        type: `error`,
        onAction: () => {},
        onClose: () => {},
        onActionText: ``,
        onCloseText: ``
    },
    [SendPaymentErrorCodeEnum.SberRecipientIsNotInDirectory]: {
        header: ``,
        message: <span>Документ не прошел проверку в СББОЛ: получатель отсутствует в справочнике контрагентов. Добавьте его в личном кабинете банка, следуя <Link target={`_blank`} href={`https://www.sberbank.com/help/business/sbbol/100070?tab=early`} noCompanyId>инструкции</Link>.</span>,
        type: `error`,
        onAction: () => {},
        onClose: () => {},
        onActionText: ``,
        onCloseText: ``
    },
    [SendPaymentErrorCodeEnum.SberRecipientAccountNotConfirmed]: {
        header: ``,
        message: <span>Документ не прошел проверку в СББОЛ: счёт контрагента не подтверждён. Подтвердите реквизиты получателя в справочнике контрагентов, следуя <Link target={`_blank`} href={`https://www.sberbank.com/help/business/sbbol/100070?tab=early`} noCompanyId>инструкции</Link>.</span>,
        type: `error`,
        onAction: () => {},
        onClose: () => {},
        onActionText: ``,
        onCloseText: ``
    }
};

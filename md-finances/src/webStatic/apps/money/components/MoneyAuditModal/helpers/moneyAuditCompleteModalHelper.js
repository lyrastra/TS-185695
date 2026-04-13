import React from 'react';
import { showModal } from '@moedelo/frontend-core-react/helpers/confirmModalHelper';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import P from '@moedelo/frontend-core-react/components/P';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import dateFormatResource from '@moedelo/frontend-core-v2/helpers/dateHelper/resources/DateFormatResource';
import MoneyAuditModalContent from '../components/MoneyAuditModalContent';
import { getAuditCompleteModalData } from '../services/moneyAuditCompleteModalService';

export const showMoneyAuditCompleteModalAsync = async () => {
    const { AuditDate } = await getAuditCompleteModalData();

    if (!AuditDate) {
        return;
    }

    // eslint-disable-next-line consistent-return
    return new Promise(resolve => showModal({
        header: `Аудит завершён`,
        confirmButtonText: `Перейти`,
        cancelButtonText: `Закрыть`,
        onConfirm: () => {
            mrkStatService.sendEventWithoutInternalUser(`go_to_money_audit_modal_click_button`);
            NavigateHelper.push(`/FirmAudit`, true);
            resolve(true);
        },
        onCancel: () => {
            mrkStatService.sendEventWithoutInternalUser(`close_dialog_money_audit_complete_modal_click_button`);
            resolve(false);
        },
        children: <MoneyAuditModalContent>
            <P>Аудит от {dateHelper(AuditDate).format(dateFormatResource.ru)} завершен</P>
        </MoneyAuditModalContent>
    }));
};

export default { showMoneyAuditCompleteModalAsync };

import React from 'react';
import { showModal } from '@moedelo/frontend-core-react/helpers/confirmModalHelper';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import MoneyAuditModalContent from '../components/MoneyAuditModalContent';
import { getReconciliationCompleteModalData } from '../services/moneyAuditCompleteModalService';

export const showMoneyReconciliationCompleteModalAsync = async () => {
    const { ReconciliationData } = await getReconciliationCompleteModalData();

    if (!ReconciliationData) {
        return;
    }

    // eslint-disable-next-line consistent-return
    return new Promise(resolve => showModal({
        header: `Автосверка завершена`,
        confirmButtonText: `Перейти`,
        cancelButtonText: `Закрыть`,
        onConfirm: () => {
            mrkStatService.sendEventWithoutInternalUser(`go_to_reconciliation_complete_modal_click_button`);
            NavigateHelper.open(ReconciliationData?.Url);
            resolve(true);
        },
        onCancel: () => {
            mrkStatService.sendEventWithoutInternalUser(`close_dialog_reconciliation_complete_modal_click_button`);
            resolve(false);
        },
        children: <MoneyAuditModalContent>
            {ReconciliationData?.Text}
        </MoneyAuditModalContent>
    }));
};

export default { showMoneyReconciliationCompleteModalAsync };

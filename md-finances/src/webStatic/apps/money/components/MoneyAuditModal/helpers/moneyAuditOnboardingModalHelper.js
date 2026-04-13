import React from 'react';
import { showModal } from '@moedelo/frontend-core-react/helpers/confirmModalHelper';
import P from '@moedelo/frontend-core-react/components/P';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import MoneyAuditModalContent from '../components/MoneyAuditModalContent';
import { canShowAuditOnboardingModal, delayShowingModal } from '../services/moneyAuditModalService';

export const showMoneyAuditOnboardingModalAsync = async checkShowed => {
    const canShow = await canShowAuditOnboardingModal();

    if (!canShow) {
        return;
    }

    checkShowed && checkShowed();

    delayShowingModal(14);

    // eslint-disable-next-line consistent-return
    return new Promise(resolve => showModal({
        header: `Аудит остатков по расчетным счетам`,
        confirmButtonText: `Пройти`,
        cancelButtonText: `Закрыть`,
        onConfirm: () => {
            mrkStatService.sendEventWithoutInternalUser(`perehod_na_stranitsy_audit_money_audit_modal_click_button`);
            NavigateHelper.push(`/FirmAudit`, true);
            resolve(true);
        },
        onCancel: () => {
            mrkStatService.sendEventWithoutInternalUser(`zakryt_dialog_money_audit_modal_click_button`);
            resolve(false);
        },
        children: <MoneyAuditModalContent>
            <P>Остаток по расчетному счету в сервисе и банке не совпадает?</P>
            <P>Рекомендуем пройти аудит по разделу «Деньги».</P>
        </MoneyAuditModalContent>
    }));
};

export default { showMoneyAuditOnboardingModalAsync };

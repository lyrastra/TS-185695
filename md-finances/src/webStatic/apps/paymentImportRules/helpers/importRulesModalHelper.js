import React from 'react';
import { showModal } from '@moedelo/frontend-core-react/helpers/confirmModalHelper';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import Link from '@moedelo/frontend-core-react/components/Link';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import { getId } from '@moedelo/frontend-core-v2/helpers/companyId';
import localStorageHelper from '@moedelo/frontend-core-v2/helpers/localStorageHelper';

const companyId = getId();

const onImportRulesManualLinkClick = () => {
    mrkStatService.sendEvent({
        event: `new_import_rules_modal_manual_link_click`
    });

    NavigateHelper.open(`https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rabota-v-servise/dengi-prof/pravila-po-raspoznavaniu-vypiski`, { useRawUrl: true });
};

const showImportRulesModal = operationType => {
    const importRulesModalShown = `newImportRulesModalShownForOperation$${operationType}_firmId$${companyId}`;

    if (localStorageHelper.get(importRulesModalShown)) {
        return null;
    }

    localStorageHelper.set(importRulesModalShown, true);

    return new Promise(resolve => showModal({
        header: `Создать правило импорта?`,
        confirmButtonText: `Создать`,
        cancelButtonText: `Отмена`,
        onConfirm: () => {
            mrkStatService.sendEvent({
                event: `new_import_rules_modal_create_button_click`,
                st5: companyId
            });

            resolve(true);
            NavigateHelper.open(`/Finances/PaymentImportRules#add`);
        },
        onCancel: () => resolve(false),
        children: <React.Fragment>
            Правила импорта позволят не вносить ручные правки в операции после их импорта. <br /> <br />
            <Link onClick={onImportRulesManualLinkClick}>Как это работает?</Link>
        </React.Fragment>
    }));
};

export default showImportRulesModal;

import React from 'react';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import Link from '@moedelo/frontend-core-react/components/Link';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import {
    disable as disableFirmFlag,
    isEnabled as isFirmFlagEnabled
} from '@moedelo/frontend-common-v2/services/firmFlagService';
import FirmFlagEnum from '../enums/FirmFlagEnum';

export function showImportRuleNewFutureDialog() {
    const onClose = () => {
        disableFirmFlag({ name: FirmFlagEnum.ShowImportRuleNewFutureDialog });
    };

    const onClickLink = () => {
        disableFirmFlag({ name: FirmFlagEnum.ShowImportRuleNewFutureDialog }).then(() => {
            NavigateHelper.push(`/Finances/PaymentImportRules`);
        });
    };

    const getChildren = () => {
        return <div>
            Создайте правило импорта для правильного определения типа операции.&nbsp;<Link
                text={`Создать`}
                onClick={onClickLink}
            />
        </div>;
    };

    isFirmFlagEnabled({ name: FirmFlagEnum.ShowImportRuleNewFutureDialog }).then(result => {
        if (result) {
            NotificationManager.show({
                children: getChildren(),
                type: `info`,
                duration: 10000,
                onClose
            });
        }
    });
}

export default {
    showImportRuleNewFutureDialog
};

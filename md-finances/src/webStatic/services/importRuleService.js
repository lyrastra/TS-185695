import {
    enable as enableFirmFlag,
    ifExist as ifExistFirmFlag,
    isEnabled as isEnabledFirmFlag
} from '@moedelo/frontend-common-v2/services/firmFlagService';
import FirmFlagEnum from '../enums/FirmFlagEnum';

export async function enableShowImportRuleNewFutureDialog() {
    return Promise.all([
        isEnabledFirmFlag({ name: FirmFlagEnum.ShowImportRuleNewFutureDialog }),
        ifExistFirmFlag({ name: FirmFlagEnum.ShowImportRuleNewFutureDialog })
    ]).then(([ifExist, isEnabled]) => {
        if (!ifExist && !isEnabled) {
            enableFirmFlag({ name: FirmFlagEnum.ShowImportRuleNewFutureDialog });
        }
    });
}

export default {
    enableShowImportRuleNewFutureDialog
};

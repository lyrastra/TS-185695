import userDataService from '@moedelo/frontend-core-v2/services/userDataService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { isEnabled, enable } from '@moedelo/frontend-common-v2/services/firmFlagService';
import { getRegistrationInService } from '@moedelo/frontend-common-v2/apps/finances/service/setupDataPreloadingService';
import FirmFlagEnum from '../../../../enums/FirmFlagEnum';

async function monthIsBeforeRegistrationDate() {
    return getRegistrationInService().then(registrationInService => {
        const monthAfterRegistration = dateHelper(registrationInService, `YYYY-MM-DD`).add(1, `months`);

        return dateHelper().isBefore(monthAfterRegistration);
    });
}

async function isRnkbTariff() {
    return userDataService.get().then(data => {
        return data?.AccessRuleFlags?.IsRnkbTariff;
    });
}

export async function canShowBubble() {
    const monthIsBeforeRegistrationDateTask = monthIsBeforeRegistrationDate();
    const isRnkbTask = isRnkbTariff();
    const isCloseBubble = await isEnabled({ name: FirmFlagEnum.IsClosedRnkbBubbleAboutDontMatchRemains });

    const [isMonthBeforeRegistration, isRnkb] = await Promise.all([
        monthIsBeforeRegistrationDateTask,
        isRnkbTask
    ]);

    return !(isMonthBeforeRegistration && isRnkb && !isCloseBubble);
}

export async function setCloseBubble() {
    return enable({ name: FirmFlagEnum.IsClosedRnkbBubbleAboutDontMatchRemains });
}

export default {
    canShowBubble,
    setCloseBubble
};

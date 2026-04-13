import { get as restApiGet } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import { getId } from '@moedelo/frontend-core-v2/helpers/companyId';
import dateFormatResource from '@moedelo/frontend-core-v2/helpers/dateHelper/resources/DateFormatResource';
import localStorageHelper from '@moedelo/frontend-core-v2/helpers/localStorageHelper';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

const localStorageKey = `MoneyAuditModalShowDate__${getId()}`;

/**
 * Определяет возможность отображения диалога.
 * Если текущая дата больше даты из localStorage, то есть возможность отобразить диалог
 * @returns {boolean}
 */
const canShow = () => {
    const showDateString = localStorageHelper.get(localStorageKey);

    if (!showDateString) {
        return true;
    }

    const showDate = dateHelper(showDateString, dateFormatResource.ru);
    const now = dateHelper();

    return showDate.isValid() && now.isSameOrAfter(showDate);
};

/**
 * Откладывает отображение диалога на количество дней спустя текущей даты
 * (нужно, чтобы уменьшить количество запросов на сервер)
 * @param daysCount
 */
export const delayShowingModal = daysCount => {
    const newShowDate = dateHelper().add(daysCount, `days`).format(dateFormatResource.ru);

    localStorageHelper.set(localStorageKey, newShowDate);
};

/**
 * Определяет видимость диалога
 * @returns {Promise<boolean>}
 */
export const canShowAuditOnboardingModal = () => {
    if (!canShow()) {
        return Promise.resolve(false);
    }

    return restApiGet(`/FirmAuditMoney/api/v1/Onboarding/IsNeedToShowDialog`)
        .then(res => {
            !res && delayShowingModal(1);

            return res;
        });
};

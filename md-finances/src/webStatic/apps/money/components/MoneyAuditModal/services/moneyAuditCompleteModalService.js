import { get as restApiGet } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import localStorageHelper from '@moedelo/frontend-core-v2/helpers/localStorageHelper';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import localStorageKeys from '../resource/completeModalLocalStorageKeysResource';

const dateFormat = `DD.MM.YYYY HH:mm`;

const url = {
    getAuditState: `/FirmAudit/api/v1/Audit/Indicator`,
    getReconciliationState: `/Finances/Money/Reconciliation/Indicator`
};

/**
 * Определяет возможность отображения диалога по дате и времени.
 * Если дата больше даты из localStorage, то есть возможность отобразить диалог
 * @returns {boolean}
 */
const canShowByDateAndTime = key => {
    const showDateString = localStorageHelper.get(key);

    if (!showDateString) {
        return true;
    }

    const showDate = dateHelper(showDateString, dateFormat, true);
    const now = dateHelper();

    return showDate.isValid() && now.isSameOrAfter(showDate);
};

/**
 * Откладывает отображение диалога на количество часов от текущего момента
 * (нужно, чтобы уменьшить количество запросов на сервер)
 * @param count
 * @param key
 */
export const delayShowingModalByHours = (count, key) => {
    const newShowDate = dateHelper().add(count, `hours`).format(dateFormat);

    localStorageHelper.set(key, newShowDate);
};

/**
 * Определяет видимость диалога, сообщающего о завершении аудита и отображаемые данные
 * @returns {Promise<object>}
 */
export const getAuditCompleteModalData = async () => {
    if (!canShowByDateAndTime(localStorageKeys.auditComplete)) {
        return Promise.resolve(false);
    }

    const { data } = await restApiGet(url.getAuditState);

    delayShowingModalByHours(1, localStorageKeys.auditComplete);

    return Promise.resolve({
        AuditDate: data
    });
};

/**
 * Определяет видимость диалога, сообщающего о завершении сверки и отображаемые данные
 * @returns {Promise<object>}
 */
export const getReconciliationCompleteModalData = async () => {
    if (!canShowByDateAndTime(localStorageKeys.reconciliationComplete)) {
        return Promise.resolve(false);
    }

    const data = await restApiGet(url.getReconciliationState);

    delayShowingModalByHours(1, localStorageKeys.reconciliationComplete);

    return Promise.resolve({
        ReconciliationData: data
    });
};

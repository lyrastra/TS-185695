import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper/dateHelper';
import getUniqueId from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper/index';
import SubcontoType from '../../../../../../../enums/newMoney/SubcontoTypeEnum';
import { getSubaccount } from '../../../../../../../services/newMoney/subcontoService';

const MAX_STORAGE_LIFETIME = 2;

export function getSubcontosScheme(allAccounts, accNumber) {
    const subcontoScheme = allAccounts.find(({ Number }) => Number === accNumber)?.SubcontoScheme?.map(type => SubcontoType[type]);

    if (!subcontoScheme) {
        console.error(`Счет ${accNumber} не найден в списке счетов в /Accounting/api/v1/subaccount`);
    }

    return subcontoScheme;
}

/**
 * Формирует новую модель для несуществующих в сохраненной проводке субконто
* */
export function getSubcontoModelToUpdate(subcontoFromPosting, subcontoScheme) {
    const newSubcontoModel = {
        Id: null,
        Name: null,
        ReadOnly: null,
        SubcontoId: null
    };

    const subcontosFromProps = subcontoFromPosting?.map(({ Type }) => Type);

    if (subcontoScheme && subcontoScheme?.length !== subcontosFromProps?.length) {
        return subcontoScheme.reduce((acc, type) => {
            if (subcontosFromProps?.includes(type)) {
                return [...acc, subcontoFromPosting.find(({ Type }) => Type === type)];
            }

            return [...acc, { Subconto: { ...newSubcontoModel, Type: type }, Type: type, key: getUniqueId() }];
        }, []);
    }

    return null;
}

/**
 * Загрузка всех счетов со схемами субконто
 * Используется для проверки корректности структуры проводки в операциях с типом "Прочие"
 * */
export async function loadAllAccountsSchemes() {
    const localStorageSavedAccountsName = `savedAccountsWithSubcontoScheme`;
    const accountsFromStorage = JSON.parse(localStorage.getItem(localStorageSavedAccountsName));
    let accountsData = null;

    const loadSubAccountsToStorage = async () => {
        accountsData = await getSubaccount();

        localStorage.setItem(localStorageSavedAccountsName, JSON.stringify({ CreateDate: dateHelper().format(`DD.MM.YYYY HH:mm`), data: accountsData }));
    };

    if (accountsFromStorage) {
        const now = dateHelper().format(`DD.MM.YYYY HH:mm`);
        // храним в сторадже структуру всех счетов 2 часа
        const diff = dateHelper(now, `DD.MM.YYYY HH:mm`).diff(dateHelper(accountsFromStorage.CreateDate, `DD.MM.YYYY HH:mm`), `hours`, true);

        if (diff > MAX_STORAGE_LIFETIME) {
            localStorage.removeItem(localStorageSavedAccountsName);
            await loadSubAccountsToStorage();
        } else {
            accountsData = accountsFromStorage.data;
        }
    } else {
        await loadSubAccountsToStorage();
    }

    return accountsData;
}

export default { getSubcontosScheme, getSubcontoModelToUpdate, loadAllAccountsSchemes };

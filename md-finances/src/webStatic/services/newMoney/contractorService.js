import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';
import { get as restGet } from '@moedelo/frontend-core-v2/helpers/restHttpClient';

/**
 * It getting list of contractors
 * @param {Array} data options for request
 *
 * @return {Array}
 * */
export async function getContractorAutocomplete(data) {
    const url = `/Accounting/Kontragents/GetKontragentsForAccountingPaymentOrderAutocomplete`;
    const { List } = await get(url, data);

    return List;
}

/**
 * Возвращает список комиссионеров
 * @param {string} query строка поиска по комиссионерам
 * @param {number} count количество загружаемых комиссионеров
 * @returns {Promise<СomissionAgentFromServer[]>}
 */
export async function getCommissionAgentsAutocomplete({ query = ``, count = 5 }) {
    const url = `/Money/api/v1/PaymentOrders/Incoming/IncomeFromCommissionAgent/Autocomplete/CommissionAgent`;
    const response = await restGet(url, { query, count });

    return response.data;
}

export async function getContractorBankAutocomplete(data) {
    const url = `/Accounting/Banks/GetBanksAutocomplete`;
    const { List } = await get(url, data);

    return List;
}

export async function getNaturalPersonAutocomplete(data) {
    const url = `/Kontragents/Autocomplete/GetWithAccountingPaymentOrderAutocomplete`;
    const { List } = await get(url, data);

    return List;
}

export async function getContractorAutocompleteSortedByFounders(data) {
    const url = `/Kontragents/Autocomplete/GetKontragentFounderAutocomplete`;
    const { List } = await get(url, data);

    return List;
}

export async function getFoundersAutocomplete(data) {
    const url = `/Kontragents/Autocomplete/GetFounderAutocomplete`;
    const { List } = await get(url, data);

    return List;
}

export async function getKontragentRequisitesByIdAndDate(data) {
    const requisites = await get(`/Accounting/PaymentOrders/GetKontragentBankRequisites`, data);

    return requisites;
}

export function getKontragentByTypeAndQuery({ type, query }) {
    return restGet(`/Finances/Money/Contractors`, { type, query })
        .then(resp => resp.data);
}

export function getKontragentNameByIdAndType({ id, type }) {
    return restGet(`/Finances/Contractors/${id}`, { type })
        .then(resp => resp.data);
}

export function getSettlementAccounts({ kontragentId }) {
    return get(`/Kontragents/SettlementAccount/GetByKontragent`, { kontragentId })
        .then(resp => resp.Value);
}

export function getSelfKontragent() {
    return restGet(`/Kontragents/api/v1/kontragent/yourself`);
}

export default getContractorAutocomplete;

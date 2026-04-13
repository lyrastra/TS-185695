import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';

export async function getOutgoingReasonDocuments(data = {}) {
    const { List } = await get(`/Accounting/PaymentAutomation/GetOutgoingReasonDocuments`, data);

    return List;
}

export async function getIncomingReasonDocuments(data = {}) {
    const { List } = await get(`/Accounting/PaymentAutomation/GetIncomingReasonDocuments`, data);

    return List;
}

export default { getOutgoingReasonDocuments, getIncomingReasonDocuments };

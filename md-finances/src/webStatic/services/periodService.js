import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';

export async function getPeriodAutocomplete({
    contractBaseId,
    paymentDocumentBaseId,
    excludeIds,
    withMinDate
}) {
    const { List } = await get(`/Contract/RentPaymentPeriod/GetPeriods`, {
        contractBaseId, paymentDocumentBaseId, excludeIds, withMinDate
    });

    return List;
}

export default { getPeriodAutocomplete };

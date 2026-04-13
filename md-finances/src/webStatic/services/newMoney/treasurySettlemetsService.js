import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';

export default function isTreasurySettlementPairMatching(settlementAccount, unifiedSettlementAccount) {
    return get(`/Catalog/FundSettlementAccount/Match`, { settlementAccount, unifiedSettlementAccount });
}

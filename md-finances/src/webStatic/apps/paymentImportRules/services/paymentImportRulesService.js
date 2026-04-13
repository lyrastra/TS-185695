import {
    post, get, put, remove
} from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import {
    mapBackPaymentImportRuleToTableData,
    mapBackPaymentImportRuleToFrontModel,
    mapBackOperationsAffectedByRuleToFront
} from '../helpers/paymentImportRulesMapper';
import withCache from '../decorators/withCache';

const apiEndpoint = `/PaymentImportRules/api/v1/Rule`;

export async function getListRulesAsync() {
    const { data } = await get(apiEndpoint);

    return data.map(mapBackPaymentImportRuleToTableData);
}

export async function getOneRuleAsync(ruleId) {
    const { data } = await get(`${apiEndpoint}/${ruleId}`);

    return mapBackPaymentImportRuleToFrontModel(data);
}

export function saveRuleAsync(data) {
    return post(apiEndpoint, data);
}

export function updateRuleAsync(ruleId, data) {
    return put(`${apiEndpoint}/${ruleId}`, data);
}

export function deleteRuleAsync(ruleId) {
    return remove(`${apiEndpoint}/${ruleId}`);
}

export const getAvailableOperationTypesAsync = withCache(async () => {
    const { data } = await get(`/PaymentImportRules/api/v1/OperationType`);

    return data;
});

export const getTaxationSystemsDataAsync = withCache(async () => {
    const { data } = await get(`/PaymentImportRules/api/v1/RuleTaxationSystem/Get`);

    return data;
});

export const getIgnoreNumberDataAsync = withCache(async () => {
    const { data } = await get(`/PaymentImportRules/api/v1/RuleIgnoreNumber/Get`);

    return data;
});

export const getMediationDataAsync = withCache(async () => {
    const { data } = await get(`/PaymentImportRules/api/v1/RuleMediation/Get`);

    return data;
});

export async function getOperationsAffectedByRuleAsync({ data, limit = 10, offset = 0 }) {
    const { data: { Operations, TotalCount = 100500 } } = await post(`/PaymentImportRules/api/v1/Operations/GetByRule`, {
        Rule: data,
        Limit: limit,
        Offset: offset
    });

    return {
        totalCount: TotalCount,
        data: Operations.map(mapBackOperationsAffectedByRuleToFront)
    };
}

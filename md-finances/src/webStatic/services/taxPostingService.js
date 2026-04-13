import { get, post } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import { getRestTaxPostingsMoneyPath } from '../helpers/newMoney/operationUrlHelper';
import postingGenerationError from '../resources/newMoney/postingGenerationError';
import {
    mapTaxPostingsForOsnoNewBackend,
    mapTaxPostingsNewBackend
} from '../mappers/taxPostingsMapper';

export async function getByBankOperationNewBackend({ baseId, taxationSystem, isOoo }) {
    if (!baseId) {
        return { ExplainingMessage: ``, Postings: [] };
    }

    return get(`/TaxPostings/api/v1/Postings/${baseId}`)
        .then(result => {
            const { data } = result;
            const { Postings, LinkedDocuments, TaxStatus } = data;

            return {
                ExplainingMessage: data.ExplainingMessage,
                Postings: taxationSystem.IsOsno && isOoo
                    ? mapTaxPostingsForOsnoNewBackend(Postings)
                    : mapTaxPostingsNewBackend(Postings),
                TaxStatus,
                LinkedDocuments
            };
        })
        .catch(() => {
            return { Error: postingGenerationError };
        });
}

export async function generate(data) {
    return generateNewBackend(data);
}

export async function generateNewBackend(params) {
    const url = getRestTaxPostingsMoneyPath({ operationType: params.OperationType });
    const { data } = await post(url, params);
    const {
        ExplainingMessage, Postings, LinkedDocuments, TaxStatus
    } = data;

    return {
        ExplainingMessage,
        Postings: mapTaxPostingsNewBackend(Postings),
        LinkedDocuments,
        TaxStatus
    };
}

export async function generateForOsno(data) {
    return generateForOsnoNewBackend(data);
}

export async function generateForOsnoNewBackend(params) {
    const url = getRestTaxPostingsMoneyPath({ operationType: params.OperationType });
    const { data } = await post(url, params);
    const {
        ExplainingMessage, Postings, LinkedDocuments, TaxStatus
    } = data;

    return {
        ExplainingMessage,
        Postings: mapTaxPostingsForOsnoNewBackend(Postings),
        LinkedDocuments,
        TaxStatus
    };
}

export default {
    getByBankOperationNewBackend,
    generate,
    generateNewBackend,
    generateForOsno,
    generateForOsnoNewBackend
};


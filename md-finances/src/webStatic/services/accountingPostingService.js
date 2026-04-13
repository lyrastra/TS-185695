import { get, post } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import { getRestAccPostingsMoneyPath } from '../helpers/newMoney/operationUrlHelper';
import postingGenerationError from '../resources/newMoney/postingGenerationError';

export default {
    async generate(data) {
        return this.generateNewBackend(data);
    },

    async generateNewBackend(params) {
        const url = getRestAccPostingsMoneyPath({ operationType: params.OperationType });
        const { data } = await post(url, params);
        const { ExplainingMessage, Postings, LinkedDocuments } = data;

        return {
            ExplainingMessage,
            Postings,
            LinkedDocuments
        };
    },

    async getByBankOperationNewBackend(id) {
        if (!id) {
            return { ExplainingMessage: ``, Postings: [] };
        }

        return get(`/AccPostingsApi/api/v1/Postings/${id}`)
            .then(result => {
                const { Message, Postings, LinkedDocuments } = result.data;

                return {
                    ExplainingMessage: Message,
                    Postings,
                    LinkedDocuments
                };
            }).catch(() => {
                return { Error: postingGenerationError };
            });
    }
};

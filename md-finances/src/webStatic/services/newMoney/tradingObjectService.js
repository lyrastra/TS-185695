import { get } from '@moedelo/frontend-core-v2/helpers/restHttpClient';

export async function getTradingObjects() {
    return get(`/Requisites/api/v2/TradingObject`).then((resp) => {
        return resp.data;
    });
}

export default {
    getTradingObjects
};

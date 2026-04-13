import { get, put, post } from '@moedelo/frontend-core-v2/helpers/restHttpClient';

const url = `/money/api/v1/PaymentOrders/Outsource/Approve`;

async function getApproveById({ Id }) {
    const { data } = await get(`${url}/${Id}`);

    return data;
}

async function getApproveByIds({ Ids }) {
    const result = await post(`${url}/GetByIds`, Ids);

    return result;
}

async function getInitialDate() {
    const { data } = await get(`${url}/InitialDate`);

    return data;
}

async function putApproveById({ Id, isApproved }) {
    await put(`${url}/${Id}?isApproved=${isApproved}`);
}

async function setApproveByIds({ Ids }) {
    await post(url, Ids);
}

export {
    getApproveById, putApproveById, getApproveByIds, setApproveByIds, getInitialDate
};

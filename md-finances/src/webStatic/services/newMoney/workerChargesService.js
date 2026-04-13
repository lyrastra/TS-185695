import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';

export async function getMappedWorkerListAsync(data) {
    const { List } = await get(`/Payroll/Workers/GetWorkersWithAccount`, data);

    return mapWorkerList(List);
}

export async function getWorkerChargePaymentsAsync(data) {
    const { WorkerChargePayments } = await get(`/Payroll/ChargePayments/GetUnboundForWorker`, data);
    let list = [];

    if (WorkerChargePayments && WorkerChargePayments[0] && WorkerChargePayments[0].ChargePayments) {
        list = WorkerChargePayments[0].ChargePayments;
    }

    return list;
}

function mapWorkerList(list = []) {
    return list.map(worker => ({
        text: worker.Name,
        value: worker.Id,
        original: worker
    }));
}

export default { getMappedWorkerListAsync, getWorkerChargePaymentsAsync };


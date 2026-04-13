import { get } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

const urls = {
    getDeductionWorkers: `/payroll/api/v1/Deductions/PaymentOrder/Worker/Autocomplete`,
    getDeductionWorkersDocuments: `/payroll/api/v1/Deductions/PaymentOrder/Worker/Document/Autocomplete`
};

export function getDeductionWorkers(query, paymentDate) {
    return get(`${urls.getDeductionWorkers}`, { paymentDate: dateHelper(paymentDate).format(`YYYY-MM-DD`), query });
}

export function getDeductionWorkerDocuments(query, workerId) {
    return get(`${urls.getDeductionWorkersDocuments}`, { workerId, query });
}

export default { getDeductionWorkers };

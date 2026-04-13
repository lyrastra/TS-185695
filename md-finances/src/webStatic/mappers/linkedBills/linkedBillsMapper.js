import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';

/**
 * @param {object} bill
 * @return {object}
 */
export const mapLinkedBillToBackend = bill => ({
    DocumentBaseId: bill.DocumentBaseId,
    Sum: toFloat(bill.Sum)
});

/**
 * @param {object} bill
 * @return {object}
 */
export const mapLinkedBillToClient = bill => ({
    DocumentBaseId: bill.DocumentBaseId,
    Date: bill.Date,
    Number: bill.Number,
    PaidSum: bill.PaidSum,
    Sum: bill.Sum,
    DocumentSum: bill.DocumentSum
});

export default { mapLinkedBillToBackend, mapLinkedBillToClient };

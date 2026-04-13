/* eslint-disable no-undef */
import WarrantReceiptFromCashboxStore from './WarrantReceiptFromCashboxStore';
import { setupFetch } from './../../../../../../../../test/utils';
import { getOperationDataForTest } from '../../../helpers/operationStoreHelper';

const kontragent = { id: 121212, name: `ИП Иван Васильевич` };

describe(`WarrantReceiptFromCashboxStore test`, () => {
    beforeEach(() => {
        setupFetch([
            {
                url: `/Accounting/PaymentAutomation/GetOutgoingReasonDocuments`,
                data: { List: [] }
            },
            {
                url: `/Kontragents/SettlementAccount/GetByKontragent`,
                data: { Value: [] }
            },
            {
                url: `/kontragents/api/v1/kontragent/${kontragent.id}`,
                data: { Name: kontragent.name }
            },
            {
                url: `/Accounting/PaymentOrders/GetAllTaxPostings`,
                data: { Operations: [{ Postings: [] }] }
            }
        ]);
    });

    afterEach(() => {
        window.fetch.restore();
    });

    it(`create store`, () => {
        const store = createStore();
        expect(store).toBeDefined();
    });

    it(`add sum, if it not exist, when add RKO document`, () => {
        const store = createStore();
        const cashOrderFromAutocomplete = {
            value: 12,
            text: `№ 1 от 11.11.2011`,
            original: {
                Sum: 100
            }
        };

        expect(store.model.Sum).toBe(0);
        store.setCashOrder(cashOrderFromAutocomplete);

        expect(store.model.Sum).toBe(100);
    });
});

// -------------------------------------- utils -----------------------------------------------

function createStore(operationData) {
    return new WarrantReceiptFromCashboxStore(getOperationDataForTest(operationData));
}

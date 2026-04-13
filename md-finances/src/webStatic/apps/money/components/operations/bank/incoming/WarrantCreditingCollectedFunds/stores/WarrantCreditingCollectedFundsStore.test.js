/* eslint-disable no-undef */
import WarrantCreditingCollectedFundsStore from './WarrantCreditingCollectedFundsStore';
import { setupFetch } from './../../../../../../../../test/utils';
import { getOperationDataForTest } from '../../../helpers/operationStoreHelper';

const kontragent = { id: 121212, name: `ИП Иван Васильевич` };

describe(`WarrantCreditingCollectedFundsStore test`, () => {
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
});

// -------------------------------------- utils -----------------------------------------------

function createStore(operationData) {
    return new WarrantCreditingCollectedFundsStore(getOperationDataForTest(operationData));
}

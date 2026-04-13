/* eslint-disable no-undef */
import MediationFeeStore from './MediationFeeStore';
import { setupFetch } from './../../../../../../../../test/utils';
import { getOperationDataForTest } from '../../../helpers/operationStoreHelper';

const kontragent = { id: 121212, name: `ИП Иван Васильевич` };

describe(`MediationFeeStore test`, () => {
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

    it(`set Bills with kontragentId undefined - store.bills length increase`, () => {
        const store = createStore();

        const bills = [{ DocumentBaseId: 1, KontragentId: kontragent.id }];

        return store.setBills(bills)
            .then(() => {
                expect(store.model.Bills.length).toBe(1);
            });
    });

    it(`set Bills with kontragentId undefined - define kontragent id and name`, () => {
        const store = createStore();

        const bills = [{ DocumentBaseId: 1, KontragentId: kontragent.id }];

        return store.setBills(bills)
            .then(() => {
                expect(store.model.Kontragent.KontragentId).toBe(kontragent.id);
                expect(store.model.Kontragent.KontragentName).toBe(kontragent.name);
            });
    });

    // BP-5051 Не работает пересчет суммы счетов при изменении суммы ПП
    it(`set sum - change bills sum`, async () => {
        const store = createStore();

        store.setSum({ value: 20 });
        await store.setBills([{
            DocumentBaseId: 1, DocumentSum: 100, Sum: 20, KontragentId: kontragent.id
        }]);

        expect(store.model.Bills[0].Sum).toBe(`20,00`);

        store.setSum({ value: 30 });

        expect(store.model.Bills[0].Sum).toBe(`30,00`);
    });
});

// -------------------------------------- utils -----------------------------------------------

function createStore(operationData) {
    return new MediationFeeStore(getOperationDataForTest(operationData));
}

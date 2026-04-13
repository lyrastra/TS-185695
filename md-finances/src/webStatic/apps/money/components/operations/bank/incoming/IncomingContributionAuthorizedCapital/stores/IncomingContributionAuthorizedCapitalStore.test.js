/* eslint-disable no-undef */
import IncomingContributionAuthorizedCapitalStore from './IncomingContributionAuthorizedCapitalStore';
import { setupFetch } from './../../../../../../../../test/utils';
import { getOperationDataForTest } from '../../../helpers/operationStoreHelper';

const kontragent = { id: 121212, name: `ИП Иван Васильевич` };

describe(`IncomingContributionAuthorizedCapitalStore.js test`, () => {
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
            },
            {
                url: `/Accounting/PaymentOrders/GetKontragentBankRequisites`,
                data: { KontragentINN: `12345`, KontragentKPP: `54321` }
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

    it(`requests contractor INN and KPP after adding`, async () => {
        const store = createStore();
        const original = {
            KontragentId: 123,
            KontragentName: `LoLo`
        };

        expect.assertions(2); // 2 ассерта, потому что есть еще запрос за реквизитами контрагента
        await store.setContractor({ original });

        expect(store.model.Kontragent.KontragentINN).toEqual(`12345`); // values from GetKontragentBankRequisites request above
        expect(store.model.Kontragent.KontragentKPP).toEqual(`54321`);
    });
});

// -------------------------------------- utils -----------------------------------------------

function createStore(operationData) {
    return new IncomingContributionAuthorizedCapitalStore(getOperationDataForTest(operationData));
}

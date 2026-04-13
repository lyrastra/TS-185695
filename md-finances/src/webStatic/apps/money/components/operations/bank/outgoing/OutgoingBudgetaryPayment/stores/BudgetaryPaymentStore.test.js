/* eslint-disable no-undef */
import BudgetaryPaymentStore from './BudgetaryPaymentStore';
import { setupFetch } from './../../../../../../../../test/utils';
import { getOperationDataForTest } from '../../../helpers/operationStoreHelper';

const kontragent = { id: 121212, name: `ИП Иван Васильевич` };

describe(`BudgetaryPaymentStore test`, () => {
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

    it(`registartion date defined and period closed - min date for document equal registration date`, () => {
        const store = createStore({
            requisites: { RegistrationDate: `01.01.2018`, FinancialResultLastClosedPeriod: `31.01.2018` }
        });

        expect(store.minDate).toEqual(`01.01.2018`);
    });

    it(`registartion date less then 01.01.2013 - min date for document equal 01.01.2013`, () => {
        const store = createStore({
            requisites: { RegistrationDate: `01.01.2010` }
        });

        expect(store.minDate).toEqual(`01.01.2013`);
    });
});

// -------------------------------------- utils -----------------------------------------------

function createStore(operationData) {
    return new BudgetaryPaymentStore(getOperationDataForTest(operationData));
}

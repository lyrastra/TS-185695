/* eslint-disable no-undef */
import { toJS } from 'mobx';
import kontragentForm from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import OutgoingProfitWithdrawingStore from './OutgoingProfitWithdrawingStore';
import { setupFetch, waitAsync } from './../../../../../../../../test/utils';
import { getOperationDataForTest } from '../../../helpers/operationStoreHelper';

const kontragent = { id: 121212, name: `ИП Иван Васильевич` };

describe(`OutgoingProfitWithdrawing test`, () => {
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
                url: `/money/api/v1/PaymentOrders/Outsource/Approve/InitialDate`,
                // дата отображения статуса Обработано в платежках
                data: `2023-06-01T00:00:00`
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

    it(`kontragent defined, settlement not defined - kontragent hasn't own requisites`, () => {
        const store = createStore({
            operation: {
                KontragentId: kontragent.id,
                Kontragent: {
                    KontragentName: kontragent.name
                },
                Sum: 100
            }
        });

        return waitAsync().then(() => {
            expect(store.isKontragentHaveOwnRequisites).toEqual(false);
        });
    });

    it(`kontragent defined, settlement defined - kontragent has own requisites`, () => {
        const store = createStore({
            operation: {
                KontragentId: kontragent.id,
                Kontragent: {
                    KontragentName: kontragent.name,
                    KontragentSettlementAccount: `111`,
                    KontragentBankName: `альфа`,
                    KontragentForm: kontragentForm.FL
                },
                Sum: 100
            }
        });

        return waitAsync().then(() => {
            expect(store.isKontragentHaveOwnRequisites).toEqual(true);
        });
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

    it(`clears kontragent requisites after kontragent change, when the second one haven't of it(https://youtrack.moedelo.org/youtrack/issue/BP-4921)`, () => {
        /**
         * because empty fields of object,
         * which is going to autocomplete,
         * have null values instead of empty string,
         * but we need empty string
         * */

        const store = createStore();
        const expected = {
            KontragentBankBIK: ``,
            KontragentBankCorrespondentAccount: ``,
            KontragentBankName: ``,
            KontragentINN: ``,
            KontragentId: 6705,
            KontragentKPP: ``,
            KontragentName: `Ололошкин`,
            KontragentSettlementAccount: ``
        };

        store.setContractor({
            original: {
                KontragentBankBIK: `045491002`,
                KontragentBankCorrespondentAccount: ``,
                KontragentBankName: `МРХ (Г.ОРЕЛ) ЦХ БАНКА РОССИИ, г. ОРЕЛ`,
                KontragentForm: undefined,
                KontragentINN: `7722444223`,
                KontragentId: 10777,
                KontragentKPP: `772201001`,
                KontragentName: `BP-4921  Не очищаются реквизиты контрагента`,
                KontragentSettlementAccount: `55555555555555555555`
            }
        });

        store.setContractor({
            original: {
                KontragentBankBIK: null,
                KontragentBankCorrespondentAccount: null,
                KontragentBankName: null,
                KontragentForm: undefined,
                KontragentINN: null,
                KontragentId: 6705,
                KontragentKPP: null,
                KontragentName: `Ололошкин`,
                KontragentSettlementAccount: null
            }
        });

        expect(toJS(store.model.Kontragent)).toMatchObject(expected);
    });
});

// -------------------------------------- utils -----------------------------------------------

function createStore(operationData) {
    return new OutgoingProfitWithdrawingStore(getOperationDataForTest(operationData));
}

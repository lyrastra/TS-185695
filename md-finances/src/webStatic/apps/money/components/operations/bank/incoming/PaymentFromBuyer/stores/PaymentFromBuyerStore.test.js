/* eslint-disable no-undef */
import { toJS } from 'mobx';
import PaymentFromBuyerStore from './PaymentFromBuyerStore';
import { setupFetch, waitAsync } from './../../../../../../../../test/utils';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import { getOperationDataForTest } from '../../../helpers/operationStoreHelper';

const kontragent = { id: 121212, name: `ИП Иван Васильевич` };

describe(`PaymentFromBuyerStore test`, () => {
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

    it(`store.setBills with kontragentId undefined - store.bills length increase`, () => {
        const store = createStore();

        const bills = [{ DocumentBaseId: 1, KontragentId: kontragent.id }];

        return store.setBills(bills)
            .then(() => {
                expect(store.bills.length).toBe(1);
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

    it(`create store with undefined IncludeNds - IncludeNds set to default(true for Osno)`, () => {
        const store = createStore({
            operation: {
                IncludeNds: undefined
            },
            taxationSystem: {
                IsOsno: true
            }
        });

        expect(store.model.IncludeNds).toBe(true);
    });

    it(`create store with defined IncludeNds - IncludeNds remains unchanged`, () => {
        const store = createStore({
            operation: {
                IncludeNds: false
            },
            taxationSystem: {
                IsOsno: true
            }
        });

        expect(store.model.IncludeNds).toBe(false);
    });

    it(`kontragent and sum defined - can auto link documents`, () => {
        const store = createStore({
            operation: {
                Kontragent: {
                    KontragentId: kontragent.id,
                    KontragentName: kontragent.name
                },
                Sum: 100
            }
        });

        expect(store.canAutoLinkDocuments).toEqual(true);
    });

    it(`kontragent and sum defined, switchOffAutoSetDocuments - cann't auto link documents`, () => {
        const store = createStore({
            operation: {
                Kontragent: {
                    KontragentId: kontragent.id,
                    KontragentName: kontragent.name
                },
                Sum: 100
            }
        });

        store.switchOffAutoSetDocuments();

        expect(store.canAutoLinkDocuments).toEqual(false);
    });

    it(`kontragent defined, settlement not defined - kontragent hasn't own requisites`, () => {
        const store = createStore({
            operation: {
                Kontragent: {
                    KontragentId: kontragent.id,
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
                Kontragent: {
                    KontragentId: kontragent.id,
                    KontragentName: kontragent.name,
                    KontragentSettlementAccount: `111`,
                    KontragentBankName: `альфа`
                },
                Sum: 100
            }
        });

        return waitAsync().then(() => {
            expect(store.isKontragentHaveOwnRequisites).toEqual(true);
        });
    });

    it(`set sum - keep documents for payment sum only`, () => {
        const store = createStore({
            operation: {
                Documents: [
                    {
                        DocumentBaseId: 1, DocumentSum: 100, PaidSum: 0, key: 1, Sum: 90
                    },
                    {
                        DocumentBaseId: 2, Sum: 10, DocumentSum: 100, key: 2
                    }
                ],
                Sum: 100
            }
        });

        store.switchOffAutoSetDocuments();
        store.setSum({ value: 75 });

        expect(toJS(store.documents)).toEqual([{
            DocumentBaseId: 1,
            DocumentSum: `100,00`,
            PaidSum: `0,00`,
            Sum: `75,00`,
            key: 1
        }]);
    });

    it(`set same sum - documents don't change`, () => {
        const doc = {
            DocumentSum: `1\u00a0000,00`,
            PaidSum: `0,00`,
            key: 1,
            Sum: `90,00`
        };

        const store = createStore({
            operation: {
                Documents: [doc],
                Sum: 100
            }
        });

        store.switchOffAutoSetDocuments();
        store.setSum({ value: 100 });

        expect(toJS(store.documents)).toEqual([doc]);
    });

    it(`set documents sum more than unpaid remain - document sum equal unpaid remain`, () => {
        const doc = {
            DocumentSum: `118,00`,
            PaidSum: `0,00`,
            key: 1
        };

        const store = createStore({
            operation: {
                Documents: [doc],
                Sum: 1000
            }
        });

        store.switchOffAutoSetDocuments();
        store.setDocuments([{ ...doc, Sum: 130 }]);

        expect(toJS(store.documents[0].Sum)).toEqual(`118,00`);
    });

    it(`set bills sum more than unpaid remain - bill sum equal unpaid remain`, () => {
        const doc = {
            DocumentSum: `118,00`,
            PaidSum: `0,00`,
            key: 1
        };

        const store = createStore({
            operation: {
                Bills: [doc],
                Sum: 1000
            }
        });

        store.setBills([{ ...doc, Sum: 130 }]);

        expect(toJS(store.bills[0].Sum)).toEqual(`118,00`);
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

    it(`clear bills after kontragent change(https://youtrack.moedelo.org/youtrack/issue/BP-4920)`, () => {
        const doc = {
            DocumentSum: `118,00`,
            PaidSum: `0,00`,
            key: 1
        };
        const store = createStore({
            operation: {
                Bills: [doc],
                Sum: 1000,
                Kontragent: {
                    KontragentId: kontragent.id,
                    KontragentName: kontragent.name,
                    KontragentSettlementAccount: `111`,
                    KontragentBankName: `альфа`
                }
            }
        });
        const newKontragent = {
            Kontragent: {
                KontragentId: kontragent.id + 1,
                KontragentName: `${kontragent.name} новый`,
                KontragentSettlementAccount: `222`,
                KontragentBankName: `шмальфа`
            }
        };

        store.setContractor({ original: newKontragent });

        expect(toJS(store.bills).length).toBe(0);
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

    it(`float documents sum more than operation sum - validation state not is empty`, () => {
        const store = createStore({
            operation: {
                Sum: 50
            }
        });

        store.setDocuments([{ DocumentSum: 1000, Sum: 100 }]);

        expect(store.validationState.DocumentsSum).not.toBe(``);
    });

    it(`float documents sum equal operation sum - validation state is empty`, () => {
        const store = createStore({
            operation: {
                Sum: 300.33
            }
        });

        store.setDocuments([{ DocumentSum: 1000, Sum: 200.33 }, { DocumentSum: 1000, Sum: 100 }]);

        expect(store.validationState.DocumentsSum).toBe(``);
    });

    it(`set kontragentAccount to other, switch on Mediation - kontragentAccount change to main`, () => {
        const main = SyntheticAccountCodesEnum._62_02;
        const other = SyntheticAccountCodesEnum._76_06;

        const store = createStore({
            operation: { KontragentAccountCode: other }
        });

        expect(store.model.KontragentAccountCode).toBe(other);

        store.setIsMediation({ checked: true });

        expect(store.model.KontragentAccountCode).toBe(main);
    });
});

// -------------------------------------- utils -----------------------------------------------

function createStore(operationData) {
    return new PaymentFromBuyerStore(getOperationDataForTest(operationData));
}

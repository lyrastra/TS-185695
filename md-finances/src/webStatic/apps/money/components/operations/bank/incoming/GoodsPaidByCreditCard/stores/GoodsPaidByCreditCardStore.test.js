/* eslint-disable no-undef */
import GoodsPaidByCreditCardStore from './GoodsPaidByCreditCardStore';
import { setupFetch } from './../../../../../../../../test/utils';
import PostingDirection from '../../../../../../../../enums/newMoney/TaxPostingDirectionEnum';
import PostingTransferType from '../../../../../../../../enums/newMoney/TaxPostingTransferTypeEnum';
import TaxPostingTransferKind from '../../../../../../../../enums/newMoney/TaxPostingTransferKindEnum';
import TaxPostingNormalizedCostType from '../../../../../../../../enums/newMoney/TaxPostingNormalizedCostTypeEnum';
import { getOperationDataForTest } from '../../../helpers/operationStoreHelper';

const kontragent = { id: 121212, name: `ИП Иван Васильевич` };

describe(`GoodsPaidByCreditCardStore.js test`, () => {
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

    it(`TransferType valid for Outgoing`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Outgoing
        };

        const exp = [
            PostingTransferType.Direct,
            PostingTransferType.Indirect,
            PostingTransferType.NonOperating
        ];

        const assert = store.getTransferType(options);
        expect(assert).toEqual(exp);
    });

    it(`TransferKind valid for Outgoing-Direct`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Outgoing,
            Type: PostingTransferType.Direct
        };

        const exp = [TaxPostingTransferKind.Material];

        const assert = store.getTransferKind(options);
        expect(assert).toEqual(exp);
    });

    it(`NormalizedCostType valid for Outgoing-Direct-Material`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Outgoing,
            Type: PostingTransferType.Direct,
            Kind: TaxPostingTransferKind.Material
        };

        const exp = Object.values(TaxPostingNormalizedCostType);

        const assert = store.getNormalizedCostType(options);
        expect(assert).toEqual(exp);
    });

    it(`TransferKind valid for Outgoing-Indirect`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Outgoing,
            Type: PostingTransferType.Indirect
        };

        const exp = [
            TaxPostingTransferKind.Material,
            TaxPostingTransferKind.Salary,
            TaxPostingTransferKind.Amortization,
            TaxPostingTransferKind.OtherOutgo
        ];

        const assert = store.getTransferKind(options);
        expect(assert).toEqual(exp);
    });

    it(`NormalizedCostType valid for Outgoing-Indirect-Material`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Outgoing,
            Type: PostingTransferType.Indirect,
            Kind: TaxPostingTransferKind.Material
        };

        const exp = Object.values(TaxPostingNormalizedCostType);

        const assert = store.getNormalizedCostType(options);
        expect(assert).toEqual(exp);
    });

    it(`NormalizedCostType valid for Outgoing-Indirect-Salary`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Outgoing,
            Type: PostingTransferType.Indirect,
            Kind: TaxPostingTransferKind.Salary
        };

        const exp = Object.values(TaxPostingNormalizedCostType);

        const assert = store.getNormalizedCostType(options);
        expect(assert).toEqual(exp);
    });

    it(`NormalizedCostType valid for Outgoing-Indirect-Amortization`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Outgoing,
            Type: PostingTransferType.Indirect,
            Kind: TaxPostingTransferKind.Amortization
        };

        const exp = Object.values(TaxPostingNormalizedCostType);

        const assert = store.getNormalizedCostType(options);
        expect(assert).toEqual(exp);
    });

    it(`NormalizedCostType valid for Outgoing-Indirect-OtherOutgo`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Outgoing,
            Type: PostingTransferType.Indirect,
            Kind: TaxPostingTransferKind.OtherOutgo
        };

        const exp = Object.values(TaxPostingNormalizedCostType);

        const assert = store.getNormalizedCostType(options);
        expect(assert).toEqual(exp);
    });

    it(`TransferKind valid for Outgoing-NonOperating`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Outgoing,
            Type: PostingTransferType.NonOperating
        };

        const exp = [TaxPostingTransferKind.None];

        const assert = store.getTransferKind(options);
        expect(assert).toEqual(exp);
    });

    it(`TransferKind valid for Outgoing-NonOperating`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Outgoing,
            Type: PostingTransferType.NonOperating,
            Kind: TaxPostingTransferKind.None
        };

        const exp = Object.values(TaxPostingNormalizedCostType);

        const assert = store.getNormalizedCostType(options);
        expect(assert).toEqual(exp);
    });

    it(`TransferType valid for Incoming`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Incoming
        };

        const exp = [
            PostingTransferType.NonOperating,
            PostingTransferType.OperationIncome
        ];

        const assert = store.getTransferType(options);
        expect(assert).toEqual(exp);
    });

    it(`TransferType valid for Incoming-NonOperating`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Incoming,
            Type: PostingTransferType.NonOperating
        };

        const exp = [TaxPostingTransferKind.None];

        const assert = store.getTransferKind(options);
        expect(assert).toEqual(exp);
    });

    it(`TransferType valid for Incoming-NonOperating-None`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Incoming,
            Type: PostingTransferType.NonOperating,
            Kind: TaxPostingTransferKind.None
        };

        const exp = [TaxPostingNormalizedCostType.None];

        const assert = store.getTransferKind(options);
        expect(assert).toEqual(exp);
    });

    it(`TransferType valid for Incoming-OperationIncome`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Incoming,
            Type: PostingTransferType.OperationIncome
        };

        const exp = [
            TaxPostingTransferKind.Service,
            TaxPostingTransferKind.ProductSale,
            TaxPostingTransferKind.PropertyRight,
            TaxPostingTransferKind.OtherPropertySale
        ];

        const assert = store.getTransferKind(options);
        expect(assert).toEqual(exp);
    });

    it(`NormalizedCostType valid for Incoming-OperationIncome-Service`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Incoming,
            Type: PostingTransferType.OperationIncome,
            Kind: TaxPostingTransferKind.Service
        };

        const exp = [TaxPostingNormalizedCostType.None];

        const assert = store.getNormalizedCostType(options);
        expect(assert).toEqual(exp);
    });

    it(`NormalizedCostType valid for Incoming-OperationIncome-ProductSale`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Incoming,
            Type: PostingTransferType.OperationIncome,
            Kind: TaxPostingTransferKind.ProductSale
        };

        const exp = [TaxPostingNormalizedCostType.None];

        const assert = store.getNormalizedCostType(options);
        expect(assert).toEqual(exp);
    });

    it(`NormalizedCostType valid for Incoming-OperationIncome-PropertyRight`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Incoming,
            Type: PostingTransferType.OperationIncome,
            Kind: TaxPostingTransferKind.PropertyRight
        };

        const exp = [TaxPostingNormalizedCostType.None];

        const assert = store.getNormalizedCostType(options);
        expect(assert).toEqual(exp);
    });

    it(`NormalizedCostType valid for Incoming-OperationIncome-OtherPropertySale`, () => {
        const store = createStore();
        const options = {
            Direction: PostingDirection.Incoming,
            Type: PostingTransferType.OperationIncome,
            Kind: TaxPostingTransferKind.OtherPropertySale
        };

        const exp = [TaxPostingNormalizedCostType.None];

        const assert = store.getNormalizedCostType(options);
        expect(assert).toEqual(exp);
    });
});

// -------------------------------------- utils -----------------------------------------------

function createStore(operationData) {
    return new GoodsPaidByCreditCardStore(getOperationDataForTest(operationData));
}

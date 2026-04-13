import expect from 'expect';
import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import notTaxableRentGetter from './notTaxableRentGetter';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

const usnProfitTaxationSystem = {
    IsUsn: true,
    UsnType: UsnTypeEnum.Profit
};

const usnProfitAndOutgoTaxationSystem = {
    IsUsn: true,
    UsnType: UsnTypeEnum.ProfitAndOutgo
};

const notUsnTaxationSystem = {
    IsUsn: false
};


describe(`notTaxableRentGetter for RentPayment operation`, () => {
    it(`not taxable for Usn profit`, () => {
        const operationData = {
            taxationSystem: usnProfitTaxationSystem
        };

        expect(notTaxableRentGetter.get(operationData)).toEqual(notTaxableMessages.simple);
    });

    it(`not taxable for Usn profit and outgo `, () => {
        const operationData = {
            taxationSystem: usnProfitAndOutgoTaxationSystem
        };

        expect(notTaxableRentGetter.get(operationData)).toEqual(notTaxableMessages.usnRentPaymentOnClosingOfMonth);
    });

    it(`not taxable for not Usn`, () => {
        const operationData = {
            taxationSystem: notUsnTaxationSystem
        };

        expect(notTaxableRentGetter.get(operationData)).toEqual(notTaxableMessages.simple);
    });
});

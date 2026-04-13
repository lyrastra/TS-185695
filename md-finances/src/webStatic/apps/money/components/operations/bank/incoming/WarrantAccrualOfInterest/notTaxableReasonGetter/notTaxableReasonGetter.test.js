import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import notTaxableReasonGetter from './notTaxableReasonGetter';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

describe(`notTaxableReasonGetter for WarrantAccrualOfInterest operation`, () => {
    it(`not taxable for Envd`, () => {
        const envd = {
            IsOsno: false,
            IsUsn: false,
            IsEnvd: true
        };

        expect(notTaxableReasonGetter.get(TaxationSystemType.Envd, { taxationSystem: envd })).toEqual(notTaxableMessages.envd);
    });
});

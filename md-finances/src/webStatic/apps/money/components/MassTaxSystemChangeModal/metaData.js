import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import {
    cashOrderOperationResources,
    paymentOrderOperationResources, purseOperationResources
} from '../../../../resources/MoneyOperationTypeResources';

const availableOperationsDescription = [
    {
        name: `Банк`,
        list: [
            paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.text,
            paymentOrderOperationResources.MemorialWarrantReceiptGoodsPaidCreditCard.text,
            paymentOrderOperationResources.MemorialWarrantAccrualOfInterest.text,
            paymentOrderOperationResources.BankFee.text
        ]
    },
    {
        name: `Касса`,
        list: [
            cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.text
        ]
    },
    {
        name: `Платежные системы`,
        list: [
            purseOperationResources.PurseOperationIncome.text,
            purseOperationResources.PurseOperationComission.text
        ]
    }
];

const taxationSystemsForDropdown = [
    {
        text: `ОСНО`,
        value: TaxationSystemType.Osno
    },
    {
        text: `ЕНВД`,
        value: TaxationSystemType.Envd
    },
    {
        text: `ПСН`,
        value: TaxationSystemType.Patent
    },
    {
        text: `УСН`,
        value: TaxationSystemType.Usn
    }
];

export {
    availableOperationsDescription,
    taxationSystemsForDropdown
};

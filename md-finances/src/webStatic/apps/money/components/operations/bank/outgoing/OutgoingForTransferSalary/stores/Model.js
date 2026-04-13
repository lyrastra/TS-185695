import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import ContractTypesEnum from '../../../../../../../../enums/newMoney/ContractTypesEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    KontragentAccountCode: null,
    Sum: 0,
    Description: ``,
    Status: ``,
    OperationType: null,
    SalaryWorkerId: null,
    WorkerName: ``,
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    TaxPostingsMode: ProvidePostingType.Auto,
    PostingsAndTaxMode: ProvidePostingType.Auto,
    AccountingPostings: {},
    ProvideInAccounting: true,
    UnderContract: ContractTypesEnum.Employment.value,
    WorkerCharges: [],
    EmployeePayments: [],
    AdvanceStatements: []
};

import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import AccountingDocumentType from '@moedelo/frontend-enums/mdEnums/AccountingDocumentType';

export default [
    { Type: AccountingDocumentType.Waybill, Direction: Direction.Incoming },
    { Type: AccountingDocumentType.Upd, Direction: Direction.Incoming },
    { Type: AccountingDocumentType.AccountingAdvanceStatement, Direction: Direction.Incoming },
    { Type: AccountingDocumentType.RetailRefund, Direction: Direction.Outgoing }
];

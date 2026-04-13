/* eslint-disable */
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

(function(common) {
    common.Data.DocumentTypeHelper = {
        getTypeName(documentType) {
            const types = common.Data.DocumentTypes || {};
            switch (documentType) {
                case types.Statement:
                    return 'акт';
                case types.Waybill:
                    return 'накладная';
                case types.Invoice:
                    return 'счет-фактура';
                case types.Bill:
                    return 'счет';
                case types.AccountingStatement:
                    return 'бухгалтерская справка';
                case types.ReturnToSupplier:
                    return 'возврат товара поставщику';
                case types.ReturnFromBuyer:
                    return 'возврат от покупателя';
                case types.Project:
                    return 'договор';
                case types.AdvanceStatement:
                    return 'авансовый отчет';
                case types.IncomingStatementOfFixedAsset:
                    return 'акт приёма-передачи ОС';
                case types.PaymentOrder:
                    return 'платежное поручение';
                case types.IncomingCashOrder:
                    return 'кассовый ордер';
                case types.OutgoingCashOrder:
                    return 'кассовый ордер';
                case types.Inventory:
                    return 'инвентаризация';
                case types.RetailReport:
                    return 'отчет о розничной продаже';
                case types.MiddlemanReport:
                    return 'отчет посредника';
                case types.Upd:
                case types.SalesUpd:
                    return 'УПД';
                default:
                    return '';
            }
        },

        getAccountingDocumentTypeName(accountingDocumentType, direction) {
            fillDictionary();
            const result = accountingDocumentTypeDictionary[accountingDocumentType];
            if (result) {
                const transferDirection = getDirection(accountingDocumentType, direction);
                return transferDirection.length > 0 ? [getDirection(accountingDocumentType, direction), result].join(' ') : result;
            }
            return '';
        },
        
        getAccountingDocumentsTypeName(accountingDocumentType, direction) {
            fillDictionary();
            const result = accountingDocumentTypeDictionary.many[accountingDocumentType] || accountingDocumentTypeDictionary[accountingDocumentType];
            if (result) {
                const directionDescription = getDirection(accountingDocumentType, direction);
                if (directionDescription.length == 0) {
                    return result;
                }
                const transferDirection = direction == Direction.Incoming ? 'входящие' : 'исходящие';
                return `${transferDirection} ${result}`;
            }
            return '';
        },

        getAccountingDocumentTypeFullName(accountingDocumentType, direction) {
            if (accountingDocumentType === common.Data.AccountingDocumentType.PaymentOrder) {
                return 'Платежное поручение';
            }

            return this.getAccountingDocumentTypeName(accountingDocumentType, direction);
        }
    };

    function getDirection(documentType, direction) {
        if (direction == undefined) {
            return '';
        }

        const types = common.Data.AccountingDocumentType || {};
        const transferDirection = direction == Direction.Incoming ? 'входящ' : 'исходящ';
        switch (documentType) {
            case types.Statement:
            case types.Invoice:
                return `${transferDirection}ий`;
            case types.Waybill:
            case types.AccountingStatements:
                return `${transferDirection}ая`;
            case types.PaymentOrder:
                return `${transferDirection}ее`;
            default:
                return '';
        }
    }

    var accountingDocumentTypeDictionary = {};
    var fillDictionary = _.once(() => {
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.PaymentOrder] = 'П/п ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.IncomingCashOrder] = 'ПКО ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.OutcomingCashOrder] = 'РКО ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.Invoice] = 'Счет-фактура ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.Waybill] = 'Накладная ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.Statement] = 'Акт ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.AccountingAdvanceStatement] = 'Авансовый отчет ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.IncomingStatementOfFixedAsset] = 'Входящий акт приема-передачи ОС ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.InventoryCard] = 'Инвентарная карточка';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.MiddlemanReport] = 'Отчет посредника ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.Bill] = 'Счет ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.AccountingStatement] = 'Бухгалтерская справка ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.Project] = 'Договор ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.RetailReport] = 'Отчет о розничной продаже ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.PurseOperation] = 'Поступление эл.денег ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.RequisitionWaybill] = 'Требование-накладная ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.Bundling] = 'Комплектация ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.Upd] = 'УПД ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.SalesUpd] = 'УПД ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.RetailRefund] = 'Возврат розничной продажи ';
        accountingDocumentTypeDictionary[common.Data.AccountingDocumentType.ReceiptStatement] = 'Акт приема-передачи ';

        accountingDocumentTypeDictionary.many = {};
        accountingDocumentTypeDictionary.many[common.Data.AccountingDocumentType.Invoice] = 'Счета-фактуры ';
        accountingDocumentTypeDictionary.many[common.Data.AccountingDocumentType.Waybill] = 'Накладные ';
        accountingDocumentTypeDictionary.many[common.Data.AccountingDocumentType.Statement] = 'Акты ';
        accountingDocumentTypeDictionary.many[common.Data.AccountingDocumentType.AccountingAdvanceStatement] = 'Авансовые отчеты ';
        accountingDocumentTypeDictionary.many[common.Data.AccountingDocumentType.IncomingStatementOfFixedAsset] = 'Входящие акты приема-передачи ОС ';
        accountingDocumentTypeDictionary.many[common.Data.AccountingDocumentType.InventoryCard] = 'Инвентарные карточки ОС ';
    });
}(Common));

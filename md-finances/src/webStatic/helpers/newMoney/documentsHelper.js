import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import AccountingDocumentType from '@moedelo/frontend-enums/mdEnums/AccountingDocumentType';
import DocumentTypeEnum from '../../enums/DocumentTypeEnum';

function getDocumentNameByType(type) {
    switch (type) {
        case DocumentTypeEnum.Upd:
        case DocumentTypeEnum.SalesUpd:
            return `УПД`;
        case DocumentTypeEnum.Waybill:
            return `Накладная`;
        case DocumentTypeEnum.Statement:
            return `Акт`;
        case DocumentTypeEnum.MiddlemanReport:
            return `Отчет посредника`;
        case DocumentTypeEnum.InventoryCard:
            return `Инвентарная карточка`;
        case DocumentTypeEnum.ReceiptStatement:
            return `Акт приема-передачи`;
        case DocumentTypeEnum.SalesCurrencyInvoice:
        case DocumentTypeEnum.PurchasesCurrencyInvoice:
            return `Инвойс`;
        default:
            return ``;
    }
}

function isNotTaxableDocuments(documentsList = []) {
    return documentsList.filter((document) => {
        return (document.DocumentId || document.Id || document.DocumentBaseId) &&
            (
                document.DocumentTaxationSystemType !== TaxationSystemType.Envd &&
                !(document.DocumentType === AccountingDocumentType.Waybill && !document.HasMaterial)
            );
    }).length === 0;
}

function isReceiptStatementDocuments(documentsList = []) {
    return documentsList.filter((document) => {
        return (document.DocumentId || document.Id) &&
            (
                document.DocumentTaxationSystemType !== TaxationSystemType.Envd &&
                (document.DocumentType === AccountingDocumentType.ReceiptStatement && !document.HasMaterial)
            );
    }).length > 0;
}

export default getDocumentNameByType;

export { getDocumentNameByType, isNotTaxableDocuments, isReceiptStatementDocuments };


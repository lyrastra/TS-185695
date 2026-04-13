import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';
import { get as restGet } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import AccountingDocumentType from '@moedelo/frontend-enums/mdEnums/AccountingDocumentType';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/** чтобы показать документы всех возможных типов по 5 шт */
const maxDocumentsInDropdown = 15;

export default {
    async autocomplete(requestArgs = { }) {
        const url = `/Accounting/BankAutocompletes/GetOutgoingReasonDocumentAutocomplete`;
        const waybillsTask = get(url, { ...requestArgs, DocumentType: AccountingDocumentType.Waybill });
        const statementsTask = get(url, { ...requestArgs, DocumentType: AccountingDocumentType.Statement });
        const updsTask = get(url, { ...requestArgs, DocumentType: AccountingDocumentType.SalesUpd });
        const [waybills, statements, upds] = await Promise.all([waybillsTask, statementsTask, updsTask]);
        const items = [].concat(statements.List, waybills.List, upds.List);

        return items
            .slice(0, maxDocumentsInDropdown)
            .map(mapDocumentAutocompleteItem);
    },

    async outgoingAutocomplete(requestArgs = {}, documentTypes = []) {
        const defaultUrl = `/Accounting/BankAutocompletes/GetIncomingReasonDocumentAutocomplete`;
        const inventoryUrl = `/Accounting/InventoryCard/GetInventoryCardForPaymentAutocomplete`;
        const tasks = documentTypes.map(type => {
            const url = type !== AccountingDocumentType.InventoryCard ? defaultUrl : inventoryUrl;

            return get(url, { ...requestArgs, DocumentType: type });
        });
        const rawResponse = await Promise.all(tasks);
        const mappedRawResponse = rawResponse.map(b => {
            return b.List;
        });
        const items = [].concat(...mappedRawResponse);

        return items
            .slice(0, requestArgs.count || 5)
            .map(mapDocumentAutocompleteItem);
    },

    async mediationDocsAutocomplete(requestArgs = { }) {
        const url = `/Accounting/MiddlemanContract/GetMiddlemanDocumentsAutocomplete`;

        const waybillsTask = get(url, { ...requestArgs, DocumentType: AccountingDocumentType.Waybill });
        const statementsTask = get(url, { ...requestArgs, DocumentType: AccountingDocumentType.Statement });
        const middlemanReportsTask = get(url, { ...requestArgs, DocumentType: AccountingDocumentType.MiddlemanReport });

        const [waybills, statements, middlemanReports] = await Promise.all([waybillsTask, statementsTask, middlemanReportsTask]);
        const items = [].concat(statements.List, waybills.List, middlemanReports.List);

        return items
            .sort((a, b) => sortDocumentsByDate(a, b, sortDocumentsByNumber))
            .slice(0, requestArgs.count || 5)
            .map(mapDocumentAutocompleteItem);
    },

    async salesCurrencyInvoiceAutocomplete(requestArgs = { }) {
        const url = `/CurrencyInvoices/api/v1/Sales/Autocomplete`;

        if (!requestArgs.kontragentId) {
            return Promise.resolve([]);
        }

        const { data } = await restGet(url, {
            SettlementAccountId: requestArgs.settlementAccountId || null,
            Query: requestArgs.query,
            Count: requestArgs.count,
            KontragentId: requestArgs.kontragentId,
            PaymentDocumentBaseId: requestArgs.parentDocumentId,
            ExcludeDocumentIds: requestArgs.excludeDocumentIds
        });

        return data
            .sort((currentCurrencyInvoice, nextCurrencyInvoice) => sortDocumentsByIsoDate(currentCurrencyInvoice, nextCurrencyInvoice, sortDocumentsByNumber))
            .map(mapCurrencyInvoiceAutocompleteItem);
    },

    async purchasesCurrencyInvoiceAutocomplete(requestArgs = { }) {
        const url = `/CurrencyInvoices/api/v1/Purchases/Autocomplete`;

        if (!requestArgs.kontragentId || !requestArgs.currency) {
            return Promise.resolve([]);
        }

        const { data } = await restGet(url, {
            Query: requestArgs.query,
            Count: requestArgs.count,
            KontragentId: requestArgs.kontragentId,
            Currency: requestArgs.currency,
            PaymentDocumentBaseId: requestArgs.parentDocumentId,
            ExcludeDocumentIds: requestArgs.excludeDocumentIds
        });

        return data
            .sort((currentCurrencyInvoice, nextCurrencyInvoice) => sortDocumentsByIsoDate(currentCurrencyInvoice, nextCurrencyInvoice, sortDocumentsByNumber))
            .map(mapCurrencyInvoiceAutocompleteItem);
    }
};

function mapCurrencyInvoiceAutocompleteItem(item) {
    const {
        Sum, DocumentNumber, DocumentDate, ...autocompleteItem
    } = item;

    autocompleteItem.Date = dateHelper(DocumentDate).format(`DD.MM.YYYY`);
    autocompleteItem.Number = DocumentNumber;
    autocompleteItem.DocumentSum = Sum;
    autocompleteItem.PaidSum = item.Sum - item.UnpaidBalance;
    autocompleteItem.Type = item.DocumentType;

    return autocompleteItem;
}

function mapDocumentAutocompleteItem(item) {
    const {
        Sum, DocumentName, DocumentDate, ...autocompleteItem
    } = item;

    autocompleteItem.Date = /\d{2}\.\d{2}\.\d{4}/.test(DocumentDate)
        ? DocumentDate
        : dateHelper(DocumentDate).format(`DD.MM.YYYY`);
    autocompleteItem.Number = DocumentName;
    autocompleteItem.DocumentSum = Sum;
    autocompleteItem.PaidSum = item.Sum - item.UnpaidBalance;

    return autocompleteItem;
}

function sortDocumentsByDate(a, b, fn) {
    const aDate = dateHelper(a.DocumentDate, `DD.MM.YYYY`);
    const bDate = dateHelper(b.DocumentDate, `DD.MM.YYYY`);

    if (aDate.isAfter(bDate)) {
        return 1;
    }

    if (aDate.isBefore(bDate)) {
        return -1;
    }

    return fn ? fn(a, b) : 0;
}

function sortDocumentsByIsoDate(currentDocument, nextDocument, fn) {
    const currentDocumentDate = dateHelper(currentDocument.DocumentDate);
    const nextDocumentDate = dateHelper(nextDocument.DocumentDate);

    if (currentDocumentDate.isAfter(nextDocumentDate)) {
        return 1;
    }

    if (currentDocumentDate.isBefore(nextDocumentDate)) {
        return -1;
    }

    return fn ? fn(currentDocument, nextDocument) : 0;
}

function sortDocumentsByNumber(a, b, fn) {
    if (a.DocumentName > b.DocumentName) {
        return 1;
    }

    if (a.DocumentName < b.DocumentName) {
        return -1;
    }

    return fn ? fn(a, b) : 0;
}

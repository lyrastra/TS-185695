import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import {
    get, post, download, downloadPost
} from '@moedelo/frontend-core-v2/helpers/httpClient';
import restHttpClient from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import { getLastClosedDate } from '@moedelo/frontend-common-v2/apps/finances/service/setupDataPreloadingService';
import FormatTypesEnum from '../../enums/FormatTypesEnum';
import MoneySourceType from '../../enums/MoneySourceType';
import { cashOrderOperationResources } from '../../resources/MoneyOperationTypeResources';
import { isCash } from '../../helpers/MoneyOperationHelper';
import ProvideInTaxEnum from '../../enums/newMoney/ProvideInTaxEnum';

const updateFromBankEvent = `obnovit_iz_banka_stranitsa_dengi_click_button`;

const MoneyOperationService = {
    getOperations({
        offset = 0,
        count = 20,
        sortType = null,
        sortColumn = null,
        query = ``,
        startDate = ``,
        endDate = ``,
        kontragentId = null,
        kontragentType = null,
        workerId = null,
        direction = null,
        operationType = [],
        budgetaryType = null,
        sumFrom = null,
        sumTo = null,
        provideInTax = null,
        closingDocumentsCondition = null,
        approvedCondition = null,
        sourceType = MoneySourceType.All,
        sourceId = 0,
        sum = null,
        sumCondition = null,
        taxationSystemType = null,
        patentId = null
    }) {
        const requestArgs = {
            offset,
            count,
            sortType,
            sortColumn,
            query: decodeURIComponent(query),
            startDate: startDate ? dateHelper(startDate, `DD.MM.YYYY`).format(`YYYY-MM-DD`) : ``,
            endDate: endDate ? dateHelper(endDate, `DD.MM.YYYY`).format(`YYYY-MM-DD`) : ``,
            kontragentId,
            kontragentType,
            workerId,
            direction,
            sumFrom,
            sumTo,
            operationType: operationType.toString(),
            budgetaryType,
            provideInTax: this.getProvideInTax(Number(provideInTax)),
            closingDocumentsCondition: Number(closingDocumentsCondition),
            approvedCondition: Number(approvedCondition),
            sourceType,
            sourceId,
            sum,
            sumCondition,
            taxationSystemType,
            patentId,
            _: Date.now() // для отмены кэширования
        };

        return restHttpClient.get(`/Finances/Money/Table/MultiCurrency`, requestArgs)
            .then(resp => resp.data);
    },

    getSources() {
        return restHttpClient.get(`/Finances/Money/Sources`)
            .then(resp => resp.data);
    // .then(resp => [...resp.data, { для мока, если нет интеграции с яндекс кассой в локальном окружении
    //         Id: 16354905,
    //         Name: `ООО НКО "Яндекс.Касса"`,
    //         Type: 3,
    //         Balance: -77422.5,
    //         Currency: 0,
    //         Number: null,
    //         IsActive: true,
    //         IsPrimary: false,
    //         IsTransit: false,
    //         IntegrationPartner: 1026,
    //         HasAvaliableIntegration: false,
    //         HasActiveIntegration: true,
    //         CanRequestMovementList: true,
    //         CanSendPaymentOrder: false,
    //         HasUnprocessedRequests: false,
    //         IsReconciliationProcessing: false,
    //         HasEmployees: false,
    //         BikBank: null
    //     }]);
    },

    getSettlementOperation({ documentBaseId }) {
        return get(`/Accounting/PaymentOrders/GetByBaseId`, { id: documentBaseId }).then(resp => mapPaymentOrderOperation(resp));
    },

    getCopySettlementOperation({ documentBaseId }) {
        return get(`/Accounting/PaymentOrders/GetCopyPaymentOrderByBaseId`, { id: documentBaseId }).then(resp => {
            const operation = mapPaymentOrderOperation(resp);
            operation.Id = null;
            operation.DocumentBaseId = null;

            return operation;
        });
    },

    getDefaultSettlementOperation({ settlementAccountId, direction }) {
        const url = direction === Direction.Incoming ? `/Accounting/PaymentOrders/GetIncomingPaymentOrder` : `/Accounting/PaymentOrders/GetOutgoingPaymentOrder`;

        return get(url, { settlementAccountId, id: 0 }).then(resp => {
            const { Sum, IncludeNds, ...operation } = resp;
            operation.SettlementAccountId = operation.SettlementAccountId || settlementAccountId;

            return operation;
        });
    },

    getDefaultSettlementOperationWithContract({ settlementAccountId, direction, contractId }) {
        const url = direction === Direction.Incoming ? `/Accounting/PaymentOrders/IncomingOrderFromContract` : `/Accounting/PaymentOrders/OutgoingOrderFromContract`;

        return get(url, { id: 0, contractId }).then(resp => {
            const { Sum, IncludeNds, ...operation } = resp;
            operation.SettlementAccountId = operation.SettlementAccountId || settlementAccountId;

            return operation;
        });
    },

    getCashOperation({ documentBaseId }) {
        return get(`/Accounting/FirmCash/GetOrderByBaseId`, { id: documentBaseId }).then(resp => mapCashOperation(resp));
    },

    getCopyCashOperation({ sourceId }) {
        return get(`/Accounting/FirmCash/CopyCashOrderByBaseId`, { id: sourceId }).then(resp => {
            const result = { ...resp, Id: null, DocumentBaseId: null };

            return mapCashOperation(result);
        });
    },

    getDefaultCashOperationWithContract({ cashId, direction, contractId }) {
        const url = direction === Direction.Incoming ? `/Accounting/FirmCash/IncomingOrderFromContract` : `/Accounting/FirmCash/OutgoingOrderFromContract`;

        return get(url, { contractId }).then(resp => {
            const result = { ...resp, CashId: resp.CashId || cashId };

            return mapCashOperation(result);
        });
    },

    async getDefaultCashOperationWithType({ operationType }) {
        if (operationType === cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.value) {
            const operation = await this.getDefaultCashOperation({ direction: cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.Direction });
            operation.OperationType = operationType;

            return operation;
        }

        throw Error(`Типы операций нужно обрабатывать вручную`);
    },

    getDefaultCashOperation({ cashId, direction }) {
        const url = direction === Direction.Incoming ? `/Accounting/FirmCash/GetIncomingCashOrder` : `/Accounting/FirmCash/GetOutgoingCashOrder`;

        return get(url, { cashId }).then(resp => {
            const result = { ...resp, CashId: resp.CashId || cashId };

            return mapCashOperation(result);
        });
    },

    downloadFile(id, format, operationType) {
        const formatType = isCash(operationType) ? FormatTypesEnum.Cash : FormatTypesEnum.Bank;
        let extension = ``;

        switch (format) {
            case `pdf`:
                extension = formatType.PDF;

                break;
            case `xls`:
                extension = formatType.XLS;

                break;
            case `1c`:
                extension = formatType.TXT;

                break;
        }

        const url = isCash(operationType) ? `Accounting/FirmCash/GetFileByBaseId?id=${id}&extention=${extension}`
            : `/Accounting/PaymentOrders/GetFileByBaseId?id=${id}&format=${extension}`;

        return download(url);
    },

    downloadJoinedByDocumentBaseIds(ids) {
        return downloadPost(` /Accounting/FirmCash/CashOrdersFile`, { ids });
    },

    async getLastClosedPeriod() {
        if (this.lastClosedPeriod) {
            return Promise.resolve(this.lastClosedPeriod);
        }

        const lastClosedDate = await getLastClosedDate();
        const correctDate = lastClosedDate.replace(/^"(.*)"$/, `$1`);

        this.lastClosedPeriod = new Date(correctDate);

        return new Date(correctDate);
    },

    getProvideInTax(provideInTax) {
        if (provideInTax && provideInTax !== ProvideInTaxEnum.DoesNotMatter) {
            return provideInTax === ProvideInTaxEnum.Taken;
        }

        return null;
    },

    openClosedPeriod(date) {
        return post(`/Accounting/ClosedPeriods/OpenPeriod`, { date });
    },

    deleteOperation(operationIds) {
        return restHttpClient.remove(`/Money/api/v1/Operations/`, operationIds);
    },

    downloadLatestStatements(sourceId) {
        restHttpClient.download(`/Finances/PaymentExport?sourceId=${sourceId}`);
    },

    downloadRegistry(id) {
        return restHttpClient.get(`/payroll/api/v1/SalaryProjects/Validate/?documentBaseId=${id}`).then(() => {
            restHttpClient.download(`/payroll/api/v1/SalaryProjects/Download/?documentBaseId=${id}`);
        });
    },

    updateFromBankBySource(startDate, endDate, sourceType, sourceId) {
        logRequestImportEvent(startDate, endDate);

        return restHttpClient.post(`/Finances/Integrations/Statements/RequestBySource`, {
            startDate: startDate ? dateHelper(startDate, `DD.MM.YYYY`).format(`YYYY-MM-DD`) : null,
            endDate: endDate ? dateHelper(endDate, `DD.MM.YYYY`).format(`YYYY-MM-DD`) : null,
            sourceType,
            sourceId
        }).then(resp => resp.data);
    },

    updateFromBank(startDate, endDate) {
        logRequestImportEvent(startDate, endDate);

        return restHttpClient.post(`/Finances/Integrations/Statements/Request`, {
            startDate: startDate ? dateHelper(startDate, `DD.MM.YYYY`).format(`YYYY-MM-DD`) : null,
            endDate: endDate ? dateHelper(endDate, `DD.MM.YYYY`).format(`YYYY-MM-DD`) : null
        }).then(resp => resp.data);
    },

    /**
     * Отправка операций в банк
     * @param {Array.<Number>} operationIds массив DocumentBaseId
     * @return {Promise<void>}
     */
    sendToBank(operationIds) {
        return restHttpClient.post(`/Finances/Integrations/SendPaymentOrders`, operationIds)
            .then(resp => resp.data);
    },

    /**
     * Отправка в банк сквозного платежа
     * @param {Number} operationId - DocumentBaseId
     * @param {String} backUrl - url для возврата после платежа из банка
     * @return {Promise<void>}
     */
    sendInvoiceToBank(operationId, backUrl) {
        return restHttpClient.post(`/Finances/Integrations/SendBankInvoice`, { operationId, backUrl })
            .then(resp => resp.data);
    },

    saveBankOperation(data) {
        return post(`/Accounting/PaymentOrders/SavePaymentOrder`, data);
    },

    async isDocumentNumberNotBusy(data) {
        const { Status } = await get(`/Accounting/PaymentOrders/DocumentNumberNotBusy`, data);

        return Status;
    },

    getTypeByBaseId({ documentBaseId }) {
        return restHttpClient.get(`/Money/api/v1/PaymentOrders/${documentBaseId}/Type`)
            .then(resp => resp.data);
    },

    getIdByBaseId({ DocumentBaseId }) {
        return restHttpClient.get(`/Money/api/v1/PaymentOrders/${DocumentBaseId}/Id`)
            .then(resp => resp.data);
    }
};

function logRequestImportEvent(startDate, endDate) {
    const eventObj = {
        Event: updateFromBankEvent,
        St5: startDate,
        St6: endDate
    };

    mrkStatService.sendEventWithoutInternalUser(eventObj);
}

function mapPaymentOrderOperation(response) {
    const empty = [`Operations`];

    if (!response.Id) {
        Object.entries(response).forEach(([name, val]) => {
            if (typeof val === `undefined` || val === null) {
                empty.push(name);
            }
        });
    }

    response.KontragentName = response.Kontragent.KontragentName;

    if (response.Documents) {
        response.Documents = response.Documents.map(doc => {
            const item = Object.assign({}, doc);

            item.DocumentKontragentId = response.KontragentId;
            item.BaseDocumentId = response.BaseDocumentId;

            return item;
        });
    }

    empty.forEach(key => delete response[key]);

    return response;
}

function mapCashOperation(response) {
    const empty = [`Operations`];

    if (!response.Id) {
        empty.push(`Date`);
        Object.entries(response).forEach(([name, val]) => {
            if (typeof val === `undefined` || val === null) {
                empty.push(name);
            }
        });
    }

    if (response.NdsType === null) {
        response.NdsType = 99;
    }

    if (response.Sum === 0) {
        empty.push(`Sum`);
    }

    if (response.OperationType === 0) {
        empty.push(`OperationType`);
    }

    if (response.CashId === 0) {
        empty.push(`CashId`);
    }

    if (response.Documents) {
        response.Documents = response.Documents.map(doc => {
            const item = Object.assign({}, doc);

            item.DocumentKontragentId = response.KontragentId;
            item.BaseDocumentId = response.BaseDocumentId;

            return item;
        });
    }

    empty.forEach(key => delete response[key]);

    if (response.OperationType === cashOrderOperationResources.UnifiedCashOrderBudgetaryPayment.value) {
        response.OperationType = cashOrderOperationResources.CashOrderBudgetaryPayment.value;
    }

    return response;
}

export default MoneyOperationService;

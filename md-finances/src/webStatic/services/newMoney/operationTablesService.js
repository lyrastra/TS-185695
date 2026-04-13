import { get, post, remove } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import dateHelper, { DateFormat } from '@moedelo/frontend-core-v2/helpers/dateHelper';

const defaultCount = 20;
const defaultSourceId = 0;
const defaultSourceType = 0;

const urls = {
    getSuccessOperationsCount: `/Finances/Money/Table/Success/Count`,
    getWarningOperationsCount: `/Finances/Money/Table/Warning/Count`,
    getOutsourceProcessingOperationsCount: `/Finances/Money/Table/OutsourceProcessing/Count`,
    warningTableOperations: `/Finances/Money/Table/Warning`,
    successTableOperations: `/Finances/Money/Table/Success`,
    outsourceProcessingTableOperations: `/Finances/Money/Table/OutsourceProcessing`,
    approveSuccessOperations: `/Money/api/v1/Operations/Imported/Approve`
};

const OperationTablesService = {
    getSuccessOperationsCount: (sourceId, sourceType) => {
        const requestData = {
            sourceId: sourceId || defaultSourceId,
            sourceType: sourceType || defaultSourceType
        };

        return get(urls.getSuccessOperationsCount, requestData)
            .then(resp => resp.data);
    },

    getWarningOperationsCount: (sourceId, sourceType) => {
        const requestData = {
            sourceId: sourceId || defaultSourceId,
            sourceType: sourceType || defaultSourceType
        };

        return get(urls.getWarningOperationsCount, requestData)
            .then(resp => resp.data);
    },

    getOutsourceProcessingOperationsCount: (sourceId, sourceType) => {
        const requestData = {
            sourceId: sourceId || defaultSourceId,
            sourceType: sourceType || defaultSourceType
        };

        return get(urls.getOutsourceProcessingOperationsCount, requestData)
            .then(resp => resp.data);
    },

    getWarningOperationsList: (offset, count, sourceId, sourceType) => {
        const requestData = {
            offset: offset || 0,
            count: count || defaultCount,
            sourceId: sourceId || defaultSourceId,
            sourceType: sourceType || defaultSourceType
        };

        return get(urls.warningTableOperations, requestData)
            .then(resp => resp.data);
    },

    getRelatedOperations: url => {
        const requestData = {
            operations: 10
        };

        return get(url, requestData)
            .then(resp => resp.data);
    },

    getOutsourceProcessingOperationsList: (offset, count, sourceId, sourceType, filter) => {
        const requestData = {
            offset: offset || 0,
            count: count || defaultCount,
            sourceId: sourceId || defaultSourceId,
            sourceType: sourceType || defaultSourceType,
            ...filter,
            startDate: filter?.startDate ? dateHelper(filter.startDate, DateFormat.ru).format(DateFormat.iso) : null,
            endDate: filter?.endDate ? dateHelper(filter.endDate, DateFormat.ru).format(DateFormat.iso) : null,
            operationType: filter?.operationType?.toString()
        };

        return get(urls.outsourceProcessingTableOperations, requestData)
            .then(resp => resp.data);
    },

    getSuccessOperationsList: (offset, count, sourceId, sourceType) => {
        const requestData = {
            offset: offset || 0,
            count: count || defaultCount,
            sourceId: sourceId || defaultSourceId,
            sourceType: sourceType || defaultSourceType
        };

        return get(urls.successTableOperations, requestData)
            .then(resp => resp.data);
    },

    deleteOperation: documentBaseId => {
        const url = `/Finances/Money/Operations/${documentBaseId}`;

        return remove(url);
    },

    importOperation: documentBaseId => {
        const url = `/Finances/Money/Operations/Duplicates/${documentBaseId}/Import`;

        return post(url);
    },

    mergeOperation: documentBaseId => {
        const url = `/Finances/Money/Operations/Duplicates/${documentBaseId}/Merge`;

        return post(url);
    },

    deleteAllOperations: data => {
        return remove(urls.warningTableOperations, data);
    },

    approveOperations(data) {
        return post(urls.approveSuccessOperations, data);
    },

    changeOperationTaxationSystem(data) {
        return post(`/Money/api/v1/Operations/ChangeTaxationSystem`, data);
    }
};

export default OperationTablesService;

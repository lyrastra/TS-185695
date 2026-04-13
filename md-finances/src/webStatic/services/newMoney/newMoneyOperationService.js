import {
    get, post, put, remove
} from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import httpClient from '@moedelo/frontend-core-v2/helpers/httpClient';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { getRestMoneyPath } from '../../helpers/newMoney/operationUrlHelper';
import DocumentStatusEnum from '../../enums/DocumentStatusEnum';
import MoneyOperationTypeResources, { paymentOrderOperationResources } from '../../resources/MoneyOperationTypeResources';
import { isOutgoingByType } from '../../helpers/MoneyOperationHelper';
import { mapLinkedBillToClient } from '../../mappers/linkedBills/linkedBillsMapper';
import { getNextNumber } from '../Bank/paymentOrderService';
import { mapCurrencyInvoicesToFrontendModel } from '../../mappers/currencyInvoicesMapper';
import KontragentType from '../../enums/KontragentType';

export function getPaymentOrder({ documentBaseId, operationType }) {
    const url = `${getRestMoneyPath({ operationType })}/${documentBaseId}`;

    return get(url).then(resp => {
        return mapPaymentOrder({ paymentOrder: resp.data, operationType, documentBaseId });
    });
}

export function getPaymentForMd({ defaultData, billNumber }) {
    return get(`/Money/api/v1/BillToPaymentOrder/GetBill`, { billNumber }).then(resp => {
        const paymentOrder = {
            ...defaultData,
            ...resp.data
        };

        return mapPaymentOrder({ paymentOrder, operationType: defaultData.OperationType });
    });
}

export function checkHasAccessToMarketplacesAndCommissionAgents() {
    return httpClient
        .get(`/Kontragents/CommissionAgents/HasAccess`)
        .then(resp => resp.HasAccess);
}

export async function getPaymentOrderFromContract(options) {
    const { ContractId, ContractorType } = options;
    const {
        Number: ProjectNumber, DocDate, KontragentId, Id: ContractBaseId
    } = await get(`/Contract/api/v2/contract/${ContractId}`).catch(e => {
        throw new Error(e);
    });
    const contractor = await get(`/Kontragents/api/v1/kontragent/${KontragentId}`).catch(e => {
        throw new Error(e);
    });
    const Kontragent = mapContractor(contractor, ContractorType === KontragentType.Worker);
    const Contract = {
        Date: dateHelper(DocDate).format(`DD.MM.YYYY`) || null,
        ContractBaseId,
        ProjectNumber
    };

    return Object.assign({}, options, { Kontragent, KontragentId, Contract });
}

/**
 * Возвращает правила импорта применённые к операции
 * @param {number} documentBaseId - ID базового документа операции
 * @returns {Promise<Array<{DocumentBaseId: number,  Id: number, Name: string}>>}
 */
export async function getImportRules(documentBaseId) {
    if (!documentBaseId) {
        return [];
    }

    const requestData = {
        DocumentBaseIds: [documentBaseId]
    };

    try {
        const { data = [] } = await post(`/PaymentImportRules/api/v1/Operations/GetAppliedRules`, requestData);

        return data;
    } catch (error) {
        return [];
    }
}

/**
 * Возвращает правила импорта с массовой страницы применённые к операции
 * @param {number} documentBaseId - ID базового документа операции
 * @returns {Promise<Array<{DocumentBaseId: number,  Id: number, Name: string}>>}
 */
export async function getOutsourceImportRules(documentBaseId) {
    if (!documentBaseId) {
        return null;
    }

    try {
        const { data = [] } = await get(`/PaymentImportRules/api/v1/Operations/GetOutsourceAppliedRule/${documentBaseId}`);

        return data;
    } catch (error) {
        return null;
    }
}

export function getCopyPaymentOrder({ documentBaseId, operationType }) {
    const url = operationType === MoneyOperationTypeResources.BudgetaryPayment.value
        ? `${getRestMoneyPath({ operationType })}/${documentBaseId}/copy`
        : `${getRestMoneyPath({ operationType })}/${documentBaseId}`;

    return get(url).then(async resp => {
        const operation = mapPaymentOrder({ paymentOrder: resp.data, operationType, documentBaseId });
        const { Date, SettlementAccountId } = operation;
        let Number = null;

        const operationTypeInt = parseInt(operationType, 10);

        if (isOutgoingByType(operationTypeInt)) {
            const nextNumber = await getNextNumber(Date, SettlementAccountId);
            Number = nextNumber || null;
        }

        if (operationTypeInt === paymentOrderOperationResources.MemorialWarrantReceiptFromCash.value
            || operationTypeInt === paymentOrderOperationResources.WithdrawalFromAccount.value) {
            operation.CashOrder = {};
        }

        if (operationTypeInt === paymentOrderOperationResources.RentPayment.value) {
            operation.RentPeriods = [{
                Id: 0,
                Sum: 0,
                DefaultSum: ``,
                Description: ``
            }];
        }

        return {
            ...operation,
            Documents: [],
            ReserveSum: null,
            Bills: [],
            ImportRules: [],
            AdvanceStatements: [],
            ...mapContract(operation.Contract, operationType),
            DocumentBaseId: null,
            BaseDocumentId: null,
            IsReadOnly: false,
            Date: dateHelper().format(`DD.MM.YYYY`),
            Number
        };
    });
}

export function createPaymentOrder(operation) {
    const url = `${getRestMoneyPath({ operationType: operation.OperationType })}`;

    return post(url, operation);
}

export function updatePaymentOrder(operation) {
    const url = `${getRestMoneyPath({ operationType: operation.OperationType })}/${operation.DocumentBaseId}`;

    return put(url, operation);
}

export function deletePaymentOrder(operation) {
    const url = `${getRestMoneyPath({ operationType: operation.OperationType })}/${operation.DocumentBaseId}`;

    return remove(url, operation);
}

export function deleteRentPaymentOrders(ids) {
    const url = `${getRestMoneyPath({ operationType: paymentOrderOperationResources.RentPayment.value })}/DeleteListOperations`;

    return remove(url, ids);
}

/**
 * Возвращает информацию импортирована операция или нет
 * @param documentBaseId
 * @returns {Promise<boolean>}
 */
export function getIsFromImport(documentBaseId) {
    return get(`/Money/api/v1/PaymentOrders/${documentBaseId}/IsFromImport`).then(resp => resp?.data);
}

export function mergeOperation(documentBaseId) {
    const url = `/Money/api/v1/PaymentOrders/Duplicates/${documentBaseId}/Merge`;

    return post(url);
}

export function importOperation(documentBaseId) {
    const url = `/Money/api/v1/PaymentOrders/Duplicates/${documentBaseId}/Import`;

    return post(url);
}

export function setReserve({ documentBaseId, operationType }, data) {
    return post(`${getRestMoneyPath({ operationType })}/${documentBaseId}/SetReserve`, data);
}

function mapPaymentOrder({ paymentOrder, operationType, documentBaseId }) {
    let Kontragent = {
        KontragentId: null,
        KontragentName: null,
        KontragentINN: null,
        KontragentKPP: null
    };
    let KontragentId = null;
    let aquiring = {};
    let cashOrder = {};

    /* todo: выпилить со старым бэком */
    if (paymentOrder.Contractor || paymentOrder.Recipient) {
        const contractor = paymentOrder.Contractor || paymentOrder.Recipient;
        Kontragent = mapContractor(contractor, paymentOrder.ContractorType === KontragentType.Worker);
        KontragentId = Kontragent.KontragentId;
    }

    const documents = { Documents: paymentOrder?.Documents?.Data || [] };

    if (paymentOrder.CashOrder && paymentOrder.CashOrder.Data) {
        const { DocumentBaseId, Number, Date } = paymentOrder.CashOrder.Data;

        cashOrder = {
            CashOrder: {
                DocumentId: DocumentBaseId || 0,
                DocumentName: `№ ${Number} от ${dateHelper(Date, `YYYY-MM-DD`).format(`DD.MM.YYYY`)}` || null
            }
        };
    }

    if (paymentOrder.Acquiring) {
        const { CommissionSum, CommissionDate } = paymentOrder.Acquiring;
        aquiring = {
            AcquiringCommission: CommissionSum,
            AcquiringCommissionDate: dateHelper(CommissionDate, `YYYY-MM-DD`).format(`DD.MM.YYYY`)
        };
    }

    let advanceStatement = {};

    if (paymentOrder.AdvanceStatement && paymentOrder.AdvanceStatement.Data !== undefined) {
        const defaultStatement = { DocumentBaseId: null, Number: null, Date: `` };
        const item = paymentOrder.AdvanceStatement.Data || defaultStatement;
        const Name = item.Number && item.Date ?
            `Авансовый отчет №${item.Number} от ${dateHelper(item.Date, `YYYY-MM-DD`).format(`DD MMM YYYY`)}` :
            ``;

        advanceStatement = {
            AdvanceStatement: {
                DocumentBaseId: item.DocumentBaseId,
                Name
            }
        };
    }

    return {
        ...paymentOrder,
        ...aquiring,
        ...cashOrder,
        ...advanceStatement,
        ...documents,
        ...mapBills(paymentOrder.Bills),
        ...mapContract(paymentOrder.Contract, operationType),
        ...mapDocuments(paymentOrder.Documents),
        ...mapReserveSum(paymentOrder.ReserveSum),
        ...mapNds(paymentOrder.Nds),
        ...mapMediationNds(paymentOrder?.MediationNds, paymentOrder?.Mediation),
        ...mapMediation(paymentOrder.Mediation, paymentOrder.IsMediation),
        ...mapPeriod(paymentOrder.Period),
        ...mapEmployee(paymentOrder.Employee),
        ...mapAdvancedStatements(paymentOrder?.AdvanceStatements?.Data),
        CurrencyInvoices: mapCurrencyInvoicesToFrontendModel(paymentOrder?.CurrencyInvoices?.Data),
        Date: dateHelper(paymentOrder.Date, `YYYY-MM-DD`).format(`DD.MM.YYYY`),
        SaleDate: dateHelper(paymentOrder.SaleDate ?? paymentOrder.Date, `YYYY-MM-DD`).format(`DD.MM.YYYY`),
        OperationType: parseInt(operationType, 10),
        DocumentBaseId: parseInt(documentBaseId, 10),
        BaseDocumentId: parseInt(documentBaseId, 10),
        ProvideInAccounting: operationType === paymentOrderOperationResources.PaymentOrderIncomingFromAnotherAccount.value
            || paymentOrder.ProvideInAccounting, // todo: выпилить после перехода на новый бэк
        Status: paymentOrder.IsPaid ? DocumentStatusEnum.Payed : DocumentStatusEnum.NotPayed,
        DocumentDate: dateHelper(paymentOrder.DocumentDate, `YYYY-MM-DD`).isValid() ? dateHelper(paymentOrder.DocumentDate, `YYYY-MM-DD`).format(`DD.MM.YYYY`) : `0`,
        Kontragent,
        KontragentId // todo: нужно для поддержки старой схемы, после выпилить
    };
}

function mapContract(contractFromServer, operationType) {
    if (contractFromServer?.Data) {
        const {
            DocumentBaseId, Number, Date, Kind
        } = contractFromServer.Data;

        if (operationType === paymentOrderOperationResources.PaymentOrderIncomingMediationFee.value) {
            return {
                MiddlemanContract: {
                    ContractBaseId: DocumentBaseId || null,
                    DocumentBaseId,
                    ContractNumber: Number || null,
                    Date: Date || null
                }
            };
        }

        return {
            Contract: {
                ContractBaseId: DocumentBaseId || null,
                ProjectNumber: Number || null,
                ProjectKind: typeof Kind === `number` ? Kind : null,
                Date: Date || null
            }
        };
    }

    return {};
}

function mapDocuments(documents) {
    if (documents?.Data?.length > 0) {
        return {
            Documents: documents.Data.map(document => {
                return {
                    Date: dateHelper(document.Date, `YYYY-MM-DD`).format(`DD.MM.YYYY`),
                    DocumentBaseId: document.DocumentBaseId,
                    Number: document.Number,
                    PaidSum: document.PaidSum, /* Уже оплачено */
                    Sum: document.Sum, /* К оплате */
                    DocumentSum: document.DocumentSum, /* Сумма */
                    Type: document.Type,
                    DocumentType: document.Type
                };
            })
        };
    }

    return {};
}

function mapReserveSum(reserve) {
    if (reserve) {
        return {
            ReserveSum: reserve.Data
        };
    }

    return {};
}

function mapBills(bills) {
    if (bills?.Data?.length > 0) {
        return {
            Bills: bills.Data.map(bill => {
                return mapLinkedBillToClient(bill);
            })
        };
    }

    return { Bills: [] };
}

function mapNds(nds) {
    if (nds) {
        return {
            IncludeNds: nds?.IncludeNds || false,
            NdsSum: nds?.Sum || 0,
            NdsType: nds?.Type
        };
    }

    return {};
}

function mapMediationNds(mediationNds, mediation) {
    if (mediationNds && mediation.IsMediation) {
        return {
            IncludeMediationCommissionNds: mediationNds?.IncludeNds,
            MediationCommissionNdsSum: mediationNds?.Sum,
            MediationCommissionNdsType: mediationNds?.Type
        };
    }

    return {};
}

function mapMediation(mediation, isMediation) {
    return {
        MediationCommission: mediation?.CommissionSum,
        IsMediation: mediation?.IsMediation ?? isMediation
    };
}

function mapContractor(contractor, isWorker = false) {
    return {
        KontragentId: isWorker ? null : contractor.Id,
        KontragentName: contractor.Name || ``,
        KontragentINN: contractor.Inn || ``,
        KontragentKPP: contractor.Kpp || ``,
        KontragentForm: contractor.Form || ``,
        KontragentBankBIK: contractor.BankBik || ``,
        KontragentBankCorrespondentAccount: contractor.BankCorrespondentAccount || ``,
        KontragentBankName: contractor.BankName || ``,
        KontragentSettlementAccount: contractor.SettlementAccount || ``,
        KontragentOKATO: contractor.Okato,
        KontragentOKTMO: contractor.Oktmo,
        SalaryWorkerId: isWorker ? contractor.Id : null
    };
}

function mapPeriod(period) {
    if (period) {
        return {
            Period: {
                Type: period.Type,
                Date: dateHelper(period.Date, `YYYY-MM-DD`, true).format(`DD.MM.YYYY`),
                Month: period.Month,
                Quarter: period.Quarter,
                HalfYear: period.HalfYear,
                Year: period.Year
            }
        };
    }

    return {};
}

function mapEmployee(employee) {
    if (employee) {
        return {
            SalaryWorkerId: employee?.Id,
            WorkerName: employee?.Name
        };
    }

    return {};
}

function mapAdvancedStatements(advanceStatements) {
    if (advanceStatements?.length) {
        return {
            AdvanceStatements: advanceStatements.map(advancedStatement => {
                return {
                    DocumentBaseId: advancedStatement.DocumentBaseId,
                    Name: `Авансовый отчет №${advancedStatement.Number} от ${dateHelper(advancedStatement.Date, `YYYY-MM-DD`).format(`DD MMM YYYY`)}`,
                    LinkSum: advancedStatement.Sum
                };
            })
        };
    }

    return { AdvanceStatements: [] };
}

export default {
    getPaymentOrder,
    updatePaymentOrder,
    createPaymentOrder,
    getCopyPaymentOrder,
    deletePaymentOrder,
    mergeOperation,
    importOperation
};

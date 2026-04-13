import requisitesService from '@moedelo/frontend-common-v2/apps/requisites/services/requisitesService';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import userInfoService from '@moedelo/frontend-core-v2/services/userDataService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toDate } from '@moedelo/frontend-core-v2/helpers/converter';
import { getAccessRuleFlags, getAccessToPostings } from '@moedelo/frontend-common-v2/apps/finances/service/setupDataPreloadingService';
import {
    getCopyPaymentOrder,
    getPaymentOrder,
    getPaymentOrderFromContract,
    getImportRules,
    getOutsourceImportRules,
    checkHasAccessToMarketplacesAndCommissionAgents,
    getPaymentForMd
} from './newMoneyOperationService';
import {
    getDefaultBankOperationType,
    getOperationType,
    isOutgoingByType
} from '../../helpers/MoneyOperationHelper';
import { isNeedToHandleOperationType } from '../../helpers/newMoney/operationTypesHelper';
import MoneyOperationService from './moneyOperationService';

import MoneyOperationTypeResources, { paymentOrderOperationResources as operationTypes } from '../../resources/MoneyOperationTypeResources';
import MoneySourceType from '../../enums/MoneySourceType';
import OpenOperationActions from '../../enums/newMoney/OpenOperationActionsEnum';
import TypeAccessRule from '../../enums/TypeAccessRuleEnum';
import ProvidePostingType from '../../enums/ProvidePostingTypeEnum';
import accountingPostingService from '../accountingPostingService';
import { getNextNumber } from '../Bank/paymentOrderService';
import { getByBankOperationNewBackend } from '../taxPostingService';

export default {
    async loadBigOperationData(data) {
        const options = await this.handleOperationType(data);

        return this.loadBigOperationDataNewBackend(options);
    },

    async loadBigOperationDataNewBackend(options) {
        let hasPurseAccount = false;
        const DocumentBaseId = parseInt(options.DocumentBaseId, 10);
        const operationTask = this.loadOperationNewBackend(options);
        const requisitesTask = requisitesService.get();
        const userInfoTask = userInfoService.get();
        const importRulesTask = getImportRules(DocumentBaseId);
        const outsourceImportRulesTask = getOutsourceImportRules(DocumentBaseId);

        const hasAccessToMarketplacesAndCommissionAgentsTask = checkHasAccessToMarketplacesAndCommissionAgents();
        // если переходим из таблицы денег, то смысла запрашивать список счетов нет, он уже загружен, получаем оттуда(а запрос вроде жирный)
        const settlementsAccountsTask = (options.sourceList?.length ?
            Promise.resolve(options.sourceList) :
            MoneyOperationService.getSources())
            .then(list => {
                hasPurseAccount = list.some(source => source.Type === MoneySourceType.Purse);

                return list.filter(source => source.Type === MoneySourceType.SettlementAccount);
            });
        const accessRuleFlagsTask = await getAccessRuleFlags();

        await requisitesService.get().then(data => {
            this.requisites = data;
        });

        const [
            requisites,
            operation,
            settlementAccounts,
            accessRuleFlags,
            userInfo,
            importRules,
            outsourceImportRules,
            hasAccessToMarketplacesAndCommissionAgents
        ] = await
        Promise.all([
            requisitesTask,
            operationTask,
            settlementsAccountsTask,
            accessRuleFlagsTask,
            userInfoTask,
            importRulesTask,
            outsourceImportRulesTask,
            hasAccessToMarketplacesAndCommissionAgentsTask
        ]);

        operation.SettlementAccountId = operation.SettlementAccountId ? operation.SettlementAccountId : options.SettlementAccountId;

        let accountingPostings = {};
        let taxPostings = {};

        if (operation.ProvideInAccounting && options.Action !== OpenOperationActions.new && await getAccessToPostings()) {
            accountingPostings = await this.loadAccountingPostingsNewBackend(DocumentBaseId);
        }

        /** костыль для переключения со склееных типов на не склееные при редактировании */
        if (DocumentBaseId) {
            operation.Id = await MoneyOperationService.getIdByBaseId({ DocumentBaseId });
        }

        const taxationSystemTask = taxationSystemService.getTaxSystem(operation.Date);
        const patentsTask = taxationSystemService.getAllPatents();
        const activePatentsTask = taxationSystemService.getActivePatents(toDate(operation.Date));

        const [
            taxationSystem,
            patents,
            activePatents
        ] = await
        Promise.all([
            taxationSystemTask,
            patentsTask,
            activePatentsTask
        ]);

        const paymentOrderPaymentToSupplierTypes = [operationTypes.PaymentOrderPaymentToSupplier.value, operationTypes.PaymentOrderOutgoingCurrencyPaymentSuppliersForGoods.value];

        const isCopyPaymentToSupplier = options.Action === OpenOperationActions.copy
            && paymentOrderPaymentToSupplierTypes.includes(operation.OperationType);

        const isPaymentToNaturalPerson = operation.OperationType === operationTypes.PaymentOrderOutgoingForTransferSalary.value;
        const isIpOsno = taxationSystem.IsOsno && !requisites.IsOoo;

        if ((!isPaymentToNaturalPerson || (isPaymentToNaturalPerson && isIpOsno)) && !isCopyPaymentToSupplier && options.Action !== OpenOperationActions.new) {
            taxPostings = await this.loadTaxPostingsNewBackend({
                taxationSystem,
                baseId: DocumentBaseId,
                isOoo: requisites.IsOoo
            });
        }

        /** кейс: открываем перевод со счета, меняем тип на любой другой, а обратно на перевод со счета переключиться не можем.
         * сохраняем состояние, что изначально открыли перевод со счета и в дальнейшем фильтруем список типов операций на основе этого флага */
        const isInitialTransferFromAnotherAccount = operation.OperationType === operationTypes.PaymentOrderIncomingFromAnotherAccount.value;

        /* Sum нужно для запроса БУ в выплатах физ лицам, т.к. новая модель не имеет такого поля */
        if (isPaymentToNaturalPerson && operation.EmployeePayments) {
            Object.assign(operation, { Sum: countEmployeePaymentsSum(operation.EmployeePayments) });
        }

        /* todo: избавиться при полном переводе денег на новый backend */
        operation.Direction = getOperationType(operation.OperationType).Direction;

        if (isOutgoingByType(operation.OperationType) && options.Action !== OpenOperationActions.edit) {
            const nextNumber = await getNextNumber(operation.Date, operation.SettlementAccountId);
            operation.Number = nextNumber || null;
        }

        if (operation.TaxPostingsInManualMode
            && taxPostings.TaxPostings
            && taxPostings.TaxPostings.Postings
            && !taxPostings.TaxPostings.Postings.length) {
            taxPostings.TaxPostings.Postings.push({});
        }

        return {
            hasPurseAccount,
            settlementAccounts: filterArchiveSettlements({ accountsList: settlementAccounts, operation }),
            requisites,
            operation: {
                isInitialTransferFromAnotherAccount,
                ...operation,
                ...taxPostings,
                ...accountingPostings,
                ...this.canCopyAndEdit(operation, true),
                ...this.canProvidePostings(operation),
                CanEditCurrencyOperations: accessRuleFlags?.HasAccessToEditCurrencyOperations,
                hasAccessToMarketplacesAndCommissionAgents,
                ImportRules: options.Action === OpenOperationActions.edit ? importRules : [],
                OutsourceImportRules: options.Action === OpenOperationActions.edit ? outsourceImportRules : null,
                Action: options.Action,
                /** При смене типа при сохранении логируем старый тип операции */
                oldOperationType: operation.OperationType
            },
            taxationSystem,
            patents,
            activePatents,
            userInfo
        };
    },

    loadOperationNewBackend(options) {
        const {
            DocumentBaseId,
            OperationType,
            ContractId,
            Action,
            BillNumber
        } = options;
        const operationType = parseInt(OperationType, 10);
        const defaultData = {
            OperationType: getDefaultBankOperationType(options),
            Date: dateHelper().format(`DD.MM.YYYY`)
        };

        if (!DocumentBaseId && !ContractId && Action === OpenOperationActions.new) {
            return defaultData;
        }

        if (ContractId) {
            defaultData.ContractId = ContractId;

            return getPaymentOrderFromContract(defaultData);
        }

        if (Action === OpenOperationActions.newPaymentForMd) {
            return getPaymentForMd({ defaultData, billNumber: BillNumber });
        }

        if (Action === OpenOperationActions.copy && DocumentBaseId) {
            return getCopyPaymentOrder({ documentBaseId: DocumentBaseId, operationType });
        }

        return getPaymentOrder({ documentBaseId: DocumentBaseId, operationType });
    },

    async loadTaxPostingsNewBackend({ baseId, taxationSystem, isOoo }) {
        return {
            TaxPostings: await getByBankOperationNewBackend({ baseId, taxationSystem, isOoo })
        };
    },

    async loadAccountingPostingsNewBackend(documentBaseId) {
        return {
            AccountingPostings: await accountingPostingService.getByBankOperationNewBackend(documentBaseId)
        };
    },

    canCopyAndEdit(operation, isNewBackend = false) {
        // ToDo: Переписать на отдельный запрос или получать вместе с моделью операции
        const {
            DocumentBaseId, BaseDocumentId, IsReadOnly, CanEdit, Date, OperationType
        } = operation;
        const isBudgetaryPayment2022 = OperationType === MoneyOperationTypeResources.BudgetaryPayment.value && dateHelper(Date).isBefore(`01.01.2023`);
        const id = DocumentBaseId || BaseDocumentId;
        const accessToBank = window.UserAccessManager.AccessRule.AccessToBank;

        return {
            CanCopy: (id > 0 && accessToBank === TypeAccessRule.AccessEdit) && !isBudgetaryPayment2022,
            CanEdit: isNewBackend
                ? !IsReadOnly && accessToBank === TypeAccessRule.AccessEdit
                : CanEdit && accessToBank === TypeAccessRule.AccessEdit,
            CanEditReserve: accessToBank === TypeAccessRule.AccessEdit
                && !IsReadOnly // "Документ проведен бухгалтером и недоступен для редактирования"
        };
    },

    canProvidePostings(operation) {
        // ToDo: Переписать на отдельный запрос или получать вместе с моделью операции
        const canViewPosting = window._preloading.CanViewPosting;
        const id = operation.DocumentBaseId || operation.BaseDocumentId;

        if (!canViewPosting) {
            return {
                ProvideInAccounting: false,
                PostingsAndTaxMode: ProvidePostingType.ByHand,
                CanViewPostings: false
            };
        }

        return {
            ProvideInAccounting: id > 0 ? operation.ProvideInAccounting : true,
            CanViewPostings: true
        };
    },

    async handleOperationType(options) {
        if (!isNeedToHandleOperationType(options)) {
            return options;
        }

        const OperationType = await MoneyOperationService.getTypeByBaseId({ documentBaseId: options.DocumentBaseId });

        return { ...options, OperationType };
    }
};

function filterArchiveSettlements(data) {
    const { accountsList = [], operation = {} } = data;
    const currentSettlementIds = [
        operation.SettlementAccountId,
        operation.TransferSettlementaccountId,
        operation.TransferSettlementAccountId,
        operation.ToSettlementAccountId,
        operation.FromSettlementAccountId
    ];

    return accountsList.filter(account => account.IsActive || currentSettlementIds.includes(account.Id));
}

function countEmployeePaymentsSum(employeePayments = []) {
    const employeePaymentsLength = employeePayments.length;
    let sum = 0;
    let i = 0;

    for (i; i < employeePaymentsLength; i += 1) {
        if (employeePayments[i]?.ChargePayments?.length) {
            sum += employeePayments[i].ChargePayments.reduce((acc, { Sum = 0 }) => {
                return acc + Sum;
            }, 0);
        }
    }

    return sum;
}

/* global Backbone, _, Converter, Money, Common, $ */
/* eslint-disable */

import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import AccountingDocumentType from '@moedelo/frontend-enums/mdEnums/AccountingDocumentType';
import { get } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import {
    paymentOrderOperationResources,
    cashOrderOperationResources,
    purseOperationResources
} from '../../../../../../resources/MoneyOperationTypeResources';
import ProvidePostingType from '../../../../../../enums/ProvidePostingTypeEnum';
import SyntheticAccountCodesEnum from '../../../../../../enums/SyntheticAccountCodesEnum';
import { isUnifiedBP } from '../../../CashApp/scripts/views/documents/operations/CashBudgetaryPayment/helpers/checkHelper';

(function(common) {
    common.Collections.PostingsAndTaxOperationsCollection = Backbone.Collection.extend({
        sourceDocumentPostingsField: null,

        initialize() {
            _.extend(this, common.Mixin.PostingAndTaxListenMixin);
            this.temporaryValues = {};
            this.isOoo = new Common.FirmRequisites().get('IsOoo');
        },

        startListen() {
            this.sourceDocument.get('action') === 'copy'
                || this.sourceDocument.get('OperationType') === purseOperationResources.PurseOperationTransferToSettlement.value
                    ? this.generatePostings()
                    : this.loadData();
            this.listenSource();
        },

        loading: false,

        onlyOneManualPosting: false,

        model: common.Models.PostingsAndTaxOperationModel,

        PostingsCollection: Backbone.Collection,

        enableLoadingState() {
            if (!this.loading) {
                this.loading = true;
                $('#saveButton, .saveDocumentButton > .mdButton').attr('disabled', true);
            }
        },

        disableLoadingState() {
            if (this.loading) {
                this.loading = false;
                $('#saveButton, .saveDocumentButton > .mdButton').removeAttr('disabled');
            }
        },

        loadData(forceLoad) {
            const collection = this;
            const data = collection.getSourceData();

            if (isUnifiedBP(data)) {
                return;
            }

            if (!collection.checkSourceData(forceLoad)) {
                collection.trigger('checkSourceFailed');
                return;
            }

            collection?.enableLoadingState();
            collection.trigger('modelStartLoad');

            const throttledFetch = _.throttle(() => {
                get(collection.getLoadUrl())
                    .then((resp = {}) => {
                        collection.reset(this.parse(resp.data));
                        this.onLoadPostings(collection, resp.data);
                    });
            }, 500);

            throttledFetch();
        },

        generatePostings(forceLoad) {
            const collection = this;

            const data = collection.getSourceData();

            if (isUnifiedBP(data)) {
                return;
            }

            if (data.NdsType === 99) {
                data.NdsType = null;
            }

            if (!collection.checkSourceData(forceLoad)) {
                collection.trigger('checkSourceFailed');
                return;
            }

            !this.isOoo && delete data.Postings;

            collection?.enableLoadingState();
            collection.trigger('modelStartLoad');

            const throttledFetch = _.throttle(() => {
                collection.fetch({
                    success: onGeneratePostings,
                    error: onErrorGeneratePostings,
                    data: JSON.stringify(data),
                    contentType: 'application/json',
                    url: collection.getGenerateUrl(),
                    type: 'POST'
                });
            }, 500);

            throttledFetch();
        },

        parse(response) {
            let operations = [];

            if (response) {
                operations = response.Operations;
            }

            return _.map(operations, this.mapOperation, this);
        },

        localDataApply(cid) {
            if (!this.checkSourceData()) {
                this.trigger('checkSourceFailed');
                return;
            }

            if (!this.find((model) => { return model.get('Cid') === cid; })) {
                const operationPrototype = {
                    Cid: cid,
                    IsManualEdit: true,
                    LinkedDocuments: [],
                    OperationName: 'Прочие поступления'
                };
                const trueOperation = this.mapOperation(operationPrototype);
                this.add(trueOperation);
            }

            this.updatePostingsInSourceDocument();
            this.trigger('ModelLoaded');
        },

        mapOperation(operation) {
            let mainPostings = [];
            let manualPostings = [];

            this.postingsPreProcessing && this.postingsPreProcessing(operation);

            this.manipulateManualEdit(operation);

            const postings = _.map(operation.Postings, (posting) => {
                if (!posting.Date) {
                    posting = _.omit(posting, 'Date');
                }

                if (!posting.PostingDate) {
                    posting = _.omit(posting, 'PostingDate');
                }

                return posting;
            });

            if (operation.IsManualEdit) {
                manualPostings = postings;
            } else {
                mainPostings = postings;
            }

            const postingCollection = this.PostingsCollection;
            operation.MainPostings = new postingCollection(mainPostings);
            operation.ManualPostings = new postingCollection(manualPostings);

            this.customTypeRules && this.customTypeRules(operation);

            operation = _.omit(operation, 'Postings');

            return operation;
        },

        sync(method, collection, options) {
            if (collection.currentRequest) {
                collection.currentRequest.abort();
            }

            const xhr = Backbone.Collection.prototype.sync.call(this, method, collection, options);
            collection.currentRequest = xhr;
            return xhr;
        },

        checkSourceData() {
            return true;
        },

        getSourceData() {
            const data = this.sourceDocument.toJSON();
            _.each(data, (val, attr) => {
                if (val && typeof val === 'object' && val.toJSON) {
                    data[attr] = val.toJSON();
                }
            });
            return data;
        },

        listenSource() {
        },

        manipulateManualEdit(operation) {
            const isOoo = this.isOoo;
            const isOsno = this.getTaxationSystem().get(`IsOsno`);
            const isIpOsno = isOsno && !isOoo;

            const operationsWithDisableManual = [
                cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value,
                paymentOrderOperationResources.PaymentToAccountablePerson.value,
                cashOrderOperationResources.CashOrderOutgoingIssuanceAccountablePerson.value
            ];

            const isOperationWithDisableManual = operationsWithDisableManual.includes(this.sourceDocument.get('OperationType'));
            const isOnlyAutoPostings = this.onlyPostingsAndTaxMode === common.Data.ProvidePostingType.Auto;

            if (isOnlyAutoPostings || (isIpOsno && isOperationWithDisableManual)) {
                operation.IsManualEdit = false;
            }
        },

        updatePostingsInSourceDocument() {
            let self = this,
                operations = this.sourceDocument.get('Operations');

            if (operations && operations.length) {
                self.each((operation, key, list) => {
                    let operatCid = operation.get('Cid'),
                        sourceOperation;

                    if (operations.getByCid) {
                        sourceOperation = operations.getByCid(operatCid);
                    } else {
                        sourceOperation = operations.get(operatCid);
                    }

                    self.customHandling && self.customHandling(operation, sourceOperation);
                    self.setPostingsToDocumentOperation(sourceOperation, operation);
                });
            } else {
                self.customDirectionHandler && self.customDirectionHandler(self.at(0));
                self.setPostingsToDocumentOperation(this.sourceDocument, self.at(0));
            }
        },

        setPostingsToDocumentOperation(sourceOperation, operation) {
            if (!this.sourceDocumentPostingsField) {
                throw 'PostingsAndTaxOperationsCollection: sourceDocumentPostingsField is not defined';
            }
            if (sourceOperation && operation) {
                const provideType = parseInt(this.sourceDocument.get('PostingsAndTaxMode'), 10);

                if (provideType === common.Data.ProvidePostingType.Auto) {
                    sourceOperation.unset(this.sourceDocumentPostingsField);
                }

                sourceOperation.set(this.sourceDocumentPostingsField, operation.get('ManualPostings'));
            }
        },

        getDocumentSpecialProperties() {
            return null;
        },

        isOsno() {
            const docTaxSystem = parseInt(this.sourceDocument.get('TaxationSystemType'), 10);
            if (docTaxSystem) {
                return docTaxSystem === common.Data.TaxationSystemType.Osno;
            }

            const taxSystem = this.getTaxationSystem();
            return taxSystem ? taxSystem.get('IsOsno') : false;
        },

        isOsnoMode() {
            // /<summary>Нужен для проверки при выборе шаблона и коллекции. Не зависит от выбранного в документе типа</summary>
            const taxSystem = this.getTaxationSystem();
            return taxSystem ? taxSystem.get('IsOsno') : false;
        },

        isEnvd() {
            const docTaxSystem = parseInt(this.sourceDocument.get('TaxationSystemType'), 10);
            if (docTaxSystem) {
                return docTaxSystem === common.Data.TaxationSystemType.Envd;
            }

            const taxSystem = this.getTaxationSystem();
            return taxSystem ? taxSystem.get('IsEnvd') : false;
        },

        isUsn() {
            const docTaxSystem = parseInt(this.sourceDocument.get('TaxationSystemType'), 10);
            if (docTaxSystem) {
                return docTaxSystem === common.Data.TaxationSystemType.Usn;
            }

            const taxSystem = this.getTaxationSystem();
            return taxSystem ? taxSystem.get('IsUsn') : false;
        },

        isPatent() {
            const docTaxSystem = parseInt(this.sourceDocument.get('TaxationSystemType'), 10);

            return docTaxSystem === common.Data.TaxationSystemType.Patent;
        },

        isUsnAndEnvd() {
            const docTaxSystem = parseInt(this.sourceDocument.get('TaxationSystemType'), 10);
            if (docTaxSystem) {
                return docTaxSystem === common.Data.TaxationSystemType.UsnAndEnvd;
            }

            const taxSystem = this.getTaxationSystem();
            return taxSystem ? taxSystem.get('IsUsn') && taxSystem.get('IsEnvd') : false;
        },

        isUsn6() {
            if (this.isUsn()) {
                const taxSystem = this.getTaxationSystem();

                return parseInt(taxSystem.get('UsnType'), 10) === common.Data.UsnTypes.Profit;
            }

            return false;
        },

        isUsn15() {
            if (this.isUsn()) {
                const taxSystem = this.getTaxationSystem();

                return parseInt(taxSystem.get('UsnType'), 10) === common.Data.UsnTypes.ProfitAndOutgo;
            }

            return false;
        },

        isUsnAndEnvd15() {
            if (this.isUsnAndEnvd()) {
                const usnSize15 = 15;
                const taxSystem = this.getTaxationSystem();

                return parseInt(taxSystem.get('UsnType'), 10) === common.Data.UsnTypes.ProfitAndOutgo && parseInt(taxSystem.get('UsnSize'), 10) === usnSize15;
            }

            return false;
        },


        getTaxationSystem() {
            const date = Converter.toDate(this.sourceDocument.get('Date'));

            const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            return ts.Current(date);
        },

        isValid() {
            let isValid = !this.any(isOperationInvalid);

            if (isValid) {
                if (this.operationValidation) {
                    const operationValidation = this.operationValidation();
                    if (operationValidation && !operationValidation.valid) {
                        isValid = this.valid;
                    }
                }

                if (this.validation && (_.isBoolean(isValid) || !isValid.options.operations || !isValid.options.operations.length)) {
                    const collectionValidation = this.validation();
                    if (collectionValidation && !collectionValidation.valid) {
                        isValid = this.valid;
                    }
                }
            }
            return isValid;
        },

        // Добавляем или убираем сообщения о необходимых полях и т.п
        customMessagesHandler(operation, sourceOperation) {
            if (!sourceOperation || !operation || !this.operationsExplainigObjects) {
                return;
            }
            const operationName = this.names.Operation;
            const type = (operationName) ? sourceOperation.get(operationName) : sourceOperation.get('Type');
            const cid = sourceOperation.cid;
            const explainigObjects = this.operationsExplainigObjects(type, cid);
            explainigObjects ? operation.set(explainigObjects) : operation.unset('ExplainingMessage');
        },

        enteredValueKeeper(operation) {
            if (!operation) {
                return;
            }

            const manualPostings = operation.get('ManualPostings');

            if (operation.get('ExplainingMessage')) {
                this.temporaryValues.manualPostings = manualPostings.models;
                manualPostings.reset();
            } else if (this.temporaryValues.manualPostings) {
                manualPostings.reset(this.temporaryValues.manualPostings);
            }
        }
    });

    const parent = common.Collections.PostingsAndTaxOperationsCollection;

    common.Collections.BuhOperationCollection = parent.extend({
        initialize() {
            parent.prototype.initialize.call(this);
            _.extend(this, common.Mixin.TaxCollectionSupportMixin);
        },

        PostingsCollection: common.Collections.PostingsCollection,
        sourceDocumentPostingsField: 'Postings',

        explainingMessage() {
            const messageWithAnchor = this.setExplainMessageWithAnchor();

            this.notTaxable = false;

            if (messageWithAnchor) {
                this.notTaxable = true;
                return messageWithAnchor;
            }

            if (this.loading) {
                return 'Подождите, идет загрузка…';
            }
        },

        isProvided() {
            return !!this.checkSourceData();
        },

        checkFillCollection() {
            return this.any((item) => {
                return item.get('ManualPostings').length || item.get('MainPostings').length;
            });
        },

        customHandling(operation, sourceOperation) {
            this.customMessagesHandler(operation, sourceOperation);
            this.enteredValueKeeper(operation, sourceOperation);
        },

        isTaxPostings() {
            return false;
        },

        /** AccPostings */
        onLoadPostings(collection, response) {
            collection?.disableLoadingState();

            let postings = response.Operations[0].Postings;

            const documents = this.sourceDocument.get('Documents').filter(item => {
                return (item.DocumentType === AccountingDocumentType.Statement
                    || item.DocumentType === AccountingDocumentType.Waybill
                    || item.DocumentType === AccountingDocumentType.Upd) && item.Sum > 0;
            });

            let operationType = this.sourceDocument.get('OperationType') || this.sourceDocument.get('PurseOperationType');
            operationType = parseInt(operationType, 10);
            const provideInAccounting = this.sourceDocument.get('ProvideInAccounting');
            const postingsAndTaxMode = this.sourceDocument.get('PostingsAndTaxMode');
            let countExpectedPostings = documents.length + 1;
            const loanInterestSum = Converter.toFloat(this.sourceDocument.get('LoanInterestSum'));
            const acquiringCommission = Converter.toFloat(this.sourceDocument.get('AcquiringCommission'));
            const sum = Converter.toFloat(this.sourceDocument.get('Sum'));
            const paidCardSum = Converter.toFloat(this.sourceDocument.get('PaidCardSum'));
            const ndsSum = Converter.toFloat(this.sourceDocument.get('NdsSum'));

            switch (operationType) {
                case paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value:
                case cashOrderOperationResources.CashOrderIncomingPaymentForGoods.value:
                case paymentOrderOperationResources.PaymentOrderPaymentToSupplier.value:
                case cashOrderOperationResources.CashOrderOutgoingPaymentSuppliersForGoods.value: {
                    const kontragentAccountCode = this.sourceDocument.get('KontragentAccountCode');
                    const { _76_06, _76_05, _60_02 } = SyntheticAccountCodesEnum;

                    if ([_76_06, _76_05, _60_02].includes(kontragentAccountCode)) {
                        countExpectedPostings = 1;
                    }
                    break;
                }
                case paymentOrderOperationResources.PaymentOrderIncomingFromAnotherAccount.value: {
                    countExpectedPostings = response.Operations[0].LinkedDocuments;
                    postings = postings.length === 0 ? response.Operations[0].LinkedDocuments : postings;
                    break;
                }
                case paymentOrderOperationResources.PaymentOrderOutgoingLoanRepayment.value:
                case cashOrderOperationResources.CashOrderOutgoingLoanRepayment.value: {
                    countExpectedPostings = loanInterestSum > 0 && sum > loanInterestSum ? countExpectedPostings + 1 : countExpectedPostings;
                    break;
                }
                case paymentOrderOperationResources.MemorialWarrantReceiptGoodsPaidCreditCard.value: {
                    countExpectedPostings = acquiringCommission > 0 && sum > 0 ? countExpectedPostings + 1 : countExpectedPostings;
                    break;
                }
                case cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.value: {
                    if (this.isEnvd() || this.isPatent()) {
                        countExpectedPostings = 1;
                    } else {
                        countExpectedPostings = ndsSum > 0 ? countExpectedPostings + 1 : countExpectedPostings;
                    }

                    break;
                }
                case paymentOrderOperationResources.PaymentOrderOutgoingForTransferSalary.value:
                case cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value: {
                    const workerCharges = this.sourceDocument.get('WorkerCharges') || this.sourceDocument.get('PayToWorkers');
                    countExpectedPostings = workerCharges.length;
                    break;
                }
                case paymentOrderOperationResources.BudgetaryPayment.value:
                case cashOrderOperationResources.CashOrderBudgetaryPayment.value: {
                    return this.generatePostings(true);
                }
                case paymentOrderOperationResources.PaymentOrderIncomingMediationFee.value:
                case cashOrderOperationResources.CashOrderIncomingMediationFee.value: {
                    countExpectedPostings = 1;
                    break;
                }
                case cashOrderOperationResources.CashOrderIncomingMiddlemanRetailRevenue.value: {
                    countExpectedPostings = paidCardSum > 0 && sum !== paidCardSum ? countExpectedPostings + 1 : countExpectedPostings;
                    break;
                }
                case cashOrderOperationResources.CashOrderIncomingFromAnotherCash.value: {
                    postings = postings.length === 0 ? response.Operations[0].LinkedDocuments : postings;
                    break;
                }
                case purseOperationResources.PurseOperationOtherOutgoing.purseOperationType:
                case purseOperationResources.PurseOperationOtherOutgoing.value: {
                    countExpectedPostings = this.isOoo ? 1 : 0;
                    break;
                }
            }

            if (provideInAccounting && countExpectedPostings > postings.length && postingsAndTaxMode !== ProvidePostingType.ByHand) {
                NotificationManager.show({
                    title: 'Ошибка!',
                    message: 'Во время предыдущего сохранения операции произошла ошибка.<br>Для устранения проблем пересохраните операцию или обратитесь в техническую поддержку.',
                    type: 'error',
                    duration: 15000
                });

                this.generatePostings(true);
            }

            onGeneratePostings(collection, response);
        }
    });

    common.Collections.TaxOperationCollection = parent.extend({

        PostingsCollection: common.Collections.TaxCollection,

        sourceDocumentPostingsField: 'TaxPostings',

        initialize() {
            parent.prototype.initialize.call(this);

            _.extend(this, common.Mixin.TaxCollectionSupportMixin);
            _.extend(this, common.Mixin.AddCustomCollectionValidation);
        },

        validator: {
            moreThanDocumentSum: common.Mixin.FunctionForPostingsAndTaxValidation.moreThanDocumentSum
        },

        validationRules: {
            SumValidation: {
                moreThanDocumentSum: { msg: 'Сумма проводок не может быть больше общей суммы документа' }
            }
        },

        startListen() {
            if (this.isOsnoMode()) {
                this.PostingsCollection = common.Collections.OsnoTaxCollection;
            } else {
                this.PostingsCollection = common.Collections.TaxCollection;
            }
            this.taxationSystem = this.getTaxationSystem();

            parent.prototype.startListen.call(this);
        },

        postingsPreProcessing(operation) {
            this.directionPreProcessing(operation.Postings);
            this.searchForEmptyOperations(operation);
        },

        searchForEmptyOperations(operation) {
            if (!(operation.LinkedDocuments && operation.LinkedDocuments.length)
                && !(operation.Postings && operation.Postings.length)) {
                operation.emptyOperation = true;
            }
        },

        directionPreProcessing(postings) {
            if (!(postings && postings.length)) {
                return;
            }
            _.each(postings, function(obj) {
                if (!obj.Direction && _.result(this, 'paymentDirection')) {
                    obj.Direction = 0;
                }
            }, this);
        },

        customTypeRules(operation) {
            if (this.sourceDocument.get('PostingsAndTaxMode') === common.Data.ProvidePostingType.ByHand) {
                operation.ManualPostings = new this.PostingsCollection(operation.Postings);
                operation.MainPostings = new this.PostingsCollection();
            }
        },

        customHandling(operation, sourceOperation) {
            this.customMessagesHandler(operation, sourceOperation);
            this.enteredValueKeeper(operation, sourceOperation);
            this.customDirectionHandler(operation, sourceOperation);
        },

        customDirectionHandler(operation, sourceOperation) {
            if (!operation) {
                return;
            }
            let direction;
            let docDirection;

            if (arguments.length === 1) {
                direction = _.result(this, 'paymentDirection');
            } else if (arguments.length === 2) {
                const directionName = this.names.Direction;

                if (sourceOperation) {
                    docDirection = sourceOperation.get(directionName) || this.sourceDocument.get(directionName);
                } else {
                    docDirection = this.sourceDocument.get(directionName);
                }

                if (docDirection === Direction.Incoming) {
                    direction = Common.Data.TaxPostingsDirection.Incoming;
                } else {
                    direction = Common.Data.TaxPostingsDirection.Outgoing;
                }
            }

            if (direction) {
                operation.set('Direction', direction);
                operation.get('ManualPostings').each((model) => {
                    model.set('Direction', direction);
                });
            }
        },

        explainingMessage() {
            const messageWithAnchor = this.setExplainMessageWithAnchor();

            this.notTaxable = false;
            if (this.isEnvd() && !(this.isUsn() || this.isOsno())) {
                this.notTaxable = true;
                return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxableEnvd;
            }

            if (messageWithAnchor) {
                this.notTaxable = true;
                return messageWithAnchor;
            }

            if (this.loading) {
                return 'Подождите, идет загрузка…';
            }
        },

        checkFillCollection() {
            let result = false;
            this.each((item) => {
                if (!result && (item.get('ManualPostings').length || item.get('MainPostings').length)) {
                    result = true;
                }
            });

            return result;
        },

        isTaxPostings() {
            return true;
        },

        generatePostings(forceLoad) {
            const collection = this;
            const data = collection.getSourceData();

            if (isUnifiedBP(data)) {
                return;
            }

            if (!collection.checkSourceData(forceLoad)) {
                collection.trigger('checkSourceFailed');
                return;
            }

           if (data.NdsType === 99) {
                data.NdsType = null;
            }

            collection?.enableLoadingState();
            collection.trigger('modelStartLoad');

            const throttledFetch = _.throttle(() => {
                collection.fetch({
                    success: onGeneratePostings,
                    data: JSON.stringify(data),
                    contentType: 'application/json',
                    url: collection.getGenerateUrl(),
                    type: 'POST'
                });
            }, 500);

            throttledFetch();
        },

        needToShowNotification(operationType) { // temporary for TS-52971
            return operationType !== paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value;
        },

        /** TaxPostings */
        onLoadPostings(collection, response) {
            collection?.disableLoadingState();

            let postingsCount = parseInt(response.Operations[0].Postings.length, 10) + parseInt(response.Operations[0].LinkedDocuments.length, 10);
            let operationType = this.sourceDocument.get('OperationType') || this.sourceDocument.get('PurseOperationType');
            operationType = parseInt(operationType, 10);
            const provideInAccounting = this.sourceDocument.get('ProvideInAccounting');
            const postingsAndTaxMode = this.sourceDocument.get('PostingsAndTaxMode');
            const sum = this.sourceDocument.get('Sum');
            const paidCardSum = this.sourceDocument.get('PaidCardSum');
            const isOoo = this.isOoo;
            const isOsno = this.taxationSystem.get(`IsOsno`);
            const isIpOsno = isOsno && !isOoo;
            const isOooOsno = isOoo && isOsno;
            const year = dateHelper(this.sourceDocument.get('Date')).year();
            let countExpectedPostings = 1;
            let documentsSum = 0;

            let documents = this.sourceDocument.get('Documents').filter(doc => {
                return (doc.DocumentType === AccountingDocumentType.Statement && !doc.IsCompensated)
                    || doc.DocumentType === AccountingDocumentType.Waybill
                    || doc.DocumentType === AccountingDocumentType.InventoryCard;
            });

            if (documents.length) {
                documentsSum = documents.reduce((total, item) => {
                    return total + item.Sum;
                }, 0);

                countExpectedPostings = sum > documentsSum ? countExpectedPostings + documents.length : countExpectedPostings;
            }

            switch (operationType) {
                case paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value:
                case cashOrderOperationResources.CashOrderIncomingPaymentForGoods.value: {
                    if (isIpOsno) {
                        countExpectedPostings = 1;
                        break;
                    }

                    if (this.sourceDocument.get('IsMediation')) {
                        countExpectedPostings = this.sourceDocument.get('MediationCommission') ? 1 : 0;
                        break;
                    }

                    countExpectedPostings = sum > documentsSum ? countExpectedPostings : documents.length;
                    break;
                }
                case paymentOrderOperationResources.PaymentOrderIncomingMaterialAid.value:
                case cashOrderOperationResources.CashOrderIncomingMaterialAid.value: {
                    const kontragentAuthorizedCapitalShare = this.sourceDocument.get('KontragentAuthorizedCapitalShare');

                    countExpectedPostings = kontragentAuthorizedCapitalShare > 0.5 ? 0 : 1;

                    if (kontragentAuthorizedCapitalShare === 0) {
                        countExpectedPostings = 0;
                    }

                    break;
                }
                case paymentOrderOperationResources.BudgetaryPayment.value:
                case cashOrderOperationResources.CashOrderBudgetaryPayment.value: {
                    return this.generatePostings(true);
                }
                case paymentOrderOperationResources.PaymentOrderIncomingFromAnotherAccount.value:
                case paymentOrderOperationResources.PaymentOrderOutgoingTransferToAccount.value: {
                    countExpectedPostings = 0;
                    break;
                }
                case paymentOrderOperationResources.PaymentOrderIncomingMediationFee.value:
                case cashOrderOperationResources.CashOrderIncomingMediationFee.value: {
                    countExpectedPostings = sum > documentsSum ? 1 : 0;
                    break;
                }
                case paymentOrderOperationResources.PaymentOrderPaymentToSupplier.value:
                case cashOrderOperationResources.CashOrderOutgoingPaymentSuppliersForGoods.value: {
                    if (isIpOsno) {
                        countExpectedPostings = 0;
                    } else {
                        documents = documents.filter(item => {
                            return item.DocumentType !== AccountingDocumentType.Waybill || (item.DocumentType === AccountingDocumentType.Waybill && item.HasMaterial);
                        });
                        countExpectedPostings = documents.length;
                    }
                    break;
                }
                case paymentOrderOperationResources.PaymentToAccountablePerson.value:
                case cashOrderOperationResources.CashOrderOutgoingIssuanceAccountablePerson.value: {
                    countExpectedPostings = 0;
                    break;
                }
                case paymentOrderOperationResources.PaymentOrderIncomingOther.value:
                case paymentOrderOperationResources.PaymentOrderOutgoingOther.value:
                case cashOrderOperationResources.CashOrderIncomingOther.value:
                case cashOrderOperationResources.CashOrderOutgoingOther.value:
                case purseOperationResources.PurseOperationOtherOutgoing.purseOperationType:
                case purseOperationResources.PurseOperationTransferToSettlement.value:
                case cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value:
                case purseOperationResources.PurseOperationIncome.purseOperationType:
                case purseOperationResources.PurseOperationOtherOutgoing.value: {                    
                    countExpectedPostings = 0;
                    break;
                }
                case cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.value: {
                    countExpectedPostings = sum > paidCardSum ? 1 : 0;
                    break;
                }
                case purseOperationResources.PurseOperationComission.value: {
                    countExpectedPostings = year >=2026 && isOooOsno ? 0 : 1;
                    break;
                }
            }

            if ((provideInAccounting && countExpectedPostings > postingsCount && postingsAndTaxMode !== ProvidePostingType.ByHand) && this.needToShowNotification(operationType)) {
                NotificationManager.show({
                    title: 'Ошибка!',
                    message: 'Во время предыдущего сохранения операции произошла ошибка.<br>Для устранения проблем пересохраните операцию или обратитесь в техническую поддержку.',
                    type: 'error',
                    duration: 15000
                });

                this.generatePostings(true);
            }

            onGeneratePostings(collection, response);
        }
    });

    function isOperationInvalid(operation) {
        return operation.get('ManualPostings').validation && !operation.get('ManualPostings').validation();
    }

    function onGeneratePostings(collection, response) {
        collection?.disableLoadingState();
        collection.updatePostingsInSourceDocument();
        collection.firstLoadComplete = true;

        if (response) {
            collection.ExplainingMessage = response.ExplainingMessage;
            collection.ServerMessage = response.Message;
        }

        collection.trigger('ModelLoaded');
    }

    function onErrorGeneratePostings(collection, response) {
        if (response.statusText === 'abort') {
            return;
        }

        collection?.disableLoadingState();

        collection.trigger('ErrorLoaded');
    }
}(Common));

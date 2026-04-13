/* eslint-disable */
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

(function(components, common) {
    components.LinkedDocumentsControl = Backbone.View.extend({
        template: 'LinkedDocumentsTableTemplate',

        events: {
            'click #addWaybill': 'addWaybill',
            'click #addStatement': 'addStatement',
            'click #addInventoryCard': 'addInventoryCard',
            'click #addMiddlemanReport': 'addMiddlemanReport',
            'click #addRetailReport': 'addRetailReport',
            'click #addUpd': 'addUpd',
            'click #addReceiptStatement': 'addReceiptStatement'
        },

        initialize(options) {
            this.options = options;
            this.collection = new components.LinkedDocumentsCollection(this.model.get('Documents'));

            this.collection.on('add', this.addRow, this);
            this.collection.on('add', this.updateCollectionData, this);
            this.collection.on('remove', this.removeRow, this);
            this.collection.on('add remove reset', this.hideForNothing, this);
            this.collection.on('remove add reset change', this.updateSourceModel, this);
            this.collection.on('remove reset change:Sum', this.updateSum, this);
            this.collection.on('remove reset change:Sum', this.toggleAddDocLink, this);
            this.collection.on('remove add change:Sum', this.setCustomEdit, this);

            this.updateCollectionData();

            this.model.on('change:KontragentId', this.onChangeKontragent, this);
            this.model.on('change:DocumentsSum', this.showResult, this);
        },

        render() {
            const template = TemplateManager.getFromPage(this.template);
            this.$el.html(template);

            this.rowTemplate = this.$('tbody tr');
            this.$('tbody').empty();

            this.bind();
            this.createPartials();
            this.hideForNothing();
            this.updateSum();
            this.showResult();

            return this;
        },

        createPartials() {
            this.rows = {};
            this.collection.each(this.addRow, this);
        },

        addRow(item) {
            const model = this.model;

            const defaults = {
                getDocumentSum: this.getDocumentSum.bind(this)
            };

            const options = _.extend(defaults, this.options, {
                el: this.rowTemplate.clone(),
                model: item,
                operationData() {
                    return {
                        KontragentIds: _.extend({}, model.get('KontragentIds')),
                        ContractBaseId: model.get('ContractBaseId')
                    };
                }
            });

            const rowView = new components.LinkedDocumentRow(options).render();
            this.$('tbody').append(rowView.$el);

            this.rows[item.cid] = rowView;
        },

        getDocumentSum(doc) {
            let unpaidBalance = doc.UnpaidBalance;
            const mainDocChannel = Backbone.Wreqr.radio.channel('document');
            const mainDocSum = mainDocChannel.reqres.request('get', 'Sum');

            const totalPaidSum = this._getTotalPaidSum(doc);
            const remainingSum = MathOperations.addition(mainDocSum, -totalPaidSum);

            if (mainDocSum && mainDocSum != remainingSum) {
                unpaidBalance = Math.min(unpaidBalance, remainingSum);
            }

            return mainDocSum ? Math.min(unpaidBalance, mainDocSum) : unpaidBalance;
        },

        resetDocuments(documents) {
            const self = this;
            const rowModels = _.map(documents, (doc) => {
                return {
                    DocumentBaseId: doc.DocumentBaseId,
                    DocumentType: doc.DocumentType,
                    Number: doc.DocumentName,
                    DocumentName: self.getNameByType(doc.DocumentType),
                    DocumentTaxationSystemType: doc.DocumentTaxationSystemType,
                    HasMaterial: doc.HasMaterial,
                    Date: doc.DocumentDate,
                    Id: doc.DocumentId,
                    DocumentSum: doc.Sum,
                    PaidSum: doc.Sum - doc.UnpaidBalance,
                    Sum: doc.ReceivedSum,
                    KontragentId: doc.KontragentId,
                    DocumentKontragentId: self.model.get('KontragentId')
                };
            });

            this.clearTable();
            this.collection.reset(rowModels);
            this.collection.each(this.addRow, this);
        },

        removeDocuments(documents) {
            _.each(documents, (doc) => {
                const item = this.collection.findWhere({ DocumentBaseId: doc.DocumentBaseId });
                if (item) {
                    this.collection.remove(item);
                }
            });
        },

        getNameByType(documentType) {
            const name = common.Data.DocumentTypeHelper.getAccountingDocumentTypeName(documentType);
            if (name) {
                return common.Utils.Converter.capitaliseFirstLetter(name);
            }
        },

        clearTable() {
            const rows = this.rows || {};
            _.each(rows, (row) => {
                row.remove();
            });
        },

        addWaybill() {
            this.collection.add({
                DocumentName: 'Накладная',
                DocumentType: common.Data.AccountingDocumentType.Waybill
            });
        },

        addStatement() {
            this.collection.add({
                DocumentName: 'Акт',
                DocumentType: common.Data.AccountingDocumentType.Statement
            });
        },

        addReceiptStatement() {
            this.collection.add({
                DocumentName: 'Акт приема-передачи',
                DocumentType: common.Data.AccountingDocumentType.ReceiptStatement
            });
        },

        addInventoryCard() {
            this.collection.add({
                DocumentName: 'Инвентарная карточка',
                DocumentType: common.Data.AccountingDocumentType.InventoryCard
            });
        },

        addUpd() {
            const DocumentType = this.model.get('Direction') === Direction.Outgoing ? common.Data.AccountingDocumentType.Upd : common.Data.AccountingDocumentType.SalesUpd;

            this.collection.add({
                DocumentName: 'УПД',
                DocumentType
            });
        },

        addDocumentToMenu(docType) {
            let docTypeName = common.Enums.getAttrByVal(common.Data.AccountingDocumentType, docType),
                docTypeDescription = common.Data.DocumentTypeHelper.getAccountingDocumentTypeName(docType);

            const $li = String.format('<li id="add{0}">{1}</li>', docTypeName, docTypeDescription);
            this.$('[data-mddropdown-list]').append($li);
        },

        addMiddlemanReport() {
            this.collection.add({
                DocumentName: 'Отчет посредника',
                DocumentType: common.Data.AccountingDocumentType.MiddlemanReport
            });
        },

        addRetailReport() {
            this.collection.add({
                DocumentName: 'Отчет о розничной продаже',
                DocumentType: common.Data.AccountingDocumentType.RetailReport
            });
        },

        hideForNothing() {
            if (!this.collection.length) {
                this.$('table').hide();
            } else {
                this.$('table').show();
            }
        },

        updateCollectionData() {
            this.collection.changeKontragent(this.model.get('KontragentId'));
            this.collection.each(function(item) {
                item.set('BaseDocumentId', this.model.get('BaseDocumentId'));
            }, this);
        },

        removeRow(model) {
            this.rows[model.cid].remove();
            this.rows = _.omit(this.rows, model.cid);
        },

        showResult() {
            const sum = this.model.get('DocumentsSum');
            if (sum) {
                this.$('tfoot .result').show();
                return;
            }

            this.$('tfoot .result').hide();
        },

        updateSum() {
            const sum = this.collection.sum();
            this.model.set('DocumentsSum', sum, { validate: true });
        },

        updateSourceModel() {
            const docs = this.model.get('Documents');
            const val = _.map(this.collection.toJSON(), (item) => {
                return _.omit(item, 'KontragentId');
            });

            if (!_.isEqual(docs, val)) {
                this.model.set('Documents', this.collection.toJSON());
            }
        },

        setCustomEdit(model, value, options) {
            if (options.programmatic) {
                return;
            }

            this.model.set('IsCustomEdit', true);
        },

        onChangeKontragent() {
            this.updateCollectionData();
            if (!this.model.get('KontragentId') && !this.collection.length) {
                this.model.set('IsCustomEdit', false);
            }
        },

        toggleAddDocLink() {
            const mainDocChannel = Backbone.Wreqr.radio.channel('document');
            const mainDocSum = mainDocChannel.reqres.request('get', 'Sum');
            const totalSum = mainDocChannel.reqres.request('get', 'DocumentsSum');
            const link = this.$('[data-element=newDoc]');

            if (mainDocSum && totalSum >= mainDocSum) {
                link.hide();
            } else {
                link.show();
            }
        },

        isValid() {
            return this.collection.every((item) => {
                return item.isValid(true);
            });
        },

        _getTotalPaidSum(selectDoc) {
            const collection = this.collection;
            return collection.reduce((zero, doc) => {
                if (selectDoc.DocumentId !== doc.get('Id')) {
                    return MathOperations.addition(doc.get('Sum'), zero);
                }
                return zero;
            }, 0);
        }
    });
}(Core.Components, Common));

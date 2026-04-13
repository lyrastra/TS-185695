(function(buy, common, money) {

    'use strict';

    buy.Models.Documents.BaseDocument = Backbone.Model.extend({
        getUrl: WebApp.ClosingDocumentsOperation.GetDocument,
        saveUrl: '',
        createFromBillUrl: WebApp.ClosingDocumentsOperation.GetDocumentByBills,
        createFromProjectUrl: WebApp.ClosingDocumentsOperation.GetDocumentByProject,
        deleteUrl: WebApp.ClosingDocumentsOperation.Delete,
        defaults: {
            IsBuy: true,
            CanEdit: true,
            Number: '',
        },
        getDefaultDate: function() {
            var date = new Date();
            var requisites = common.Utils.CommonDataLoader.FirmRequisites;
            if (requisites.inClosedPeriod(date)) {
                date = requisites.getFirstOpenDate();
            }
            return date;
        },

        loaded: false,

        toJSON: function() {
            var data = Backbone.Model.prototype.toJSON.call(this);
            _.each(data, function(value, key) {
                if (_.isObject(value) && _.isFunction(value.toJSON)) {
                    data[key] = value.toJSON();
                }
            });

            return data;
        },

        load: function(options) {
            var documentModel = this;
            var onSuccess = options.success;
            var action = this.get('action');

            options = options || {};

            options = _.extend(options, {
                success: function(model, response) {
                    var timeLimit = 40000;
                    var act;
                    documentModel.onLoad();
                    documentModel.loadFields();

                    act = function() {
                        if (documentModel.loaded) {
                            documentModel.initialValues = documentModel.toJSON();
                            if (onSuccess) {
                                onSuccess(model, response);
                            }
                        } else {
                            timeLimit -= 100;
                            if (timeLimit > 0) {
                                _.delay(act, 100);
                            } else {
                                window.console.log('time expire, fields not load - ', documentModel.loadedFields);
                                Backbone.history.navigate('error/', { replace: true, trigger: true });
                            }
                        }
                    };

                    act();
                }
            });

            if (action === 'new' || action === 'investment' || action === 'type') {
                options.success();
            } else {
                return documentModel.fetch(options);
            }
        },

        onLoad: function() {
            if (this.get('action') === 'copy') {
                this.unset('DocumentBaseId');
                this.unset('Id');
            }
        },

        fetch: function(options) {
            var billIds;
            options = options || {};

            if (this.get('action') === 'fromBill') {
                if (this.get('billIds')) {
                    billIds = this.get('billIds');
                } else {
                    billIds = amplify.store(SaleDocumentResource.StorageKeys.GetDocumentByBillsData);
                }

                options = _.extend(options,
                    {
                        url: this.createFromBillUrl,
                        data: JSON.stringify({
                            documentType: this.getDocumentType(),
                            billIds: billIds
                        }),
                        contentType: 'application/json; charset=utf-8',
                        type: 'POST'
                    });
            }

            if (this.get('action') == 'fromContract') {
                options = _.extend(options,
                    {
                        url: this.createFromProjectUrl,
                        data: JSON.stringify({
                            documentType: this.getDocumentType(),
                            projectId: this.get('ProjectId')
                        }),
                        contentType: 'application/json; charset=utf-8',
                        type: 'POST'
                    });
            }

            return Backbone.Model.prototype.fetch.call(this, options);
        },

        loadFields: function() {
            this.loaded = true;
        },

        parse: function(resp) {
            resp = resp || {};

            if (resp.Status === false) {
                Backbone.history.navigate('error/', { replace: true, trigger: true });
            } else {
                var response = resp;

                if (resp.ClosingDocument) {
                    response = resp.ClosingDocument;
                    delete response.ClosingDocument;
                }
                
                response = this.parseItems(response);

                if (response.MiddlemanContract) {
                    response.MiddlemanContract = new PrimaryDocuments.Models.MiddlemanContractModel(response.MiddlemanContract);
                }

                if (this.get('action') === 'copy') {
                    response = _.omit(response,
                        'Number', 'Date', 'AccountingReadOnly', 'PaymentDocumentLinks', 'Invoice',
                        'AccountingDocumentId', 'BalanceSum', 'DocumentBaseId', 'Id');
                }

                if (!response.Date || response.Date === '') {
                    delete response.Date;
                }

                return response;
            }
        },

        mapItems: function(items) {
            return items;
        },

        parseItems: function(response) {
            if (response.Items) {
                var resp = this.mapItems(response.Items);

                var items = this.get('Items');
                if (items && items.reset) {
                    items.reset(resp);
                    response = _.omit(response, 'Items');
                } else {
                    response.Items = new Backbone.Collection(resp);
                }
            }
            return response;
        },
        
        sync: function(method, model, options) {
            options = options || {};

            if (method === 'create' || method === 'update') {
                this.url = this.saveUrl;
            } else if (method === 'delete') {
                this.urlRoot = this.deleteUrl;
            } else {
                this.urlRoot = _.result(this, 'getUrl');
            }

            if (method !== 'read') {
                options = _.extend(options, { type: 'POST' });

                if (!options.data) {
                    var attributesForSave = model.getDataForSave();
                    options.data = JSON.stringify(attributesForSave);
                } else {
                    options.data = JSON.stringify(options.data);
                }

                options.contentType = 'application/json; charset=utf-8';
            }

            return Backbone.Model.prototype.sync.call(this, method, model, options);
        },

        destroy: function(options) {
            var data = [{
                DocumentType: this.getDocumentType(),
                ids: [this.get('Id')]
            }];
            var args = {
                url: _.result(this, 'deleteUrl'),
                type: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json; charset=utf-8'
            };
            return $.ajax(_.extend(args, options));
        },

        notForSave: [],

        /// attr - массив атрибутов, которые не будут отправлены на сервер при сохранении формы
        notSaveAttributes: function(attr) {
            this.notForSave = _.union(this.notForSave, attr);
        },

        /// применяется, чтобы удалить действие метода notSaveAttributes
        saveAttributes: function(attr) {
            this.notForSave = _.difference(this.notForSave, attr);
        },

        getDataForSave: function() {
            var data = this.toJSON();
            data = _.omit(data, this.notForSave);

            if (!this.get('UseNds')) {
                data.Items = _.map(data.Items, function(item) {
                    return _.omit(item, 'NdsType');
                });
            }

            data.TemporaryBaseId = this._getTemporaryBaseId();

            return data;
        },

        getDocumentType: function() {
            /// <summary>Тип документа</summary>
            /// <returns type='Enums.SalesDocumentTypes.[Type]'>Тип - акт/накладная/счет-фактура/счет</returns>

            return this.get('DocumentType');
        },

        isStatement: function() {
            var docType = this.getDocumentType();
            return docType === Enums.SalesDocumentTypes.Statement;
        },

        isWaybill: function() {
            var docType = this.getDocumentType();
            return docType === Enums.SalesDocumentTypes.Waybill;
        },

        isInvoice: function() {
            var docType = this.getDocumentType();
            return docType === Enums.SalesDocumentTypes.Invoice;
        },

        isEnabledInClosedPeriod: function() {
            return this.isInvoice() || this.isStatement() || this.isWaybill();
        },

        setLoaded: function(fieldName) {
            this.loadedFields = _.without(this.loadedFields, fieldName);

            if (this.loadedFields.length === 0) {
                this.loaded = true;
            }
        },

        loadDocumentNumber: function() {
            var model = this;
            this.DocumentNumber = new buy.Models.Documents.DocumentNumber({
                documentType: model.getDocumentType()
            });
            this.DocumentNumber.load(function() {
                model.set({ Number: model.DocumentNumber.get('Value') });
                model.setLoaded('Number');
            });
        },

        loadKontragentName: function(options) {
            var model = this;
            var id = Converter.toInteger(model.get('KontragentId'));

            if ($.trim(this.get('KontragentName')).length === 0 && id > 0) {
                this.Kontragent = new money.Models.Common.Kontragent({
                    id: id,
                    kontragentType: model.get('KontragentType') || Enums.KontragentTypes.Kontragent
                });

                this.Kontragent.fetch({
                    success: function(kontragentModel, response) {
                        if (response.Status) {
                            model.set({ KontragentName: kontragentModel.get('Name') });
                            model.setLoaded('KontragentName');

                            if (options && options.success) {
                                options.success();
                            }
                        }
                    }
                });
            } else {
                model.setLoaded('KontragentName');
            }
        },

        loadSenderName: function() {
            var model = this;

            this.Sender = new money.Models.Common.Kontragent({
                id: Converter.toInteger(model.get('SenderId')),
                kontragentType: Enums.KontragentTypes.Kontragent
            });

            this.Sender.fetch({
                success: function(kontragentModel, response) {
                    if (response.Status) {
                        model.set({ SenderName: kontragentModel.get('Name') });
                        model.setLoaded('SenderName');
                    }
                }
            });
        },

        loadSupplierName: function() {
            var model = this;

            this.Supplier = new money.Models.Common.Kontragent({
                id: Converter.toInteger(model.get('SupplierId')),
                kontragentType: Enums.KontragentTypes.Kontragent
            });

            this.Supplier.fetch({
                success: function(kontragentModel, response) {
                    if (response.Status) {
                        model.set({
                            SupplierName: kontragentModel.get('Name')
                        });
                        model.setLoaded('SupplierName');
                    }
                }
            });
        },

        loadReceiverName: function() {
            var model = this;

            this.Receiver = new money.Models.Common.Kontragent({
                id: Converter.toInteger(model.get('ReceiverId')),
                kontragentType: Enums.KontragentTypes.Kontragent
            });

            if (!this.get('ReceiverName') || this.get('ReceiverName').length === 0) {
                this.Receiver.fetch({
                    success: function(kontragentModel, response) {
                        if (response.Status) {
                            model.set({
                                ReceiverName: kontragentModel.get('Name'),
                                ReceiverType: Enums.WaybillKontragentTypes.Kontragent
                            });
                            model.setLoaded('ReceiverName');
                        }
                    }
                });
            } else {
                model.setLoaded('ReceiverName');
            }
        },

        loadPayerName: function() {
            var model = this;

            this.Payer = new money.Models.Common.Kontragent({
                id: Converter.toInteger(model.get('PayerId')),
                kontragentType: Enums.KontragentTypes.Kontragent
            });

            if (!this.get('PayerName') || this.get('PayerName').length === 0) {
                this.Payer.fetch({
                    success: function(kontragentModel, response) {
                        if (response.Status) {
                            model.set({ PayerName: kontragentModel.get('Name') });
                            model.setLoaded('PayerName');
                        }
                    }
                });
            }
        },

        disabled: [],

        _disable: function(name) {
            this.disabled = _.union(this.disabled, name);
        },

        _enable: function(name) {
            this.disabled = _.difference(this.disabled, name);
        },

        getSum: function(sumField) {
            if (!sumField) {
                sumField = 'SumWithNds';
            }

            return Converter.toFloat(this.get(sumField));
        },

        _getTemporaryBaseId: function() {
            var service = window.mdNew.documentIdService;
            if (service) {
                return service.getTemporaryDocumentId();
            }
        }
    });

})(Buy, Common, Money);

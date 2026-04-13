/* eslint-disable */
import React from 'react';
import ReactDOM from 'react-dom';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import isPostingsOfTypeOtherForIp from '../../../../../../../../helpers/postingsForIpHelper';
import { purseOperationResources } from '../../../../../../../../resources/MoneyOperationTypeResources';
import storage from '../../../../../../../../helpers/newMoney/storage';

// платежная система исходящие

(function(bank, md) {
    bank.Views.OutgoingPurseDocument = Marionette.ItemView.extend({
        template: '#OutgoingPurseDocument',
        className: 'newWave',

        events: {
            'click #saveButton': 'save',
            'click #cancel': 'cancel'
        },

        bindings: {
            'select[data-bind=Purse]': {
                observe: 'PurseId',
                selectOptions: {
                    collection() {
                        return this.getPurses();
                    }
                },
                onGet(val) {
                    return parseInt(val, 10);
                }
            },

            'select[data-bind=Type]': {
                observe: 'PurseOperationType',
                selectOptions: {
                    collection() {
                        return this.getOperationTypes();
                    }
                }
            },

            '[data-bind=Comment]': {
                observe: 'Comment',
                events: ['change', 'blur']
            },

            '[data-bind=Number]': {
                observe: 'Number',
                events: ['change', 'blur'],
                afterUpdate($el) {
                    $el.blur();
                }
            }
        },

        initialize(options) {      
            this.isForIp = isPostingsOfTypeOtherForIp(purseOperationResources.PurseOperationOtherOutgoing.value);
            this.purseGetter = new Bank.PurseGetter();
            this.settlementGetter = new Bank.SettlementGetter();
            this.taxSystemGetter = new Bank.TaxSystemGetter();
            this.operationService = new bank.PurseOperationService();
            this.model.set(`CanEdit`, this._hasEditAccess());     
            
            Backbone.Validation.bind(this);
        },

        getOperationType(purseOperationType) {
            return Object.values(purseOperationResources).find(value => value.purseOperationType === purseOperationType)?.value || 0;
        },

        onRender() {
            this.documentHeader = this.createHeader();            

            this.stickit();
            this.$('select').mdSelectUls().change();
            this.removeControls();

            this.operation = this.createOperation();
            if (this.model.get(`Id`) > 0) {
                this.actions = this.createActions();
            } else {
                this.listenTo(this.model, `change:Year`, this.updateNumber);                
            }

            this.listenTo(this.model, 'change:PurseOperationType', () => {
                this.changeOperation();
            });
            this.disableForm();          
        },
       
        isClosed() {
            const requisites = new Common.FirmRequisites();
            return requisites.inClosedPeriod(this.model.get('Date'));
        },

        _hasEditAccess() {
            return UserAccessManager.AccessRule.AccessToBank === Enums.TypeAccessRule.AccessEdit;
        },

        disableForm() {
            if (this.isClosed()) {
                this.closedPeriodIcon = new bank.ClosedPeriodIcon({ model: this.model }).render();
                this.$el.prepend(this.closedPeriodIcon.$el);

                bank.Views.PurseDocumentHelper.disableForm(this.$('.documentForm'));
            }

            if (!this._hasEditAccess()) {
                bank.Views.PurseDocumentHelper.disableForm(this.$('.documentForm'));
                this.$('#copyDocument,#deleteDocument').remove();
            }
        },

        removeControls() {
            const isImport = this.model.get('action') === 'import';
            const purses = this.purseGetter.getPurses();
            if (!purses.length || isImport) {
                this.$('[data-bind="Purse"]').closest('.mdCol').remove();
            }

            if (isImport) {
                this.$('#saveButton').closest('.mdRow').remove();
            }
        },

        getPurses() {
            const items = this.purseGetter.getPurses();
            return mapListToSelect(items);
        },

        getOperationTypes() {
            const items = getOutgoingPurseOperationTypes.call(this);
            return mapListToSelect(items);
        },

        createHeader() {
            const documentHeader = new Common.Controls.DocumentHeaderControl({
                model: this.model,
                el: this.$('[data-control=documentheader]'),
                validate: false
            });
            documentHeader.render();
            return documentHeader;
        },

        onDelete(model) {
            model.trigger('delete');
        },

        createActions() {
            return new bank.DocumentActions({
                model: this.model,
                el: this.$('[data-control=documentActions]'),
                operationService: this.operationService,
                onDelete: this.onDelete,
                onError: this.onError,
                readonly: this.isClosed()
            }).render();
        },

        changeOperation() {
            this.operation.destroy();
            this.operation = this.createOperation();
        },

        createOperation() {
            const operationType = this.model.get('PurseOperationType');
            const factory = this.getOperationFactory();
            return factory.createOperation(operationType);
        },

        getOperationFactory() {
            const view = this;
            const factory = {};

            this.model.set("OperationType", this.getOperationType(this.model.get("PurseOperationType")));

            factory[Md.Data.PurseOperationType.Transfer] = this.createMovementToSettlementOperation;
            factory[Md.Data.PurseOperationType.Comission] = this.createCommissionHoldOperation;
            factory[Md.Data.PurseOperationType.OtherOutgoing] = this.createOtherOperation;

            factory.createOperation = function(type) {
                const operation = factory[type].call(view);
                view.$('[data-control=operation]').html(operation.$el);
                return operation;
            };

            return factory;
        },

        createMovementToSettlementOperation() {
            return new bank.Views.MovementToSettlementPurseOperation({
                model: this.model,
                settlements: this.settlementGetter.getSettlements()
            }).render();
        },

        createCommissionHoldOperation() {
            return new bank.Views.CommissionHoldPurseOperation({
                model: this.model,
                taxSystems: this.taxSystemGetter.getTaxSystemTypes(this.model.get('Date'))
            }).render();
        },

        createOtherOperation() {
            return new bank.Views.OtherOutgoingPurseOperation({
                model: this.model
            }).render();
        },

        isValid() {
            return this.model.isValid(true) && (!this.postingsAndTaxControl || this.postingsAndTaxControl.isValid());
        },

        getPostingsLoading() {
            const dfd = new $.Deferred();

            if (!this.postingsAndTaxControl) {
                dfd.resolve();
                return dfd;
            }

            const postings = this.postingsAndTaxControl.postingsCollection;
            if (postings.loading) {
                this.listenTo(postings, 'ModelLoaded', () => {
                    dfd.resolve();
                });
            } else {
                dfd.resolve();
            }

            return dfd;
        },

        save() {
            const purseId = this.model.get('PurseId');
            const filter = storage.get('filter');

            if(!this.model.get(`Id`)) {
                storage.save(`Scroll`, 0);
                storage.save(`tableData`, {});
            }

            if (this.model.get("PurseOperationType") !== Md.Data.PurseOperationType.Comission) {
                this.model.set(`NdsType`, null);
                this.model.set('IncludeNds', null);
                this.model.set('NdsSum', 0);
            }

            if (this.model.get(`NdsType`) === Common.Data.BankAndCashNdsTypes.Empty) {
                this.model.set(`NdsType`, ``, { silent: true });
            }

            if (filter && filter.sourceId !== 0 && filter.sourceId !== purseId) {
                filter.sourceId = purseId;
                storage.save('filter', filter);
            }

            this.getPostingsLoading().done(() => {
                if (!this.saveXhr && this.isValid()) {
                    this.saveXhr = this.model.save()
                        .done(this._onSave.bind(this))
                        .fail(this.onError);
                }
            });
        },

        _onSave() {
            this.trigger('save');
        },

        cancel() {
            const current = window.location.href;

            window.history.back();

            setTimeout(() => {
                if (window.location.href === current) {
                    window.location = '/Finances';
                }
            }, 1000);
        },

        onError() {
            window.location = '#error/';
        },

        behaviors() {
            return {
                PostingControlsBehavior: {
                    behaviorClass: BankApp.Views.PostingControlsBehavior,
                    postingCollectionClass: bank.Collections.PostingsAndTax.PursePostingCollection,
                    taxCollectionClass: bank.Collections.PostingsAndTax.PurseTaxCollection
                }
            };
        },

        updateNumber() {
            const date = this.model.get('Date');
            this.operationService.getNumberForDate(date, Direction.Outgoing).done((number) => {
                this.model.set('Number', number);
            });
        }
    });

    function mapListToSelect(collection) {
        return collection.map((item) => {
            return {
                value: item.get('Id'),
                label: item.get('Name')
            };
        });
    }

    function getOutgoingPurseOperationTypes() {
        const types = [
            md.Data.PurseOperationType.Transfer,
            md.Data.PurseOperationType.Comission,
            md.Data.PurseOperationType.OtherOutgoing
        ];
        const list = _.map(types, (item) => {
            return {
                Name: md.Data.PurseOperationType.getDescription(item),
                Id: item
            };
        });

        return new Backbone.Collection(list);
    }
}(Bank, Md));

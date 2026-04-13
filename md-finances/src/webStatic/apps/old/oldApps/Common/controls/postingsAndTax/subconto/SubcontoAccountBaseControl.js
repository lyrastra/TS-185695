import { cashOrderOperationResources } from "../../../../../../../resources/MoneyOperationTypeResources";

(function(common) {

    common.Views.SubcontoBaseControl = Backbone.View.extend({

        panel: {
            defaultPanel: '<div class="subcontoPanel"><span class="subcontoEmptyAccount">Укажите счёт</span></div>',
            notFoundPanel: '<div class="subcontoPanel"><span>По данному счёту нет ни одного объекта учёта</span></div>'
        },

        typeList: common.Data.SubcontoType,

        container: null,

        postponedInit: [],

        initialize: function(container, model, isEdit, { sourceDocument = {} } = {}) {
            this.sourceDocument = sourceDocument;
            this.model = new common.Models.SubcontoModel();
            this.container = container;
            this.parentModel = model;
            this.isEdit = isEdit;
            this.postponedInit = [];

            this.setSubcontoRegion();

            if (this.isEdit) {
                this.setStartView();
            } else {
                this.render();
            }
        },

        render: function() {
            if (this.container !== null) {
                this.container.find(this.regionName).html(this.panel.defaultPanel);
            }
        },

        refreshView: function(id) {
            var self = this;
            if (id === 0) {
                self.render();
            } else {

                const cashList = this.sourceDocument.get('CashList');

                let cashId;

                if ((this.sourceDocument.get('OperationType') === cashOrderOperationResources.CashOrderIncomingOther.value
                || this.sourceDocument.get('OperationType') === cashOrderOperationResources.CashOrderOutgoingOther.value) && cashList[0] === this.sourceDocument.get('CashId')) {
                    cashId = null;
                } else {
                    cashId = this.sourceDocument.get('CashId');
                }

                const settlementAccountId = this.sourceDocument.get('SettlementAccountId');

                this.model.load(id, function() {
                    self.postponedInit.length = 0;
                    self.clearSubconto();

                    var subcontos = _.sortBy(self.model.get('List'), function(subconto) {
                        return subconto.Level;
                    });

            
                    self.createView(subcontos);
                }, { cashId, settlementAccountId, cashList });
            }
        },

        getAccountCode: function() {
            return null;
        },

        getDate: function() {
            return null;
        },

        createView: function(collection) {
            var canvas = '',
                oneTypeImpl = false;

                

            this.parentModel.subcontoValidation = this.parentModel.subcontoValidation || {};
            this.parentModel.subcontoValidation[this.getAccountCode()] = [];

            for (var i = 0; i < collection.length; i++) {
                var subcontoLevel = collection[i];

                var name = this.getType(subcontoLevel.Type),
                    implModel = common.Options.GetSubcontoOptions(name, this.getAccountCode(), this.getDate());

                if (implModel !== null && implModel !== undefined) {
                    var initObject = {
                        field: this.getSubcontoPrefix() + i + '_' + this.parentModel.cid,
                        func: implModel.autocomplete,
                        type: subcontoLevel.Type
                    };

                    canvas += implModel.view.replace(new RegExp(implModel.id, 'g'), initObject.field);

                    if (subcontoLevel.Name) {
                        initObject.object = subcontoLevel;
                    }

                    if (subcontoLevel.Subconto) {
                        initObject.object = subcontoLevel.Subconto;
                    }

                    initObject.readOnly = this.getReadOnlyProperty(subcontoLevel);

                    this.postponedInit.push(initObject);

                    if (!oneTypeImpl) {
                        oneTypeImpl = true;
                    }

                    const cashList = this.sourceDocument.get('CashList');

                    this.updateSubcontoValidationForAccount(this.getAccountCode(), subcontoLevel.Type, implModel, i, cashList);
                }
            }

            if (!oneTypeImpl) {
                canvas = this.panel.notFoundPanel;
            }

            this.container.find(this.regionName).html(canvas);
            this.initAutocomplete();
            if (this.isEdit) {
                this.initFields();
            }
        },

        getReadOnlyProperty: function(subconto) {
            subconto = subconto || {};

            if (subconto.Subconto) {
                return subconto.Subconto.ReadOnly;
            }

            if (subconto.Item) {
                return subconto.Item.ReadOnly;
            }

            return subconto.ReadOnly;
        },

        updateSubcontoValidationForAccount: function(accountCode, type, subconto, level, cashList) {
            this.parentModel.subcontoValidation[accountCode].push({
                type: type,
                field: this.getSubcontoPrefix() + level,
                required: subconto.required,
                label: subconto.placeholder,
                level: level,
                cashList: cashList
            });
        },

        getType: function(type) {
            for (var item in this.typeList) {
                if (this.typeList[item] === type) {
                    return item;
                }
            }

            return null;
        },

        initAutocomplete: function() {
            for (var i = 0; i < this.postponedInit.length; i++) {
                var item = this.postponedInit[i];
                if (_.isFunction(item.func)) {
                    var input = this.onSubcontoRegion.find('input[name="' + item.field + '"]');
                    item.func(input, this, item.type, item.object);
                } else {
                    throw item.field + ' has not implement autocomplete function';
                }
            }
        },

        getSubcontoByType: function(list, type) {
            for (var i = 0; i < list.length; i++) {
                if (list[i] && list[i].SubcontoType == type) {
                    return { item: list[i], index: i };
                }
            }

            return null;
        },

        initFields: function() {
            for (var i = 0; i < this.postponedInit.length; i++) {
                this.onSelectForEditFunc(this.postponedInit[i].object, this.postponedInit[i].type);
            }
        },

        getCreateViewCollection: function(list) {
            var result = [];
            if (!list || !list.length) {
                return result;
            }

            for (var i = 0; i < list.length; i++) {
                result.push({ Type: list[i].SubcontoType, Name: list[i].Name, Item: list[i] });
            }

            return result;
        },

        changeAssotiateSpan: function(input, value) {
            var cell = input.closest('.mdTableCell'),
                name = input.attr('name');
            cell.find('span[name="' + name + '"]').text(value);
        },

        updateSubcontoCollection: function(name, item, type, options) {
            options = options || { validate: true };

            if (item !== undefined) {
                var collection = this.parentModel.get(name),
                    findItem = this.getSubcontoByType(collection, type),
                    subconto = common.Options.GetEntityIdByType(item, type);

                if (findItem !== null && findItem !== undefined) {
                    collection.splice(findItem.index, 1);
                }
                var availableSubcontos = this.parentModel.subcontoValidation[this.getAccountCode()];
                var index = _.find(availableSubcontos, function(availableSubconto) {
                    return availableSubconto.type == type;
                }).level;

                collection[index] = subconto;

                var setOptions = {};
                if (options.validate === false) {
                    setOptions.silent = true;
                }

                collection = this.filterSubcontoCollection(type, collection);

                this.parentModel.set(name, collection, setOptions);
            }

            if (options.validate !== false) {
                if (this.parentModel.isValidAttr(name, item, type)) {
                    const element = $(this.onSubcontoRegion);

                    element.find('.input-validation-error').removeClass('input-validation-error');
                    element.find('.field-validation-error').remove();
                }
            }
        },

        filterSubcontoCollection: function(type, collection) {
            if (type === common.Data.SubcontoType.Kontragent) {
                var contract = _.findWhere(collection, { SubcontoType: common.Data.SubcontoType.Contract });
                collection = _.without(collection, contract);

                var inputId = (_.findWhere(this.postponedInit, { type: common.Data.SubcontoType.Contract }) || {}).field;
                var $input = this.onSubcontoRegion.find('#' + inputId);
                $input.val('');
            }

            return collection;
        }
    });

})(Common);
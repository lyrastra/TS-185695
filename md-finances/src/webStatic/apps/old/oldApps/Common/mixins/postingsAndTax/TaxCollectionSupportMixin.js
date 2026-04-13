import { round } from '../../../../../../helpers/numberConverter';

(function(common, window) {
    'use strict';

    common.Mixin.TaxCollectionSupportMixin = {
        modeCheking: function() {
            if (this.sourceDocument.get('PostingsAndTaxMode') == common.Data.ProvidePostingType.ByHand && this.firstLoadComplete) {
                return true;
            }
        },

        checkEmptyFields: function() {
            var self = this,
                field,
                emptyFields = [],
                requiredFields = _.result(this, 'requiredFields');

            _.each(requiredFields, function(obj) {
                field = self.sourceDocument.get(obj.fieldName);

                if (obj.dependentFields && obj.dependentFields.length) {
                    field.each(function(model) {
                        _.each(obj.dependentFields, function(value) {
                            if (!model.get(value)) {
                                emptyFields.push(obj.name);
                            }
                        });
                    });
                } else {
                    if (_.isUndefined(field)) {
                        emptyFields.push(obj.name);
                    }
                }
            });

            return emptyFields;
        },

        checkRequiredFields: function(requiredFields) {
            var result = true,
                fields = requiredFields || _.result(this, 'requiredFields'),
                field;

            _.each(fields, function(obj) {
                field = this.definitionRequiredFieldValue(obj);
                if (_.isUndefined(field) || field === null || !field.toString().length || (_.isObject(field) && !field.length)) {
                    result = false;
                }

                if (obj.dependentFields && obj.dependentFields.length) {
                    var filledStack = lookingForAtleastOneFilledItem(field, obj);

                    if (!_.contains(filledStack, true)) {
                        result = false;
                    }
                }
            }, this);

            return result;
        },

        /**
         * Полный цикл создания объясняющего сообщения для конкретной операции
         *
         * @this {taxCollection}
         * @param {number} operationType тип операции.
         * @param {string} cid модели операции из общей модели документа.
         */
        setOperationExplainMessageWithAnchor: function(operationType, cid) {
            var requiredFields = this.operationsRequiredFields(operationType);

            if (!requiredFields) {
                return;
            }

            var self = this,
                message;

            $.each(requiredFields, function() {
                var emptyField = self.checkForEmptyRequiredFields(this, cid);
                if (emptyField) {
                    message = self.createMessageWithAnchor(emptyField, cid);
                }
            });

            return message;
        },

        /** Полный цикл создания объясняющего сообщения для всего документа */
        setExplainMessageWithAnchor: function() {
            var requiredFields;

            if (this.isOsno()) {
                requiredFields = _.result(this, 'requiredFieldsOsno') || _.result(this, 'requiredFields');
            } else {
                requiredFields = _.result(this, 'requiredFields');
            }

            if (!requiredFields) {
                return;
            }

            var self = this,
                message;

            $.each(requiredFields, function() {
                var emptyField = self.checkForEmptyRequiredFields(this);
                if (emptyField) {
                    message = self.createMessageWithAnchor(emptyField);
                }
            });

            return message;
        },

        checkForEmptyRequiredFields: function(obj, cid) {
            var field = this.definitionRequiredFieldValue(obj, cid),
                returnObj;

            if (obj.dependentFields) {
                if (!field.length) {
                    returnObj = obj;
                }

                var filledStack = lookingForAtleastOneFilledItem(field, obj);

                if (!_.contains(filledStack, true)) {
                    returnObj = obj;
                }
            } else if (!field || (Array.isArray(field) && field.length === 0)) {
                returnObj = obj;
            }

            return returnObj;
        },

        definitionRequiredFieldValue: function(obj, cid) {
            var document = cid ? this.sourceDocument.get('Operations').get(cid) : this.sourceDocument,
                field;

            if (obj.otherCondition) {
                field = obj.otherCondition();
            } else if (_.isArray(obj.fieldName)) {
                _.each(obj.fieldName, function(name) {
                    field = this.getFieldValueByFieldName(name, document);
                }, this);
            } else {
                field = this.getFieldValueByFieldName(obj.fieldName, document);
            }
            return field;
        },


        getFieldValueByFieldName: function(name, document) {
            if (!name || !document) {
                return null;
            }

            var splitted = name.split('.');
            var targetField = _.clone(document.get(splitted[0]));

            var deepCount = 1;
            while (deepCount < splitted.length) {
                if (!targetField) {
                    return null;
                }

                var attr = splitted[deepCount];

                if (targetField.get) {
                    targetField = targetField.get(attr);
                } else {
                    targetField = targetField[attr];
                }

                deepCount += 1;
            }

            return targetField;
        },

        createMessageWithAnchor: function(obj, cid) {
            var name = _.result(obj, 'name');

            if (obj.fullName) {
                return obj.fullName;
            } else if (cid) {
                return 'Не учитывается. Заполните <a data-model=' + cid + ' data-scroll-to-el="' + obj.selector + '">' + name + '</a>.';
            } else {
                return 'Не учитывается. Заполните <a data-scroll-to-el="' + obj.selector + '">' + name + '</a>.';
            }
        },

        calculatePostingsSum: function() {
            var totalSum = 0;
            this.each(function(bigModel) {
                bigModel.get('ManualPostings').each(function(model) {
                    if (model.isEmpty()) {
                        return;
                    } else if (model.get('Direction')) {
                        var field;
                        if (model.get('Direction') == common.Data.TaxPostingsDirection.Incoming) {
                            field = model.get('Incoming');
                        } else if (model.get('Direction') == common.Data.TaxPostingsDirection.Outgoing) {
                            field = model.get('Outgoing');
                        } else {
                            field = 0;
                        }

                        totalSum += window.Converter.toFloat(field);
                    }
                });
            });

            return round(totalSum, 2);
        },

        /**
         * Проверка того, что в Items позиции только 1 вида
         *
         * @this {taxCollection}
         * @param {number} type тип продукта.
         */
        checkingForOnlyOneProductType: function(type) {
            if (!this.sourceDocument) {
                return;
            }
            var items = this.sourceDocument.get('Items'),
                typeStack = [];


            if (!items || !items.length) {
                return;
            }
            ;

            items.each(function(model) {
                var currentType = model.get('Type');
                if (currentType !== undefined) {
                    typeStack.push(currentType);
                }
            });

            if (typeStack.length) {
                var onlyOfType;
                if (!_.without(typeStack, type).length) {
                    onlyOfType = true;
                }
            }

            return onlyOfType;
        },

        /** Проверяет, что коллекция пуста путем проверки длинны внутренних коллекций */
        isEmptyCollection: function() {
            var result = true;
            if (this.length) {
                this.each(function(model) {
                    if (model.get('ManualPostings').length
                        || model.get('LinkedDocuments').length
                        || model.get('MainPostings').length) {
                        result = false;
                    }
                });
            }
            return result;
        },

        calculatePostingsOperationSum: function(bigModel) {
            var totalSum = 0;
            bigModel.get('ManualPostings').each(function(model) {
                if (model.isEmpty()) {
                    return;
                } else if (model.get('Direction')) {
                    var field;
                    if (model.get('Direction') == common.Data.TaxPostingsDirection.Incoming) {
                        field = model.get('Incoming');
                    } else if (model.get('Direction') == common.Data.TaxPostingsDirection.Outgoing) {
                        field = model.get('Outgoing');
                    } else {
                        field = 0;
                    }

                    totalSum += window.Converter.toFloat(field);
                }
            });

            return totalSum;
        },

        sourceDocumentItemsCheckForOnlyMaterials: function(type) {
            var items = this.sourceDocument.get('Items'),
                onlyMaterials = true;

            if (items && items.length) {
                items.each(function(model) {
                    if (model.get('Type') == type) {
                        onlyMaterials = false;
                    }
                });
            }

            return onlyMaterials;
        }
    };

    function lookingForAtleastOneFilledItem(field, obj) {
        var filledStack = [];
        field.length && field.each(function(model) {
            var allFilled = _.every(obj.dependentFields, function(val) {
                if (_.isObject(val)) {
                    var modelFiled = model.get(val.name);
                    return modelFiled && val.condition.call(modelFiled);
                }
                return model.get(val);
            });
            filledStack.push(allFilled);
        });

        return filledStack;
    };

})(Common, window);

(function (common) {

    common.Mixin.ModelValidationMixin = {

        validator: {},

        isValidAttr: function (property) {
            this.validateProp(property);
            this.trigger('ValidationEnd', this.valid, this);
            return this.valid[property].valid === true;
        },

        validateProp: function (property) {
            var rules = this.validationRules[property];
            if (rules != undefined) {
                for (var func in rules) {
                    var validFunc = _.result(this, 'validator')[func];
                    if (validFunc == undefined) {
                        throw new Error('Function "' + func + '" is not defined');
                    }
                    var options = rules[func];
                    var validResult = validFunc.call(this, this.get(property), options);
                    validResult.options = _.extend({}, options, validResult.options);
                    this.valid[property] = validResult;

                    if (!validResult.valid) {
                        break;
                    }
                }
            }
        },

        validateAttrs: function (attrs) {
            this.valid = {};
            if (this.validationRules != undefined) {
                for (var property in attrs) {
                    this.validateProp(property);
                }

                this.trigger('ValidationEnd', this.valid, this);

                for (var key in this.valid) {
                    if (this.valid[key].valid !== true && (!this.valid[key].options || this.valid[key].options.level !== 'warning')) {
                        return false;
                    }
                }
            }
            return true;
        },

        makeAttrsValid: function() {
            this.valid = {};
            var validObj = this.valid;
            if (this.validationRules) {
                $.each(this.validationRules, function (key) {
                    validObj[key] = { valid: true };
                });
            }

            this.trigger('ValidationEnd', this.valid, this);
        },

        validateModel: function () {
            return this.validateAttrs(this.attributes);
        },

        remove: function (model) {
            model.makeAttrsValid = common.Mixin.ModelValidationMixin.makeAttrsValid;
            model.off("change", model.validationFunction);
            model.makeAttrsValid();
        },

        init: function (model) {
            model.valid = {};
            model.validateProp = common.Mixin.ModelValidationMixin.validateProp;
            model.validateAttrs = common.Mixin.ModelValidationMixin.validateAttrs;
            model.validateModel = common.Mixin.ModelValidationMixin.validateModel;
            model.isValidAttr = common.Mixin.ModelValidationMixin.isValidAttr,
            model.validationFunction = function() {
                model.validateAttrs(model.changedAttributes());
            };

            if (model.validationRules != undefined) {
                model.on("change", model.validationFunction);
            }
        }
    };

    common.Mixin.removeCollectionValidation = function(collection, validationField) {
        var changeEvent = 'change';
        if (validationField) {
            var fields = validationField.replace(new RegExp(' ', 'g'), ',');
            changeEvent = 'change: ' + fields;
        }

        collection.validation = undefined;
        collection.off(changeEvent);
    };

    common.Mixin.AddCollectionValidation = {

        init: function (collection, validationField) {
            var changeEvent = 'change';

            collection.validation = this.validation;

            if (validationField) {
                var fields = validationField.replace(new RegExp(' ', 'g'), ',');
                changeEvent = 'change: ' + fields;
            }

            collection.on(changeEvent, function () { collection.trigger('change'); });
        },

        validation: function() {
            var result = true;
            this.each(function (item) {
                if (item.isEmpty && item.isEmpty()) {
                    return;
                }
                if (item.validateModel) {
                    result = Boolean(result & item.validateModel());
                }
            });

            return result;
        }
    };

    common.Mixin.AddCustomCollectionValidation = {
        validation: function () {
            return this.validateCollection();
        },

        validateCollection: function () {
            var self = this;
            if (this.validationRules) {
                var rules = _.result(this, 'validationRules');
                if (!rules) {
                    return this.valid;
                }

                $.each(rules, function (property) {
                    self.validateProp(property);
                });
            }
            return this.valid;
        },

        validateProp: function (property) {
            var rules = _.result(this, 'validationRules')[property];
            if (rules != undefined) {
                for (var func in rules) {
                    var validFunc = _.result(this, 'validator')[func];
                    if (validFunc == undefined) {
                        throw new Error('Function "' + func + '" is not defined');
                    }
                    var validResult = validFunc.call(this, this.get(property), rules[func]);
                    this.valid = validResult;

                    if (!validResult.valid) {
                        break;
                    }
                }
            }
        }
    };

    common.Mixin.AddOperationsValidation = {
        operationValidation: function () {
            return this.validateOperations();
        },

        validateOperations: function () {
            var self = this,
                validationRules = _.result(this, 'operationsValidationRules');
            if (validationRules) {
                $.each(validationRules, function (property) {
                    self.validateProp(property);
                });
            }
            return this.valid;
        },

        validateProp: function (property) {
            var rules = _.result(this, 'operationsValidationRules')[property];
            if (rules != undefined) {
                for (var func in rules) {
                    var validFunc = _.result(this, 'validator')[func];
                    if (validFunc == undefined) {
                        throw new Error('Function "' + func + '" is not defined');
                    }
                    var validResult = validFunc.call(this, this.get(property), rules[func]);
                    this.valid = validResult;

                    if (!validResult.valid) {
                        break;
                    }
                }
            }
        }
    };

    common.Mixin.BindViewValidationEvent = {
        bindValidationResult: function (view, model) {
            view = view || this;
            model = model || view.model;

            if (model && model.on) {
                model.on('ValidationEnd', function (valid) {
                    for (var key in valid) {
                        var settings = valid[key],
                            selector = _.result(settings.options, 'selector') || '[data-bind=' + getBindAttribute(key, settings) + ']';

                        setValidationErrorInElement(view.$el.find(selector), settings.message || settings, this.setErrorMessage);
                    }
                });
            }
        },

        bindCollectionValidation: function (view, collection) {
            collection = collection || view.collection;

            if (collection && collection.on && collection.off) {
                view.setErrorCollectionValidation = this.setErrorCollectionValidation;
                collection.off('ValidationEnd');
                collection.on('ValidationEnd', function (valid, model) {
                    view.setErrorCollectionValidation(valid, model);
                });
            }
        },

        unbindCollectionValidation: function (view, collection) {
            collection = collection || view.collection;

            if (collection && collection.off) {
                collection.off('ValidationEnd');
            }
        },

        setErrorCollectionValidation: function (valid, model) {
            for (var key in valid) {
                var dataBindAttr = getBindAttribute(key, valid[key], model.cid);
                var attrs = _.union([], dataBindAttr);
                _.each(attrs, function(attr) {
                    var $el = this.$el.find('[data-bind=' + attr + '_' + model.cid + ']'),
                        msg = valid[key].message || valid[key];
                    setValidationErrorInElement($el, msg, this.setErrorMessage);
                }, this);
            }
        },

        setErrorMessage: function (input, message) {
            setErrorMessage(input, message);
        }
    };

    function getBindAttribute(key, errorObj, cid) {
        if (errorObj.options) {
            if (errorObj.options.getField && cid) {
                return errorObj.options.getField(cid);
            } else if (errorObj.options.field) {
                return errorObj.options.field;
            }
        }

        return key;
    }

    function getValidatedElement(el) {
        if (el.is('select')) {
            return el.parent();
        }
        if (el.is('span')) {
            return null;
        }

        return el.is('input,textarea') ? el : el.find('input,textarea').first();
    }

    function setValidationErrorInElement($el, msg, setErrorMessageFunction) {
        if (msg.valid) {
            $el = getValidatedElement($el);
            if ($el === null) return;

            $el.removeClass('input-validation-error');
            $el.parent().find('.field-validation-error').remove();
        } else {
            for (var i = 0, item = $($el[0]) ; i < $el.length; i++, item = $($el[i])) {

                item = getValidatedElement(item);
                if (item === null) return;

                setErrorMessageFunction = setErrorMessageFunction || setErrorMessage;
                setErrorMessageFunction(item, msg);

                item.addClass('input-validation-error');
            }
        }
    }

    function setErrorMessage(input, message) {
        var closestParent = input.parent();

        if (!input.hasClass('input-validation-error')) {
            closestParent.find(".field-validation-error").remove();
            closestParent.append('<div class="field-validation-error"><span>' + message + '</span></div>');
        } else {
            closestParent.find('.field-validation-error span').text(message);
        }
    }

})(Common);

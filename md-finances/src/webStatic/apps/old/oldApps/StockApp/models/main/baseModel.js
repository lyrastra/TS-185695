(function (base) {
    base.Models.BaseModel = Backbone.Model.extend({

        validator: base.Helpers.BaseValidationHelper,

        isValid: function (property) {
            this.validateProp(property);
            return this.valid[property];
        },

        validateProp: function (property) {
            var rules = this.validationRules[property];
            if (rules != undefined) {
                for (var func in rules) {
                    var validFunc = this.validator[func];
                    if (validFunc == undefined) {
                        throw new Error('Функция ' + func + ' неопределена');
                    }
                    var validResult = validFunc.call(this, this.get(property), rules[func]);
                    if (validResult !== true) {
                        this.valid[property] = validResult;
                        break;
                    }
                    this.valid[property] = true;
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
                    if (this.valid[key] != true) {
                        return false;
                    }
                }
            }
            return true;
        },

        validateModel: function () {
            return this.validateAttrs(this.attributes);
        },

        constructor: function (attr, opt) {
            if (this.validationRules != undefined) {
                this.on("change", function () {
                    this.validateAttrs(this.changedAttributes());
                });
            }
            Backbone.Model.prototype.constructor.call(this, attr, opt);
        },

        save: function (opt) {
            if (this.validateModel()) {
                Backbone.Model.prototype.save.call(this, this.attributes, opt);
                return true;
            }
            return false;
        },

        postfetch: function (opt) {
            opt.type = 'POST';
            opt.dataType = "json";
            opt.contentType = "application/json; charset=utf-8;";
            Backbone.Model.prototype.fetch.call(this, opt);
        }
    });

})(Stock.module('Main'));

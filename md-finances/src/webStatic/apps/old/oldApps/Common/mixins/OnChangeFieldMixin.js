(function (common, window) {

    common.Mixin.OnChangeFieldMixin = {
        
        events: {
            "change input": "onChangeField",
            "change textarea": "onChangeField",
            "change select": "onChangeField"
        },

        localChangedValues: [],

        onChangeField: function (event) {
            var self = this,
                element = $(event.currentTarget || event.target),
                fieldName = self.bindChecking(element),
                value;
            
            if (element.is("input[type=checkbox]")) {
                value = element.is(":checked");
            }
            else {
                value = self.parseFormat(element);
            }
            
            if (self.isSameValue(fieldName, value)) {
                return false;
            }

            (value) ? this.model.set(fieldName, value, { validate: true }) : this.model.unset(fieldName, { validate: true });
            
            self.localChangedValues = _.union(self.localChangedValues, fieldName);
        },

        isSameValue: function(fieldName, value) {
            var model = this.model,
                oldValue = model.get(fieldName);

            return (oldValue === value) ? true : false;
        },

        bindChecking: function (elem) {
            if (elem.attr("name") && elem.data("bind")) {
                return (elem.attr("name") != elem.data("bind")) ? elem.data("bind") : elem.attr("name");
            } else {
                return elem.attr("name");
            }
        },

        parseFormat: function ($el) {
            var format = $el.data("format"),
                value = $el.val();
            switch (format) {
                case 'int':
                    return parseInt(value, 10);
                case 'float':
                    return window.Converter.toFloat(value);
                default:
                    return value;
            }
        }
    };

})(Common, window);
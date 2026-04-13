(function (common) {
    common.Controls.SwitchesChainControl = common.Controls.BaseControl.extend({

        initialize: function (options) {
            common.Controls.BaseControl.prototype.initialize.call(this, options);
            common.Helpers.Mixer.addMixin(this, common.Mixin.OnChangeFieldMixin);
        },
        
        initializeEvents: function() {
            this.model.on("change:" + this.fieldName, this.selectSwitcherItem, this);
        },

        initializeVariables: function (options) {
            this.items = options.items;
            this.fieldName = this.$el.attr("data-bind") || this.$el.attr("name") ;
        },
        
        events: {
            "click .mdSwitchesItem": "selectItem"
        },

        onRender: function () {
            this.switcherFilling();
        },
        
        switcherFilling: function () {
            var select = this.$el,
                list = this.items;

            select.render(_.values(list), {
                type: {
                    'data-val': function () {
                        return this.id;
                    },
                    text: function() {
                        return "";
                    }
                },
                mdSwitchesLink: {
                    text: function () {
                        return this.name;
                    }
                }
            });
            this.selectSwitcherItem();
            this.disabledIfNotEdit();
        },
        
        selectItem: function (e) {
            if (this.canEdit()) {
                return;
            }

            var $elem = $(e.currentTarget || e.target),
                value = Converter.toInteger($elem.attr('data-val'));

            this.model.set(this.fieldName, value);
        },

        selectSwitcherItem: function () {
            var view = this,
                model = view.model,
                switcher = this.$el,
                items = switcher.children(".mdSwitchesItem"),
                value;
            
            items.removeClass("selected");
            if (!_.isUndefined(model.get("InvoiceType"))) {
                items.filter("[data-val=" + model.get("InvoiceType") + "]").addClass("selected");
            } else {
                value = items.first().addClass("selected").attr("data-val");
                model.set(this.fieldName, value);
            }
        },
        
        disabledIfNotEdit: function() {
            if (this.canEdit()) {
                var items = this.$el.children(".mdSwitchesItem");
                items.addClass("disabled");
            }
        },

        canEdit: function() {
            return this.model.get("NotCanEditType") || this.model.get('id') > 0 || this.model.get('Id') > 0;
        }
    });

})(Common);
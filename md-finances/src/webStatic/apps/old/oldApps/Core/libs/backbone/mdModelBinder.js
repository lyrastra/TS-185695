(function () {

    if (!Backbone) {
        throw 'include backbone.js before mdModelBinder';
    }

    Backbone.MdModelBinder = function (options) {
        _.each(_.functions(binder), fn => {
            this[fn] = this[fn].bind(this);
        });
        this.currentOptions = options || {};
    };

    var binder = {
        bind: function (model, view, attributeBindings) {
            this.model = model;
            this.view = view;
            this.el = view.el;
            this.attributeBindings = attributeBindings;

            if (!this.model) {
                throw 'model must be specified';
            }
            if (!this.view) {
                throw 'view must be specified';
            }
            if (!this.el) {
                throw 'html part view must be specified';
            }

            this.bindModelToView();
            this.bindViewToModel();
        },

        unbind: function () {
            this.unbindModelToView();
            this.unbindViewToModel();
        },

        bindModelToView: function () {
            if (this.attributeBindings) {
                for (var i = 0; i < this.attributeBindings.length; i++) {
                    this.model.bind('change:' + this.attributeBindings[i], this.modelChange, this);
                }
            } else {
                this.model.bind('change', this.modelChange, this);
            }
        },
        bindViewToModel: function () {
            $(this.el).bind('change', this.viewChange);
            $(this.el).delegate('[contenteditable]', 'blur', this.viewChange);
        },

        unbindModelToView: function () {
            this.model.unbind('change', this.modelChange);
            this.model = undefined;
        },
        unbindViewToModel: function () {
            $(this.el).unbind('change', this.viewChange);
            //$(this.el).undelegate('', 'change', this.viewChange);
            $(this.el).undelegate('[contenteditable]', 'blur', this.viewChange);
        },

        modelChange: function () {
            var changeAttr;
            for (changeAttr in this.model.changedAttributes()) {
                this.modelToViewValue(changeAttr);
            }
        },

        viewChange: function (event) {
            var el = $($(event.target)[0]),
                attrName = this.getAttrBinding(el);

            if (!this.attributeBindings || _.indexOf(this.attributeBindings, attrName) !== -1) {
                this.viewToModelValue(el, attrName);
            }
        },

        modelToViewValue: function (attr) {
            var value = this.model.get(attr),
                elBindings = this.getElBindings(attr);

            for (var i = 0; i < elBindings.length; i++) {
                this.elValue($(elBindings[i]), value);
            }
        },

        viewToModelValue: function (el, attr) {
            var value = this.getViewElValue(el);

            this.elViewValue(attr, value);
        },

        elValue: function (el, value) {
            if (el.attr('type')) {
                var oldValue;
                switch (el.attr('type')) {
                    case 'radio':
                        oldValue = value;
                        if (el.val() === value) {
                            el.attr('checked', 'checked');
                        }
                        break;
                    case 'checkbox':
                        oldValue = el.attr('checked');
                        if (value) {
                            el.attr('checked', 'checked');
                        } else {
                            el.removeAttr('checked');
                        }
                        break;
                    default:
                        oldValue = el.val();
                        el.val(value);
                        break;
                }

                if (oldValue !== value) {
                    el.change();
                }

            } else if (el.is('input') || el.is('select') || el.is('textarea')) {
                if (el.val() !== value) {
                    el.val(value).change();
                }
            } else {
                el.text(value);
            }
        },

        elViewValue: function (attr, value) {
            this.model.set(attr, value);
        },

        getViewElValue: function (el) {
            switch (el.attr('type')) {
                case 'checkbox':
                    return el.prop('checked') ? true : false;
                default:
                    if (el.attr('contenteditable') !== undefined) {
                        return el.html();
                    } else {
                        return el.val();
                    }
            }
        },

        getElBindings: function (attr) {
            return $(this.el).find('[data-bind=' + attr + ']');
        },

        getAttrBinding: function ($el) {
            return $el.attr('data-bind');
        }
    };

    _.extend(Backbone.MdModelBinder.prototype, binder);

})();
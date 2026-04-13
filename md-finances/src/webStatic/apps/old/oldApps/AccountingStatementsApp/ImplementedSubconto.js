(function(module) {

    var subcontoOptions = {
        
        Kontragent: function () {
            var object = {
                    id: 'Kontragent',
                    placeholder: 'Контрагент',
                    view: '',
                    autocomplete: ''
                };

            object.view = this.getInput(object.id, object.placeholder);
            object.autocomplete = subcontoAutokomplete;

            return object;
        },
        
        Good: function() {
            var object = {
                    id: 'StockProduct',
                    placeholder: 'Товар',
                    view: '',
                    autocomplete: ''
                };

            object.view = this.getInput(object.id, object.placeholder);
            object.autocomplete = subcontoAutokomplete;

            return object;
        },
        
        getInput: function(id, placeholder) {
            return '<div><input type="text" class="medium" data-bind="' + id + '" placeholder="' + placeholder + '" id="' + id + '" name="' + id + '"><span name="' + id + '" data-bind=""></span></div>';
        }

    };



    var subcontoAutokomplete = function (input, view, type, object) {

        var options = {
            onSelect: function (item) {
                view.onSelectFunc(item, type);
                view.changeAssotiateSpan(input, item.label || item.object.Name);
            },
            getData: function() {
                return {
                    type: type
                };
            },
            isStockEnabled: true
        };

        input.subcontoAutocompleteForPostings(_.extend(options, switchforAutocompliteDependingOnType(type, options, input)));

        if (object) {
            input.val(object.Name);
            input.next().text(object.Name);
        }
    };

    var switchforAutocompliteDependingOnType = function (type, options) {
        switch(type) {
            case Common.Data.SubcontoType.Kontragent:
                return {
                    onCreate: function (autocomplete) {
                        subcontoKontragentCreateFunction(options, autocomplete);
                    }
                };
            case Common.Data.SubcontoType.Good:
                return {
                    onCreate: function (autocomplete) {
                        subcontoGoodsCreateFunction(options, autocomplete);
                    },
                    settings: {
                        addLinkName: 'новый товар / материал'
                    }
                };
        }
    };

    var subcontoKontragentCreateFunction = function (options, autocomplete) {
            var model = new Common.Models.Dialogs.Kontragent({
                Name: autocomplete.el.val()
            });
            var dialog = new Common.Views.Dialogs.KontragentDialogView({
                model: model
            });
            dialog.render();
            
            dialog.model.on("save", function () {
                autocomplete.el.val(dialog.model.get("Name"));
                options.onSelect({ object: dialog.model.toJSON() });
                model = null;
                dialog.remove();
            });
            
            dialog.model.on("cancel", function () {
                autocomplete.el.val("").change();
                model = null;
                dialog.remove();
            });
    };

    var subcontoGoodsCreateFunction = function(options, autocomplete) {
        PrimaryDocuments.Utils.StockProductDialogHelper.showDialog(null, function(model) {

            options.onSelect({ object: model });
            autocomplete.el.val(model.ShortName);

        }, function() {
            autocomplete.el.val("").change();
            model = null;
        });
    };

    module.Options.GetSubcontoOptions = function (subcontoType) {
        
        if (subcontoOptions[subcontoType]) {
            return subcontoOptions[subcontoType]();
        }

        return null;

    };

    module.Options.GetEntityIdByType = function (entity, type) {
        var object = {};
        object.SubcontoType = type;
        object.Name = entity.object.ShortName || entity.object.Name;
        object.Id = entity.object.SubcontoId ? entity.object.SubcontoId : entity.object.StockProductId || entity.object.Id;

        return object;
    };

})(AccountingStatements);
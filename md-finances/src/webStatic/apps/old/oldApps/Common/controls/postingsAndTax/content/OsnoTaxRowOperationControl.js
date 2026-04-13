/* eslint-disable */
import ContractKind from '@moedelo/frontend-enums/mdEnums/ContractKind';

(function (common) {
    var parent = common.Controls.TaxRowOperationControl;

    common.Controls.OsnoTaxRowOperationControl = parent.extend({

        template: "OsnoTaxRowOperationControlTemplate",
        emptyRowTemplate: "OsnoTaxEditItemTemplate",
        
        initializeEvents: function () {
            parent.prototype.initializeEvents.call(this);

            this.model.get("ManualPostings").on("change:Direction change:Incoming change:Outgoing", function (model) {
                this.complexSelectCheck(model);
            }, this);
            
            this.model.get("ManualPostings").on("change:Type", function (model) {
                this.checkKindSelect(model);
            }, this);
        },

        onRender: function () {
            parent.prototype.onRender.call(this);
            this.model.get("ManualPostings").each(function (model) {
                this.complexSelectCheck(model);

                if(this.options.readonly){
                    var $row = this.$("#" + model.cid);

                    if(model.get('Kind') === ContractKind.Conventional){
                        $row.find("[data-cell=Kind] :first-child").remove();
                    }

                    if(model.get('Direction') === common.Data .TaxPostingsDirection.Incoming){
                        $row.find("[data-cell=NormalizedCostType] :first-child").remove();
                    }
                }

            }, this);
        },

        afterNewItemAdded: function (model) {
            parent.prototype.afterNewItemAdded.call(this, model);
            this.applySelectPlugin(model);
            this.complexSelectCheck(model);
        },

        applySelectPlugin: function (model) {
            var modelRow = this.$("#" + model.cid);
            modelRow.find(".ttType select, .ttKind select").mdSelectUls();
            modelRow.find(".ttNormalizedCostType select").mdSelectUls({ overflow: true });
        },

        complexSelectCheck: function (model) {
            this.checkOsnoSelects(model);
            this.checkKindSelect(model);
        },

        initializeControls: function () {
            parent.prototype.initializeControls.call(this);

            this.$(".ttType select, .ttKind select").mdSelectUls();
            this.$(".ttNormalizedCostType select").mdSelectUls({overflow: true});
        },

        checkOsnoSelects: function (model) {
            var $row = this.$("#" + model.cid);
            
            this.showAllSelects($row);
            if (model.get("Direction") == common.Data .TaxPostingsDirection.Incoming) {

                $row.find("[data-cell=NormalizedCostType] :first-child").hide();
                model.set("NormalizedCostType", 0);
                this.showSpecialSelectOptions($row, "Type", [common.Data.OsnoTransferType.OperationIncome, common.Data.OsnoTransferType.NonOperating]);
                this.showSpecialSelectOptions($row, "Kind", [common.Data.OsnoTransferKind.Service,
                    common.Data.OsnoTransferKind.ProductSale, common.Data.OsnoTransferKind.PropertyRight, common.Data.OsnoTransferKind.OtherPropertySale]);
                
            } else if (model.get("Direction") == common.Data.TaxPostingsDirection.Outgoing) {
                this.showSpecialSelectOptions($row, "Type", [common.Data.OsnoTransferType.Direct, common.Data.OsnoTransferType.Indirect, common.Data.OsnoTransferType.NonOperating]);
            } else {
                $row.find("select").attr("disabled", true);
            }
        },

        checkKindSelect: function (model) {
            var $row = this.$("#" + model.cid),
                type = parseInt(model.get("Type"));

            $row.find("[data-cell=Kind] :first-child").show();
            $row.find("[data-cell=Kind] select")
                .removeAttr("disabled")
                .find("option").show();

            if (type === common.Data.OsnoTransferType.NonOperating) {
                $row.find("[data-cell=Kind] :first-child").hide();
                model.set("Kind", 0);
            }

            var incomingAvailableKinds = [
                common.Data.OsnoTransferKind.Service,
                common.Data.OsnoTransferKind.ProductSale,
                common.Data.OsnoTransferKind.PropertyRight,
                common.Data.OsnoTransferKind.OtherPropertySale
            ];

            var kind = parseInt(model.get('Kind'));
            if (type == common.Data.OsnoTransferType.OperationIncome && !_.contains(incomingAvailableKinds, kind)) {
                model.set('Kind', common.Data.OsnoTransferKind.Service);
            }

            if (model.get("Direction") == common.Data.TaxPostingsDirection.Outgoing) {
                switch (type) {
                    case common.Data.OsnoTransferType.Indirect:
                        this.showSpecialSelectOptions($row, "Kind", [common.Data.OsnoTransferKind.Material,
                            common.Data.OsnoTransferKind.Salary, common.Data.OsnoTransferKind.Amortization, common.Data.OsnoTransferKind.OtherOutgo]);
                        break;
                    case common.Data.OsnoTransferType.Direct:
                        this.showSpecialSelectOptions($row, "Kind", [common.Data.OsnoTransferKind.Material]);
                        break;
                }
            }
        },

        showAllSelects: function ($row) {
            $row.find("select")
                .removeAttr("disabled")
                .find("option").show();
            $row.find("[data-cell=NormalizedCostType] :first-child").show();
            $row.find("[data-cell=Kind] :first-child").show();
        },
        
        showSpecialSelectOptions: function($row, cellName, optionValues) {
            var $select = $row.find("[data-cell=" + cellName + "] select");
            var mdSelectPlugin = $select.data('MdSelect');

            if (mdSelectPlugin) {
                mdSelectPlugin.showOptions(optionValues);
            }

            $select.find("option")
                    .hide()
                    .filter(function () {
                        return optionValues.indexOf(parseInt(this.value)) != -1;
                    })
                    .show();
            
            if (optionValues.indexOf(parseInt($select.val())) == -1) {
                var firstValue = $select.find("option:visible").first().val();
                $select.val(firstValue);
            }
            $select.trigger("change");
        },

        TaxDirectives: _.extend({}, {
            Type: {
                text: function () {
                    return common.Utils.OsnoTypesForSelectGetter.getTransferTypeLabel(this.Type);
                },
                html: function (target) {
                    $(target.element).find("option[value=" + this.Type + "]").attr("selected", true);
                }
            },
            Kind: {
                text: function () {
                    if (this.Type === common.Data.OsnoTransferType.NonOperating) {
                        return "";
                    }
                    return common.Utils.OsnoTypesForSelectGetter.getTransferKindLabel(this.Kind);
                },
                html: function (target) {
                    if (this.Type !== common.Data.OsnoTransferType.NonOperating) {
                        $(target.element).find("option[value=" + this.Kind + "]").attr("selected", true);
                    }
                }
            },
            NormalizedCostType: {
                text: function () {
                    return common.Utils.OsnoTypesForSelectGetter.getNormalizedCostLabel(this.NormalizedCostType);
                },
                html: function (target) {
                    $(target.element).find("option[value=" + this.NormalizedCostType + "]").attr("selected", true);
                }
            }
        }, parent.prototype.TaxDirectives)
        
    });

})(Common);


const templates = require.context(`../templates`, true, /\.html$/);

(function (accountingPosting, common) {
    accountingPosting.Views.TooltipTaxPostingsView = common.Views.BaseView.extend({
        templateUrl: templates,
        template: 'tooltipTaxPostings',

        elementId: _.uniqueId("md-tax-posting-tooltip-"),
        
        initialize: function (options) {
            this.$el = this.el = this._getOrCreateContainer();
            this.collection = new accountingPosting.Collections.TaxPostingCollection();
            this.$el.hide();
            this.render();
        },

        _getOrCreateContainer: function () {
            var container = $("#" + this.elementId);
            if (container.length == 0) {
                container = $("<div />").attr("id", this.elementId).addClass("md-tax-posting-tooltip mdTooltip tableTooltip");
                $('body').append(container);
            }

            return container;
        },

        show: function(id, target) {
            var view = this;
            this.collection.load(id, {
                success: function (collection) {
                    if (target.is(":visible") && target.hasClass("hover")) {
                        view._showTooltip();
                    }
                }
            });
        },
        
        _showTooltip: function () {
            if (!this.rendered) {
                this.on("renderComplete", this._showTooltip);
                return;
            } else {
                this.off("renderComplete", this._showTooltip);
            }

            this.fillTemplate();
            if (this.checkEmptyOfPopup()) {
                this.hideLinkDocIfItNotExist();
                this.hideEmptyColunm();
                this.$el.show();
            }
        },
        
        hideEmptyColunm: function() {
            var date = new Date();
            const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            var taxation = ts.Current(date);
            if (taxation && taxation.get("IsOsno")) {
                this.$el.find(".destination").hide();
            } else {
                this.$el.find(".kind").hide();
                this.$el.find(".type").hide();
                this.$el.find(".normalizedCostType").hide();
            }
        },
        
        hideLinkDocIfItNotExist: function () {
            if (this.$el.find(".documentName").length == 0) {
                this.$(".linkDoc").hide();
            } else {
                this.$(".linkDoc").show();
            }
        },

        checkEmptyOfPopup: function() {
            var self = this,
                content = self.$(".taxPostings").html().length + this.$el.find(".documentName").length;

            if (!content) {
                return false;
            }
            
            return true;
        },

        clearCash: function() {
            this.collection.loaded = {};
        },

        getDataForRender: function () {
            return {
                List : this.collection.toJSON()
            };
        },

        getDirectives: function() {
            return {
                List: {
                    MainPostings: {
                        Incoming: common.Utils.DirectiveHelper.textMoneyDirectiveWithZero("Incoming"),
                        
                        Outgoing: common.Utils.DirectiveHelper.textMoneyDirectiveWithZero("Outgoing"),
                        
                        Type: {
                            text: function () {
                                return common.Utils.OsnoTypesForSelectGetter.getTransferTypeLabel(this.Type);
                            }                            
                        },
                        
                        Kind: {
                            text: function () {
                                if (this.Kind == 0) {
                                    return "";
                                }
                                return common.Utils.OsnoTypesForSelectGetter.getTransferKindLabel(this.Kind);
                            }                            
                        },

                        NormalizedCostType: {
                            text: function () {
                                return common.Utils.OsnoTypesForSelectGetter.getNormalizedCostLabel(this.NormalizedCostType);
                            }                            
                        }
                    },
                    LinkedDocuments: {
                        Type: {
                            text: function () {
                                return common.Utils.Converter.capitaliseFirstLetter(common.Data.DocumentTypeHelper.getAccountingDocumentTypeName(this.Type));
                            }
                        },
                        
                        Postings: {
                            Incoming: common.Utils.DirectiveHelper.textMoneyDirectiveWithZero("Incoming"),

                            Outgoing: common.Utils.DirectiveHelper.textMoneyDirectiveWithZero("Outgoing"),
                            
                            Type: {
                                text: function () {
                                    return common.Utils.OsnoTypesForSelectGetter.getTransferTypeLabel(this.Type);
                                }
                            },

                            Kind: {
                                text: function () {
                                    if (this.Kind == 0) {
                                        return "";
                                    }
                                    return common.Utils.OsnoTypesForSelectGetter.getTransferKindLabel(this.Kind);
                                }
                            },
                            
                            NormalizedCostType: {
                                text: function () {
                                    return common.Utils.OsnoTypesForSelectGetter.getNormalizedCostLabel(this.NormalizedCostType);
                                }                            
                            }
                        }
                    }
                }
            };
        }
    });
})(AccountingPosting, Common);
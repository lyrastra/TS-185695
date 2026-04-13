const templates = require.context(`../templates`, true, /\.html$/);

(function (accountingPosting, common) {
    accountingPosting.Views.TooltipPostingsView = common.Views.BaseView.extend({
        templateUrl: templates,
        template: 'tooltipPostings',

        elementId: _.uniqueId("md-posting-tooltip-"),
        
        initialize: function (options) {
            this.$el = this.el = this._getOrCreateContainer();
            this.collection = new accountingPosting.Collections.AccountingDocumentPostingCollection();
            this.$el.hide();
            this.render();
        },

        _getOrCreateContainer: function () {
            var container = $("#" + this.elementId);
            if (container.length == 0) {
                container = $("<div />").attr("id", this.elementId).addClass("md-posting-tooltip mdTooltip tableTooltip");
                $('body').append(container);
            }

            return container;
        },

        show: function (id, target, isBaseId) {
            var view = this;
            this.collection.load(id, isBaseId, {
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
                this.$el.show();
            }
        },

        checkEmptyOfPopup: function() {
            var self = this,
                content = self.$(".postings").html().length;

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
                    Sum: common.Utils.DirectiveHelper.textMoneyDirective("Sum")
                }
            };
        }
    });
})(AccountingPosting, Common);
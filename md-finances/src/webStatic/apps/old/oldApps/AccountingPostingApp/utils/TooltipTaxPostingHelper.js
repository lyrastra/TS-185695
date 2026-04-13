(function (accountingPosting) {
    
    accountingPosting.Utils.TooltipTaxPostingHelper = {
        _getOrCreateView: function (options) {
            if (!this.tootipView) {
                this.tootipView = new accountingPosting.Views.TooltipTaxPostingsView();
            }
            
            return this.tootipView;
        },
            
        show: function (id, options) {
            /// <summary></summary>
            /// <param name="id" type="Object">AccountingDocumentId</param>
            /// <param name="options" type="Object">
            /// options = {
            ///     target: элемент к которому 'крепится' tooltip 
            /// }
            /// </param>

            var view = this._getOrCreateView();
            var target = $(options.target),
                additionalClass = options.additionalClass;

            view.$el.css({
                top: target.offset().top + target.outerHeight(),
                left: target.offset().left + target.width() - view.$el.outerWidth()
            });
            if (additionalClass) {
                view.$el.addClass(additionalClass);
            }
            view.show(id, target);
        },
        
        showMessage: function(message) {
            var view = this._getOrCreateView();
            view.$el.html(message);
            this.$el.show();
        },
        
        hide: function () {
            if (this.tootipView) {
                this.tootipView.$el.hide();
            }
        },
        
        clearCash: function () {
            if (this.tootipView) {
                this.tootipView.clearCash();
            }
        }
    };
})(AccountingPosting);
(function (accountingPosting) {
    
    accountingPosting.Utils.TooltipHelper = {
        _getOrCreateView: function (options) {
            if (!this.tootipView) {
                this.tootipView = new accountingPosting.Views.TooltipPostingsView(options);
            }
            
            return this.tootipView;
        },
            
        show: function (id, options, isBaseId) {
            /// <summary></summary>
            /// <param name="id" type="Object">AccountingDocumentId</param>
            /// <param name="options" type="Object">
            /// options = {
            ///     target: элемент к которому 'крепится' tooltip 
            /// }
            /// </param>

            var view = this._getOrCreateView();
            var target = $(options.target);

            view.$el.css({
                top: target.offset().top + target.outerHeight(),
                left: target.offset().left + target.width() - view.$el.outerWidth()
            });

            view.show(id, target, isBaseId);
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
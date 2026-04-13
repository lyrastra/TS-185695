import UriHelper from '@moedelo/md-frontendcore/mdCommon/helpers/UriHelper/UriHelper';

(function(module) {

    module.Models.SubcontoModel = Backbone.Model.extend({

        urlRoot: WebApp.ChartOfAccount.SubcontoLevelForAccount,
        
        load: function (syntheticAccountTypeId, success, { cashId, settlementAccountId }) {
            if (syntheticAccountTypeId !== undefined) {
                this.url = UriHelper.addParams(this.urlRoot, { syntheticAccountTypeId, cashId, settlementAccountId });
                this.fetch({
                    success: success,
                    error: function () { }
                });
            }
        }
        
    });

})(Common);

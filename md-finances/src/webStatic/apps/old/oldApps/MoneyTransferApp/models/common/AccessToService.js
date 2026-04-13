(function (money, common) {
    money.Models.Common.AccessToService = Backbone.Model.extend({
        initialize: function (page) {
            if (!page) {
                throw "access to which page do you want?";
            }
            if (page == common.Data.Applications.Money.Code) {
                this.url = WebApp.UserFirmRule.GetMoneyPermissions;
            }
            if (page == common.Data.Applications.Sales.Pages.Office) {
                this.url = WebApp.UserFirmRule.GetAdditionalDocumentsPermissions;
                this.clear({ silent: true });
                this.set("direction", 1);
            }

            if (page == common.Data.Applications.Buy.Code) {
                this.url = WebApp.UserFirmRule.GetAdditionalDocumentsPermissions;
                this.clear({ silent: true });
                this.set("direction", 2);
            }
        },

        loaded: false,
        isError: false,

        sync: function (method, model, options) {
            var success = options.success;

            options.success = function (resp, status, xhr) {
                model.loaded = true;
                if (success) success(resp, status, xhr);
            };

            options.error = function (xhr, ajaxOptions, thrownError) {
                model.loaded = true;
                model.isError = true;
            };
            Backbone.Model.prototype.sync.call(this, method, model, options);
        }
    });

})(Money, Common);

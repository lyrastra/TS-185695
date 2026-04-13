(function (money) {

    money.Models.Common.IntegrationsAndDischarges = Backbone.Model.extend({
        
        loaded: false,
        isError: false,

        skipFunctionName: "SkipIntegratedFile",
        importFunctionName: "ImportIntegratedFile",
        serviceName: "/Requisites/Integrations/",


        skipIntegration: function (id, success) {
            var model = this;
            $.ajax({
                type: "POST",
                url: model.serviceName + model.skipFunctionName,
                contentType: "application/json",
                dataType: (jQuery.browser.msie) ? "text" : "xml",
                data: id,
                async: false,
                success: function (data) {
                    IntegrationsWidget.skipingIntegration();
                    success();
                },
                error: function () {
                    ToolTip.globalMessage(1, false, "Произошла непредвиденная ошибка. Просьба сообщить об этом в техническую поддержку онлайн-сервиса «Моё дело», написав на почту <a href='mailto:support@moedelo.org'>support@moedelo.org</a> или позвонив по телефону 8 800 200 77 27. Мы устраним причины её появления в максимально короткие сроки. Сейчас вы можете перейти на любую из предыдущих страниц или на главную страницу сайта. Приносим свои извинения за доставленные неудобства!");
                }
            });
        },

        sendToServiceJSONSync: function (id) {
            var model = this;
            var responseText = $.ajax(
            {
                type: "POST",
                url: model.serviceName + model.importFunctionName,
                contentType: "application/json; charset=utf-8;",
                dataType: "json",
                data: $.toJSON(id),
                async: false,
                traditional: false
            }).responseText;

            var result = $.parseJSON(responseText);
            if (result.hasOwnProperty("d")) {
                return result.d;
            }
            else {
                return result;
            }
        },
        
        sync: function (method, model, options) {
            var success = options.success;
            
            options.success = function (resp, status, xhr) {
                model.loaded = true;
                if (success) success(resp, status, xhr);
            };
            
            options.error = function(xhr, ajaxOptions, thrownError) {
                model.loaded = true;
                model.isError = true;
            };
            Backbone.Model.prototype.sync.call(this, method, model, options);
        }
    });

})(Money.module("Models.Common"));

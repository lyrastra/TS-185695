(function (common) {

    common.Mixin.documentStateMixin = {
            
        documentStateMixin: {
            self: this,

            makeReadOnly: function () {
                var mixin = this;
                self.$("#saveDocument").off();

                self.$("input").each(function () {
                    $(this).attr("readonly", true);

                    if ($(this).is(":data(datepicker)")) {
                        $(this).removeAttr("disabled");
                    }

                    if ($(this).is(":data(autocomplete)")) {
                        $(this).autocomplete("destroy");
                    }
                });

                self.$("input[type=radio], input[type=checkbox]").each(function () {
                    $(this).attr("disabled", true);
                });

                self.$("select").each(function () {
                    $(this).attr("disabled", true);
                });

                self.$("textarea").each(function () {
                    $(this).attr("readonly", true);
                });

                self.$(".mdButton").addClass("disabled");

                mixin.makeReadOnlyLinksAndSpans();
                
                mixin.documentFormBlockDisabling();
            },
            
            makeReadOnlyLinksAndSpans: function (string) {
                var mixin = this;

                _.each(self.$(".generalFields").find("a, span").filter(mixin.exscludeFromBlock).not("[data-readonly=enable]"), mixin.disableLink);
                _.each(self.$(".document_items").find("a, span").filter(mixin.exscludeFromBlock).not("[data-readonly=enable]"), mixin.disableLink);
            },

            makeRequestForPayment: function (elem) {
                var companyId = Md.Core.Engines.CompanyId;

                _.each(elem, function (val) {
                    var $el = $(val);
                    var url = companyId.getLinkWithParams('/Pay/');

                    $el.addClass('disabled');
                    $el.unbind('click').bind('click', function() {
                        ToolTip.popupMessage('Для сохранения <a href="' + url + '">оплатите</a> доступ к сервису.', $el);
                        return false;
                    });
                });
            },
            
            disableLink: function(link) {
                $(link).addClass("disabled-link").unbind("click").bind("click", function () { return false; });
            },

            documentFormBlockDisabling: function() {
                this.disableLink(self.$("#deleteDocument"));
            },
            
            notBlockingThisElements: function(array) {
                if (array.length) this.notBlockingElements = array;
            }
        }
    };

})(Common);

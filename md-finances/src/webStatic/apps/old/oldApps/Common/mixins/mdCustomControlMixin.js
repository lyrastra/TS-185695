(function (commonMixin) {

    'use strict';

    commonMixin.SetMdNumberInput = {
        setMdNumberPlugin: function () {
            this.$('[data-number]').mdNumberInputMask();
        }
    };

    commonMixin.ButtonMixin = {        
        setDropDownButton: function(event) {
            if ($(event.target).closest("ul").hasClass("mdDropDownList")) {
                return;
            }

            var openLink = $(event.currentTarget) || $(event.target);
            if (openLink.hasClass("actionLink") || openLink.closest(".arrowWrapper").length) {
                openLink = openLink.closest(".download");
            }

            var list = openLink.find(".docFormat_list");
            if (!list.length) {
                list = openLink.parent().find(".docFormat_list");
            }
            openLink.toggleClass("active");

            if (list.is(":visible")) {
                list.hide();
            } else {
                list.show();
            }
        }
    };

    $(document).on('click', function(event) {
        var element = $(event.target),
            currentDropDown = element.closest('.mdDropDown');

        if (currentDropDown.length) {
            var current = $('.mdDropDown').index(currentDropDown);
            $('.mdDropDown .mdDropDownList:not(:eq(' + current + '))').hide();
        } else {
            $(".mdDropDown .mdDropDownList").hide();
        }
    });
    
})(Common.Mixin);
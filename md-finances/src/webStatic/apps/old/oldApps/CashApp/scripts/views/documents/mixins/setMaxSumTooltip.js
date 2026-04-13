(function (cash, common) {

    'use strict';

    var text = 'Предельный размер расчетов наличными денежными средствами в рамках предпринимательской деятельности по одному договору не должен превышать 100 тыс. руб.';

    cash.Views.setMaxSumTooltipMixin = {
        setMaxSumTooltip: function () {
            var icon = $('<span class=qtip_icon />');
            this.$('[data-bind="Sum"]').siblings('label').after(icon);

            icon.qtip({
                style: { classes: "qtip-yellow withTail newWave", width: 280 },
                position: { my: "bottom center", at: "top center" },
                content: { text: text },
                hide: { fixed: true }
            });
        }
    };

})(Cash, Common);
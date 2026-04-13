import ClosedPeriodUtils from '@moedelo/frontend-common/helpers/ClosedPeriodUtils';

(function (bank) {

    bank.ClosedPeriodIcon = Marionette.ItemView.extend({
        template: function() {
            return '<span class="closedPeriodLabel closedPeriodLabel-withIcon">{0}</span>'.format('В закрытом периоде');
        },

        events: {
            'click .closedPeriodLabel': 'showOpenPeriodDialog'
        },

        showOpenPeriodDialog: function() {
            var date = this.model.get('Date');
            var link = this.$('.closedPeriodLabel');
            if (link.hasClass('disabled')) {
                return;
            }

            link.addClass('disabled');

            ClosedPeriodUtils.showOpenPeriodDialog({
                date,
                $target: link
            }).then(
                () => window.location.reload(),
                () => link.removeClass('disabled')
            );
        }
    });

})(Bank);
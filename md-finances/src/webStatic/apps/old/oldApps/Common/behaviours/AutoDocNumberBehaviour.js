/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function(common) {

    common.Behaviours.AutoDocNumberBehaviour = Marionette.Behavior.extend({
        events: {
            'keyup [data-bind=Number]': 'disableAutoNumberUpdate'
        },

        onRender: function() {
            this.view.model.set('NumberSetManually', false);
            this.view.model.on('change:Date', this.onDateChange, this);
            this.view.model.on('change:Number', this.onNumberChange, this);

            var events = this.getOption('behaviorEvents');
            if (events) {
                this.listenTo(this.view.model, events, this.updateDocNumber);
            }
        },

        onDateChange: function (model, value) {
            var dateFormat = 'DD.MM.YYYY',
                newDate = dateHelper(value, dateFormat),
                oldDate = dateHelper(model.previous('Date'), dateFormat);

            if (!newDate.isSame(oldDate, 'year')) {
                this.updateDocNumber();
            }
        },

        onNumberChange: function (model, value) {
            this.view.$('[data-bind=Number]').val(value).blur();
        },

        disableAutoNumberUpdate: function () {
            var currentValue = this.view.$('[data-bind=Number]').val();
            if (this.view.model.previous('Number') != currentValue) {
                this.view.model.set('NumberSetManually', true);
            }
        },

        updateDocNumber: function () {
            var model = this.view.model;
            if (model.get('Id') !== 0 || model.get('NumberSetManually')) {
                return;
            }

            var handler = this.getOption('getNumber') || this._getNumber.bind(this);
            handler().then(function (response) {
                model.set('Number', response.Value);
            }.bind(this));
        },

        _getNumber: function() {
            if (!_.isFunction(this.view.model.getAutoDocNumberUrl)) {
                throw new Error("Unable update doc number - document model has no method 'getAutoDocNumberUrl'");
            }

            var url = this.view.model.getAutoDocNumberUrl();

            return $.ajax(url, {
                data: {
                    date: this.view.model.get('Date'),
                    direction: this.view.model.get('Direction')
                }
            });
        }
    });

})(Common);

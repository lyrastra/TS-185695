/* eslint-disable */
(function (cash) {

    'use strict';

    var accountablePerson = Marionette.ItemView.extend({
        onRender: function() {
            this.bind();

            const isOoo = (new Common.FirmRequisites()).get(`IsOoo`);
            this.model.set({ WithIpAsWorker: !isOoo });

            cash.Views.initWorkerAutocompleteMixin.initWorkerAutocomplete.call(this);

            this.initAdvanceStatement();
            this.$('[data-type=float]').decimalMask();
        },

        onDestroy: function(){
            this.$('[data-bind=WorkerName]').workerAutocomplete('destroy');

            this.model.unset('WorkerId');
            this.model.unset('WorkerName');
        }
    });

    cash.Views.toAccountablePerson = accountablePerson.extend({
        template: '#ToAccountablePersonTemplate',

        initAdvanceStatement: function () {
            var advanceStatements = this.model.get('AdvanceStatements');
            if (!advanceStatements || advanceStatements.length === 0) {
                this.$('.js_advanceStatementList').parents('.fieldRow').remove();
                return;
            } else {
                /* Только ИП ОСНО */
                const selectedDate = Converter.toDate(this.model.get('Date'));
                const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
                const taxSystem = ts.Current(selectedDate);
                const isOoo = (new Common.FirmRequisites()).get(`IsOoo`);
                const isIpOsno = taxSystem.get(`IsOsno`) && !isOoo;

                isIpOsno && this.$(`[data-bind=Sum]`).attr(`disabled`, `disabled`);
                /* конец Только ИП ОСНО */
            }

            var html = advanceStatements.map(function(item) {
                return String.format('<div><a href="/AccDocuments/AdvanceStatement/Edit/{0}">{1}, {2} р.</a></div>',item.DocumentBaseId, item.Name, Converter.toAmountString(item.DocumentSum));;
            }).join('');
            this.$('.js_advanceStatementList').html(html);
        }
    });

    cash.Views.fromAccountablePerson = accountablePerson.extend({
        template: '#FromAccountablePersonTemplate',

        initAdvanceStatement: function () {
            this.model.off('change:WorkerId', this.cleanAdvanceStatement);
            this.model.on('change:WorkerId', this.cleanAdvanceStatement, this);

            this.initAdvanceStatementAutocomplete();
        },

        initAdvanceStatementAutocomplete: function () {
            var model = this.model;
            var $input = this.$('[data-bind=AdvanceStatement]');

            var advanceStatement = _.first(this.model.get('AdvanceStatements'));
            if (advanceStatement) {
                $input.val(advanceStatement.Name);
            }

            $input.advanceStatementAutocomplete({
                onSelect: function (selected) {
                    model.set({
                        AdvanceStatements:  [{
                            DocumentBaseId: selected.object.DocumentId,
                            LinkSum: model.get('Sum')
                        }]
                    });
                },
                getData: function () {
                    return {
                        workerId: model.get('WorkerId')
                    };
                },
                clean: function () {
                    model.unset('AdvanceStatements');
                },
                onBlur: function() {
                    if(!model.get('KontragentName')) {
                        model.unset('KontragentId');
                    }
                }
            });
        },

        cleanAdvanceStatement: function () {
            this.model.unset('AdvanceStatements');

            this.$('[data-bind=AdvanceStatement]').change();
        }
    });

})(Cash);

(function(money) {
    'use strict';

    // написать
    money.Models.Common.MainModel = Backbone.Model.extend({

        url: '/Accounting/MoneyTransferTable/GetTotal',

        // Sorting (Date, Incoming, Kontragent, etc.
        Sort: 'date',
        SortDirection: 1,

        // TYPE ONLY (IncomingTransfer, OutgoingTransfer, MovementTransfer)
        Type: 'Type',
        TypeValue: '',

        // START DATE
        StartDate: 'StartDate',
        StartDateValue: '',

        // FINAL DATE
        FinalDate: 'FinalDate',
        FinalDateValue: '',

        // TIME FILTER (m-1, k-2, y-3)
        TimeFilter: 'TimeFilter',
        TimeFilterValue: '',

        // KONTRAGENT ID (INT)
        KontragentId: 'KontragentId',
        KontragentIdValue: '',

        // WORKER ID (INT)
        WorkerId: 'WorkerId',
        WorkerIdValue: '',

        // MOVEMENT TYPE (value)
        MovementType: 'MovementType',
        MovementTypeValue: '',

        BudgetarySubType: 'BudgetarySubType',
        BudgetarySubTypeValue: '',

        PayrollType: 'PayrollType',
        PayrollTypeValue: '',

        // SETTLEMENT FILTER (value)
        SettlementFilter: 'SettlementFilter',
        SettlementFilterValue: '',

        ProjectId: 'ProjectId',
        ProjectIdValue: '',

        BillId: 'BillId',
        BillIdValue: '',

        sync: function(method, model, options) {

            var params;

            var request = [
                { Key: this.Sort, Value: this.SortDirection },
                { Key: this.Type, Value: this.TypeValue },
                { Key: this.StartDate, Value: this.StartDateValue },
                { Key: this.FinalDate, Value: this.FinalDateValue },
                { Key: this.TimeFilter, Value: this.TimeFilterValue },
                { Key: this.KontragentId, Value: this.KontragentIdValue },
                { Key: this.WorkerId, Value: this.WorkerIdValue },
                { Key: this.MovementType, Value: this.MovementTypeValue },
                { Key: this.BudgetarySubType, Value: this.BudgetarySubTypeValue },
                { Key: this.PayrollType, Value: this.PayrollTypeValue },
                { Key: this.SettlementFilter, Value: this.SettlementFilterValue },
                { Key: this.ProjectId, Value: this.ProjectIdValue },
                { Key: this.BillId, Value: this.BillIdValue }
            ];

            params = _.extend({
                type: 'POST',
                contentType: 'application/json; charset=utf-8;',
                dataType: 'json',
                data: $.toJSON(request),
                url: this.url,
                processData: false
            }, options);

            return $.ajax(params);
        }
    });

})(Money);
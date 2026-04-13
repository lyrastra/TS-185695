(function (closingPeriod, common) {
    closingPeriod.Models.OpenClosedPeriodDialog = Backbone.Model.extend({
      
        load: function (success, error, context) {
            this.fetch({
                data: {
                    date: this.get("Date")
                },
                success: function (model, response) {
                    if (response && response.Status) {
                        success.call(context);
                    } else {
                        error.call(context);
                    }
                },
                error: function () {
                    error.call(context);
                },
                url: WebApp.ClosingPeriodWizard.GetClosedPeriod
            });
        },
        
        openPeriod: function (success, error, context) {
            this.fetch({
                data: {
                    date: this.get("StartDate") || this.get("Date")
                },
                success: function (model, response) {
                    if (response && response.Status) {
                        model.updateRequisites().then(function(){
                            success.call(context);
                        });
                    } else {
                        error.call(context);
                    }
                },
                error: function () {
                    error.call(context);
                },
                type: 'POST',
                url: WebApp.ClosingPeriodWizard.OpenPeriod
            });
        },
        
        updateRequisites: function() {
            var requisites = new Common.FirmRequisites;
            return requisites.reload();
        }
    });

})(ClosingPeriodValidation, Common);
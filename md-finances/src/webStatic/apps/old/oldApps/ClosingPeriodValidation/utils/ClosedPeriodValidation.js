(function (closingPeriod, common) {
    closingPeriod.Utils.ClosedPeriodValidation = {
        isValid: function (value, element) {
            var date = value;

            var requisites = common.Utils.CommonDataLoader.FirmRequisites;
            if (requisites) {
                return !requisites.inClosedPeriod(date);
            } else {
                common.Utils.CommonDataLoader.loadFirmRequisites();
                common.Utils.CommonDataLoader.waitForLoading([common.Utils.CommonDataLoader.FirmRequisites], function () {
                    $(element).blur();
                });
                
                return true;
            }
        }
    };
    
    var checkClosedPeriod = function (value, element) {
        return closingPeriod.Utils.ClosedPeriodValidation.isValid(value, element);
    };

    $.validator.addMethod("checkClosedPeriod", checkClosedPeriod);
    $.validator.unobtrusive.adapters.addBool("checkClosedPeriod");
    
})(ClosingPeriodValidation, Common);
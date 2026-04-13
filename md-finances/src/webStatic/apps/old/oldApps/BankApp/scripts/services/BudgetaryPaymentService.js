/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function (md) {

    md.Services = md.Services || {};

    md.Services.BudgetaryPaymentService = function(options){
        var model = options.model;
        initialize();

        return {
            isTradingFees: isTradingFees,
            isTax: isTax
        };

        function initialize(){
            updatePeriod();
            listenModelEvent('change:BudgetaryTaxesAndFees', updatePeriod);
        }

        function listenModelEvent(event, handler){
            model.off(event, handler);
            model.on(event, handler);
        }

        function isTradingFees(){
            var tradingFeesCode = '680900';
            var accountCode = '' + model.get('BudgetaryTaxesAndFees');
            return accountCode === tradingFeesCode;
        }

        function isTax(){
            var taxKbkPaymentType = '0';
            return model.get('KbkPaymentType').toString() === taxKbkPaymentType;
        }

        function updatePeriod(){
            var data = {
                MinDate: dateHelper(getMinDate()).format('DD.MM.YYYY'),
                CanEditCalendarType: true
            };

            var period = model.get('Period') || {};
            if(period.set){
                period.set(data);
            }
            else{
                _.extend(period, data);
            }
            model.set('Period', period);
        }

        function getMinDate(){
            var requisites = new Common.FirmRequisites();
            var registrationDate = parseDate(requisites.get('RegistrationDate'));

            if(isTradingFees()){
                var minDate = parseDate('01.07.2015');
                return minDate > registrationDate ? minDate : registrationDate;
            }

            return parseDate(_.max(['01.01.2011', requisites.get('RegistrationDate')], function(date) {
                return dateHelper(date, 'DD.MM.YYYY').valueOf();
            }));
        }

        function parseDate(date){
            return dateHelper(date, 'DD.MM.YYYY').toDate();
        }
    };

})(Md);

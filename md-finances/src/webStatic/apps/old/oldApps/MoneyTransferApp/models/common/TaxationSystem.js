(function (money, common) {

    money.Models.Common.TaxationSystem = Backbone.Model.extend({
        isUsn15: function () {
            return this.isUsn() && this.isProfitAndOutgo();
        },

        isUsn6: function() {
            return this.isUsn() && !this.isProfitAndOutgo();
        },

        isEnvd: function () {
            return this.get('IsEnvd');
        },

        isUsn: function () {
            return this.get('IsUsn');
        },
        
        isOsno: function() {
            return this.get('IsOsno');
        },
        
        isUsnAndEnvd: function() {
            return this.isEnvd() && this.isUsn();
        },

        isOsnoAndEnvd: function() {
            return this.isOsno() && this.isEnvd();
        },

        isProfitAndOutgo: function () {
            return this.get('UsnType') === common.Data.UsnTypes.ProfitAndOutgo;
        },

        getTaxSystemType: function(){
            var types = common.Data.TaxationSystemType;

            if(this.isOsno()){
                return this.isEnvd() ? types.OsnoAndEnvd : types.Osno;
            }

            if(this.isUsn()){
                return this.isEnvd() ? types.UsnAndEnvd : types.Usn;
            }

            return types.Envd;
        },

        defaultType: function() {
            var types = common.Data.TaxationSystemType;

            if (this.get('IsOsno')) {
                return types.Osno;
            }
            
            if (this.get('IsUsn')) {
                return types.Usn;
            }

            return types.Envd;
        }
    });

})(Money, Common);

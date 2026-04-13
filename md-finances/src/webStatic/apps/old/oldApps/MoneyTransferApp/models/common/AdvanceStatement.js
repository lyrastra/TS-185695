(function (money) {

    money.Models.Common.AdvanceStatement = Backbone.Model.extend({
        url: WebApp.AdvanceStatements.GetAdvanceStatement,

        getFullName: function () {
            var number = "б/н";
            if (this.get("Number") && this.get("Number").length) {
                number = "№ " + this.get("Number");
            }
            
            return number + " за " + this.get("Date") + " (" + this.get("Sum") + " р.)";
        },

        defaults: {
            "Id": 0,
            "Number": "",
            "Date": "",
            "Sum": 0
        },

        fetch: function (options) {
            options = _.extend(options, {
                data: {
                    id: this.get("Id")
                }
            });

            Backbone.Model.prototype.fetch.call(this, options);
        }
    });

})(Money);

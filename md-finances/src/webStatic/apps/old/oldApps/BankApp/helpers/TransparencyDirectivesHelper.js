(function (module) {

    var main = module.Helpers.Directives ? module.Helpers.Directives : module.Helpers.Directives = {};

    main.Bank = {
        List: {
            Id: {
                html: function (target) {
                    $(target.element).data("id", this.Id);
                    if (this.IsPrimary) {
                        $(target.element).addClass("active");
                    }
                },
                text: function () {
                    return '';
                }
            },
            CurrentBalance: {
                text: function() {
                    return $.trim(ValueCrusher.parseStr(this.CurrentBalance) + " р.");
                }
            },
            Name: {
                text: function () {

                    var name = this.BankName;

                    if (this.Number && this.Number.length > 4) {
                        name += " р/с ..." + this.Number.slice(this.Number.length - 4, this.Number.length);
                    }

                    return name;
                }
            }
        },
        IncomingBalance: {
            text: function () {
                return $.trim(ValueCrusher.parseStr(this.IncomingBalance) + " р.");
            }
        },
        Turnover: {
            text: function () {
                return $.trim(ValueCrusher.parseStr(this.Turnover) + " р.");
            }
        },
        OutgoingBalance: {
            text: function () {
                return $.trim(ValueCrusher.parseStr(this.OutgoingBalance) + " р.");
            }
        }
    };

})(Bank.module('Main'));
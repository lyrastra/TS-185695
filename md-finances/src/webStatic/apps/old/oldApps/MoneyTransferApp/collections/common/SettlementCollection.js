(function(money) {
    'use strict';

    money.Collections.Common.SettlementCollection = Backbone.Collection.extend({

        url: WebApp.SettlementAccounts.GetSettlementAccounts,

        loaded: false,
        isError: false,

        initialize: function(list) {
            if (list && list.length) {
                this.Deleted = new Backbone.Collection(_.where(list, { IsDeleted: true }));
            }
        },

        clearDeleted: function() {
            this.reset(this.where({ IsDeleted: false }));
        },

        parse: function(response) {
            if (response.Status) {
                this.Deleted = new Backbone.Collection(_.where(response.List, { IsDeleted: true }));
                return _.where(response.List, { IsDeleted: false });
            }
            return [];
        },

        isRoubleSettlements: function() {
            return !!this.find(function(model) {
                return !model.get('Currency');
            });
        },

        isCurrencySettlements: function() {
            return !!this.find(function(model) {
                return model.get('Currency');
            });
        },

        isCurrencySettlement: function(id) {
            return !!this.find(function(model) {
                return model.get('Id') === id && model.get('Currency');
            });
        },

        isDeleted: function(settlement) {
            return this.Deleted.where({ Number: settlement }).length > 0;
        },

        isDeletedId: function(id) {
            return this.Deleted.where({ Id: id }).length > 0;
        },

        getRoubleSettlements: function() {
            return this.filter(function(model) {
                return !model.get('Currency');
            });
        },

        getPrimaryAccount: function() {
            var primaryAccounts = this.where({ IsPrimary: true });
            if (primaryAccounts.length) {
                return primaryAccounts[0];
            } else {
                return this.first();
            }
        },

        getBySettlement: function(settlement) {
            if (this.isDeleted(settlement)) {
                return this.Deleted.where({ Number: settlement })[0];
            } else {
                return this.where({ Number: settlement })[0];
            }
        },

        getAllCurrencySettlementsById: function(id) {
            var settlements = this.getById(id);
            var currency = settlements.get('Currency');
            var deleted = this.Deleted.where({ Currency: currency });
            var active = this.where({ Currency: currency });

            return _.union(active, deleted).map(function(model) {
                return model.toJSON();
            });
        },

        getById: function(id) {
            if (this.isDeletedId(id)) {
                return this.Deleted.where({ Id: id })[0];
            } else {
                return this.where({ Id: id })[0];
            }
        },

        addToMainFromDeleted: function(id) {
            var isExistNotDeleted = this.where({ Id: id }).length > 0;
            if (!isExistNotDeleted && this.Deleted) {
                var deletedSettlement = this.Deleted.where({ Id: id });
                if (deletedSettlement.length > 0) {
                    this.add(deletedSettlement[0]);
                    return true;
                }
            }
            return false;
        },

        addToMainFromDeletedById: function(id) {
            var isExistNotDeleted = this.where({ Id: id, IsDeleted: false }).length > 0;
            if (!isExistNotDeleted && this.Deleted) {
                var deletedSettlement = this.Deleted.where({ Id: id });
                if (deletedSettlement.length > 0) {
                    this.add(deletedSettlement[0]);
                    return true;
                }
            }
            return false;
        },

        toFormatString: function(number) {
            var account = this.where({ Number: number });
            if (!account.length) {
                return number;
            }
            return account[0].get('Number') + ' в ' + account[0].get('BankName');
        },

        toFormatStringById: function(id) {
            var active = this.where({ Id: id });
            var deleted = this.Deleted.where({ Id: id });

            var account = _.union(active, deleted);

            if (!account.length) {
                return number;
            }
            return account[0].get('Number') + ' в ' + account[0].get('BankName');
        },

        sync: function(method, model, options) {
            var success = options.success;

            options.success = function(resp, status, xhr) {
                model.loaded = true;
                if (success) {
                    success(resp, status, xhr);
                }
            };

            options.error = function(xhr, ajaxOptions, thrownError) {
                model.loaded = true;
                model.isError = true;
            };
            Backbone.Model.prototype.sync.call(this, method, model, options);
        },

        clone: function() {
            var clonedCollection = new money.Collections.Common.SettlementCollection(this.toJSON());
            clonedCollection.Deleted = new Backbone.Collection(this.Deleted.toJSON());
            return clonedCollection;
        }
    });

})(Money.module('Collections.Common'));

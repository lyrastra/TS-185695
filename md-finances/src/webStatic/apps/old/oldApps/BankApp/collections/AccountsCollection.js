(function (bank) {

    bank.Collections.AccountsCollection = Backbone.Collection.extend({
        url: BankUrl.GetSettlementAccountsOfBankWithDeleted,
        
        parse: function(resp) {
            return resp.List;
        },
        
        getAccount: function (settlementId) {
            if (settlementId === 0) {
                return;
            }

            var account = this.getBySettlementId(settlementId);

            if (!account && this.length > 1) {
                return null;
            }

            if (!account) {
                account =  this.getPrimary();
            }

            if (!account) {
                account = this.first();
            }

            return account;
        },
        
        getBySettlementId: function(settlementId) {
            return this.find(function(account) {
                return account.get('Id') == settlementId;
            });
        },
        
        getPrimary: function() {
            return this.find(function (account) {
                return account.get("IsPrimary");
            });
        }
    });

})(Bank);

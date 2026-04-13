/* eslint-disable */
(function(bank) {
    bank.Utils.CommonDataLoader = {
        loadIncomingOperationTypes() {
            if (!this.IncomingOperationTypes) {
                this.IncomingOperationTypes = new bank.Collections.BaseDocumentFillingCollection({
                    url: BankUrl.GetIncomingOperationDictionary
                });
                this.IncomingOperationTypes.fetch();
            }
        },
        loadOutgoingOperationTypes() {
            if (!this.OutgoingOperationTypes) {
                this.OutgoingOperationTypes = new bank.Collections.BaseDocumentFillingCollection({
                    url: BankUrl.GetOutgoingOperationDictionary
                });
                this.OutgoingOperationTypes.fetch();
            }
        },
        loadBudgetaryPaymentSequences() {
            if (!this.BudgetaryPaymentSequences) {
                this.BudgetaryPaymentSequences = new bank.Collections.BaseDocumentFillingCollection({
                    url: BankUrl.GetBudgetaryPaymentSequences
                });
                this.BudgetaryPaymentSequences.fetch({
                    data: JSON.stringify({ isPaymentOrder: true })
                });
            }
        },
        getOperationTypes() {
            if (!this.OperationTypes) {
                this.OperationTypes = new bank.Models.OperationTypeCollection();
                this.OperationTypes.fillTypes();
            }

            return this.OperationTypes;
        }
    };
    _.extend(bank.Utils.CommonDataLoader, Common.Utils.CommonDataLoader);
}(Bank));

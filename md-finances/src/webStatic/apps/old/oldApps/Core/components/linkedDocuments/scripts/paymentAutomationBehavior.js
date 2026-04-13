/* eslint-disable no-param-reassign */
/* global $, _ */

import { paymentOrderOperationResources, cashOrderOperationResources } from '../../../../../../../resources/MoneyOperationTypeResources';

(function(md) {
    // eslint-disable-next-line no-param-reassign
    md.Behaviors.PaymentAutomation = Marionette.Behavior.extend({

        modelEvents: {
            'change:KontragentId change:ContractBaseId change:BillDocumentBaseId': `loadAutoReasonDocuments`,
            'change:Sum': `fillReasonDocuments`
        },

        loadAutoReasonDocuments() {
            const self = this;
            const { model } = self.view;

            if (!needAutoReasonDocuments(model)) {
                return;
            }

            $.ajax({
                url: self.options.autoDocsUrl,
                data: self.getAutoReasonDocumentsParams(model),
                success(response) {
                    self.AutoReasonDocuments = response.List;
                    self.fillReasonDocuments();
                }
            });
        },

        getAutoReasonDocumentsParams(model) {
            const data = {
                kontragentId: model.get(`KontragentId`)
            };
            const billBaseId = model.get(`BillDocumentBaseId`);
            const contractBaseId = model.get(`ContractBaseId`);

            if (contractBaseId) {
                data.contractBaseId = contractBaseId;
            }

            if (billBaseId) {
                data.billBaseId = billBaseId;
            }

            if (_.contains([
                paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value,
                paymentOrderOperationResources.PaymentOrderPaymentToSupplier.value,
                cashOrderOperationResources.CashOrderIncomingPaymentForGoods.value,
                cashOrderOperationResources.CashOrderOutgoingPaymentSuppliersForGoods.value
            ], parseInt(model.get(`OperationType`), 10))) {
                data.withUpd = true;
            }

            return data;
        },

        fillReasonDocuments() {
            const { model } = this.view;
            const self = this;

            if (needAutoReasonDocuments(model)) {
                const reasonDocuments = [];

                if (!model.get(`Sum`)) {
                    this.view.trigger(`resetDocuments`, reasonDocuments);

                    return;
                }

                let sum = 0;
                _.each(this.AutoReasonDocuments, (item) => {
                    if (sum >= model.get(`Sum`)) {
                        return;
                    }

                    const balance = self.roundSum(model.get(`Sum`) - sum);
                    item.ReceivedSum = item.UnpaidBalance;

                    if (item.UnpaidBalance > balance) {
                        item.ReceivedSum = balance;
                    }

                    reasonDocuments.push(item);
                    sum += item.UnpaidBalance;
                });

                this.view.trigger(`resetDocuments`, reasonDocuments);
            }
        },

        roundSum(sum) {
            return +sum.toFixed(2);
        }
    });

    function needAutoReasonDocuments(model) {
        return model.get(`KontragentId`) > 0 && !model.get(`IsCustomEdit`);
    }
}(Md));

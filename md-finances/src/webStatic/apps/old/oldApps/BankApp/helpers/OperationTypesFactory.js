/* eslint-disable */
import { paymentOrderOperationResources } from '../../../../../resources/MoneyOperationTypeResources';

(function(bank) {
    bank.Helpers.OperationTypesFactory = {
        selectType(type, model) {
            switch (type) {
                case paymentOrderOperationResources.PaymentOrderIncomingFromAnotherAccount.value:
                    return {
                        view: new bank.Views.Documents.Operations.Incoming.ReceiptFromAnotherAccount({
                            model: model || new bank.Models.Documents.Operations.Incoming.ReceiptFromAnotherAccount()
                        })
                    };
                case paymentOrderOperationResources.PaymentOrderIncomingOther.value:
                    return {
                        view: new bank.Views.Documents.Operations.Incoming.OtherIncome({
                            model: model || new bank.Models.Documents.Operations.Incoming.OtherIncome()
                        })
                    };
                case paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value:
                    return {
                        view: new bank.Views.Documents.Operations.Incoming.ReceiptForSaleOfGoods({
                            model: model || new bank.Models.Documents.Operations.Incoming.ReceiptForSaleOfGoods()
                        })
                    };
                case paymentOrderOperationResources.PaymentOrderIncomingReturnFromAccountablePerson.value:
                    return {
                        view: new bank.Views.Documents.Operations.Incoming.ReturnFromAccountablePerson({
                            model: model || new bank.Models.Documents.Operations.Incoming.ReturnFromAccountablePerson()
                        })
                    };
                case paymentOrderOperationResources.PaymentOrderOutgoingReturnToBuyer.value:
                    return {
                        view: new bank.Views.Documents.Operations.Outgoing.BackToBuyer({
                            model: model || new bank.Models.Documents.Operations.Outgoing.BackToBuyer()
                        })
                    };
                case paymentOrderOperationResources.PaymentToAccountablePerson.value:
                    return {
                        view: new bank.Views.Documents.Operations.Outgoing.IssuanceAccountablePerson({
                            model: model || new bank.Models.Documents.Operations.Outgoing.IssuanceAccountablePerson()
                        })
                    };
                case paymentOrderOperationResources.PaymentOrderOutgoingOther.value:
                    return {
                        view: new bank.Views.Documents.Operations.Outgoing.OtherPayment({
                            model: model || new bank.Models.Documents.Operations.Outgoing.OtherPayment()
                        })
                    };
                case paymentOrderOperationResources.PaymentOrderPaymentToSupplier.value:
                    return {
                        view: new bank.Views.Documents.Operations.Outgoing.PaymentToSupplierForGoods({
                            model: model || new bank.Models.Documents.Operations.Outgoing.PaymentToSupplierForGoods()
                        })
                    };
                case paymentOrderOperationResources.PaymentOrderOutgoingPaymentAgencyContract.value:
                    return {
                        view: new bank.Views.Documents.Operations.Outgoing.PaymentAgencyContract({
                            model: model || new bank.Models.Documents.Operations.Outgoing.PaymentAgencyContract()
                        })
                    };
                case paymentOrderOperationResources.PaymentOrderOutgoingForTransferSalary.value:
                    return {
                        view: new bank.Views.Documents.Operations.Outgoing.TransferInConnectionWithPayment({
                            model: model || new bank.Models.Documents.Operations.Outgoing.TransferInConnectionWithPayment()
                        })
                    };
                case paymentOrderOperationResources.PaymentOrderOutgoingTransferToAccount.value:
                    return {
                        view: new bank.Views.Documents.Operations.Outgoing.TranslatedToOtherAccount({
                            model: model || new bank.Models.Documents.Operations.Outgoing.TranslatedToOtherAccount()
                        })
                    };

                // Операции мемориального ордера
                case paymentOrderOperationResources.MemorialWarrantReceiptFromCash.value:
                    return {
                        view: new bank.Views.Documents.Operations.Memorial.ReceiptFromCash({
                            model: model || new bank.Models.Documents.Operations.Memorial.ReceiptFromCash()
                        })
                    };
                case paymentOrderOperationResources.BankFee.value:
                    return {
                        view: new bank.Views.Documents.Operations.Memorial.BankFeeIsDeducted({
                            model: model || new bank.Models.Documents.Operations.Memorial.BankFeeIsDeducted()
                        })
                    };
                case paymentOrderOperationResources.MemorialWarrantCreditingCollectedFunds.value:
                    return {
                        view: new bank.Views.Documents.Operations.Memorial.CreditingCollectedFunds({
                            model: model || new bank.Models.Documents.Operations.Memorial.CreditingCollectedFunds()
                        })
                    };
                case paymentOrderOperationResources.MemorialWarrantReceiptGoodsPaidCreditCard.value:
                    return {
                        view: new bank.Views.Documents.Operations.Memorial.ReceiptGoodsPaidCreditCard({
                            model: model || new bank.Models.Documents.Operations.Memorial.ReceiptGoodsPaidCreditCard()
                        })
                    };
                case paymentOrderOperationResources.WithdrawalFromAccount.value:
                    return {
                        view: new bank.Views.Documents.Operations.Memorial.ReceivedCashInBank({
                            model: model || new bank.Models.Documents.Operations.Memorial.ReceivedCashInBank()
                        })
                    };

                /* todo: операций ниже не существует в БД возможно они не нужны совсем */
                case 11:
                    return {
                        view: new bank.Views.Documents.Operations.Incoming.PrepaymentFromBuyerOfProperty({
                            model: model || new bank.Models.Documents.Operations.Incoming.PrepaymentFromBuyerOfProperty()
                        })
                    };
                case 12: // не уверен что такой тип операции вообще есть
                    return {
                        view: new bank.Views.Documents.Operations.Incoming.PrepaymentFromBuyerOfGoods({
                            model: model || new bank.Models.Documents.Operations.Incoming.PrepaymentFromBuyerOfGoods()
                        })
                    };
                case 13: // не уверен что такой тип операции вообще есть
                    return {
                        view: new bank.Views.Documents.Operations.Incoming.BudgetaryPaymentsBack({
                            model: model || new bank.Models.Documents.Operations.Incoming.BudgetaryPaymentsBack()
                        })
                    };

                case 15: // не уверен что такой тип операции вообще есть
                    return {
                        view: new bank.Views.Documents.Operations.Incoming.ReceiptForSaleOfProperty({
                            model: model || new bank.Models.Documents.Operations.Incoming.ReceiptForSaleOfProperty()
                        })
                    };

                case 17: // не уверен что такой тип операции вообще есть
                    return {
                        view: new bank.Views.Documents.Operations.Incoming.NonBudgetaryReceipts({
                            model: model || new bank.Models.Documents.Operations.Incoming.NonBudgetaryReceipts()
                        })
                    };

                case 19: // не уверен что такой тип операции вообще есть
                    return {
                        view: new bank.Views.Documents.Operations.Incoming.ReturnFromSupplier({
                            model: model || new bank.Models.Documents.Operations.Incoming.ReturnFromSupplier()
                        })
                    };
                case 20: // не уверен что такой тип операции вообще есть
                    return {
                        view: new bank.Views.Documents.Operations.Outgoing.AdvanceToSupplierOfProperty({
                            model: model || new bank.Models.Documents.Operations.Outgoing.AdvanceToSupplierOfProperty()
                        })
                    };
                case 21: // не уверен что такой тип операции вообще есть
                    return {
                        view: new bank.Views.Documents.Operations.Outgoing.AdvanceToSupplierOfGoods({
                            model: model || new bank.Models.Documents.Operations.Outgoing.AdvanceToSupplierOfGoods()
                        })
                    };
                case 25: // не уверен что такой тип операции вообще есть
                    return {
                        view: new bank.Views.Documents.Operations.Outgoing.PaymentToSupplierForProperty({
                            model: model || new bank.Models.Documents.Operations.Outgoing.PaymentToSupplierForProperty()
                        })
                    };

                case 27: // не уверен что такой тип операции вообще есть
                    return {
                        view: new bank.Views.Documents.Operations.Outgoing.PaymentOfTaxesAndFees({
                            model: model || new bank.Models.Documents.Operations.Outgoing.PaymentOfTaxesAndFees()
                        })
                    };
            }
        },

        createOperationModel(params) {
            switch (params.Type) {
                case paymentOrderOperationResources.PaymentOrderIncomingFromAnotherAccount.value:
                    return new bank.Models.Documents.Operations.Incoming.ReceiptFromAnotherAccount(params);
                case paymentOrderOperationResources.PaymentOrderIncomingOther.value:
                    return new bank.Models.Documents.Operations.Incoming.OtherIncome(params);
                case paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value:
                    return new bank.Models.Documents.Operations.Incoming.ReceiptForSaleOfGoods(params);
                case paymentOrderOperationResources.PaymentOrderIncomingReturnFromAccountablePerson.value:
                    return new bank.Models.Documents.Operations.Incoming.ReturnFromAccountablePerson(params);
                case paymentOrderOperationResources.PaymentOrderOutgoingReturnToBuyer.value:
                    return new bank.Models.Documents.Operations.Outgoing.BackToBuyer(params);
                case paymentOrderOperationResources.PaymentToAccountablePerson.value:
                    return new bank.Models.Documents.Operations.Outgoing.IssuanceAccountablePerson(params);
                case paymentOrderOperationResources.PaymentOrderOutgoingOther.value:
                    return new bank.Models.Documents.Operations.Outgoing.OtherPayment(params);
                case paymentOrderOperationResources.PaymentOrderOutgoingPaymentAgencyContract.value:
                    return new bank.Models.Documents.Operations.Outgoing.PaymentAgencyContract(params);
                case paymentOrderOperationResources.PaymentOrderPaymentToSupplier.value:
                    return new bank.Models.Documents.Operations.Outgoing.PaymentToSupplierForGoods(params);
                case paymentOrderOperationResources.PaymentOrderOutgoingForTransferSalary.value:
                    return new bank.Models.Documents.Operations.Outgoing.TransferInConnectionWithPayment(params);
                case paymentOrderOperationResources.PaymentOrderOutgoingTransferToAccount.value:
                    return new bank.Models.Documents.Operations.Outgoing.TranslatedToOtherAccount(params);

                // Операции мемориального ордера
                case paymentOrderOperationResources.MemorialWarrantReceiptFromCash.value:
                    return new bank.Models.Documents.Operations.Memorial.ReceiptFromCash(params);
                case paymentOrderOperationResources.BankFee.value:
                    return new bank.Models.Documents.Operations.Memorial.BankFeeIsDeducted(params);
                case paymentOrderOperationResources.MemorialWarrantCreditingCollectedFunds.value:
                    return new bank.Models.Documents.Operations.Memorial.CreditingCollectedFunds(params);
                case paymentOrderOperationResources.MemorialWarrantReceiptGoodsPaidCreditCard.value:
                    return new bank.Models.Documents.Operations.Memorial.ReceiptGoodsPaidCreditCard(params);
                case paymentOrderOperationResources.WithdrawalFromAccount.value:
                    return new bank.Models.Documents.Operations.Memorial.ReceivedCashInBank(params);

                case paymentOrderOperationResources.BudgetaryPayment.value:
                    return new bank.Models.Documents.Operations.BaseOperationModel(params);

                /* todo: операций ниже не существует в БД возможно они не нужны совсем */
                case 11: // не уверен что такой тип операции вообще есть
                    return new bank.Models.Documents.Operations.Incoming.PrepaymentFromBuyerOfProperty(params);
                case 12: // не уверен что такой тип операции вообще есть
                    return new bank.Models.Documents.Operations.Incoming.PrepaymentFromBuyerOfGoods(params);
                case 13: // не уверен что такой тип операции вообще есть
                    return new bank.Models.Documents.Operations.Incoming.BudgetaryPaymentsBack(params);
                case 15: // не уверен что такой тип операции вообще есть
                    return new bank.Models.Documents.Operations.Incoming.ReceiptForSaleOfProperty(params);
                case 17: // не уверен что такой тип операции вообще есть
                    return new bank.Models.Documents.Operations.Incoming.NonBudgetaryReceipts(params);
                case 19:
                    return new bank.Models.Documents.Operations.Incoming.ReturnFromSupplier(params);
                case 20:
                    return new bank.Models.Documents.Operations.Outgoing.AdvanceToSupplierOfProperty(params);
                case 21:
                    return new bank.Models.Documents.Operations.Outgoing.AdvanceToSupplierOfGoods(params);
                case 25: // не уверен что такой тип операции вообще есть
                    return new bank.Models.Documents.Operations.Outgoing.PaymentToSupplierForProperty(params);
                case 27:
                    return new bank.Models.Documents.Operations.Outgoing.PaymentOfTaxesAndFees(params);
            }
        }
    };
}(Bank));

/* eslint-disable */
/* global Backbone, Cash, Common, Converter, Money, mdNew  */

import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { cashOrderOperationResources } from '../../../../../../../resources/MoneyOperationTypeResources';
import operationBillsHelper from '../../../../../../../components/OperationBills/operationBillsHelper';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { isUnifiedBP } from '../../views/documents/operations/CashBudgetaryPayment/helpers/checkHelper';

(function(cash, common) {
    cash.Models.BaseCashOrder = Backbone.Model.extend({
        getAutoDocNumberUrl() {
            return cash.Data.GetNextCashOrderNumberForYear;
        },

        initialize() {
            throw Error('function initialize not implemented in child model');
        },

        validation: {
            Number: {
                required: true,
                msg: 'Введите номер'
            },
            Date: [
                {
                    required: true,
                    msg: 'Введите дату'
                },
                {
                    date: true,
                    msg: 'Введите корректную дату'
                },
                {
                    fn: 'registrationDateValidation'
                },
                {
                    notClosedDate(val) {
                        return this.hasAttrChanged('Date', val);
                    }
                },
                {
                    notBeforeBalanceDate: true
                }
            ],
            Sum: [
                {
                    required(val, attr, m) {
                        if (isUnifiedBP(m)) {
                            return false;
                        }

                        return !this.isSalaryPayment();
                    },
                    msg: 'Введите сумму'
                },
                {
                    positive(val, attr, m) {
                        return !isUnifiedBP(m);
                    },
                    msg: 'Сумма должна быть больше 0'
                },
                {
                    notZero() {
                        if (isUnifiedBP(this.toJSON())) {
                            return false;
                        }

                        return !this.isSalaryPayment();
                    },
                    msg: 'Сумма не может быть равна 0'
                },
                {
                    max: function() {
                        if (isUnifiedBP(this.toJSON())) {
                            return 999999999.99;
                        }

                        return 1000000000;
                    },
                    msg: 'Слишком большое число'
                },
                {
                    fn: 'operationBillsSumValidation'
                }
            ],
            MediationCommission: [
                {
                    required: false
                },
                {
                    lessThan: 'Sum',
                    msg: 'Нельзя указать больше, чем в поле Сумма.'
                },
                {
                    positive: true,
                    msg: 'Сумма должна быть больше 0'
                },
                {
                    notZero: true,
                    msg: 'Сумма не может быть равна 0'
                },
                {
                    max: 1000000000,
                    msg: 'Слишком большое число'
                }
            ],
            WorkerName: {
                required() {
                    const type = this.get('OperationType');
                    const workerOrIp = type === cashOrderOperationResources.CashOrderOutgoingCollectionOfMoney.value ||
                        type === cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.value;

                    return this.isWorkerPayment() || workerOrIp;
                },
                msg: 'Введите сотрудника'
            },
            KontragentName: {
                required() {
                    return this.isKontragentPayment()
                        || (this.isCashContributing && this.isCashContributing())
                        || this.isOtherPayment()
                        || this.isLoanObtain()
                        || this.isMaterialAid();
                },
                msg: 'Введите контрагента'
            },
            NdsSum: [
                {
                    required() {
                        return this.isKontragentPayment() && this.useNds();
                    },
                    msg: 'Введите сумму НДС'
                },
                {
                    max: 1000000000,
                    msg: 'Слишком большое число'
                },
                {
                    lessOrEqualThan: `Sum`,
                    msg: `Неверная сумма НДС`
                }
            ],
            MediationNdsSum: [
                {
                    required() {
                        return this.isKontragentPayment() && this.useMediationNds();
                    },
                    msg: 'Введите сумму НДС'
                },
                {
                    max: 1000000000,
                    msg: 'Слишком большое число'
                }
            ],
            ProjectNumber: {
                required() {
                    return this.isLoanObtain() || this.isAgencyContract();
                },
                msg: 'Укажите договор'
            },
            PaidCardSum: [
                {
                    required: false
                },
                {
                    lessThan: 'Sum',
                    msg: 'Не может быть больше общей суммы'
                },
                {
                    min: 0.01,
                    msg: 'Не может быть отрицательным либо равным нулю'
                }
            ],
            DocumentsSum: {
                lessThan: 'Sum',
                msg: 'Сумма оплаты по документам не может превышать сумму ордера'
            },
            PaybillDate: [
                {
                    date() {
                        return this.isSalaryPayment();
                    },
                    msg: 'Введите корректную дату'
                }
            ],
            ZReportNumber: [
                {
                    required() {
                        return this.zReportRequired();
                    },
                    msg: 'Введите номер'
                },
                {
                    fn: 'validateZReportValue'
                }
            ],
            MiddlemanContract: {
                required() {
                    const operationType = parseInt(this.get('OperationType'), 10);
                    return operationType === cashOrderOperationResources.CashOrderIncomingMiddlemanRetailRevenue.value;
                },
                msg: 'Введите номер договора'
            },
            MyReward: {
                msg: 'Не может быть больше суммы, полученной за товар принципала',
                fn: 'isValidMyReward'
            }
        },

        isValidMyReward() {
            const myReward = parseInt(this.get('MyReward'), 10);
            const sum = parseInt(this.get('Sum'), 10);

            return myReward > sum;
        },

        getTaxationSystemsByDocumentDate(date) {
            const documentDate = Converter.toDate(date);
            const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            return ts.Current(documentDate);
        },

        validateZReportValue(value) {
            const currentTaxationSystem = this.getTaxationSystemsByDocumentDate(this.get('Date'));
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const isUsnAfter2025 = dateHelper(this.get('Date')).isSameOrAfter(ndsUsn2025Date) && currentTaxationSystem.get('IsUsn')
            const zReportPattern = /^[0-9/_-]+$/; // TS-85724, допустимые символы подтвердила Людмила

            if ((currentTaxationSystem.get('IsOsno') || isUsnAfter2025) && !zReportPattern.test(value)) {
                return 'Содержит недопустимые символы. Может содержать цифры, знаки - _ /';
            }
        },

        zReportRequired() {
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const currentTaxationSystem = this.getTaxationSystemsByDocumentDate(this.get('Date'));
            const isUsnAfter2025 = dateHelper(this.get('Date')).isSameOrAfter(ndsUsn2025Date) && currentTaxationSystem.get('IsUsn');   

            return (this.isIncoming() && this.isRetailRevenue() && currentTaxationSystem.get('IsOsno')) || (isUsnAfter2025 && this.isIncoming() && this.isRetailRevenueOnly());
        },

        operationBillsSumValidation() {
            return operationBillsHelper.validateModelAndBillsSum(this);
        },

        registrationDateValidation(value) {
            const date = dateHelper(value, 'DD.MM.YYYY');

            if (date === false) {
                return;
            }

            const registrationDate = dateHelper(this.getMinDate(), 'DD.MM.YYYY');
            if (date.isBefore(registrationDate)) {
                return 'Дата документа не может быть ранее даты регистрации компании';
            }
        },

        useNds() {
            return this.get('IncludeNds') && this.get('NdsType') > 0;
        },

        useMediationNds() {
            return this.get('IsMediation') && this.get('IncludeMediationNds') && this.get('MediationNdsType') > 0;
        },

        afterLoading() {
            Backbone.Wreqr.radio.channel('document').reqres.setHandler('get', this.get, this);

            if (this.get('Id') && this.isOtherPayment() && !this.get('KontragentName')) {
                this.set('KontragentName', this.get('FirmName'));
            }

            this.on('change:KontragentId', function() {
                if (!this.get('KontragentId')) {
                    return;
                }

                _.defer(() => {
                    !this.get('KontragentName') && this.loadKontragentName();
                });
            });
        },

        loadKontragentName() {
            const kontragentId = this.get('KontragentId');
            mdNew.KontragentService.getName(kontragentId).then((data) => {
                this.set({
                    KontragentId: data.Id,
                    KontragentName: data.Name
                });
            });
        },

        save(data, options) {
            options = options || {};
            options.url = cash.Data.SaveCashOrder;

            return Backbone.Model.prototype.save.call(this, data, options);
        },

        remove(options) {
            options = options || {};
            options.url = cash.Data.DeleteCashOrders;
            options.data = { deleteIds: [this.get('Id')] };

            this.set('id', this.get('Id'));

            return this.destroy(options);
        },

        setNdsSum() {
            if (!this.get('IncludeNds')) {
                this.set('NdsSum', 0);
                return;
            }

            let ndsType = this.get('NdsType'),
                ndsSum = common.Utils.NdsConverter.toPercent({
                    value: this.get('Sum'),
                    type: ndsType,
                    typeEnum: common.Data.BankAndCashNdsTypes
                });

            if (ndsType === common.Data.BankAndCashNdsTypes.Empty) {
                this.set('NdsSum', null);
                this.forceValid('NdsSum');
                return;
            }

            this.set('NdsSum', ndsSum);
            this.forceValid('NdsSum');
        },

        setMediationNdsSum() {
            if (!this.get('IncludeMediationNds')) {
                this.unset('MediationNdsSum')
                return;
            }

            const MediationNdsType = this.get('MediationNdsType');
            const MediationNdsSum = common.Utils.NdsConverter.toPercent({
                    value: this.get('MediationCommission') || this.get('MyReward'),
                    type: MediationNdsType,
                    typeEnum: common.Data.BankAndCashNdsTypes
                });
                
            this.set('MediationNdsSum', MediationNdsSum);

            if (MediationNdsType === common.Data.BankAndCashNdsTypes.Empty) {
                this.unset('MediationNdsSum');
                this.forceValid('MediationNdsSum');
                return;
            }

            this.forceValid('MediationNdsSum');
        },

        setInitialsAttributes() {
            this.initialsAttributes = this.toJSON();
        },

        hasAttrChanged(attr, val) {
            const modelState = this.initialsAttributes;

            if (val === undefined) {
                val = this.get(attr);
            }

            if (modelState) {
                return modelState[attr] != val;
            }

            return false;
        },

        checkNumberIsUnique(check) {
            if (!this.hasAttrChanged('Number')) {
                check(true);
                return;
            }

            $.ajax({
                url: cash.Data.CheckExistOrderNumber,
                data: {
                    number: this.get('Number'),
                    directionType: this.get('Direction'),
                    year: dateHelper(this.get('Date'), 'DD.MM.YYYY').year()
                },
                success(response) {
                    const notUnique = response && response.isExist;
                    check(!notUnique);
                }
            });
        },

        forceValid(attr) {
            this.trigger('forceValid', attr);
        },

        getMinDate() {
            const requisites = new common.FirmRequisites();
            return requisites.get('RegistrationDate');
        },

        isIncoming() {
            return this.get('Direction') == Direction.Incoming;
        },

        isKontragentPayment() {
            throw Error('function isKontragentPayment not implemented in child model');
        },

        isWorkerPayment() {
            throw Error('function isWorkerPayment not implemented in child model');
        },

        isOtherPayment() {
            throw Error('function isOtherPayment not implemented in child model');
        },

        isMediationPayment() {
            return this.get('OperationType') == cashOrderOperationResources.CashOrderIncomingMediationFee.value;
        },

        isLoanObtain() {
            const operationType = parseInt(this.get('OperationType'), 10);
            return operationType === cashOrderOperationResources.CashOrderIncomingLoanObtaining.value
                || operationType === cashOrderOperationResources.CashOrderOutgoingLoanRepayment.value;
        },

        isAgencyContract() {
            const operationType = parseInt(this.get('OperationType'), 10);
            return operationType === cashOrderOperationResources.CashOrderOutgoingPaymentAgencyContract.value;
        },

        isMaterialAid() {
            const operationType = parseInt(this.get('OperationType'), 10);
            return operationType === cashOrderOperationResources.CashOrderIncomingMaterialAid.value;
        }
    });
})(Cash, Common);

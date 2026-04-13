/* eslint-disable */
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';


(function(primaryDocuments, common) {
    primaryDocuments.Models.ReasonModel = Backbone.Model.extend({
        initialize() {
            this.on('change:DocumentSum', function() {
                this.isValid(true);
            });

            this.listenTo(this.collection, 'remove change:DocumentSum', (model) => {
                const item = model.collection.last();
                item && item.isValid(true);
            });
        },

        validation: {
            DocumentDate: {
                fn() {
                    if (parentIsAdvance() && this.get('DocumentDate')) {
                        const modelDate = dateHelper(this.get('DocumentDate'), 'DD.MM.YYYY');
                        const parentDate = dateHelper(getParentDocumentAttr('Date'), 'DD.MM.YYYY');

                        if (modelDate.isBefore(parentDate, 'day')) {
                            return 'Не может быть ранее даты документа';
                        }
                    }
                }
            },
            DocumentSum: [
                {
                    fn() {
                        let val = Converter.toFloat(this.get('DocumentSum')),
                            usedSum = this.get('UsedSum'),
                            sum,
                            msg;

                        if (isIncoming()) {
                            if (isCommon()) {
                                sum = roundValue(this.get('SumNdsForDeduction') - usedSum);
                                if (val > sum) {
                                    msg = 'Превышает сумму НДС, принятую к вычету по авансовому счету-фактуре №{number} от {date}';
                                    if (!usedSum) {
                                        msg += ' − {sum} р.';
                                    }

                                    return this.getInvalidMsg(sum, msg);
                                }

                                return this.overDocumentNds();
                            }
                            usedSum = getParentDocumentAttr('BalanceSum') || 0;
                            sum = roundValue(getParentDocumentAttr('NdsDeductionSum') - usedSum);
                            if (val > sum) {
                                msg = this.addBalanceSumInMessage('Превышает сумму НДС, принятую к вычету по авансовому счету-фактуре − {sum} р.', usedSum);
                                return this.getInvalidMsg(sum, msg);
                            }

                            return this.overItemNds();
                        }
                        if (isCommon()) {
                            sum = roundValue(this.get('NdsSum') - usedSum);
                            if (val > sum) {
                                msg = 'Превышает сумму НДС авансового счета-фактуры №{number} от {date}';
                                if (!usedSum) {
                                    msg += ' − {sum} р.';
                                }
                                return this.getInvalidMsg(sum, msg);
                            }

                            return this.overDocumentNds();
                        }
                        usedSum = getParentDocumentAttr('BalanceSum') || 0;
                        sum = roundValue(getParentDocumentAttr('NdsSum') - usedSum);
                        if (val > sum) {
                            msg = this.addBalanceSumInMessage('Превышает сумму НДС по авансовому счету-фактуре − {sum} р.', usedSum);
                            return this.getInvalidMsg(sum, msg);
                        }

                        return this.overItemNds();
                    }
                },
                {
                    fn() {
                        if (this.collection.length <= 1) {
                            return;
                        }

                        if (this.collection.last() !== this) {
                            return;
                        }

                        let sum = common.Utils.Converter.round(this.collection.sum('DocumentSum')),
                            parentDocumentSum = this.getParentDocumentSum();

                        if (sum > parentDocumentSum) {
                            const attr = getParentDocumentSumAttr();
                            let field = 'сумму НДС',
                                document = 'счету-фактуре';

                            if (attr === 'NdsDeductionSum') {
                                field = 'сумму НДС, принятую к вычету';
                            }

                            if (!isCommon()) {
                                document = 'авансовому счету-фактуре';
                            }

                            const msg = 'Превышает {field} по {document} - {sum} р.'
                                .replace('{field}', field)
                                .replace('{document}', document);

                            return this.getInvalidMsg(parentDocumentSum, msg);
                        }
                    }
                }
            ]
        },

        overDocumentNds() {
            let val = Converter.toFloat(this.get('DocumentSum')),
                nds = getParentDocumentAttr('TotalNdsSum');

            if (val > nds) {
                return this.getInvalidMsg(nds, 'Превышает сумму НДС документа − {sum} р.');
            }
        },

        overItemNds() {
            let val = Converter.toFloat(this.get('DocumentSum')),
                nds = this.get('NdsSum'),
                usedSum = this.get('UsedSum');

            if (val > roundValue(nds - usedSum)) {
                let msg = 'Превышает сумму НДС по счету-фактуре №{number} от {date}';
                if (!usedSum) {
                    msg += ' − {sum} р.';
                }
                return this.getInvalidMsg(nds, msg);
            }
        },

        getParentDocumentSum() {
            const attr = getParentDocumentSumAttr();
            return Converter.toFloat(getParentDocumentAttr(attr));
        },

        addBalanceSumInMessage(msg, usedSum) {
            if (usedSum) {
                msg += ` (сумма, учтенная в остатках ${Converter.toAmountString(usedSum)} р.)`;
            }
            return msg;
        },

        getInvalidMsg(sum, msg) {
            let number = $.trim(this.get('DocumentNumber').replace('№', '')),
                date = this.get('DocumentDate');

            return msg.replace('{number}', number)
                .replace('{date}', date)
                .replace('{sum}', Converter.toAmountString(sum));
        }

    });

    function getParentDocumentAttr(attr) {
        return Backbone.Wreqr.radio.channel('document').reqres.request('get', attr);
    }

    function getInvoiceField(attr) {
        if (getParentDocumentAttr('DocumentType') === common.Data.DocumentTypes.Invoice) {
            return getParentDocumentAttr(attr);
        }

        const invoice = getParentDocumentAttr('Invoice') || {};
        return invoice[attr];
    }

    function parentIsAdvance() {
        return parseInt(getInvoiceField('InvoiceType')) === common.Data.InvoiceType.Advance;
    }

    function isIncoming() {
        return getInvoiceField('Direction') === Direction.Incoming;
    }

    function isCommon() {
        return parseInt(getInvoiceField('InvoiceType')) === common.Data.InvoiceType.Common;
    }

    function getParentDocumentSumAttr() {
        const invoiceType = parseInt(getInvoiceField('InvoiceType'));

        if (invoiceType === common.Data.InvoiceType.Common) {
            return 'TotalNdsSum';
        }

        if (invoiceType === common.Data.InvoiceType.Advance) {
            if (getInvoiceField('Direction') === Direction.Incoming) {
                return 'NdsDeductionSum';
            }
            return 'NdsSum';
        }
    }

    function roundValue(val, decimalPart) {
        decimalPart = decimalPart || 2;
        const factor = Math.pow(10, decimalPart);
        return Math.round(val * factor) / factor;
    }
}(PrimaryDocuments, Common));

(function(buy, common) {
    const parent = common.Collections.TaxOperationCollection;
    
    buy.Collections.PostingsAndTax.AccountingStatementTaxCollection = parent.extend({
        url: WebApp.AccountingStatements.GetAllTaxPostings,

        listenSource() {
            this.sourceDocument.on('change:Description', this.setDescription, this);
            this.sourceDocument.on('change:TaxationSystemType', this.setTaxMode, this);
            this.sourceDocument.on('change:Date', this.setDate, this);
            this.listenPostingsFileds(['TaxationSystemType']);
        },
        
        setDescription() {
            const description = this.sourceDocument.get('Description');

            const update = _.partial(updatePosting, description);
            this.each((row) => {
                row.get('ManualPostings').each(update);
            });
        },

        setDate() {
            const date = this.sourceDocument.get('Date');

            this.each((row) => {
                row.get('ManualPostings').each((posting) => {
                    posting.set('PostingDate', date);
                });
            });
        },
        
        onlyPostingsAndTaxMode: common.Data.ProvidePostingType.ByHand,

        settings() {
            if (this.sourceDocument.get('Closed')) {
                return {
                    outgoing: {
                        allowNegative: true
                    }
                };
            }
        },

        explainingMessage() {
            if (this.isEnvd()) {
                this.notTaxable = true;
                return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxableEnvd;
            }
            
            if (this.isFixedAsset()) {
                this.notTaxable = true;
                
                if (this.isEnvdTax()) {
                    return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxable;
                }

                if (this.isOsno()) {
                    return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxableOsno;
                }
                
                return this.isUsn6()
                    ? common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxableUsn6
                    : common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxableUsn15;
            }

            if (this.isTradingFees()) {
                this.notTaxable = true;
                if (this.isOsno()) {
                    return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.tradingFeesOsno;
                }
                if (this.isUsn6()) {
                    return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.tradingFeesUsn6;
                }
                if (this.isUsn15()) {
                    return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.tradingFeesUsn15;
                }
            }
        },

        paymentDirection() {
            if (this.isUsn6()) {
                return common.Data.TaxPostingsDirection.Incoming;
            }
        },

        isTradingFees() {
            return this.sourceDocument.get('Type') == Common.Data.AccountingStatementType.TradingFeesPayment;
        },
        
        isUsn6() {
            return this.taxationSystem.get('IsUsn') && this.taxationSystem.get('UsnSize') === 6;
        },

        isUsn15() {
            return this.taxationSystem.get('IsUsn') && this.taxationSystem.get('UsnSize') === 15;
        },

        checkSourceData() {
            this.explainingMessage(); // pick up this.notTaxable value
            return !this.notTaxable;
        },
        
        isEnvd() {
            return this.sourceDocument.get('TaxationSystemType') == Common.Data.TaxationSystemType.Envd;
        },
        
        setTaxMode() {
            const postingsAndTaxMode = this.isEnvd() ? common.Data.ProvidePostingType.Auto : common.Data.ProvidePostingType.ByHand;
            this.sourceDocument.set('PostingsAndTaxMode', postingsAndTaxMode);
        },
        
        isFixedAsset() {
            return this.sourceDocument.get('FixedAssetBaseId') > 0;
        },
        
        isOsno() {
            return this.taxationSystem.get('IsOsno');
        },
        
        isEnvdTax() {
            return this.taxationSystem.get('IsEnvd') && !this.isOsno() && !this.taxationSystem.get('IsUsn');
        }
    });

    function updatePosting(description, posting) {
        if (Converter.toFloat(posting.get('Incoming')) > 0 || Converter.toFloat(posting.get('Outgoing')) > 0) {
            posting.set('Destination', description);
        }
    }
}(Buy, Common));

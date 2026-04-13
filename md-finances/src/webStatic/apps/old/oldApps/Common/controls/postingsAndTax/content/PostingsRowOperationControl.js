(function (common) {
    var parent = common.Controls.BaseRowOperationControl;
    
    common.Controls.PostingsRowOperationControl = parent.extend({

        template: "PostingsRowOperationControlTemplate",
        tagName: "div",

        emptyRowTemplate: "PostingEmptyItemTemplate",

        accountTypeEnum: {
            Debit: 'Debit',
            Credit: 'Credit'
        },

        subcontoDebitControls: {},
        subcontoCreditControls: {},

        initialize: function(options) {
            parent.prototype.initialize.call(this, options);
            
            this.model.get("ManualPostings").on("change remove add", function () {
                this.onChangeCollection();
            }, this);
        },

        onRender: function () {
            parent.prototype.onRender.call(this);
            this.$('.subcontoReadMode').truncateString();

            if (this.setExplainigMessage()) {
                return;
            }

            if (this.model.get("IsManualEdit")) {
                if (this.options.onlyOneManualPosting) {
                    this.setMaxSum();
                }
                
                this.bindPostings("ManualPostings");
                this.initPostingsValidation();
                this.showAddPostingLink();

                var manualPostings = this.model.get('ManualPostings');
                if (manualPostings.length) {
                    var date = this.options.sourceDocument.get('Date');
                    manualPostings.each(function (posting) {
                        if (!posting.get('Date')) {
                            posting.set('Date', date);
                        }

                        this.rowManipulations(posting);
                        this.addDocumentSpecialProperties(posting);
                    }, this);
                } else {
                    this.addNewItem();
                }
            } else {
                this.bindPostings("MainPostings");
            }

            this.applyDecimalMask(this.$('[data-number=float]'));
            this.addDatePicker();
        },
        
        initializeEvents: function () {
            parent.prototype.initializeEvents.call(this);
            this.options.sourceDocument.on("change:Date", this.setPostingsDate, this);
            if (this.options.onlyOneManualPosting) {
                this.options.sourceDocument.on("change:Sum", this.setMaxSum, this);
            }
        },

        rowManipulations: function (posting) {
            var $row = this.$("#" + posting.cid);
            this.initConrolsForRow($row, posting);
            this.customizeRow($row, posting);
        },
        
        customizeRow: function ($row, posting) {
            this.checkRowForSyntheticAccounts($row, posting);
        },

        checkRowForSyntheticAccounts: function ($row, posting) {
            var view = this;
            $row.find("input[syntheticaccount]").each(function () {
                var input = $(this),
                    accountType = input.attr('syntheticaccount'),
                    rowModelCid = input.closest(".mdTableRow").attr("id"),
                    model = view.model.get("ManualPostings").get(rowModelCid),
                    balanceType = (accountType == "Debit") ? model.get('DebitBalanceType') : model.get('CreditBalanceType');
                
                view.turnOfCreditFieldForOffBalanceBill($row, posting, balanceType, accountType);
            });
        },

        afterNewItemAdded: function (posting) {
            this.bindPostings('ManualPostings');
            posting.set('Date', this.options.sourceDocument.get('Date'));
            this.addDatePicker();
            if (this.options.onlyOneManualPosting) {
                this.setMaxSum();
                posting.set('Sum', this.calculateOperationSum());
            }

            this.addDocumentSpecialProperties(posting);
            this.rowManipulations(posting);
        },

        calculateOperationSum: function () {
            var operationCid = this.model.get("Cid"),
                operations = this.options.sourceDocument.get("Operations");
            
            if (!operations || operations.length === 0) {
                return this.options.sourceDocument.get('Sum');
            }

            return operations.get(operationCid).get("Sum") || operations.get(operationCid).get("AdditionalSum");
        },

        initConrolsForRow: function ($row, posting) {
            $row.find("textarea").resizableTextarea();
            this.initializeSyntheticAccountAutocomplete($row, posting);
            this.refreshSubcontoView(posting);
        },

        initPostingsValidation: function() {
            common.Mixin.BindViewValidationEvent.bindCollectionValidation(this, this.model.get("ManualPostings"));
        },

        removePostingsValidation: function () {
            common.Mixin.BindViewValidationEvent.unbindCollectionValidation(this, this.model.get("ManualPostings"));
        },

        setPostingsDate: function() {
            this.model.get('ManualPostings').each(function (posting) {
                posting.set('Date', this.options.sourceDocument.get('Date'));
            }, this);
        },

        setMaxSum: function () {
            this.model.get("ManualPostings").each(function (posting) {
                posting.addNoMoreSumValidation(this.options.sourceDocument.get("Sum"));
            }, this);

            this.initPostingsValidation();
        },

        showAddPostingLink: function () {
            if (!this.options.onlyOneManualPosting && this.model.get("IsManualEdit")) {
                this.$(".addItemBlock").show();
            }
        },
        
        turnOfCreditFieldForOffBalanceBill: function ($row, posting, balanceType, accountType) {
            var view = this;
            var accountNumber = accountType == view.accountTypeEnum.Debit ? posting.get('DebitNumber') : posting.get('CreditNumber');
            if (balanceType == 1 && accountNumber !== '000') {
                var otherAccountType = accountType == view.accountTypeEnum.Debit ? view.accountTypeEnum.Credit : view.accountTypeEnum.Debit;
                view.setOffBalanceAccount(posting, otherAccountType, $row.find("[syntheticaccount=" + otherAccountType + "]"));
            }
        },

        initializeSyntheticAccountAutocomplete: function ($row, posting) {
            var view = this;
            var collection = this.model.collection;

            $row.find("input[syntheticaccount]").each(function () {
                var input = $(this),
                    accountType = input.attr('syntheticaccount');

                var isDebit = accountType == view.accountTypeEnum.Debit;

                var options = {
                    onSelect: function (item) {
                        view.enableBalanceAccounts($row);
                        
                        view.updateDebitOrCreditInPosting(posting, accountType, item.object.Code, item.object.TypeId, item.object.BalanceType);

                        if (item.object.BalanceType == 1) {
                            var otherAccountType = isDebit ? view.accountTypeEnum.Credit : view.accountTypeEnum.Debit;
                            view.setOffBalanceAccount(posting, otherAccountType, $row.find("[syntheticaccount=" + otherAccountType + "]"));
                        }
                        view.turnOfCreditFieldForOffBalanceBill($row, posting, item.object.BalanceType, accountType);

                        input.trigger('change');
                        posting.isValidAttr("DebitNumber");
                        posting.isValidAttr("CreditNumber");


                        var onChangeAccount = isDebit ? collection.onChangeDebit : collection.onChangeCredit;
                        if(typeof onChangeAccount === 'function'){
                            onChangeAccount.call(collection, item.object.Code, posting);
                        }
                    },
                    onBlur: function () {
                        view.enableBalanceAccounts($row);
                        view.clearBalanceAccount(posting, accountType);
                    },
                    dataList: view.options.syntheticCollection
                };

                if (_.isFunction(view.options.dataFilter)) {
                    options.dataFilter = view.options.dataFilter;
                }
                
                var filter = view.getDataFilter(accountType);
                if (filter) {
                    options.dataFilter = filter;
                }

                if (!input.data("syntheticaccountautocomplete")) {
                    input.syntheticAccountAutocomplete(options);
                }
            });
        },
        
        getDataFilter: function (accountType) {
            var specialProperties = _.isFunction(this.options.documentSpecialProperties) ? this.options.documentSpecialProperties()[accountType] : null;
            if (specialProperties) {
                var accountsFilter = specialProperties.accountsFilter;
                var excludeFilter = specialProperties.excludeCodes;

                if (accountsFilter || excludeFilter) {
                    return function (item) {
                        return (accountsFilter && _.indexOf(accountsFilter, item.Code) != -1)
                            || (excludeFilter && _.indexOf(excludeFilter, item.Code) != 0)
                            || item.BalanceType == -1;
                    };
                }
            }
            
        },

        enableBalanceAccounts: function ($li) {
            $li.find("input[syntheticaccount][disabled][offbalance]").removeAttr("disabled").removeAttr("offbalance");
        },

        setOffBalanceAccount: function (posting, accountType, $el) {
            this.clearBalanceAccount(posting, accountType);
            $el.attr({ "disabled": true, "offbalance": true });
        },

        updateDebitOrCreditInPosting: function (posting, accountType, code, typeId, balanceType) {
            var view = this;
            if (accountType == view.accountTypeEnum.Debit) {
                posting.set('Debit', code);
                posting.set('DebitTypeId', typeId);
                posting.set('DebitBalanceType', balanceType);
            } else if (accountType == view.accountTypeEnum.Credit) {
                posting.set('Credit', code);
                posting.set('CreditTypeId', typeId);
                posting.set('CreditBalanceType', balanceType);
            }
        },
        
        clearBalanceAccount: function (posting, accountType) {
            var fieldName = accountType == this.accountTypeEnum.Debit ? "Debit" : "Credit";
            var emptyAccount = this.options.syntheticCollection.EmptyAccount;
            posting.setByAccount(fieldName, new Backbone.Model(emptyAccount));
            this.refreshSubcontoView(posting);
        },
        
        refreshSubcontoView: function (model) {
            const self = this;
            const { cid } = model;
            const sourceDocument = self.model.collection && self.model.collection.sourceDocument ? self.model.collection.sourceDocument : {};

            this.refreshSubcontoDebitView(cid, model, { sourceDocument });
            this.refreshSubcontoCreditView(cid, model, { sourceDocument });
        },

        refreshSubcontoDebitView: function (cid, model, { sourceDocument } = {}) {
            const self = this;

            if (this.subcontoDebitControls[cid]) {
                self.subcontoDebitControls[cid].undelegateEvents();
                self.subcontoDebitControls[cid].remove();
            }
            const li = this.getLiByModelCid(cid);

            self.subcontoDebitControls[cid] = new common.Views.AccountOnDebitControl(li, model, Boolean(model.get("DebitNumber")), { sourceDocument });
            model.off('change:DebitTypeId');
            model.on('change:DebitTypeId', function (changeModel) {
                self.subcontoDebitControls[cid].refreshView(changeModel.get('DebitTypeId'));
            });
            
            if (!model.get("SubcontoDebit") || !model.get("SubcontoDebit").length) {
                self.subcontoDebitControls[cid].refreshView(model.get('DebitTypeId'));
            }
        },

        refreshSubcontoCreditView: function (cid, model, { sourceDocument }) {
            var self = this;
            if (this.subcontoCreditControls[cid]) {
                this.subcontoCreditControls[cid].undelegateEvents();
                self.subcontoCreditControls[cid].remove();
            }
            var li = this.getLiByModelCid(cid);
            self.subcontoCreditControls[cid] = new common.Views.AccountOnCreditControl(li, model, Boolean(model.get("CreditNumber")), { sourceDocument });
            model.off('change:CreditTypeId');
            model.on('change:CreditTypeId', function (changeModel) {
                self.subcontoCreditControls[cid].refreshView(changeModel.get('CreditTypeId'));
            });
            if (!model.get("SubcontoCredit") || !model.get("SubcontoCredit").length) {
                self.subcontoCreditControls[cid].refreshView(model.get('CreditTypeId'));
            }
        },

        getLiByModelCid: function (modelCid) {
            return this.$('ul[data-bind=ManualPostings] li[id=' + modelCid + ']');
        },

        deleteSubcontoView: function (id) {
            if (this.subcontoDebitControls[id]) {
                delete this.subcontoDebitControls[id];
            }

            if (this.subcontoCreditControls[id]) {
                delete this.subcontoCreditControls[id];
            }
        },

        addDocumentSpecialProperties: function (posting) {
            var self = this;

            var props = _.isFunction(this.options.documentSpecialProperties) ?
                this.options.documentSpecialProperties() :
                this.options.documentSpecialProperties;

            if (!props) {
                return;
            }

            _.each(props, function (obj, key) {
                switch (key) {
                    case 'Credit':
                    case 'Debit':
                        if (obj.defaultAccountCode) {
                            self.addDefaultAccountCode(posting, key, obj.defaultAccountCode);
                        }
                        if (obj.disabled) {
                            self.disableField(posting, String.format("[syntheticaccount={0}]", key));
                        }
                        
                        break;
                    case 'SubcontoCredit':
                    case 'SubcontoDebit':
                        if (obj.disabled) {
                            self.disableField(posting, '.tt{0} input'.format(key));
                        }
                        break;
                    case 'Description':
                    case 'Sum':
                        if(obj && typeof obj === 'object'){
                            if(obj.val !== undefined){
                                posting.set(key, obj.val);
                            }

                            if(obj.disabled){
                                self.disableField(posting, '.ttSumm input');
                            }
                        }
                        else if(obj){
                            posting.set(key, obj);
                        }
                        break;
                }
            });
            
            if (props.DelateFirstRow === false) {
                var postingRow = posting.collection.first();
                if (postingRow) {
                    this.$(String.format("#{0} .ttDelete", postingRow.cid)).hide();
                }
            }
        },

        addDefaultAccountCode: function (posting, type, accountCode) {
            var self = this;
            
            if (self.options.syntheticCollection.length == 0) {
                _.delay(function () {
                    self.addDefaultAccountCode(posting, type, accountCode);
                }, 100);
                return;
            }

            var account = self.options.syntheticCollection.getItemByCode(accountCode);
            if (!account) {
                return;
            }

            posting.setByAccount(type, account);
        },

        disableField: function(posting, fieldSelector) {
            this.$(String.format("#{0} {1}", posting.cid, fieldSelector)).prop("disabled", true);
        },

        getDirectives: function () {
            return {
                MainPostings: postingsDirectives,
                LinkedDocuments: {
                    Postings: postingsDirectives
                }
            };
        }

    });

    var postingsDirectives = {
        Sum: {
            text: function () {
                return common.Utils.Converter.toAmountString(this.Sum);
            }
        }
    };
   
})(Common);
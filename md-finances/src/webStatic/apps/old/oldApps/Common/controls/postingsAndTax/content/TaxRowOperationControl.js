/* eslint-disable */
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';

(function(common) {
    const parent = common.Controls.BaseRowOperationControl;
    common.Controls.TaxRowOperationControl = parent.extend({

        template: 'TaxRowOperationControlTemplate',
        emptyRowTemplate: 'TaxItemControlTemplate',

        initialize(options) {
            parent.prototype.initialize.call(this, options);
        },

        events: _.extend({
            'keyup .ttIncoming  input, .ttOutgoing  input': 'disablingSiblingsDirection'
        }, parent.prototype.events),


        /** Костыль для IE. */
        disablingSiblingsDirection(e) {
            let $elem = $(e.currentTarget),
                modelCid = $elem.closest('.mdTableRow').attr('id'),
                model = this.model.get('ManualPostings').get(modelCid),
                fieldDirection = $elem.data('direction'),
                value = $elem.val();

            if (!model) {
                return;
            }
            if (fieldDirection == 'incoming') {
                model.set('Incoming', value);
            } else if (fieldDirection == 'outgoing') {
                model.set('Outgoing', value);
            }
        },

        initializeEvents() {
            this.model.get('ManualPostings').on('remove', function() {
                this.bindPostings('ManualPostings');
                this.onChangeCollection();
                this.initPostingsValidation();
            }, this);

            this.model.get('ManualPostings').on('reset', function() {
                this.render();
            }, this);

            this.model.get('ManualPostings').on('change', function(model) {
                this.chooseDirection(model);
                this.onChangeCollection();
                this.model.trigger('change:ManualPostings');
            }, this);

            this.model.on('change:Incoming change:Outgoing', this.chooseDirection, this);
        },

        initializeVariables() {
            this.switcherMode = common.Data.ProvidePostingType;
        },

        onRender() {
            parent.prototype.onRender.call(this);
            if (this.setExplainigMessage()) {
                return;
            }

            this.bindPostings('ManualPostings');
            this.initializeControls();
            this.emptyPostingsHandling();
            this.initPostingsValidation();
            this.hideLinkedDocuments();
            this.hideAndShowDeleteRowLink();
            this.hideAdnShowLinkedDocuments();
            this.hideAndShowDate();
            this.blockAnotherDirectionField();
        },

        initializeControls() {
            this.$('textarea[data-bind=Destination]').resizableTextarea();

            let options = _.result(this.model.collection, 'settings') || {},
                filter = '*';

            const $inputs = this.$('input[data-format=decimal]');

            if (options.incoming && options.incoming.allowNegative) {
                filter = '[data-bind^="Incoming"]';
                $inputs.filter(filter).mdNumberInputMask({
                    numberOfDecimals: 2,
                    allowedNegative: true
                });
                filter = `:not(${filter})`;
            }

            if (options.outgoing && options.outgoing.allowNegative) {
                filter = '[data-bind^="Outgoing"]';
                $inputs.filter(filter).mdNumberInputMask({
                    numberOfDecimals: 2,
                    allowedNegative: true
                });
                filter = `:not(${filter})`;
            }

            $inputs.filter(filter).mdNumberInputMask();
            $inputs.on('blur change', (event) => { formatSum($(event.target)); });
            $inputs.each(function() { formatSum($(this)); });

            this.addDatePicker();

            const isPatent = this.model.collection.sourceDocument.get(`TaxationSystemType`) === TaxationSystemType.Patent;

            if (this.isIpOsno && !isPatent) {
                this.$(`.ttDescription input`).remove();
                const isPurse = Number.isInteger(this.options.sourceDocument.get(`PurseId`));
                let selector = `.ttIncoming input`;

                if (!isPurse) {
                    selector = `${selector}, .ttOutgoing input`;
                }

                this.$(selector).attr(`disabled`, true);
            }
        },

        initPostingsValidation() {
            const options = _.result(this.model.collection, 'settings') || {};
            const collection = this.model.get('ManualPostings');

            if (options.incoming && options.incoming.validation) {
                collection.each((model) => {
                    model.validator = $.extend(true, {}, model.validator);
                    model.validationRules = $.extend(true, {}, model.validationRules);

                    model.validator.incomingValidation = options.incoming.validation;
                    model.validationRules.Incoming.incomingValidation = {};
                });
            }

            common.Mixin.BindViewValidationEvent.bindCollectionValidation(this, collection);
        },

        showErrorMessage(messageText) {
            let validationBlock;

            if (this.$('.validationMessage').length) {
                validationBlock = this.$('.validationMessage');
            } else {
                validationBlock = $('<div>', { class: 'validationMessage ' }).appendTo(this.$el);
            }

            validationBlock.html(messageText).slideDown('fast');
        },

        hideErrorMessage() {
            this.$('.validationMessage').slideUp('fast');
        },

        chooseDirection(model) {
            if (!model || this.model.get('Direction')) {
                return;
            }

            let incomingSum = Converter.toFloat(model.get('Incoming')) || 0,
                isOutgoing = Boolean(model.get('Outgoing')),
                node = this.$(`#${model.cid}`),
                incomingNode = node.find('[data-bind^=Incoming]'),
                outgoingNode = node.find('[data-bind^=Outgoing]');

            incomingNode.add(outgoingNode).removeAttr('disabled');

            if (incomingSum !== 0) {
                outgoingNode.attr('disabled', 'disabled').val('').change();
                model.set('Direction', taxPostingsDirection.Incoming);
            } else if (isOutgoing) {
                incomingNode.attr('disabled', 'disabled').val('').change();
                model.set('Direction', taxPostingsDirection.Outgoing);
            } else {
                model.unset('Direction');
            }
            model.validateAttrs(model.changedAttributes());
        },

        addAndRemoveEmptyMessage(add) {
            if (add) {
                const message = 'Заполните все необходимые поля';
                this.createAndPlacementExplainigBlock(message);
            } else {
                this.removeExplainingBlock();
            }
        },

        hideAdnShowLinkedDocuments() {
            let currentMode = this.options.sourceDocument.get('PostingsAndTaxMode');
            let addLink = this.$('.addItemBlock');

            if (currentMode == this.switcherMode.ByHand || this.model.get('IsManualEdit')) {
                addLink.show();
            } else if (currentMode == this.switcherMode.Auto) {
                addLink.hide();
            }
        },

        hideLinkedDocuments() {
            const mode = this.options.sourceDocument.get('PostingsAndTaxMode');

            if (mode == common.Data.ProvidePostingType.ByHand) {
                this.$('.linkedDocumentsHeader').remove();
                this.$('[data-bind=LinkedDocuments]').remove();
            }
        },

        afterNewItemAdded(model) {
            this.bindPostings('ManualPostings');
            this.onChangeCollection();
            this.initPostingsValidation();

            this.setDefaultDate(model);
            this.blockAnotherDirectionField();
            this.initializeControls();
            this.hideAndShowDate();
        },

        setDefaultDate(model) {
            const docDate = this.options.sourceDocument.get('Date');

            if (docDate) {
                model.set('PostingDate', docDate);
            }
        },

        hideAndShowDate() {
            const isPatent = this.model.collection.sourceDocument.get(`TaxationSystemType`) === TaxationSystemType.Patent;

            if (this.isIpOsno && !isPatent) {
                this.$(`.ttDate`).remove();
            }
        },

        blockAnotherDirectionField() {
            const modelDirection = this.model.get('taxDirection') || this.model.get('Direction');
            const directions = common.Data.TaxPostingsDirection;
            let directionField;

            if (!modelDirection) {
                return;
            }

            const sumInputs = this.$(`[data-bind^=Outgoing], [data-bind^=Incoming]`);
            const otherOperationTypes = [54, 60];

            if (this.isIpOsno && !otherOperationTypes.includes(this.model.get(`OperationType`))) {
                sumInputs.attr(`disabled`, true);
            } else {
                sumInputs.removeAttr(`disabled`);
            }

            if (modelDirection === directions.Incoming) {
                directionField = this.$('[data-bind^=Outgoing]');
            } else if (modelDirection === directions.Outgoing) {
                directionField = this.$('[data-bind^=Incoming]');
            }

            if (directionField) {
                directionField.val('').attr('disabled', 'disabled')
                    .change();
            }
        },

        getDirectives() {
            return {
                MainPostings: this.TaxDirectives,
                ManualPostings: this.TaxDirectives,
                LinkedDocuments: {
                    Postings: this.TaxDirectives,
                    PostingsAndTaxMode: {
                        html(target) {
                            if (this.PostingsAndTaxMode == common.Data.ProvidePostingType.ByHand) {
                                return 'отредактирован вручную';
                            }
                            $(target.element).remove();
                        }
                    },
                    DocumentName: {
                        text() {
                            const operationTypeName = common.Data.DocumentTypeHelper.getAccountingDocumentTypeName(this.Type);
                            return `${operationTypeName}№ ${this.DocumentNumber} от ${this.DocumentDate}`;
                        }
                    }
                }
            };
        },

        TaxDirectives: {
            Incoming: directionSumFieldDirectives('Incoming'),
            Outgoing: directionSumFieldDirectives('Outgoing')
        }
    });

    var taxPostingsDirection = common.Data.TaxPostingsDirection;

    function directionSumFieldDirectives(direction) {
        const inputTagName = 'INPUT';

        return {
            html(target) {
                if (target.element.tagName === inputTagName && this.Direction != taxPostingsDirection[direction]) {
                    $(target.element).attr('disabled', 'disabled').val('');
                }
            },
            text(target) {
                if (target.element.tagName !== inputTagName) {
                    return getFormatVal.call(this, direction);
                }
            },
            value() {
                return getFormatVal.call(this, direction);
            }
        };
    }

    function getFormatVal(direction) {
        if (this.Direction === taxPostingsDirection[direction]) {
            return common.Utils.Converter.toAmountString(this[direction]);
        }
        return '';
    }

    function formatSum(el) {
        el.val(common.Utils.Converter.toAmountString(el.val()));
    }
}(Common));

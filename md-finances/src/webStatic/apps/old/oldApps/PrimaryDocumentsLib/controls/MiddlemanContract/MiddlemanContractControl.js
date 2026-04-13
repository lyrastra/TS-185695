/* eslint-disable */
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

(function(primaryDocuments) {
    primaryDocuments.Controls.MiddlemanContractControl = Backbone.View.extend({
        template: 'MiddlemanContractControlTemplate',

        initialize(options) {
            this.options = options || {};
            Backbone.Validation.bind(this);
        },

        bindings: {
            '[data-bind=ContractNumber]': {
                observe: 'ContractNumber',
                setOptions: {
                    validate: true
                },
                events: ['change']
            },
            '[data-bind=MiddlemanId]': 'MiddlemanId',

            '[data-bind=MiddlemanName]': {
                observe: 'MiddlemanName',
                setOptions: {
                    validate: true
                }
            }
        },

        render() {
            this.$el.html(TemplateManager.getFromPage(this.template));
            this.onRender();

            return this;
        },

        onRender() {
            this.stickit();

            this.initMiddlemanAutocomplete();
            this.initContractNumberAutocomplete();

            this.checkClosedPeriod();

            this.listenTo(this.model, 'change:MiddlemanId', this.onChangeMiddleman);
            this._checkKontragent();
        },

        _checkKontragent() {
            if (!$.fn.checkKontragentById) {
                return;
            }

            this.$('[data-bind=MiddlemanName]').checkKontragentById({
                KontragentId: this.model.get('MiddlemanId'),
                Date: Backbone.Wreqr.radio.channel('document').reqres.request('get', 'Date')
            });
        },

        isValid() {
            return this.model.isValid(true);
        },

        onChangeMiddleman() {
            const prev = this.model.previous('MiddlemanId');
            if (prev && this.model.get('MiddlemanId') != prev) {
                this.clear();
            }

            this._checkKontragent();
        },

        initMiddlemanAutocomplete() {
            const view = this;
            let input;

            input = view.$('#MiddlemanName');
            input.saleKontragentWaybillAutocomplete({
                url: KontragentsApp.Autocomplete.MiddlemanAutocomplete,
                onSelect(selectedItem) {
                    view.onSelectMiddlemanName(selectedItem.object.Id, selectedItem.object.Name);
                },
                onBlur($el) {
                    if (!$el.val()) {
                        view.model.set({
                            MiddlemanId: null,
                            MiddlemanName: null
                        });
                    }
                }
            });
        },

        onSelectMiddlemanName(id, name) {
            const input = this.$('#MiddlemanName');

            this.model.set('MiddlemanId', id);
            this.$('#MiddlemanId').val(id).change();
            input.val(name).change();

            const index = this.$('.field').index(input.closest('.field'));
            this.$(`.field:visible:gt(${index})`).find('input[type=text], textarea').first().focus();
        },

        initContractNumberAutocomplete() {
            const model = this.model;

            this.$('[data-bind=ContractNumber]').middlemanContractNumberAutocomplete({
                getData: function() {
                    return _.extend(
                        { kontragentId: this.model.get('MiddlemanId') || null },
                        _.result(this.options, 'contractNumberAutoCompleteParams')
                    );
                }.bind(this),
                onSelect(item) {
                    model.set(item.object, { validate: true });
                },
                onCreate() {
                    mdNew.Contracts.addDialogHelper.showDialog({
                        data: {
                            Direction: Direction.Incoming,
                            KontragentId: model.get('KontragentId'),
                            KontragentName: model.get('KontragentName')
                        },
                        onSave(options) {
                            model.set({
                                ContractDate: options.ContractDate,
                                ContractNumber: options.ProjectNumber,
                                DocumentBaseId: options.DocumentBaseId,
                                Id: options.ProjectId,
                                MiddlemanId: options.KontragentId,
                                MiddlemanName: options.KontragentName
                            }, { validate: true });

                            model.trigger('forChangeMiddlemanId');
                        }
                    });
                }
            });
        },

        checkClosedPeriod() {
            let requisites = new Common.FirmRequisites(),
                date = Backbone.Wreqr.radio.channel('document').reqres.request('get', 'Date');

            if (requisites.inClosedPeriod(date)) {
                this.$('.cancelMiddlemanContract').remove();
                this.$('.middlemanContractLink').replaceWith(function() {
                    return $(this).text();
                });
            }
        },

        clear() {
            _.each(['ContractNumber', 'Id'], (attr) => {
                this.model.unset(attr);
                Backbone.Validation.callbacks.valid(this, attr, 'data-bind'); // data-bind from oldApps/Core/libs/backbone/extensions/backbone-validationSettings.js
            });
        }
    });
}(PrimaryDocuments));

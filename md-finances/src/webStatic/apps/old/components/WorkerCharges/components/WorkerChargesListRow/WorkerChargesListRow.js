import { MdLayoutView } from '@moedelo/md-frontendcore/mdCore';
import MdInput from '@moedelo/md-frontendcore/mdCommon/components/MdInput';
import MdSelect from '@moedelo/md-frontendcore/mdCommon/components/MdSelect';
import MdLoader from '@moedelo/md-frontendcore/mdCommon/components/MdLoader';
import MdConverter from '@moedelo/md-frontendcore/mdCommon/helpers/MdConverter';

import template from './template.hbs';
import style from './style.m.less';

export default MdLayoutView.extend({
    template,

    initialize({ model, handlers, service, readonly }) {
        this.model = model;
        this.handlers = handlers;
        this.service = service;

        this.model.workerId = this.service.getWorkerId();
        this.readonly = readonly;
    },

    regions: {
        input: '.js-inputRegion',
        select: '.js-selectRegion'
    },

    events: {
        'click .js-remove': function() {
            this._onRemove();
        }
    },

    templateHelpers() {
        return {
            style,
            readonly: this._mdView.readonly
        };
    },

    onRender() {
        this._showInput();
        this._renderSelect();
    },

    instanceMethods: {
        _renderSelect() {
            if (this.service.isListLoaded()) {
                this.loadAndFillSelect();
            } else {
                this._fishFillSelect();
            }
        },

        _showInput() {
            const invalidMsg = this.model.getValidateErrorMessage('Sum');
            const { model } = this;
            const value = (model && model.get('Sum')) || 0;
            const view = new MdInput({
                value,
                numberInputMask: value ? 'sumAmount' : 'none',
                customClass: style['row__input'],
                invalid: invalidMsg,
                disabled: this.readonly,
                invalidMsg,
                onChange: val => {
                    const formattedVal = MdConverter.toFloat(val) || '';
                    model.set('Sum', formattedVal, { silent: true });
                    this.model.isValid();

                    this._onChange();
                    this._showInput();
                }
            });

            this.regions.input.show(view);
        },

        _fishFillSelect() {
            const list = this.service.getDefaultCharger(this.model.toJSON());
            this._showSelect({ list });
        },

        _showSelect({ list, isOpen }) {
            const isLoadedList = this.service.isListLoaded();
            const view = new MdSelect({
                className: style['row__select'],
                data: list,
                check: true,
                needScroll: true,
                current: this.model.get('ChargeId'),
                active: isOpen,
                disabled: this.readonly,
                handlers: {
                    onClick: () => {
                        if (!isLoadedList && !this.readonly) {
                            this._showLoader();
                            this.loadAndFillSelect({ openAfter: true });
                        } else {
                            this._handleSelectItemsContent();
                        }
                    },
                    onSelect: ({ text, value, sum }) => {
                        const { model } = this;

                        model.set({
                            Sum: sum
                        }, { silent: true });

                        sum && model.isValid();

                        model.set({
                            ChargeId: value,
                            Description: text
                        });

                        this._onChangeSelect();
                    }
                }
            });
            this.regions.select.show(view);
        },

        _showLoader() {
            const view = new MdLoader({
                className: style['row__loader'],
                height: 27,
                margin: '0',
                length: 4,
                width: 2,
                radius: 4,
                lines: 8
            });

            this.regions.select.show(view);
        },

        loadAndFillSelect(options = {}) {
            const changeId = this.model.get('ChargeId');

            return this.service.getCharges({ excluded: changeId })
                .then((list) => {
                    this._showSelect({ list, isOpen: options.openAfter });
                    this._handleSelectItemsContent();
                });
        },

        _handleSelectItemsContent() { // TS-51688
            this.regions.select.$el.find('.md-selectListItem__text').each((index, element) => {
                const text = element.innerText;

                (!this._isStringFit(text, 350) && !element.hasAttribute('title')) && element.setAttribute('title', text); // 350 from styles ¯\_(ツ)_/¯ for TS-51688
            });
        },

        _isStringFit(string, maxWidth) { // TS-51688
            const canvas = this._isStringFit.canvas || (this._isStringFit.canvas = document.createElement('canvas'));

            if (canvas) {
                const ctx = canvas.getContext('2d');

                ctx.font = '13px';

                return ctx.measureText(string).width < maxWidth;
            }

            return string.length < 45; // average amount of symbols to fit container. in case of problems with canvas
        },

        _onRemove() {
            this.handlers.onRemove(this.model);
        },

        _onChangeSelect() {
            this.handlers.onChangeSelect();
        },

        _onChange() {
            this.handlers.onChange();
        }
    }
});

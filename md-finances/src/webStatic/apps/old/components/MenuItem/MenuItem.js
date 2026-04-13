import { MdLayoutView } from '@moedelo/md-frontendcore/mdCore';
import MdConverter from '@moedelo/md-frontendcore/mdCommon/helpers/MdConverter';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import MdInput from '@moedelo/md-frontendcore/mdCommon/components/MdInput';
import SettlementName from './SettlementName';
import SettlementAccountService from './../../../../services/SettlementAccountService';

import template from './template.hbs';
import css from './MenuItem.m.less';

const MenuItem = MdLayoutView.extend({
    template,

    initialize({ model, onSelect = () => {} }) {
        this.model = model;
        this.onSelect = onSelect;
    },

    events: {
        'click .js-settlement': () => { this._select(); },
        'click .js-incoming': () => { this._select(Direction.Incoming); },
        'click .js-outgoing': () => { this._select(Direction.Outgoing); },
        'click .js-edit': (event) => {
            event.stopPropagation();
            this._edit();
        }
    },

    regions: {
        settlementName: '.js-settlement'
    },

    templateHelpers() {
        return {
            css,
            money: val => MdConverter.toAmountString(val),
            ShowDirectionFilter: this.model.get('Selected') || this.model.get('Active')
        };
    },

    onRender() {
        this._showSettlementName();

        const direction = this.model.get('Direction');
        this.$el.find('.js-incoming').toggleClass('selected', direction === Direction.Incoming);
        this.$el.find('.js-outgoing').toggleClass('selected', direction === Direction.Outgoing);
    },

    instanceMethods: {
        _select(direction = null) {
            this.select(this.model.get('Id'), direction);
        },

        _showSettlementName() {
            this.regions.settlementName.show(new SettlementName({ data: this.model.toJSON() }));
        },

        _edit() {
            this.regions.settlementName.show(new MdInput({
                maxLength: 50,
                onChange: val => {
                    const name = val.trim();
                    if (name) {
                        const id = this.model.get('Id');
                        SettlementAccountService.setName(id, name);
                        this.model.set('Name', name);
                    } else {
                        this._showSettlementName();
                    }
                },
                onBlur: this._showSettlementName()
            }));
            this.$el.find('input').focus();
        },

        select(id, direction = null) {
            const isSelected = this.model.get('Id') === id;

            this.model.set({
                Active: isSelected && !!direction,
                Selected: isSelected && !direction,
                Direction: direction
            });

            this.onSelect({ Id: id, Direction: direction });
        }
    }
});

export default MenuItem;

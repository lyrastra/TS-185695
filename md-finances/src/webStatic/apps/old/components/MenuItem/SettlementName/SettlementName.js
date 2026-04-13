import {MdItemView} from '@moedelo/md-frontendcore/mdCore';
import Backbone from 'backbone';
import MdTooltip from '@moedelo/md-frontendcore/mdCommon/components/MdTooltip';

import template from './template.hbs';
import css from './SettlementName.m.less';

let SettlementName = MdItemView.extend({
    template,

    initialize({data}) {
        this.model = new Backbone.Model(data);
    },

    templateHelpers(){
        return {
            css
        };
    },

    onRender(){
        let content = [this.model.get('BankName'), this.model.get('Number')].join('<br/>');

        this.tooltip = new MdTooltip({
            $anchor: this.$el.find('.js-name'),
            position: 'bottom center',
            customClass: css.tooltip,
            content
        });
    },

    onBeforeDestroy() {
        this.tooltip && this.tooltip.destroy();
    }
});

export default SettlementName;

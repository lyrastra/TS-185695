import { MdItemView } from '@moedelo/md-frontendcore/mdCore';
import { Model } from 'backbone';
import MdConverter from '@moedelo/md-frontendcore/mdCommon/helpers/MdConverter';

import template from './template.hbs';
import style from './style.m.less';

export default MdItemView.extend({
    template,

    initialize(data) {
        this.model = new Model(data);
    },

    templateHelpers() {
        return {
            Sum() {
                return MdConverter.toAmountString(this.sum);
            },
            style
        }
    }

});

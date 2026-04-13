import {MdLayoutView} from '@moedelo/md-frontendcore/mdCore';
import MdButton from '@moedelo/md-frontendcore/mdCommon/components/MdButton';
import template from './template.hbs';
import css from './bankIntegrationButton.m.less';

let BankIntegrationButton = MdLayoutView.extend({
    template,

    regions: {
        bankIntegrationButton: '#bankIntegrationButtonRegion'
    },

    initialize({ settlementAccountData, onClick, hideIcon = false }) {
        this.data = settlementAccountData;
        this.onClick = onClick;
        this.hideIcon = hideIcon;
    },

    onRender() {
        this._showButton(this.hideIcon);
    },

    templateHelpers() {
        return {
            css,
        };
    },

    instanceMethods: {
        _showButton
    }
});

function _showButton(hideIcon) {
    let base64Image = this.data.IntegrationPartnerLogo;
    let icon = !hideIcon ? $('<i>').css('background', 'url(data:image/gif;base64,' + base64Image + ')').prop('outerHTML') : '';
    let bankTitle = this.data.IntegrationPartnerTitle || 'банк';
    let title = icon + `<span>Отправить в ${bankTitle}</span>`;

    let control = new MdButton({
        title,
        onClick: () => this.onClick()
    });
    this.regions.bankIntegrationButton.show(control);
}

export default BankIntegrationButton;

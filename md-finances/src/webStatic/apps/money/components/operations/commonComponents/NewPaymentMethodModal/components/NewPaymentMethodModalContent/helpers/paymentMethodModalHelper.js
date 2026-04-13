import whiteLabelHelper from '@moedelo/frontend-common-v2/apps/marketing/helpers/whiteLabelHelper';
import { getJsx } from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import style from '../style.m.less';
import IntegrationPartnerIconName from '../resources/IntegrationPartnerIconName';
// eslint-disable-next-line import/no-unresolved,import/extensions
import * as IconResource from '../img';

export const getImage = integrationPartner => {
    const isWlSber = whiteLabelHelper.isSberWl();

    // т.к. Wl сбера имеет тот же номер integrationPartner, приходится отдельно вычислять, что это WL сбера
    if (isWlSber) {
        return getJsx({ file: IconResource.sberbankWLM, className: style.sberbankWallet });
    }

    const iconName = `${IntegrationPartnerIconName[integrationPartner]}M`;

    const walletImage = getJsx({ file: IconResource[iconName], className: style[iconName] });
    
    return walletImage;
};

export default getImage;

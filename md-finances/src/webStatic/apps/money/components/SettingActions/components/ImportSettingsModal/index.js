import React from 'react';
import PropTypes from 'prop-types';
import cn from 'classnames/bind';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import Switch from '@moedelo/frontend-core-react/components/Switch';
import Link from '@moedelo/frontend-core-react/components/Link';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { disable, enable, isEnabled } from '@moedelo/frontend-common-v2/services/firmFlagService';
import navigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import userDataService from '@moedelo/frontend-core-v2/services/userDataService';
import style from './style.m.less';
import { metrics, sendMetric } from '../../../../../paymentImportRules/helpers/metricsHelper';

const linkingWithReasonDocumentsForImportFlag = `LinkingWithReasonDocumentsForImport`;

const ImportSettingsModal = props => {
    const [linkingWithReasonDocumentsForImport, setLinkingWithReasonDocumentsForImport] = React.useState(false);
    const [isAutoLinkingAvailable, setIsAutoLinkingAvailable] = React.useState(false);
    const mountedRef = React.useRef(true);

    const onChange = () => {
        if (linkingWithReasonDocumentsForImport) {
            disable({ name: linkingWithReasonDocumentsForImportFlag });
        } else {
            enable({ name: linkingWithReasonDocumentsForImportFlag });
        }

        setLinkingWithReasonDocumentsForImport(!linkingWithReasonDocumentsForImport);
    };

    const fetch = () => {
        Promise.all([isEnabled({ name: linkingWithReasonDocumentsForImportFlag }), userDataService.get()]).then(([flag, userData]) => {
            if (!mountedRef.current) {
                return;
            }

            const { AccessRuleFlags: { HasFinControlIntegrationWithBanksTariff } } = userData;

            setIsAutoLinkingAvailable(!HasFinControlIntegrationWithBanksTariff);
            setLinkingWithReasonDocumentsForImport(flag);
        });
    };

    React.useEffect(() => {
        fetch();

        return () => {
            mountedRef.current = false;
        };
    }, []);

    const renderAutolinkingControl = () => {
        if (isAutoLinkingAvailable) {
            return (
                <div className={cn(grid.row)}>
                    <Switch
                        text={`Автопривязка документов при импорте`}
                        onChange={onChange}
                        checked={linkingWithReasonDocumentsForImport}
                    />
                </div>
            );
        }

        return null;
    };

    return <Modal header={`Настройки импорта`} width={`320px`} onClose={props.onClose} visible={props.isVisible}>
        {renderAutolinkingControl()}
        <div className={cn(grid.row)}>
            <Link
                href={`/Finances/PaymentImportRules`}
                text={`Правила импорта`}
                className={cn({ [style.link]: isAutoLinkingAvailable })}
                onClick={() => {
                    sendMetric(metrics.import_settings_payment_import_link_link_click);
                    navigateHelper.push(`/Finances/PaymentImportRules`);
                }}
            />
        </div>
    </Modal>;
};

ImportSettingsModal.propTypes = {
    onClose: PropTypes.func,
    isVisible: PropTypes.bool
};

export default ImportSettingsModal;

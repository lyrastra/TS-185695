import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import IconButton from '@moedelo/frontend-core-react/components/IconButton';
import DropdownList from '@moedelo/frontend-core-react/components/dropdown/DropdownList';
import rule from '@moedelo/frontend-core-react/icons/rule.m.svg';
import userDataService from '@moedelo/frontend-core-v2/services/userDataService';
import { mapPaymentRulesToDropdownDate, mapOutsourcePaymentRulesToDropdownDate } from './mappers/paymentRulesMapper';
import style from './style.m.less';

const cn = classnames.bind(style);

const PaymentRules = ({ paymentRules, outsourcePaymentRules }) => {
    const [visible, toggleVisibility] = React.useState(false);
    const [isOutsourceRuleDisabled, setOutsourceRuleDisabled] = React.useState(false);

    React.useEffect(() => {
        const getUserData = async () => {
            const { IsProfOutsourceUser } = await userDataService.get();

            setOutsourceRuleDisabled(!IsProfOutsourceUser);
        };

        getUserData();
    }, []);

    const onPaymentRuleClick = event => {
        NavigateHelper.open(event.value);
        event.preventDefault();
    };

    const showPaymentRules = event => {
        toggleVisibility(true);
        event.preventDefault();
        event.stopPropagation();
    };

    const handleClickOutside = () => {
        visible && toggleVisibility(false);
    };

    const mappedPaymentRules = mapPaymentRulesToDropdownDate(paymentRules);
    const mappedOutsourcePaymentRules = mapOutsourcePaymentRulesToDropdownDate(outsourcePaymentRules, isOutsourceRuleDisabled);
    const data = [...mappedPaymentRules, ...mappedOutsourcePaymentRules];

    return (
        <div
            onMouseLeave={handleClickOutside}
            className={cn(`paymentRules`)}
        >
            <IconButton
                onClick={showPaymentRules}
                icon={rule}
            />
            {visible ?
                <DropdownList
                    data={data}
                    onClick={onPaymentRuleClick}
                    maxWidth={250}
                    className={cn(`dropdown`)}
                /> :
                null
            }
        </div>
    );
};

PaymentRules.defaultProps = {
    outsourcePaymentRules: []
};

PaymentRules.propTypes = {
    paymentRules: PropTypes.arrayOf(PropTypes.shape({
        id: PropTypes.number.isRequired,
        name: PropTypes.string.isRequired
    })),
    outsourcePaymentRules: PropTypes.arrayOf(PropTypes.shape({
        id: PropTypes.number.isRequired,
        name: PropTypes.string.isRequired
    }))
};

export default PaymentRules;

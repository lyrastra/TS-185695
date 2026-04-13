import React from 'react';
import PropTypes from 'prop-types';
import Button, { Type } from '@moedelo/frontend-core-react/components/buttons/Button';
import Tooltip from '@moedelo/frontend-core-react/components/Tooltip';
import reports from '@moedelo/frontend-core-react/icons/reports.m.svg';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper/svgIconHelper';

const ApproveOperationButton = ({ isDisableApproved, setMassApprove, checkedOperations }) => {
    const onApproveClick = () => {
        const Ids = checkedOperations.map(el => el.DocumentBaseId);
        setMassApprove({ Ids });
    };

    if (isDisableApproved) {
        return <Tooltip
            content={`Выберите банковские операции в статусе "не обработано"`}
            width={240}
        >
            <Button
                onClick={onApproveClick}
                type={Type.Panel}
                disabled
            >
                { svgIconHelper.getJsx({ file: reports }) }
                Обработать
            </Button>
        </Tooltip>;
    }

    return (
        <Button
            onClick={onApproveClick}
            type={Type.Panel}
        >
            { svgIconHelper.getJsx({ file: reports }) }
            Обработать
        </Button>
    );
};
        
ApproveOperationButton.propTypes = {
    isDisableApproved: PropTypes.bool,
    setMassApprove: PropTypes.func,
    checkedOperations: PropTypes.arrayOf(PropTypes.object)

};

export default ApproveOperationButton;

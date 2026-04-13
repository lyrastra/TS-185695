import React from 'react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import PropTypes from 'prop-types';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import ButtonDropdown from '@moedelo/frontend-core-react/components/buttons/ButtonDropdown';
import classnames from 'classnames/bind';
import importOperationStatusEnum from '../../../../../../../../enums/newMoney/importOperationStatusEnum';
import style from './style.m.less';

const cn = classnames.bind(style);

class OperationHeader extends React.Component {
    constructor(props) {
        super(props);

        this.operationsStateIndex = props.data.OperationState || 3;
        this.headerText = importOperationStatusEnum[this.operationsStateIndex].text;
        this.iconType = importOperationStatusEnum[this.operationsStateIndex].icon;
    }

    onWhatToDoClick = (e) => {
        e.preventDefault();
        e.stopPropagation();
    }

    onWhatToDoSelect = (event) => {
        const { data } = this.props;

        switch (event.value) {
            case `Merge`:
                return this.props.onMerge();
            case `Import`:
                return this.props.onImport();
            case `Edit`:
                return this.props.onClick();
            case `Delete`:
                return this.props.onDelete(data);
        }

        return null;
    }

    render() {
        const dropDownData = [
            {
                value: `Merge`,
                text: `Обновить дату в сервисе (не импортируя)`,
                icon: svgIconHelper.getJsx({ name: `updateDate` })
            },
            {
                value: `Import`,
                text: `Не дубликат (импортировать)`,
                icon: svgIconHelper.getJsx({ name: `notDuplicate` })
            },
            {
                value: `Edit`,
                text: `Редактировать`,
                icon: svgIconHelper.getJsx({ name: `edit` })
            },
            {
                value: `Delete`,
                text: `Удалить`,
                icon: svgIconHelper.getJsx({ name: `remove` })
            }
        ];

        if (this.props.isOriginal) {
            return (
                <div className={grid.col_24}>
                    <div className={cn(`operation-type`)}>
                        <div className={cn(`operation-type`)}>
                            <div className={cn(`operation-type__icon`, `operation-type__icon--success`)}>
                                {svgIconHelper.getJsx({ name: `success` })}
                            </div>
                            Оригинал
                        </div>
                    </div>
                </div>
            );
        }

        return (
            <div className={grid.col_24}>
                <div className={cn(`operation-type`)}>
                    <div className={cn(`operation-type`)}>
                        <div className={cn(`operation-type__icon`, `operation-type__icon--${this.iconType}`)}>
                            {svgIconHelper.getJsx({ name: this.iconType })}
                        </div>
                        {this.headerText}
                        {this.operationsStateIndex === 2 &&
                        <ButtonDropdown
                            data={dropDownData}
                            color="white"
                            onClick={this.onWhatToDoClick}
                            onSelect={this.onWhatToDoSelect}
                            className={cn(`what-to-do__button`)}
                        >
                            <span className={cn(`what-to-do__button__text`)}>Что сделать ?</span>
                        </ButtonDropdown>}
                    </div>
                </div>
            </div>
        );
    }
}

OperationHeader.propTypes = {
    data: PropTypes.object,
    onClick: PropTypes.func,
    onDelete: PropTypes.func,
    onMerge: PropTypes.func,
    onImport: PropTypes.func,
    isOriginal: PropTypes.bool
};

export default OperationHeader;


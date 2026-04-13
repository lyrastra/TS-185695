import React from 'react';
import { observer, inject } from 'mobx-react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import Button, { Color } from '@moedelo/frontend-core-react/components/buttons/Button';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import MarkedList from '@moedelo/frontend-core-react/components/MarkedList';
import Marker from '@moedelo/frontend-core-react/components/MarkedList/enums/Marker';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import P from '@moedelo/frontend-core-react/components/P';
import { pluralNoun } from '@moedelo/frontend-core-v2/helpers/PluralHelper';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import { availableOperationsDescription } from './metaData';
import style from './style.m.less';

const cn = classnames.bind(style);

@inject(`massChangeTaxSystemStore`)
@observer
class MassTaxSystemChangeModal extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.massChangeTaxSystemStore;
    }

    onSelectTaxationSystem = data => {
        const { setNewTaxSystem, updateInvalidCount } = this.store;

        setNewTaxSystem(data);
        updateInvalidCount();
    };

    getDropdownData = () => {
        const { getTaxationSystemsForDropdown } = this.store;

        return getTaxationSystemsForDropdown();
    };

    rednerSelectedOperationsValidity = () => {
        const { invalidCount } = this.store;

        if (invalidCount) {
            const pluralOperationCnt = pluralNoun(invalidCount, `операции`, `операций`, `операций`, true);

            return (
                <div>
                    <b>Внимание!</b><br />
                    У {pluralOperationCnt} невозможно сменить СНО.<br />
                    Возможные причины: неподходящий тип, закрытый период,
                    несовмещенная СНО на дату создания операции.
                </div>
            );
        }

        return null;
    };

    changeOperationsTaxSystem = async () => {
        const { changeOperationsTaxSystem } = this.store;

        try {
            const { onSuccess } = this.props;

            await changeOperationsTaxSystem();

            NotificationManager.show({
                message: `Запрос на смену СНО у операций успешно отправлен`,
                type: `success`,
                duration: 4000
            });

            onSuccess && onSuccess();
        } catch (e) {
            NotificationManager.show({
                message: `При отправке возникла неизвестная ошибка`,
                type: `error`,
                duration: 4000
            });

            throw new Error(e);
        }
    };

    render() {
        const {
            validList,
            isDualTaxationSystem,
            updateInvalidCount,
            isModalVisible,
            setModalVisibility,
            newTaxSystem,
            loading,
            checkedOperations,
            invalidCount
        } = this.store;

        updateInvalidCount();

        if (!validList.length || !isDualTaxationSystem()) {
            return null;
        }

        return (
            <Modal
                visible={isModalVisible}
                header="Смена системы налогообложения"
                needCloseIcon={false}
                onClick={() => setModalVisibility(false)}
                onClose={() => {}}
                width="320px"
            >
                <div className={grid.row}>
                    <Dropdown
                        data={this.getDropdownData()}
                        value={newTaxSystem}
                        onSelect={data => this.onSelectTaxationSystem(data)}
                        className={cn(style.taxStructuresDropdown)}
                        width={`100%`}
                    />
                </div>
                <div className={cn(style.availableOperationsContainer)}>
                    <P>СНО можно сменить у операций</P>
                    <MarkedList
                        className="customClass"
                        list={availableOperationsDescription}
                        marker={Marker.disc}
                    />
                </div>
                <div className={grid.row}>
                    {this.rednerSelectedOperationsValidity()}
                </div>
                <ElementsGroup
                    className={cn(style.elementsGroup)}
                    margin={15}
                >
                    <Button
                        disabled={loading}
                        onClick={() => setModalVisibility(false)}
                        color={Color.White}
                    >
                        Отменить
                    </Button>
                    <Button
                        loading={loading}
                        onClick={this.changeOperationsTaxSystem}
                        className="button_change_tax_system"
                        disabled={checkedOperations?.length === invalidCount}
                    >
                        Применить
                    </Button>
                </ElementsGroup>
            </Modal>
        );
    }
}

MassTaxSystemChangeModal.propTypes = {
    onSuccess: PropTypes.func,
    massChangeTaxSystemStore: PropTypes.object
};

export default MassTaxSystemChangeModal;

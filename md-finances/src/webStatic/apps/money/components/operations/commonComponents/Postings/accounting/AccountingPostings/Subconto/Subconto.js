import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import List from '@moedelo/frontend-core-react/components/list/List';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import contractDialogHelper from '@moedelo/frontend-common/helpers/addDialogHelper';
import ContractorCard from '@moedelo/frontend-common-v2/apps/kontragents/components/ContractorCard';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { subcontoAutocomplete } from '../../../../../../../../../services/newMoney/subcontoService';
import subcontoTypeResource from '../../../../../../../../../resources/newMoney/subcontoTypeResource';
import SubcontoType from '../../../../../../../../../enums/newMoney/SubcontoTypeEnum';
import SyntheticAccountCodesEnum from '../../../../../../../../../enums/SyntheticAccountCodesEnum';

class Subconto extends React.Component {
    constructor(props) {
        super(props);

        const {
            _53_01, _55_03, _55_04, _860100
        } = SyntheticAccountCodesEnum;

        this.state = {
            showContractorDialog: false
        };

        this.query = ``;
        this.autocompleteRef = React.createRef();
        this.canSetCustomSubcontoNumber = [_53_01, _55_03, _55_04, _860100].includes(props.accountCode);
    }

    onChange = value => {
        if (typeof value === `string`) {
            this.query = value;

            return;
        }

        const subconto = { ...this.props.subconto.Subconto, ...value.value };
        this.props.onChange({ ...this.props.subconto, ...{ Subconto: subconto } });
    };

    onBlur = value => {
        const { onChange, subconto } = this.props;

        if (!value) {
            onChange({ ...subconto, ...{ Subconto: {} } });

            return;
        }

        if (this.canSetCustomSubcontoNumber && subconto?.Subconto?.Name !== value) {
            const customSubconto = {
                Id: 0,
                Name: value,
                SubcontoType: subconto?.Type
            };

            onChange(Object.assign({}, subconto, { Subconto: customSubconto }));

            return;
        }

        onChange(subconto);
    };

    getData = async ({ query }) => {
        const {
            subconto,
            accountCode,
            kontragent,
            dateDocument
        } = this.props;

        const subcontos = await subcontoAutocomplete({
            query: query || ``,
            type: subconto.Type,
            accountCode,
            kontragentSubcontoId: kontragent && kontragent.SubcontoId,
            date: dateDocument
        });

        return Promise.resolve({
            data: subcontos.map(item => ({ text: item.Name, value: item })),
            value: query
        });
    };

    getActionsSection = () => {
        const { Type } = this.props.subconto;

        if (Type === SubcontoType.Kontragent) {
            return <List data={[{ text: `+ контрагент` }]} onClick={this.toggleContractorAddingDialog} />;
        }

        if (Type === SubcontoType.Contract) {
            return <List data={[{ text: `+ договор` }]} onClick={this.showContractAddDialog} />;
        }

        /* eslint-disable-next-line */
        return <React.Fragment />;
    };

    getQaSelectorClassName = () => {
        const { Type } = this.props.subconto;

        if (Type === SubcontoType.Kontragent) {
            return `qa-subcontoContractor`;
        }

        return `qa-subcontoDocument`;
    };

    setContractor = contractor => {
        this.props.onChange({
            ...this.props.subconto,
            ...{
                Subconto: {
                    Id: contractor.value,
                    Name: contractor.text,
                    SubcontoId: contractor.SubcontoId,
                    SubcontoType: SubcontoType.Kontragent
                }
            }
        });
        this.toggleContractorAddingDialog();
    };

    toggleContractorAddingDialog = () => {
        this.setState({ showContractorDialog: !this.state.showContractorDialog });
    };

    showContractAddDialog = () => {
        const { kontragent } = this.props;
        const { current: { instanceRef: { handleClickOutside, isShowDropdownList } } } = this.autocompleteRef;

        if (handleClickOutside && isShowDropdownList) {
            isShowDropdownList() && handleClickOutside({});
        }

        contractDialogHelper.showDialog({
            data: {
                Direction: Direction.Incoming,
                KontragentName: kontragent && kontragent.Name,
                KontragentSubcontoId: kontragent && kontragent.SubcontoId
            },
            onSave: contract => {
                this.props.onChange({
                    ...this.props.subconto,
                    ...{
                        Subconto: {
                            Id: contract.SubcontoId,
                            Name: `Договор №${contract.ProjectNumber} от ${contract.ContractDate}`,
                            SubcontoId: contract.SubcontoId,
                            SubcontoType: SubcontoType.Contract
                        }
                    }
                });
            }
        });
    };

    render() {
        const { subconto, error, message } = this.props;
        const { showContractorDialog } = this.state;
        const placeholder = subcontoTypeResource[subconto.Type];

        return (
            <Fragment>
                { showContractorDialog && <ContractorCard onClose={this.toggleContractorAddingDialog} onContractorAdded={this.setContractor} /> }

                <Autocomplete
                    onChange={this.onChange}
                    onBlur={this.onBlur}
                    getData={this.getData}
                    placeholder={placeholder}
                    getActionsSection={this.getActionsSection}
                    value={subconto.Subconto ? subconto.Subconto.Name : ``}
                    showAsText={subconto.Subconto && subconto.Subconto.ReadOnly}
                    iconName={`none`}
                    maxWidth={540}
                    maxLength={20}
                    ref={this.autocompleteRef}
                    notOnlyFromList
                    error={error}
                    message={message}
                    className={this.getQaSelectorClassName()}
                />
            </Fragment>
        );
    }
}

Subconto.propTypes = {
    onChange: PropTypes.func,
    subconto: PropTypes.object,
    accountCode: PropTypes.number,
    kontragent: PropTypes.object,
    dateDocument: PropTypes.string,
    error: PropTypes.bool,
    message: PropTypes.string
};

export default Subconto;


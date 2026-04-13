import React from 'react';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import { observer } from 'mobx-react';
import KontragentTypeResource from './KontragentTypeResources';
import { getKontragentByTypeAndQuery, getKontragentNameByIdAndType } from '../../../../../../services/newMoney/contractorService';
import KontragentType from '../../../../../../enums/KontragentType';
import style from './style.m.less';

const cn = classnames.bind(style);

class KontragentSection extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.store;

        this.state = {
            kontragentName: ``
        };

        this._loadKontragentName();
    }

    onChangeType = ({ value }) => {
        if (value === KontragentType.All) {
            this.store.kontragentType = value;
        } else {
            this.store.setKontragentValues({
                kontragentType: value,
                kontragentId: null
            });

            this.setState({
                kontragentName: ``
            });
        }
    }

    onChangeAutocomplete = ({ value, type, text }) => {
        if (value) {
            this.store.setKontragentValues({
                kontragentType: type,
                kontragentId: value
            });

            this.setState({
                kontragentName: text
            });
        } else {
            this._onClear();
        }
    }

    getAutocompleteData = ({ query }) => {
        const { kontragentType } = this.store;

        return getKontragentByTypeAndQuery({
            type: kontragentType,
            query
        }).then(resp => {
            return {
                data: mapKontragentData(resp),
                value: query
            };
        });
    }

    _onClear = () => {
        this.store.kontragentId = null;

        this.setState({
            kontragentName: ``
        });
    }

    _loadKontragentName() {
        const { kontragentId, kontragentType } = this.store;

        if (kontragentId) {
            this.state.loading = true;

            getKontragentNameByIdAndType({
                id: kontragentId,
                type: kontragentType
            }).then(resp => {
                this.setState({
                    loading: false,
                    kontragentName: resp.Name
                });
            });
        }
    }

    render() {
        const { kontragentType } = this.store;
        const { kontragentName, loading } = this.state;

        return (
            <div>
                <div className={cn(`kontragentType`)}>
                    <Dropdown
                        data={KontragentTypeResource}
                        value={kontragentType}
                        type={`link`}
                        onSelect={this.onChangeType}
                    />
                </div>
                <Autocomplete
                    value={kontragentName}
                    loading={loading}
                    maxWidth={400}
                    getData={this.getAutocompleteData}
                    onChange={this.onChangeAutocomplete}
                />
            </div>
        );
    }
}

KontragentSection.propTypes = {
    store: PropTypes.object
};

function mapKontragentData(data = []) {
    return data.length && data.map(obj => {
        return { text: obj.Name, value: obj.Id, type: obj.Type };
    });
}

export default observer(KontragentSection);

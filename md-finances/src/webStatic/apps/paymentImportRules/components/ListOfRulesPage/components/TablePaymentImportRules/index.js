import React from 'react';
import Loader, { Size as LoaderSize } from '@moedelo/frontend-core-react/components/Loader';
import P from '@moedelo/frontend-core-react/components/P';
import NewTable from '@moedelo/frontend-core-react/components/NewTable';
import IconButton from '@moedelo/frontend-core-react/components/IconButton';
import navigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import { getListRulesAsync } from '../../../../services/paymentImportRulesService';
import tableConfig from './tableConfig';
import { deletePaymentImportRule } from '../../../../helpers/deleteHelper';
import style from './style.m.less';
import { metrics, sendMetric } from '../../../../helpers/metricsHelper';

class TablePaymentImportRules extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            data: null
        };
    }

    componentDidMount = async () => {
        const list = await getListRulesAsync();
        const listWithActions = list.map(tableRowData => {
            return {
                ...tableRowData,
                removeButton: (<IconButton
                    onClick={
                        e => {
                            e.stopPropagation();
                            this.deleteRow(tableRowData.id);
                        }
                    }
                    icon="remove"
                    className={style.delIcon}
                />),
                onClick: () => {
                    sendMetric(metrics.import_settings_rules_table_row_click, tableRowData.id);
                    navigateHelper.push(`edit/${tableRowData.id}`);
                }
            };
        });
        this.setState({ data: listWithActions });
    }

    deleteRow = async id => {
        await deletePaymentImportRule(id);
        this.setState({ data: this.state.data.filter(x => x.id !== id) });
    }

    render() {
        const { data } = this.state;

        if (data == null) {
            return <Loader size={LoaderSize.medium} className={style.emptyTable} />;
        }

        if (!data.length) {
            return <P className={style.emptyTable}>Список пуст</P>;
        }

        return <NewTable cols={tableConfig.cols} data={data} />;
    }
}

export default TablePaymentImportRules;

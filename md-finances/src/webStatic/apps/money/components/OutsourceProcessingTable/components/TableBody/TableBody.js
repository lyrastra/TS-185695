import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import TableLoader from '@moedelo/frontend-core-react/components/table/TableLoader';
import TableOverFooter from '@moedelo/frontend-core-react/components/table/TableOverFooter';
import { pluralNoun } from '@moedelo/frontend-core-v2/helpers/PluralHelper';
import { observer } from 'mobx-react';
import Row from '../Row';
import TableStore from '../../../../stores/TableStore';
import style from '../../style.m.less';

const cn = classnames.bind(style);
const pageCount = 20;

let height = 0;

@observer
class TableBody extends React.Component {
    constructor(props) {
        super(props);

        this.store = new TableStore();

        this.state = {
            rowsHeight: 0,
            isOpen: props.isOpen,
            loaded: props.loaded,
            tableCount: props.tableCount
        };

        this.store.loadOperations();
    }

    componentDidMount() {
        if (this.props.filter.successTable) {
            this.getTableHeight();
        }
    }

    componentDidUpdate() {
        this.tableBody.childNodes.forEach(child => {
            height += child.clientHeight;
        });

        const { isOpen, loaded, tableCount } = this.props;
        this.setState({
            tableCount,
            isOpen,
            loaded,
            rowsHeight: this.tableLoader ? height - this.tableLoader.clientHeight : height
        });
        height = 0;
    }

    onClickShowMoreButton = () => {
        this.props.onClickShowMoreButton();
        this.store.loadOperations();
    }

    getTableHeight = () => {
        this.tableBody.childNodes.forEach(child => {
            height += child.clientHeight;
        });

        const { isOpen, loaded, tableCount } = this.state;
        this.setState({
            tableCount,
            isOpen,
            loaded,
            rowsHeight: this.tableLoader ? height - this.tableLoader.clientHeight : height
        });
        height = 0;
    }

    loader = () => {
        return <TableLoader ref={c => { this.tableLoader = c; }} />;
    }

    render() {
        const {
            totalCount,
            onClickRow
        } = this.props;
        const { operations } = this.store;
        const {
            isOpen, loaded, rowsHeight, tableCount
        } = this.state;

        const tableStyle = {
            height: isOpen && `${rowsHeight}px`
        };

        return (
            <div
                ref={c => {
                    this.tableBody = c;
                }}
                className={cn(`tableBody`, { 'tableBody--closed': !isOpen })}
                style={tableStyle}
            >
                {!loaded && this.loader()}
                {operations.map(operation => <Row
                    onClick={onClickRow}
                    key={operation.DocumentBaseId}
                    value={operation}
                    ref={operation.DocumentBaseId}
                />)}
                <TableOverFooter
                    ref={c => { this.tableFooter = c; }}
                    className={cn(`tableOverFooter`)}
                    showButton={tableCount < totalCount}
                    buttonText={`Показать еще ${pluralNoun(Math.min(totalCount - tableCount, pageCount), `операцию`, `операции`, `операций`, true)}`}
                    text={`Отображено: ${tableCount} из ${pluralNoun(totalCount, `операции`, `операций`, `операций`, true)}`}
                    onClick={this.onClickShowMoreButton}
                    buttonLoading={false}
                />
            </div>
        );
    }
}

TableBody.propTypes = {
    isOpen: PropTypes.bool,
    loaded: PropTypes.bool,
    tableCount: PropTypes.number,
    filter: PropTypes.object,
    onClickShowMoreButton: PropTypes.func,
    totalCount: PropTypes.number,
    onClickRow: PropTypes.func
};

export default TableBody;

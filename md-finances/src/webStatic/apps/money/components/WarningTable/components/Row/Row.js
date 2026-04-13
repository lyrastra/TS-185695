import React from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import TableRow from '@moedelo/frontend-core-react/components/table/TableRow';
import Operation from '../Operation/';
import style from './style.m.less';

const cn = classnames.bind(style);

const Row = props => {
    const operation = props.data;

    return (
        <TableRow className={cn(`unrecognized-ops-table-row`)} onClickRow={() => {}}>
            <Operation
                isOriginal={props.isOriginal}
                data={operation}
                onDelete={props.onDelete}
                onClickRow={props.onClickRow}
                onMerge={props.onMerge}
                onImport={props.onImport}
            />
        </TableRow>
    );
};

Row.propTypes = {
    data: PropTypes.object,
    onDelete: PropTypes.func,
    onClickRow: PropTypes.func,
    onMerge: PropTypes.func,
    onImport: PropTypes.func,
    isOriginal: PropTypes.bool
};

export default Row;

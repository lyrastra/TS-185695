import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import OperationHeader from './components/OperationHeader';
import OperationBody from './components/OperationBody';
import style from './style.m.less';

const cn = classnames.bind(style);

const Operation = props => {
    const operation = props.data;

    const editOperation = () => {
        props.onClickRow(props.data);
    };

    return (
        <div
            className={cn(`operation`)}
            role={`button`}
            onClick={editOperation}
            onKeyPress={() => {}}
            tabIndex="-1"
        >
            <OperationHeader
                isOriginal={props.isOriginal}
                duplicate={!!operation.BaseOperation}
                data={operation}
                onDelete={props.onDelete}
                onClick={editOperation}
                onMerge={props.onMerge}
                onImport={props.onImport}
            />
            <OperationBody
                data={operation}
                base={props.isOriginal}
                onDelete={props.onDelete}
            />
        </div>
    );
};

Operation.propTypes = {
    data: PropTypes.object,
    onDelete: PropTypes.func,
    onClickRow: PropTypes.func,
    onMerge: PropTypes.func,
    onImport: PropTypes.func,
    isOriginal: PropTypes.bool
};

export default Operation;

import React from 'react';
import { observer } from 'mobx-react';
import style from './style.m.less';

const OperationList = observer(({
    operations, tableCount, copyOperation, onSelect, onClick, onDelete, onSendToBank, Row, commonDataStore, onApprove, isHistoryButtonVisible
}) => {
    return operations.map(operation => {
        let sendToBank = false;

        if (operation.CanSendToBank) {
            sendToBank = onSendToBank;
        }
        
        return (
            <div key={operation.DocumentBaseId} className={style.linkRow}>
                <Row
                    onClick={onClick}
                    copyOperation={value => { return copyOperation(value); }}
                    value={operation}
                    onSelect={onSelect}
                    onDelete={onDelete}
                    onSendToBank={sendToBank}
                    tableCount={tableCount}
                    commonDataStore={commonDataStore}
                    onApprove={onApprove}
                    isHistoryButtonVisible={isHistoryButtonVisible}
                />
            </div>
        );
    });
});

export default OperationList;

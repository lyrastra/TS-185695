import React from 'react';
import PropTypes from 'prop-types';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import TextAlignEnum from '@moedelo/frontend-core-react/components/Input/enums/TextAlign';
import Link from '@moedelo/frontend-core-react/components/Link';
import IconButton from '@moedelo/frontend-core-react/components/IconButton';
import Tooltip from '@moedelo/frontend-core-react/components/Tooltip';
import PositionEnum from '@moedelo/frontend-core-react/components/Tooltip/enums/PositionEnum';
import style from './style.m.less';

const Sum = ({
    value = null, label, error, onChange, unit, showAsText, onEnter, textAlign, onOpen, onClose, isOpen
}) => {
    // const [isOpen, setOpen] = React.useState(!!value);

    React.useEffect(() => {
        if (value && !isOpen) {
            onOpen();
        }
    }, []);

    if (showAsText) {
        return <div className={style.noWrap}>
            <Input
                value={value}
                label={label}
                type={InputType.number}
                unit={unit}
                showAsText
            />
        </div>;
    }

    if (!isOpen) {
        return <div className={style.openLinkRow}>
            <Link
                text={`+ ${label}`}
                onClick={onOpen}
            />
            <Tooltip
                content={
                    <React.Fragment>
                        <p> Используется, если у вас нет закрывающих документов. Например, для таких операций как:</p>
                        <ol>
                            <li>обеспечительный платеж</li>
                            <li>возврат от контрагента (контрагенту)</li>
                            <li>ошибочный платеж</li>
                        </ol>
                        <p>
                            Настройка влияет на <a href="https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/otchetnost-nalogi-prof/v-nalogovuyu-prof/reestr-nedostayushhih-dokumentov" target="blank">реестр</a> и на связи документов с платежами.
                        </p>
                    </React.Fragment>}
                position={PositionEnum.right}
            />
        </div>;
    }

    return <div className={style.sum}>
        <p className={style.label}>Сумма резерва</p>
        <div className={style.inputRow}>
            <Input
                value={value}
                onBlur={onChange}
                onChange={onEnter}
                decimalLimit={2}
                allowDecimal
                allowNegative={false}
                type={InputType.number}
                error={!!error}
                message={error}
                min={0}
                textAlign={textAlign}
                className={style.input}
                unit={unit}
            />
            <div className={style.removeButton}>
                <IconButton
                    onClick={() => {
                        // setOpen(false);
                        onClose();
                    }}
                    size={`small`}
                    icon={`clear`}
                />
            </div>
        </div>
    </div>;
};

Sum.defaultProps = {
    textAlign: TextAlignEnum.right,
    unit: `₽`
};

Sum.propTypes = {
    value: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
    label: PropTypes.string,
    onChange: PropTypes.func,
    onOpen: PropTypes.func,
    onClose: PropTypes.func,
    onEnter: PropTypes.func,
    error: PropTypes.string,
    unit: PropTypes.string,
    showAsText: PropTypes.bool,
    isOpen: PropTypes.bool,
    textAlign: PropTypes.oneOf(Object.values(TextAlignEnum))
};

export default Sum;

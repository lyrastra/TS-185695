import React from 'react';
import PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import ContractKind from '@moedelo/frontend-enums/mdEnums/ContractKind';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { autocomplete, getContractById } from '../../../../../../services/contractService';
import { paymentOrderOperationResources } from '../../../../../../resources/MoneyOperationTypeResources';
import RuleConditionObject from '../../../../enums/RuleConditionObject';

const incomingLoanOperationsTypes = [
    paymentOrderOperationResources.PaymentOrderIncomingLoanObtaining.value,
    paymentOrderOperationResources.PaymentOrderOutgoingLoanRepayment.value
];

const mediationContractOperationsTypes = [
    paymentOrderOperationResources.PaymentOrderOutgoingPaymentAgencyContract.value,
    paymentOrderOperationResources.PaymentOrderIncomingIncomeFromCommissionAgent.value
];

const defaultContract = { text: ``, value: null };

const getContractText = ({ number, date }) => `Договор № ${number} от ${dateHelper(date).format(`DD.MM.YYYY`)}`;

const ContractAutocomplete = ({
    contractId, selectContractId, operationType, disabled, conditions
}) => {
    const [currentContract, setContract] = React.useState(defaultContract);

    React.useEffect(() => {
        setContract(defaultContract);
    }, [operationType]);

    React.useEffect(() => {
        if (contractId && !currentContract.value) {
            const getContract = async () => {
                const value = await getContractById({ contractId });

                setContract({
                    text: getContractText({ number: value.Number, date: value.Date }),
                    value
                });
                selectContractId(value);
            };

            getContract();
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [contractId]);

    const contractKinds = React.useMemo(() => {
        if (mediationContractOperationsTypes.includes(operationType)) {
            return [ContractKind.Mediation];
        }

        return incomingLoanOperationsTypes.includes(operationType) ?
            [ContractKind.ReceivedLoan, ContractKind.ReceivedCredit] :
            [ContractKind.OutgoingLoan];
    }, [operationType]);

    const getKontragentId = () => {
        const kontragentsFromConditions = conditions.reduce((arr, { contractorId }) => (contractorId && !arr.includes(contractorId) ? [...arr, contractorId] : [...arr]), []);

        if (kontragentsFromConditions.length === 1) {
            return kontragentsFromConditions[0];
        }

        return null;
    };

    const getContractData = async ({ query }) => {
        const splittedQuery = query?.split(` `);
        const queryString = splittedQuery.length > 2 ? splittedQuery[2] : query;

        // если в условиях указан один комиссионер, устанавливаем его для запроса договора. Если указаны разные комиссионеры, возвращаем пустой список договоров
        const kontragentId = getKontragentId();
        const mustReturnEmptyData = !kontragentId && conditions.filter(({ type, contractorId }) => type === RuleConditionObject.Contractor && contractorId)?.length;
        let data = [];

        if (!mustReturnEmptyData) {
            data = await autocomplete({
                query: queryString,
                kind: contractKinds,
                kontragentId,
                withMainContract: false
            });
        }

        return {
            data: data.map(item => ({
                text: getContractText({ number: item.Number, date: item.Date }),
                value: item
            })),
            value: query
        };
    };

    const onSelectContract = ({ text, value }) => {
        const selectedContract = value ? { text, value } : defaultContract;
        setContract(selectedContract);

        selectContractId(selectedContract.value);
    };

    return <div className={grid.row}>
        <div className={grid.col_8} />
        <div className={grid.col_10}>
            <Autocomplete
                onChange={onSelectContract}
                value={currentContract.text}
                getData={getContractData}
                placeholder={`Выберите договор`}
                disabled={disabled}
            />
        </div>
    </div>;
};

ContractAutocomplete.propTypes = {
    contractId: PropTypes.number,
    selectContractId: PropTypes.func.isRequired,
    operationType: PropTypes.number,
    disabled: PropTypes.bool,
    conditions: PropTypes.arrayOf(PropTypes.shape({
        contractorId: PropTypes.number
    }))
};

export default ContractAutocomplete;

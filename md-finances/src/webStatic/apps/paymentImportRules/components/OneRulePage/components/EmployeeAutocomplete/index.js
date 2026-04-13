import React from 'react';
import PropTypes from 'prop-types';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import workerService from '../../../../../../services/workerService';

const defaultEmployee = { id: null, name: `` };

const EmployeeAutocomplete = ({
    employeeId, selectEmployee, startDate, disabled
}) => {
    const [currentEmployee, setEmployee] = React.useState(defaultEmployee);

    React.useEffect(() => {
        if (employeeId) {
            const getEmployee = async () => {
                const curEmployee = await workerService.getEmployeeById(employeeId);

                curEmployee && setEmployee({
                    id: employeeId,
                    name: `${curEmployee.Surname} ${curEmployee.Name} ${curEmployee.PatronymicName}`
                });
            };

            getEmployee();
        }
    }, []);

    const getEmployeesData = async ({ query }) => {
        const employees = await workerService.autocomplete({ query, date: startDate, onlyInStaff: true });

        return {
            data: employees.map(employee => ({ value: employee, text: employee.Name })),
            value: query
        };
    };

    const onSelectEmployee = ({ value }) => {
        const selectedEmployee = value ? { id: value.Id, name: value.Name } : defaultEmployee;

        setEmployee(selectedEmployee);
        selectEmployee(selectedEmployee.id);
    };

    return <div className={grid.row}>
        <div className={grid.col_8} />
        <div className={grid.col_10}>
            <Autocomplete
                getData={getEmployeesData}
                value={currentEmployee.name}
                onChange={onSelectEmployee}
                placeholder={`Выберите сотрудника`}
                disabled={disabled}
            />
        </div>
    </div>;
};

EmployeeAutocomplete.defaultProps = {
    startDate: dateHelper().format(`DD.MM.YYYY`),
    employeeId: defaultEmployee.id
};

EmployeeAutocomplete.propTypes = {
    employeeId: PropTypes.number,
    selectEmployee: PropTypes.func.isRequired,
    startDate: PropTypes.string,
    disabled: PropTypes.bool
};

export default EmployeeAutocomplete;

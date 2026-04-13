import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';

const TaxationSystemNameResource = {
    [TaxationSystemType.Usn]: `УСН`,
    [TaxationSystemType.UsnAndEnvd]: `УСН+ЕНВД`,
    [TaxationSystemType.Osno]: `ОСНО`,
    [TaxationSystemType.OsnoAndEnvd]: `ОСНО+ЕНВД`,
    [TaxationSystemType.Envd]: `ЕНВД`,
    [TaxationSystemType.Patent]: `ПСН`
};

export default TaxationSystemNameResource;

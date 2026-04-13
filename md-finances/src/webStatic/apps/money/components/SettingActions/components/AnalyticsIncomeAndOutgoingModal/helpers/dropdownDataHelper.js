import ReportEnum from '../enums/ReportEnum';

export const getReports = (patentList, onClickReport) => {
    const reports = [
        {
            text: `Аналитика УСН`,
            value: ReportEnum.Usn,
            onClick: () => onClickReport(ReportEnum.Usn)
        }
    ];

    if (patentList?.length) {
        reports.push({
            text: `Аналитика Патент`,
            value: ReportEnum.Patent,
            onClick: () => onClickReport(ReportEnum.Patent)
        });
    }

    return reports;
};

export default getReports;

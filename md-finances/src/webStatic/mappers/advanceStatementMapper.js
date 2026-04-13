export function mapAdvanceStatementsToBackendModel(list = []) {
    return list.map(advanceStatement => {
        return {
            DocumentBaseId: advanceStatement.DocumentBaseId,
            Sum: advanceStatement.LinkSum
        };
    });
}

export default {
    mapAdvanceStatementsToBackendModel
};

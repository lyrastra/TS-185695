export default {
    mapToSelect(patents) {
        return patents.map((patent) => ({
            label: patent.ShortName,
            value: patent.Id
        }));
    }
};

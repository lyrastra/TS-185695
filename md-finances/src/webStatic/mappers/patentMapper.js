export default {
    mapPatentsToDropDown(patents) {
        return patents.map(patent => ({
            value: patent.Id,
            text: patent.ShortName
        }));
    }
};

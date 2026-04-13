const stringHelper = {
    clip(value, length, endSymbol) {
        const valueStr = value.toString();
        return valueStr.length > length
            ? valueStr.substring(0, length) + endSymbol
            : valueStr;
    }
};

export default stringHelper;

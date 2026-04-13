module.exports = {
    entry: {
        header: `apps/header/index.js`,
        index: `apps/money/index.js`,
        paymentImportRules: `apps/paymentImportRules/index.js`,
        oldCore: `apps/old/oldCore.js`
    },
    chunks: {
        vendor: {
            test: /node_modules\\(?!@moedelo)/,
            name: `vendor`,
            chunks: `initial`
        },
        md: {
            test: /node_modules\\@moedelo\\(?!frontend-header)(?!chatbot)/,
            name: `md`,
            chunks: `initial`
        }
    }
};

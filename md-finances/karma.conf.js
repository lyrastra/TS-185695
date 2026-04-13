const config = require(`@moedelo/karma-config`);
const path = require(`path`);

const root = path.resolve(`./`);

module.exports = config({
    baseUrl: root
});

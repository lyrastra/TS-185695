/* eslint-disable */
import ndsRatesFromAccPolService from '../../../../../services/ndsRatesFromAccPolService';

(function(common, money) {
    common.Utils.CommonDataLoader = (function() {
        return {
            getTaxationSystemsFromLayer() {
                if (window.Account) {
                    this.TaxationSystems = Account.TaxationSystems;
                    if (this.TaxationSystems) {
                        this.TaxationSystems.loaded = true;
                    }
                }
            },

            loadTaxationSystems() {
                this.getTaxationSystemsFromLayer();

                const loader = this;
                if (!this.TaxationSystems) {
                    this.TaxationSystems = new money.Collections.Common.TaxationSystemCollection();
                    this.TaxationSystems.fetch({
                        success() {
                            loader.TaxationSystems.loaded = true;
                        }
                    });
                }
            },

            loadAccountingPolicy(year) {
                const loader = this;

                if (!this.AccountingPolicy) {
                    this.AccountingPolicy = new common.Models.AccountingPolicyModel();
                }

                if (!this.AccountingPolicy.yearStack || !this.AccountingPolicy.yearStack[year]) {
                    loader.AccountingPolicy.loaded = false;
                    this.AccountingPolicy.fetch({
                        data: { year },
                        success() {
                            loader.AccountingPolicy.loaded = true;
                        },
                        type: `POST`
                    });
                } else {
                    this.AccountingPolicy.set(this.AccountingPolicy.yearStack[year]);
                }
            },

            loadSettlements() {
                const loader = this;
                if (!this.Settlements) {
                    this.Settlements = new money.Collections.Common.SettlementCollection();
                    this.Settlements.fetch({
                        success() {
                            loader.Settlements.loaded = true;
                        }
                    });
                }
            },

            loadFirmRequisites() {
                this.FirmRequisites = this.FirmRequisites || createRequisitesModel();
                !this.FirmRequisites.loaded && this.FirmRequisites.load();
            },
            
            loadNdsRatesFromAccPol() {
                const self = this;
                if (!this.NdsRates) {
                    this.NdsRates = [];
                    ndsRatesFromAccPolService.getNdsRatesAsync().then((ndsRates) => {
                        self.NdsRates = ndsRates;
                        self.NdsRates.loaded = true;
                    })
                }
            },


            loadNdsRatesFromAccPol() {
                const self = this;
                if (!this.NdsRates) {
                    this.NdsRates = [];
                    ndsRatesFromAccPolService.getNdsRatesAsync().then((ndsRates) => {
                        self.NdsRates = ndsRates;
                        self.NdsRates.loaded = true;
                    })
                }
            },

            loadNdsRatesFromAccPol() {
                const self = this;
                if (!this.NdsRates) {
                    this.NdsRates = [];
                    ndsRatesFromAccPolService.getNdsRatesAsync().then((ndsRates) => {
                        self.NdsRates = ndsRates;
                        self.NdsRates.loaded = true;
                    })
                }
            },


            MoneyBalanceMaster: {
                loadUsnDeclarationData(year) {
                    if (!this.UsnDeclaration) {
                        this.UsnDeclaration = new money.Models.Common.UsnDeclaration({
                            Year: year
                        });
                        this.UsnDeclaration.fetch();
                    }
                },
                loadEnvdDeclarationData(year) {
                    if (!this.EnvdDeclaration) {
                        this.EnvdDeclaration = new money.Models.Common.EnvdDeclaration({
                            Year: year
                        });
                        this.EnvdDeclaration.fetch();
                    }
                },
                loadForwardLink(year) {
                    if (!this.ForwardLink) {
                        this.ForwardLink = new money.Models.Common.ForwardLink({
                            Year: year
                        });
                        this.ForwardLink.fetch();
                    }
                },
                loadStockData(year) {
                    const self = this;
                    if (!this.StockData) {
                        this.StockData = {};
                        AnalyticsModule.Server.post(
                            `/Accounting/MoneyBalanceMaster/IsStockRemainsEntered`,
                            { year },
                            (result) => {
                                self.StockData.IsStockRemainsEntered = result;
                                self.StockData.loaded = true;
                            }
                        );
                    }
                }
            },

            waitForLoading(requires, func, errFunc, functionContext) {
                var act = function(context) {
                    let hasError;

                    const allLoaded = _.all(requires, (value) => {
                        return value && value.loaded;
                    });

                    if (allLoaded) {
                        _.all(requires, (value) => {
                            if (value.isError) {
                                hasError = true;
                            }
                        });
                    } else if (!allLoaded) {
                        _.delay(act, 100, context);
                        return;
                    }
                    if (!hasError) {
                        func.call(context);
                    } else {
                        errFunc.call(context);
                    }
                };

                act(functionContext);
            }
        };
    }());

    function createRequisitesModel() {
        const requisites = new money.Models.Common.FirmRequisites();
        const preloadedData = requisites.getPreloadedData();

        if (preloadedData) {
            requisites.set(preloadedData).loaded = true;
        }

        return requisites;
    }
}(Common, Money));

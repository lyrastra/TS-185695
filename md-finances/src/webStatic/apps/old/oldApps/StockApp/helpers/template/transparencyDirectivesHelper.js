(function (stockModule) {

    var directives = stockModule.Helpers.Directives ? stockModule.Helpers.Directives : stockModule.Helpers.Directives = {};

    directives.ButtonRegion = {
        movementOperationLink: {
            html: function(target) {
                var collection = this.get('List'),
                    count = collection.length;
                
                if (count == 1) {
                    $(target.element).hide();
                } else {
                    return;
                }
            }
        }    
    };

    directives.StockUlDirectives = {
        Id: {
            isMain: function() {
                return this.IsMain;
            },
            stockid: function () {
                return this.Id;
            },
            text: function () {
                return '';
            }
        },
        
        Type: {
            text: function() {
                return this.Type == 1 ? ' (опт)' : ' (розн)';
            }
        },
        
        IsMain: {
            text: function() {
                return (this.IsMain && this.Name != 'Основной склад') ? 'основной склад' : '';
            }
        }
    };

    directives.NomenclatureUlDirectives = {
        Id: {
            nomenclatureid: function () {
                var id = this.Id == 0 && this.TemporaryId ? this.TemporaryId : this.Id;
                return id;
            },
            text: function () {
                return '';
            }
        }    
    };

    directives.DebitOperation = {
        StockList: getStockListDirectives(),
        DateSpan: getDateSpanDirectives(),
        NumberSpan: getNumberSpanDirectives(),
        transitBlock: {
            html: function(target) {
                if (this.List.models.length == 1) {
                    $(target.element).remove();
                } 
            }
        }
    };

    directives.DebitProductList = {
        ProductName: {
            value: function() {
                return this.Product.ProductName;
            }
        },
        UnitOfMeasurement: {
            value: function () {
                return this.Product.UnitOfMeasurement;
            }
        }
    };

    directives.MovementOperation = {
        StockList: {
            html: function (target) {
                var $select = $(target.element),
                    list = this.List;

                if (list.models.length == 1) {
                    $select.after('<span>' + list.models[0].get('Name') + '</span>');
                    $select.remove();
                } else {
                    var line = '';

                    _.each(list.models, function (item) {
                        line += '<option value="' + item.get('Id') + '">' + item.get('Name') + '</option>';
                    });

                    $select.append(line);
                }
            }
        },
        DateSpan: {
            html: function (target) {
                var $el = $(target.element),
                    $input = $el.next('input');

                $el.text(this.DateSpan);
                $input.attr('value', this.DateInput);
                $input.hide();
            }
        },
        NumberSpan: {
            html: function (target) {
                var $el = $(target.element),
                    $input = $el.next('input');

                $el.text(this.NumberSpan);
                $input.attr('value', this.NumberSpan);
                $input.hide();
            }
        }
    };

    directives.MovementProductList = {
        ProductName: getProductNameDirectives()
    };

    directives.ProductRowTable = {
        AtBeginning: {
            text: function (target) {
                return getValueNotCondition(target.value, this.ProductInStock.length);
            }
        },
        Arrived: {
            text: function (target) {
                return getValueNotCondition(target.value, this.ProductInStock.length);
            }
        },
        Retired: {
            text: function (target) {
                return getValueNotCondition(target.value, this.ProductInStock.length);
            }
        },
        Balance: {
            text: function (target) {
                return getValueNotCondition(target.value, this.ProductInStock.length);
            }
        },
        ProductUnit: {
            text: function (target) {
                return getValueNotCondition(target.value, this.ProductInStock.length);
            }
        },
        StockName: {
            text: function (target) {
                return getValueNotCondition(target.value, this.ProductInStock.length);
            }
        }
    };

    directives.RequisitionWaybill = {
        StockList: getStockListDirectives(),
        DateSpan: getDateSpanDirectives(),
        NumberSpan: getNumberSpanDirectives(),
        IsOtherOutgo: {
            checked: function () {
                if (this.IsOtherOutgo) {
                    return "checked";
                }
                return undefined;
            }
        },
        IsNonOperatingOutgo: {
            checked: function () {
                if (this.IsNonOperatingOutgo) {
                    return "checked";
                }
                return undefined;
            }
        }
    };
    
    directives.RequisitionWaybillList = {
        UnitOfMeasurement: {
            text: function () {
                if (this.Product) {
                    return this.Product.UnitOfMeasurement;
                }

                return '';
            }
        },
        IsOffbalance: {
            checked: function () {
                if (this.IsOffbalance) {
                    return "checked";
                }
                return undefined;
            }
        }
    };

    function getValueNotCondition (value, condition) {
        if (condition) {
            return '';
        }

        return value;
    }

    function getStockListDirectives() {
        return {
            html: function (target) {
                var $select = $(target.element),
                    list = this.List;

                if (list == 0) {
                    return;
                }

                if (list.models.length == 1) {
                    $select.after('<span>' + list.models[0].get('Name') + '</span>');
                    $select.remove();
                } else {
                    var line = '';

                    _.each(list.models, function (item) {
                        line += '<option value="' + item.get('Id') + '">' + item.get('Name') + '</option>';
                    });

                    $select.append(line);
                    
                    if (this.StockId) {
                        $select.val(this.StockId);
                    }
                }
            }
        };
    }
    
    function getDateSpanDirectives() {
        return {
            html: function(target) {
                var $el = $(target.element),
                    $input = $el.next('input');

                $el.text(this.DateSpan);
                $input.attr('value', this.DateInput);
                $input.hide();
            }
        };
    }
    
    function getNumberSpanDirectives() {
        return {
            html: function(target) {
                var $el = $(target.element),
                    $input = $el.next('input');

                $el.text(this.NumberSpan);
                $input.attr('value', this.NumberSpan);
                $input.hide();
            }
        };
    }
    
    function getProductNameDirectives() {
        return {
            value: function() {
                if (this.Product) {
                    return this.Product.ProductName;
                }

                return '';
            }
        };
    }

})(Stock);
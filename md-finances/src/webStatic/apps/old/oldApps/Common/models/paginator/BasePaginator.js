Backbone.BasePaginator = (function (Backbone, _, $) {

    var BasePaginator = {};

    BasePaginator.BasePager = Backbone.Collection.extend({
        Page: {
            PageNum: 1,
            PageSize: 20
        },
        
        sync: function (method, collection, options) {
            if (collection.currentRequest) {
                collection.currentRequest.abort();
            }
            if (method == "read") {
                var data = [];

                if (options.data && _.isObject(options.data)) {
                    $.each(options.data, function (key, value) {
                        data.push({ Key: key, Value: value });
                    });
                }

                if (!this.filterParser) {
                    var filter = this.parseFilterObject();
                    if (filter) {
                        data = data.concat(filter);
                    }

                    if (this.parseFilterDateObject()) {
                        data = data.concat(this.parseFilterDateObject());
                    }

                    data.push({
                        Key: "Type",
                        Value: collection.documentType
                    });
                } else {
                    data = data.concat([{ Key: "Page", Value: JSON.stringify(this.Page) }]);
                    data = data.concat(this.filterParser.makeData());
                }

                addCompanyIdParam(data);

                options = _.extend({type: 'POST'}, options, {
                    contentType: "application/json; charset=utf-8;",
                    data: $.toJSON(data)
                });

                var s = function (data, status, xhr) {
                    delete collection.currentRequest;
                };

                var oldSuccess = options.success;
                options.success = oldSuccess ? function (data, status, xhr) {
                    s(data, status, xhr);
                    oldSuccess(data, status, xhr);
                } : s;
            }

            var xhr = Backbone.Collection.prototype.sync.call(this, method, collection, options);
            collection.currentRequest = xhr;
            return xhr;
        },

        requestNextPage: function () {
            if (this.Page.PageNum !== undefined) {
                this.Page.PageNum += 1;
                
                this.pager(this.getPreviousPagerOptions());
            }
        },

        requestPreviousPage: function () {
            if (this.Page.PageNum !== undefined) {
                this.Page.PageNum -= 1;

                this.pager(this.getPreviousPagerOptions());
            }
        },

        updateOrder: function (column) {
            if (column !== undefined) {
                this.sortField = column;
                this.pager(this.getPreviousPagerOptions());
            }
        },

        goTo: function (page) {
            if (page !== undefined) {
                this.Page.PageNum = parseInt(page, 10);
                this.pager(this.getPreviousPagerOptions());
            }
        },

        howManyPer: function (count) {
            if (count !== undefined) {
                if (count == "Все") {
                    count = 1000000;
                }
                this.Page.PageNum = this.firstPage;
                this.Page.PageSize = count;
                this.pager(this.getPreviousPagerOptions());
            }
        },

        sort: function (type, direction) {
            if (type !== undefined) {
                this.filterObject.save({
                    Sorter: {
                        Sort: type,
                        SortDirection: direction
                    }
                });
                this.pager(this.getPreviousPagerOptions());
            }
        },

        info: function () {

            var info = {
                page: this.Page.PageNum,
                firstPage: this.firstPage,
                totalPages: this.totalPages,
                lastPage: this.totalPages,
                perPage: this.Page.PageSize,
                totalCount: this.totalCount
            };

            this.information = info;
            return info;
        },

        pager: function (options) {
            if (options && options.documentType) {
                this.documentType = options.documentType;
                options = _.omit(options, "documentType");
            }
            this._previousPagerOptions = options;
            var settings = $.extend({}, options, { reset: true });
            this.fetch(settings);
        },

        getPreviousPagerOptions: function() {
            return this._previousPagerOptions || { };
        },

        parseFilterObject: function () {
            var parsedFilter = [{ Key: "Page", Value: JSON.stringify(this.Page)}];

            var sorter = this.filterObject.get("Sorter");
            parsedFilter.push({ Key: sorter.Sort, Value: sorter.SortDirection });
            
            $.each(this.filterObject.get("Filter"), function (key, value) {
                parsedFilter.push({ Key: key, Value: value });    
            });


            return parsedFilter;
        },
        
        parseFilterDateObject: function () {
            var parsedDateFilter = [];
            
            if(this.filterDateObject) {
            if (this.filterDateObject.get("State").Type != Enums.TimeFilterTypes.Year && this.filterDateObject.get("State").Type != Enums.TimeFilterTypes.Custom) {
                    $.each(this.filterDateObject.get("Filter"), function(key, value) {
                    parsedDateFilter.push({ Key: key, Value: JSON.stringify(value) });
                });
                } else {
                    $.each(this.filterDateObject.get("Filter"), function(key, value) {
                    parsedDateFilter.push({ Key: key, Value: value });
                });
            }
            }
            return parsedDateFilter;
        }
    });
    
    /** @access private */ 
    function addCompanyIdParam(data) {
        try {
            var id = parseInt(Md.Data.companyId, 10);
            data.push({companyId: id});
        } catch (e) {
            throw 'Md.Data not defined';
        }
    }
    
    return BasePaginator;

} (Backbone, _, $));
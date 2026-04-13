/*! backbone.paginator - v0.1.5 - 3/30/2012
* http://github.com/addyosmani/backbone.paginator
* Copyright (c) 2012 Addy Osmani; Licensed MIT */

Backbone.Paginator = (function (Backbone, _, $) {
    'use strict';

    var Paginator = {};
    Paginator.version = '0.15';

    // @name: clientPager
    //
    // @tagline: Paginator for client-side data
    //
    // @description:
    // This paginator is responsible for providing pagination
    // and sort capabilities for a single payload of data
    // we wish to paginate by the UI for easier browsering.
    //
    Paginator.clientPager = Backbone.Collection.extend({

        /*sync: function (method, model, options) {

        var queryMap = {};
        queryMap[this.perPageAttribute] = this.perPage;
        queryMap[this.skipAttribute] = this.page * this.perPage;
        queryMap[this.orderAttribute] = this.sortField;
        queryMap[this.customAttribute1] = this.customParam1;
        queryMap[this.formatAttribute] = this.format;
        queryMap[this.customAttribute2] = this.customParam2;
        queryMap[this.queryAttribute] = this.query;

        var params = _.extend({
        type: 'GET',
        dataType: 'jsonp',
        jsonpCallback: 'callback',
        data: decodeURIComponent($.param(queryMap)),
        url: this.url,
        processData: false
        }, options);

        return $.ajax(params);
        },*/

        nextPage: function () {
            this.page = ++this.page;
            this.pager();
        },

        previousPage: function () {
            this.page = --this.page || 1;
            this.pager();
        },

        goTo: function (page) {
            if (page !== undefined) {
                this.page = parseInt(page, 10);
                this.pager();
            }
        },

        howManyPer: function (perPage) {
            if (perPage !== undefined) {
                this.displayPerPage = parseInt(perPage, 10);
                this.pager();
            }
        },


        // where column is the key to sort on
        setSort: function (column, direction) {
            if (column !== undefined && direction !== undefined) {
                this.pager(column, direction);
            }
        },

        pager: function (sort, direction) {
            var self = this,
				disp = this.displayPerPage,
				start = (self.page - 1) * disp,
				stop = start + disp;

            if (self.origModels === undefined) {
                self.origModels = self.models;
            }

            self.models = self.origModels;

            if (sort) {
                self.models = self._sort(self.models, sort, direction);
            }
            self.reset(self.models.slice(start, stop));
        },

        _sort: function (models, sort, direction) {
            models = models.sort(function (a, b) {
                var ac = a.get(sort),
					bc = b.get(sort);

                if (direction === 'desc') {
                    if (ac > bc) {
                        return -1;
                    }

                    if (ac < bc) {
                        return 1;
                    }
                } else {
                    if (ac < bc) {
                        return -1;
                    }

                    if (ac > bc) {
                        return 1;
                    }
                }

                return 0;
            });

            return models;
        },

        info: function () {
            var self = this,
				info = {},
				totalRecords = (self.origModels) ? self.origModels.length : self.length,
				totalPages = Math.ceil(totalRecords / self.displayPerPage);

            info = {
                totalRecords: totalRecords,
                page: self.page,
                perPage: this.displayPerPage,
                totalPages: totalPages,
                lastPage: totalPages,
                lastPagem1: totalPages - 1,
                previous: false,
                next: false,
                page_set: [],
                startRecord: (self.page - 1) * this.displayPerPage + 1,
                endRecord: Math.min(totalRecords, self.page * this.displayPerPage)
            };

            if (self.page > 1) {
                info.prev = self.page - 1;
            }

            if (self.page < info.totalPages) {
                info.next = self.page + 1;
            }

            info.pageSet = self.setPagination(info);

            self.information = info;
            return info;
        },


        setPagination: function (info) {
            var pages = [], i = 0, l = 0;


            // How many adjacent pages should be shown on each side?
            var ADJACENT = 3,
				ADJACENTx2 = ADJACENT * 2,
				LASTPAGE = Math.ceil(info.totalRecords / info.perPage),
				LPM1 = -1;

            if (LASTPAGE > 1) {
                // not enough pages to bother breaking it up
                if (LASTPAGE < (7 + ADJACENTx2)) {
                    for (i = 1, l = LASTPAGE; i <= l; i++) {
                        pages.push(i);
                    }
                }
                // enough pages to hide some
                else if (LASTPAGE > (5 + ADJACENTx2)) {

                    //close to beginning; only hide later pages
                    if (info.page < (1 + ADJACENTx2)) {
                        for (i = 1, l = 4 + ADJACENTx2; i < l; i++) {
                            pages.push(i);
                        }
                    }

                    // in middle; hide some front and some back
                    else if (LASTPAGE - ADJACENTx2 > info.page && info.page > ADJACENTx2) {
                        for (i = info.page - ADJACENT; i <= info.page + ADJACENT; i++) {
                            pages.push(i);
                        }
                    }
                    // close to end; only hide early pages
                    else {
                        for (i = LASTPAGE - (2 + ADJACENTx2); i <= LASTPAGE; i++) {
                            pages.push(i);
                        }
                    }
                }
            }

            return pages;
        }

    });


    // @name: requestPager
    //
    // Paginator for server-side data being requested from a backend/API
    //
    // @description:
    // This paginator is responsible for providing pagination
    // and sort capabilities for requests to a server-side
    // data service (e.g an API)
    //
    Paginator.requestPager = Backbone.Collection.extend({

        sync: function (method, model, options) {

            var params, request = [{ Key: "Page", Value: "{\"PageNum\":" + this.PageNum + ", \"PageSize\":" + this.PageSize + "}" },
                            { Key: this.Sort, Value: this.SortDirection },
                            { Key: this.Type, Value: this.TypeValue },
                            { Key: this.BudgetarySubType, Value: this.BudgetarySubTypeValue },
                            { Key: this.PayrollType, Value: this.PayrollTypeValue },
                            { Key: this.StartDate, Value: this.StartDateValue },
                            { Key: this.FinalDate, Value: this.FinalDateValue },
                            { Key: this.TimeFilter, Value: this.TimeFilterValue },
                            { Key: this.KontragentId, Value: this.KontragentIdValue },
                            { Key: this.WorkerId, Value: this.WorkerIdValue },
                            { Key: this.MovementType, Value: this.MovementTypeValue },
                            { Key: this.SettlementFilter, Value: this.SettlementFilterValue },
                            { Key: this.ProjectId, Value: this.ProjectIdValue },
                            { Key: this.BillId, Value: this.BillIdValue}];

            params = _.extend({
                type: 'POST',
                contentType: "application/json; charset=utf-8;",
                dataType: "json",
                data: $.toJSON(request),
                url: this.url,
                processData: false
            }, options);

            return $.ajax(params);
        },


        requestNextPage: function () {
            if (this.PageNum !== undefined) {
                this.PageNum += 1;
                this.pager();
            }
        },

        requestPreviousPage: function () {
            if (this.PageNum !== undefined) {
                this.PageNum -= 1;
                // customize as needed.
                this.pager();
            }
        },

        updateOrder: function (column) {
            if (column !== undefined) {
                this.sortField = column;
                this.pager();
            }

        },

        goTo: function (page) {
            if (page !== undefined) {
                this.PageNum = parseInt(page, 10);
                this.pager();
            }
        },

        howManyPer: function (count) {
            if (count !== undefined) {
                if (count == "Все") {
                    count = 1000000;
                }
                this.PageNum = this.firstPage;
                this.PageSize = count;
                this.pager();
            }
        },

        sort: function (type, direction) {
            if (type !== undefined) {
                this.Sort = type;
                this.SortDirection = direction;
                this.pager();
            }
        },

        info: function () {

            var info = {
                page: this.PageNum,
                firstPage: this.firstPage,
                totalPages: this.totalPages,
                lastPage: this.totalPages,
                perPage: this.PageSize,
                totalCount: this.totalCount
            };

            this.information = info;
            return info;
        },

        // fetches the latest results from the server
        pager: function () {
            this.fetch({ reset: true });
        }
    });


    Paginator.requestPagerSales = Backbone.Collection.extend({
        //filterObject: "",
        sync: function (method, model, options) {

            var params, request = this.parseFilterObject();

            params = _.extend({
                type: 'POST',
                contentType: "application/json; charset=utf-8;",
                dataType: "json",
                data: $.toJSON(request),
                url: this.url,
                processData: false
            }, options);

            return $.ajax(params);
        },


        requestNextPage: function () {
            if (this.PageNum !== undefined) {
                this.PageNum += 1;
                this.pager();
            }
        },

        requestPreviousPage: function () {
            if (this.PageNum !== undefined) {
                this.PageNum -= 1;
                // customize as needed.
                this.pager();
            }
        },

        updateOrder: function (column) {
            if (column !== undefined) {
                this.sortField = column;
                this.pager();
            }

        },

        goTo: function (page) {
            if (page !== undefined) {
                this.PageNum = parseInt(page, 10);
                this.pager();
            }
        },

        howManyPer: function (count) {
            if (count !== undefined) {
                if (count == "Все") {
                    count = 1000000;
                }
                this.PageNum = this.firstPage;
                this.PageSize = count;
                this.pager();
            }
        },

        sort: function (type, direction) {
            if (type !== undefined) {
                this.Sort = type;
                this.SortDirection = direction;
                this.pager();
            }
        },

        info: function () {

            var info = {
                page: this.PageNum,
                firstPage: this.firstPage,
                totalPages: this.totalPages,
                lastPage: this.totalPages,
                perPage: this.PageSize,
                totalCount: this.totalCount
            };

            this.information = info;
            return info;
        },

        // fetches the latest results from the server
        pager: function () {
            this.fetch({ reset: true });

        },

        parseFilterObject: function () {
            var filterObject = this.filterObject;
            var parsedFilter = [
                            { Key: "Page", Value: "{\"PageNum\":" + filterObject.pageNum + ", \"PageSize\":" + filterObject.pageSize + "}" },
                            { Key: "Sort", Value: filterObject.sortDirection },
                            { Key: "Type", Value: filterObject.type },
                            { Key: "StartDate", Value: filterObject.startDate },
                            { Key: "FinalDate", Value: filterObject.finalDate },
                            { Key: "TimeFilter", Value: filterObject.timeFilter },
                            { Key: "KontragentId", Value: filterObject.kontragentId },
                            { Key: "SubType", Value: filterObject.subType },
                            { Key: "BudgetarySubType", Value: filterObject.budgetarySubType },
                            { Key: "PayrollType", Value: filterObject.payrollType }
            ];

            if (filterObject.subType && filterObject.subType.length) {
                parsedFilter.push({ Key: "SubType", Value: filterObject.subType });
            }

            if (filterObject.budgetarySubType && filterObject.budgetarySubType.length) {
                parsedFilter.push({ Key: "BudgetarySubType", Value: filterObject.budgetarySubType });
            }

            if (filterObject.payrollType && filterObject.payrollType.length) {
                parsedFilter.push({ Key: "PayrollType", Value: filterObject.payrollType });
            }

            if (filterObject.status && filterObject.status.length) {
                parsedFilter.push({ Key: 'Status', Value: filterObject.status });
            }

            else {
                parsedFilter.push({ Key: "BillType", Value: filterObject.billType });
            }
            
            var paginatorClientDataObject = {
                Page: { PageNum: filterObject.pageNum, PageSize: filterObject.pageSize },
                Sorter: { SortDirection: filterObject.sortDirection },
                Filter: {}
                
            };                   

            return parsedFilter;
        }


    });

    return Paginator;

} (Backbone, _, $));
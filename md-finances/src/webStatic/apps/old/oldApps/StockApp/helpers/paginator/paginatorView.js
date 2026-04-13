(function (stockModule) {
	'use strict';

    var pageRange = 3;
    var templateUrl = 'ClientSideApps/StockApp/helpers/paginator/';
    var template = 'paginatorTemplate';

    stockModule.Views.PaginatorView = Backbone.View.extend({
        model: new stockModule.Models.PaginatorModel(),

        setModel: function () {
            var self = this;
            var totalPage = self.totalPages();

            var currentPage = self.model.get('currentPage');

            var startPage = currentPage - pageRange;
            if (startPage < 1) {
                startPage = 1;
            }

            var endPage = currentPage + pageRange;
            if (endPage > totalPage) {
                endPage = totalPage;
            }

            self.model.set({
                totalPage: totalPage,
                hasNext: self.hasNextPage(),
                hasPrevious: self.hasPreviousPage(),
                startPage: startPage,
                endPage: endPage
            });
        },

        getPageSize: function () {
            var size = this.model.get('pageSize');
            return size === 'Все' ? this.model.get('totalSize') : size;
        },

        totalPages: function () {
            return window.Math.ceil(this.model.get('totalSize') / this.getPageSize());
        },

        hasNextPage: function () {
            return this.model.get('currentPage') !== this.totalPages();
        },

        hasPreviousPage: function () {
            return this.model.get('currentPage') !== 1;
        },

        setTotal: function (total) {
            this.model.set('totalSize', total);
            this.render();
        },

        nextPage: function () {
            if (this.hasNextPage()) {
                this.goTo(this.model.get('currentPage') + 1);
            }
        },

        previousPage: function () {
            if (this.hasPreviousPage()) {
                this.goTo(this.model.get('currentPage') - 1);
            }
        },

        goTo: function (newPage) {
            newPage = window.parseInt(newPage, 10);
            if (newPage >= 1 && newPage <= this.totalPages()) {
                this.model.set('currentPage', newPage);
                this.render();
                this.trigger('PageChange', this.model.get('currentPage'), this.getPageSize());
            }
        },

        setPageSize: function (size) {
            if (size === 'Все' || size > 0) {
                this.model.set('pageSize', size);
                this.goTo(1);
            }
        },

        className: 'paginator',

        events: {
            'click .serverprevious': function () {
                this.previousPage();
            },
            'click .servernext': function () {
                this.nextPage();
            },
            'click .page': function (e) {
                this.goTo($(e.currentTarget).text());
            },
            'change .countSelector': function (e) {
                this.setPageSize($(e.currentTarget).val());
            }
        },

        render: function () {
            var self = this;
            this.setModel();
            TemplateManager.get(template, function (tmpl) {
                self.$el.html(_.template(tmpl, self.model.toJSON()));
            }, templateUrl);
            
            return this;
        }
    });
})(Stock);
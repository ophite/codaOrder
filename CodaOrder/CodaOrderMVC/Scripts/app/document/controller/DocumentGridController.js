/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/angular-cache.js" />

(function () {
    'use strict';

    function DocumentGridController($scope, $location, apiService, filterStrToSql, parameterService, DSCacheFactory) {

        $scope.title = 'DocumentGridController';

        // filter plugin
        var filterBarPlugin = {
            init: function (scope, grid) {
                filterBarPlugin.scope = scope;
                filterBarPlugin.grid = grid;
                $scope.$watch(function () {

                    var searchQuery = "";
                    angular.forEach(filterBarPlugin.scope.columns, function (col) {
                        if (col.visible && col.filterText) {
                            var filterText = (col.filterText.indexOf('*') == 0 ? col.filterText.replace('*', '') : "^" + col.filterText) + ";";
                            searchQuery += col.displayName + ": " + filterText;
                        }
                    });

                    return searchQuery;
                },
                function (searchQuery) {

                    filterBarPlugin.scope.$parent.filterText = searchQuery;
                    filterBarPlugin.grid.searchProvider.evalFilter();
                });
            },
            scope: undefined,
            grid: undefined,
        };

        // grid
        $scope.gridOptions = {

            data: 'documents',
            showColumnMenu: true,
            showFilter: true,
            plugins: [filterBarPlugin],
            //showGroupPanel: true,
            //jqueryUIDraggable: true,
            headerRowHeight: 60, // give room for filter bar
            enableCellSelection: true,
            enableRowSelection: false,
            enableCellEdit: true,
            columnDefs: [
                { enableCellEdit: true, field: 'Amount', displayName: 'Сумма', headerCellTemplate: '../template/filterHeaderTemplate' },
                { enableCellEdit: false, field: 'DocCode', displayName: 'Код док-та', headerCellTemplate: '../template/filterHeaderTemplate' },
                { enableCellEdit: false, field: 'DocDate', displayName: 'Дата док-та', headerCellTemplate: '../template/filterHeaderTemplate' },
                { enableCellEdit: false, field: 'OID', displayName: 'ID', headerCellTemplate: '../template/filterHeaderTemplate' },
                { enableCellEdit: false, field: 'Comments', displayName: 'Комментарий', headerCellTemplate: '../template/filterHeaderTemplate' }
            ],

            //enablePaging: true,
            showFooter: false,
            //totalServerItems: 'totalServerItems',
            //pagingOptions: $scope.pagingOptions,
            filterOptions: $scope.filterOptions
        }

        $scope.$on('ngGridEventEndCellEdit',
            function (element) {
                console.log(element.targetScope.row.entity);
            }); //focus the input element on 'start cell edit'


        // pagination
        $scope.setPagingData = function (data, page, pageSize) {
            $scope.documents = data;
            if (!$scope.$$phase)
                $scope.$apply();
        };

        // send url from MVC .net
        $scope.init = function (url_GetDocument) {
            $scope.url_GetDocument = url_GetDocument;
        };

        // listeners
        $scope.$on(ConstantHelper.Watchers.broadcastGetDocuments, function (event, args) {
            // filter
            if (filterBarPlugin.scope != null) {
                var sqlFilter = filterBarPlugin.scope.$parent.filterText;
                sqlFilter = filterStrToSql.fn(sqlFilter, $scope.gridOptions.columnDefs);

                parameterService.setDocumentParam(ConstantHelper.Document.paramFullTextFilter.value, sqlFilter[ConstantHelper.Document.paramFullTextFilter.value]);
                parameterService.setDocumentParam(ConstantHelper.Document.paramWhereText.value, sqlFilter[ConstantHelper.Document.paramWhereText.value]);
            }

            // params
            var params = parameterService.getDocumentParams();
            params = ConstantHelper.prepareAllDocumentParams(params);

            // get docs
            var callbackFunc = function (jsonData) {
                // sending params
                params[ConstantHelper.Document.paramTotalRows.value] = jsonData[ConstantHelper.Document.paramTotalRows.value];
                params[ConstantHelper.Document.paramPagesCount.value] = jsonData[ConstantHelper.Document.paramPagesCount.value];
                $scope.$emit(ConstantHelper.Watchers.setPagingInfo, params);
                // filling grid
                $scope.setPagingData(JSON.parse(jsonData.Documents), params[ConstantHelper.Document.paramCurrentPage.value]);
            };

            apiService.getDocuments(params, $scope.url_GetDocument, callbackFunc);
        });

        // test
        //var filterStr = "Сумма: ^41;Код док-та: ^48;ID: ^198;";
        //var res = filterStrToSql.fn(filterStr, $scope.gridOptions.columnDefs)
        //alert(res);
    }

    DocumentGridController.$inject = ['$scope', '$location', 'apiService', 'filterStrToSql', 'parameterService'];
    angular.module('app').controller('DocumentGridController', DocumentGridController);
})();
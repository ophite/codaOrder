/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/angular-cache.js" />

(function () {
    'use strict';

    function DocumentGridController($scope, $location, apiService, filterStrToSql, parameterService, DSCacheFactory) {

        $scope.model = {};

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

            data: 'model.data',
            showColumnMenu: true,
            showFilter: true,
            plugins: [filterBarPlugin],
            //showGroupPanel: true,
            //jqueryUIDraggable: true,
            headerRowHeight: 60, // give room for filter bar
            //enableCellSelection: true,
            enableRowSelection: true,
            multiSelect: false,
            rowTemplate: '<div ng-dblclick="onDblClickRow(row)" ng-style="{ \'cursor\': row.cursor }" ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell {{col.cellClass}}"><div class="ngVerticalBar" ng-style="{height: rowHeight}" ng-class="{ ngVerticalBarVisible: !$last }">&nbsp;</div><div ng-cell></div></div>',
            columnDefs: [
                { field: 'Amount', displayName: 'Сумма', headerCellTemplate: '../template/filterHeaderTemplate' },
                { field: 'DocCode', displayName: 'Код док-та', headerCellTemplate: '../template/filterHeaderTemplate' },
                { field: 'DocDate', displayName: 'Дата док-та', headerCellTemplate: '../template/filterHeaderTemplate' },
                { field: 'OID', displayName: 'ID', headerCellTemplate: '../template/filterHeaderTemplate' },
                { field: 'Comments', displayName: 'Комментарий', headerCellTemplate: '../template/filterHeaderTemplate' }
            ],

            //enablePaging: true,
            showFooter: false,
            //totalServerItems: 'totalServerItems',
            //pagingOptions: $scope.pagingOptions,
            filterOptions: $scope.filterOptions
        }

        $scope.onDblClickRow = function (rowItem) {
            var defaultCache = DSCacheFactory.get('defaultCache');
            defaultCache.put(ConstantHelper.DocumentID, rowItem.entity.OID);
            $location.path(ConstantHelper.router.lines.url);
        };

        $scope.$on('ngGridEventEndCellEdit', function (element) {
            console.log(element.targetScope.row.entity);
        }); //focus the input element on 'start cell edit'


        // pagination
        $scope.setPagingData = function (data, page, pageSize) {
            $scope.model.data = data;
            if (!$scope.$$phase)
                $scope.$apply();
        };

        // send url from MVC .net
        $scope.init = function (urlGetDocument) {
            $scope.model.urlGetDocument = urlGetDocument;
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
                $scope.setPagingData(JSON.parse(jsonData[ConstantHelper.GridData]), params[ConstantHelper.Document.paramCurrentPage.value]);
            };

            apiService.getDocuments(params, $scope.model.urlGetDocument, callbackFunc);
        });

        // test
        //var filterStr = "Сумма: ^41;Код док-та: ^48;ID: ^198;";
        //var res = filterStrToSql.fn(filterStr, $scope.gridOptions.columnDefs)
        //alert(res);
    }

    DocumentGridController.$inject = ['$scope', '$location', 'apiService', 'filterStrToSql', 'parameterService', 'DSCacheFactory'];
    angular.module('app').controller('DocumentGridController', DocumentGridController);
})();
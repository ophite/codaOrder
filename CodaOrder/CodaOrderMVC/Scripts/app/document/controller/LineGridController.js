/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/angular-cache.js" />

(function () {
    'use strict';

    function LineGridController($scope, $location, apiService, filterStrToSql, parameterService, DSCacheFactory, $routeParams, $urlRouterProvider) {

        $scope.model = {
            isEditable: false,
            isDirty: false,
        };

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
            enableCellSelection: true,
            enableRowSelection: false,
            //enableCellEdit: false,
            columnDefs: [
                { enableCellEdit: false, field: 'OID', displayName: 'ID', headerCellTemplate: '../template/filterHeaderTemplate' },
                { enableCellEdit: false, field: 'ItemID', displayName: 'Товар ID', headerCellTemplate: '../template/filterHeaderTemplate' },
                { enableCellEdit: false, field: 'SortNO', displayName: 'Сортировка', headerCellTemplate: '../template/filterHeaderTemplate' },
                { enableCellEdit: false, field: 'Price', displayName: 'Цена', headerCellTemplate: '../template/filterHeaderTemplate' },
                {
                    enableCellEdit: true, field: 'Quantity', displayName: 'Количество', headerCellTemplate: '../template/filterHeaderTemplate',
                    cellTemplate: '<div class="ngCellText" ng-class="{isDirtyCell: row.entity.Quantity != row.entity.Ordered}"><span ng-cell-text>{{row.getProperty(col.field)}}</span></div>',
                },
            ],
            //$scope.model.isEditable,
            //enablePaging: true,
            showFooter: false,
            //totalServerItems: 'totalServerItems',
            //pagingOptions: $scope.pagingOptions,
            filterOptions: $scope.filterOptions
        }

        $scope.$on('ngGridEventEndCellEdit',
            function (element) {
                console.log(element.targetScope.row.entity);
                $scope.model.isDirty = true;
            }); //focus the input element on 'start cell edit'


        // pagination
        $scope.setPagingData = function (data, page, pageSize) {
            $scope.model.data = data;
            if (!$scope.$$phase)
                $scope.$apply();
        };

        // listeners
        var documentID = $routeParams.documentID;
        if (documentID) {

            // params
            var params = parameterService.getDocumentParams();
            params = ConstantHelper.prepareAllDocumentParams(params);

            var callbackFunc = function (jsonData) {
                $scope.setPagingData(JSON.parse(jsonData[ConstantHelper.GridData]), params[ConstantHelper.Document.paramCurrentPage.value]);
                $scope.model.isEditable = JSON.parse(String(jsonData[ConstantHelper.IsEditable]).toLowerCase());
                $scope.gridOptions.columnDefs[4].enableCellEdit = true;
            }

            var paramDict = {};
            paramDict[ConstantHelper.DocumentID] = documentID;
            apiService.getDocuments(paramDict, ConstantHelper.router.lines.urlGetJSON, callbackFunc);
        }

        //api
        $scope.saveDocument = function () {
            console.log('save');
        };
    }

    LineGridController.$inject = ['$scope', '$location', 'apiService', 'filterStrToSql', 'parameterService', 'DSCacheFactory', '$routeParams', '$urlRouterProvider'];
    angular.module(ConstantHelper.App).controller('LineGridController', LineGridController);
})();
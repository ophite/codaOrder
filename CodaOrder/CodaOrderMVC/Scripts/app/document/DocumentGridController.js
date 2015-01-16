(function () {
    'use strict';

    function DocumentGridController($scope, $location, getDocuments, filterStrToSql, parameterService) {

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

        // pagination
        $scope.setPagingData = function (data, page, pageSize, searchText) {

            var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
            $scope.documents = pagedData;
            $scope.totalServerItems = data.length;

            if (!$scope.$$phase) {
                $scope.$apply();
            }
        };

        // listeners
        $scope.$on('broadcastGetDocuments', function (event, args) {

            if (filterBarPlugin.scope != null) {
                var searchText = filterBarPlugin.scope.$parent.filterText;
            }

            var params = parameterService.getDocumentParams()
            console.log(params);
            
            getDocuments.get(function (jsonData) {
                $scope.setPagingData(JSON.parse(jsonData.Documents), args.currentPage, args.pageSize, searchText);
            });
        });


        // test
        //var filterStr = "Сумма: ^41;Код док-та: ^48;ID: ^198;";
        //var res = filterStrToSql.fn(filterStr, $scope.gridOptions.columnDefs)
        //alert(res);

        // test params
        //var documentParams = parameterService.getDocumentsParams();
        //console.log(documentParams);
    }

    DocumentGridController.$inject = ['$scope', '$location', 'getDocuments', 'filterStrToSql', 'parameterService'];
    angular.module('app').controller('DocumentGridController', DocumentGridController);
})();
(function () {
    'use strict';

    function GetSubjects($resource) {

        return $resource("GetSubjects")
    }

    GetSubjects.$inject = ['$resource'];
    angular.module('app').factory('GetSubjects', GetSubjects);

    function GridController($scope, GetSubjects) {
        $scope.title = 'GridController';

        //GetSubjects.query(function (data) {
        //    $scope.subjects = data;
        //});

        //activate();

        //function activate() { }
        //$scope.myData = [
        //   { name: "Moroni", age: 50 },
        //   { name: "Tiancum", age: 43 },
        //   { name: "Jacob", age: 27 },
        //   { name: "Nephi", age: 29 },
        //   { name: "Enos", age: 34 }
        //];

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
                }, function (searchQuery) {
                    filterBarPlugin.scope.$parent.filterText = searchQuery;
                    filterBarPlugin.grid.searchProvider.evalFilter();
                });
            },
            scope: undefined,
            grid: undefined,
        };





        // paging
        //$scope.filterOptions = {
        //    filterText: "",
        //    useExternalFilter: true
        //};
        $scope.totalServerItems = 0;
        $scope.pagingOptions = {
            pageSizes: [5, 10, 20],
            pageSize: 5,
            currentPage: 1
        };
        $scope.setPagingData = function (data, page, pageSize) {
            var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
            $scope.subjects = pagedData;
            $scope.totalServerItems = data.length;
            if (!$scope.$$phase) {
                $scope.$apply();
            }
        };
        $scope.getPagedDataAsync = function (pageSize, page, searchText) {
            setTimeout(function () {
                var data;
                if (searchText) {
                    var ft = searchText.toLowerCase();
                    GetSubjects.query(function (largeLoad) {
                        //$scope.subjects = data;
                        data = largeLoad.filter(function (item) {
                            return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                        });
                        $scope.setPagingData(data, page, pageSize);
                    });
                } else {
                    GetSubjects.query(function (largeLoad) {
                        $scope.setPagingData(largeLoad, page, pageSize);
                    });
                }
            }, 100);
        };

        $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);

        $scope.$watch('pagingOptions', function (newVal, oldVal) {
            if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
                $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.gridOptions.$gridScope.filterText);
            }
        }, true);
        $scope.$watch('filterOptions', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
            }
        }, true);
        //






        $scope.gridOptions = {
            data: 'subjects',
            showColumnMenu: true,
            showFilter: true,
            plugins: [filterBarPlugin],
            showGroupPanel: true,
            //jqueryUIDraggable: true,
            headerRowHeight: 60, // give room for filter bar
            columnDefs: [
                { field: 'FullName', displayName: 'Наименование', headerCellTemplate: 'filterHeaderTemplate' },
                { field: 'PaperRegistrationCode', displayName: 'Регистрационный код', headerCellTemplate: 'filterHeaderTemplate' },
                { field: 'ShortName', displayName: 'Краткое наименование', headerCellTemplate: 'filterHeaderTemplate' },
                { field: 'ID', displayName: 'ID', headerCellTemplate: 'filterHeaderTemplate' }
            ],

            enablePaging: true,
            showFooter: true,
            totalServerItems: 'totalServerItems',
            pagingOptions: $scope.pagingOptions,
            filterOptions: $scope.filterOptions
        }



        //$scope.LoadGrid = function () {
        //    alert('');
        //    $scope.myData = [{ name: "Moroni", age: 50 },
        //         { name: "Tiancum", age: 43 },
        //         { name: "Jacob", age: 27 },
        //         { name: "Nephi", age: 29 },
        //         { name: "Enos", age: 34 }];
        //    $scope.$apply();
        //}

        //$http.get('data.json')
        //  .success(function (data) {
        //      $scope.myData = data;
        //  });
    }

    GridController.$inject = ['$scope', 'GetSubjects'];
    angular.module('app').controller('GridController', GridController);
    //angular.module('gridApp').run(['$rootScope',
    //    function ($rootScope) {
    //    }
    //]);
})();
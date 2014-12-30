
(function () {
    'use strict';

    // api goto Factory
    function GetDocuments($resource) {

        var promise = $resource("GetDocuments")
        return promise;
    }

    GetDocuments.$inject = ['$resource'];
    angular.module('app').factory('GetDocuments', GetDocuments);



    // controller
    function DocumentGridController($scope, $location, GetDocuments) {

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
                }, function (searchQuery) {

                    filterBarPlugin.scope.$parent.filterText = searchQuery;
                    filterBarPlugin.grid.searchProvider.evalFilter();
                });
            },
            scope: undefined,
            grid: undefined,
        };

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

        $scope.setPagingData = function (data, page, pageSize, searchText) {

            var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
            $scope.documents = pagedData;
            $scope.totalServerItems = data.length;
            if (!$scope.$$phase) {

                $scope.$apply();
            }
        };

        // convert grid filter string to sql query where and full text query
        function filterStrToSql(filterStr, columns) {

            var dict = {

                whereQuery: '',
                fullTextQuery: '<Root/>'
            };

            if (filterStr.indexOf(';') < 0)
                return dict;

            var whereQueryArr = [];
            var fullTextQueryArr = [];
            var iColumns = Enumerable.From(columns);

            function callBack(element, index, array) {

                if (!element)
                    return;

                var captionValue = element.split(':');
                var item = iColumns
                    .Where(function (x) { return x.displayName === captionValue[0] })
                    .Select(function (x) { return x })
                    .FirstOrDefault()

                var value = captionValue[1].replace(' ^', '');
                var fieldStr = String(item.field);

                //N'<Root><Filter TableID="-1" Not="false" Value="Новус" TableName="Subject" ColumnName="Name"/></Root>'
                if (fieldStr.indexOf('_') > -1) {

                    var table = fieldStr.split('_')[0];
                    var field = fieldStr.split('_')[1];
                    fullTextQueryArr.push('<Filter TableID ="-1" Not="false" Value="' + value + '" TableName="' + table + '" ColumnName="' + field + '"/>');
                }
                    //N' and ((isnull(charindex(''прод'', _journalalias_.name), 0) > 0) and (isnull(charindex(''71'', _journalalias_.doccode), 0) > 0))'
                else {

                    if (whereQueryArr.length > 0)
                        whereQueryArr.push('and ');

                    whereQueryArr.push('(isnull(charindex(\'\'' + value + '\'\', _journalalias_.' + fieldStr.toLowerCase() + '), 0) > 0) ');
                }
            }
            filterStr.split(';').forEach(callBack)

            if (fullTextQueryArr.length > 0) {

                fullTextQueryArr.splice(0, 0, '<Root>');
                fullTextQueryArr.push('</Root>');
            }
            else
                fullTextQueryArr.push('<Root/>')

            if (whereQueryArr.length > 0) {

                whereQueryArr.splice(0, 0, ' and (');
                whereQueryArr.push(')');
            }

            dict.whereQuery = whereQueryArr.join('');
            dict.fullTextQuery = fullTextQueryArr.join('');

            return dict;
        }

        var filterStr = "Сумма: ^41;Код док-та: ^48;ID: ^198;";
        var res = filterStrToSql(filterStr, $scope.gridOptions.columnDefs)

        $scope.$on('broadcastGetDocuments', function (event, args) {

            if (filterBarPlugin.scope != null) {

                var searchText = filterBarPlugin.scope.$parent.filterText;
            }

            GetDocuments.get(function (jsonData) {

                $scope.setPagingData(JSON.parse(jsonData.Documents), args.currentPage, args.pageSize, searchText);
            });
        });
    }

    DocumentGridController.$inject = ['$scope', '$location', 'GetDocuments'];
    angular.module('app').controller('DocumentGridController', DocumentGridController);
})();
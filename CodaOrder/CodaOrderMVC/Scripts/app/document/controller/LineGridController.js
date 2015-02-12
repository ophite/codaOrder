/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/jquery-2.1.1.js" />
/// <reference path="~/Scripts/linq-vsdoc.js" />

(function () {
    'use strict';

    angular.module(ConstantHelper.App).controller('LineGridController',
        ['$scope', '$location', 'apiService', 'filterStrToSql', 'parameterService', '$stateParams', 'showErrorService', 'gridFilterBarService',
        function LineGridController($scope, $location, apiService, filterStrToSql, parameterService, $stateParams, showErrorService, gridFilterBarService) {

            $scope.model = {
                isEditable: false,
                isDirty: false,
            };

            // grid
            var filterBarPlugin = gridFilterBarService.getPlugin($scope);
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
                        cellTemplate: '<div class="ngCellText" ng-class="col.colIndex()"><div ng-class = "{isDirtyCell: row.entity.Quantity != row.entity.Ordered}"><span ng-cell-text>{{row.getProperty(col.field)}}</span></div></div>',
                        editableCellTemplate: '<input type="number" ng-class="\'colt\' + col.index" ng-input="COL_FIELD" ng-model="COL_FIELD" />',
                    },
                ],
                //$scope.model.isEditable,
                //enablePaging: true,
                showFooter: false,
                //totalServerItems: 'totalServerItems',
                //pagingOptions: $scope.pagingOptions,
                filterOptions: $scope.filterOptions
            }

            // send url from MVC .net
            $scope.init = function (urlSaveDocument) {
                $scope.model.urlSaveDocument = urlSaveDocument;
            };

            //$scope.$on('ngGridEventStartCellEdit', function (element) {
            //    $scope.model.isDirty = true;
            //});

            $scope.$on('ngGridEventEndCellEdit',
                function (element) {
                    //console.log(element.targetScope.row.entity);
                    $scope.model.isDirty = true;
                });


            // pagination
            $scope.setPagingData = function (data, page, pageSize) {
                $scope.model.data = data;
                if (!$scope.$$phase)
                    $scope.$apply();
            };

            // listeners
            var documentID = $stateParams.documentID;
            if (documentID) {

                // params
                var params = parameterService.getDocumentParams();
                params = ConstantHelper.prepareAllDocumentParams(params);

                var callbackFunc = function (jsonData) {
                    $scope.setPagingData(JSON.parse(jsonData[ConstantHelper.GridData]), params[ConstantHelper.Document.paramCurrentPage.value]);
                    $scope.model.isEditable = JSON.parse(String(jsonData[ConstantHelper.IsEditable]).toLowerCase());
                }

                var paramDict = {};
                paramDict[ConstantHelper.DocumentID] = documentID;
                apiService.getLines(paramDict, ConstantHelper.router.lines.urlGetJSON, callbackFunc);
            }

            //api
            $scope.saveDocument = function () {
                var callbackFunc = function (jsonData) {
                    var isError = jsonData[ConstantHelper.IsResponseError];
                    if (isError.toLowerCase() === 'true') {
                        showErrorService.show('Error during save document', jsonData[ConstantHelper.ResponseErrorMessage]);
                    }
                };

                var lines = Enumerable.From($scope.model.data);
                var resultLines = lines
                    .Where(function (x) { return x.Quantity != x.Ordered })
                    .Select(function (x) { return x })
                    .ToArray();

                apiService.saveDocument([angular.toJson(resultLines)], $scope.model.urlSaveDocument, callbackFunc);
            };
        }
        ])
})();
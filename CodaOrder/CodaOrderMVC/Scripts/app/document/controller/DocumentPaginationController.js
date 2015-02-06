/// <reference path="~/Scripts/app/Constant.js" />
(function () {
    'use strict';

    function DocumentPaginationController($scope, $log, parameterService) {

        function saveParams() {
            parameterService.setDocumentParam(ConstantHelper.Document.paramCurrentPage.value, $scope.currentPage);
            parameterService.setDocumentParam(ConstantHelper.Document.paramTotalRows.value, $scope.totalRows);
        }

        function loadParams(args) {
            $scope.currentPage = args[ConstantHelper.Document.paramCurrentPage.value];
            $scope.totalRows = args[ConstantHelper.Document.paramTotalRows.value];
            $scope.pageNumberCounts = args[ConstantHelper.Document.paramPageNumberCount.value];
            $scope.pageSize = args[ConstantHelper.Document.paramPageSize.value];
        }
        // was 2 times running getDocuments if when click Refresh run getDocuments. One time fire pageChanged (redundant)
        $scope.$on(ConstantHelper.Watchers.broadcastStartLoadingDocuments, function (event, args) {
            $scope.currentPage = ConstantHelper.Document.paramCurrentPage.default;
            $scope.pageChanged();
        });

        $scope.pageChanged = function () {
            saveParams();
            var params = {};
            params[ConstantHelper.Document.paramCurrentPage.value] = $scope.currentPage;
            params[ConstantHelper.Document.paramTotalRows.value] = $scope.totalRows;
            params[ConstantHelper.Document.paramPageSize.value] = $scope.pageSize;
            // init get documents
            $scope.$emit(ConstantHelper.Watchers.pageChanged, params);
        };

        $scope.$on(ConstantHelper.Watchers.broadcastPagingInfoChange, function (event, args) {
            loadParams(args);
            saveParams();
        });

        // init - 1 time 
        var params = {
            currentPage: ConstantHelper.Document.paramCurrentPage.default,
            totalRows: ConstantHelper.Document.paramTotalRows.default,
            // read-only
            pageNumberCounts: ConstantHelper.Document.paramPageNumberCount.default,
            pageSize: ConstantHelper.Document.paramPageSize.default
        };
        loadParams(params);
        $scope.pageChanged();
    }

    DocumentPaginationController.$inject = ['$scope', '$log', 'parameterService'];
    angular.module(ConstantHelper.App).controller('DocumentPaginationController', DocumentPaginationController);
})();

(function () {
    'use strict';

    function DocumentPaginationController($scope, $log, parameterService) {

        $scope.totalItems = 1;
        $scope.currentPage = 1;

        $scope.selectPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () {

            parameterService.setDocumentParams('currentPage', $scope.bigCurrentPage);
            parameterService.setDocumentParams('pageSize', $scope.maxSize);

            $scope.$emit('pageChanged', {
                currentPage: $scope.bigCurrentPage,
                pageSize: $scope.maxSize
            });
            //$log.log('Page changed to: ' + $scope.currentPage);
            //$log.log('Page changed to: ' + $scope.bigCurrentPage);
        };

        $scope.maxSize = 4;
        $scope.bigTotalItems = 111;
        $scope.bigCurrentPage = 1;

        // init - 1 time 
        $scope.pageChanged();
    }

    DocumentPaginationController.$inject = ['$scope', '$log', 'parameterService'];
    angular.module('app').controller('DocumentPaginationController', DocumentPaginationController);
})();

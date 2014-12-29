(function () {
    'use strict';

    function DocumentPaginationController($scope, $log) {

        $scope.totalItems = 1;
        $scope.currentPage = 1;

        $scope.selectPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () {

            $scope.$emit('pageChanged', { 
                currentPage: $scope.bigCurrentPage,
                pageSize: $scope.maxSize = 4
            });
            //$log.log('Page changed to: ' + $scope.currentPage);
            $log.log('Page changed to: ' + $scope.bigCurrentPage);
        };

        $scope.maxSize = 4;
        $scope.bigTotalItems = 111;
        $scope.bigCurrentPage = 1;
    }

    DocumentPaginationController.$inject = ['$scope', '$log'];
    angular.module('app').controller('DocumentPaginationController', DocumentPaginationController);
})();

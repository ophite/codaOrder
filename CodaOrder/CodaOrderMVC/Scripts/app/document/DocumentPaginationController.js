(function () {
    'use strict';

    function DocumentPaginationController($scope, $log) {

        $scope.totalItems = 1;
        $scope.currentPage = 1;

        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () {
            $log.log('Page changed to: ' + $scope.currentPage);
        };

        $scope.maxSize = 4;
        $scope.bigTotalItems = 111;
        $scope.bigCurrentPage = 1;
    }

    DocumentPaginationController.$inject = ['$scope', '$log'];
    angular.module('app').controller('DocumentPaginationController', DocumentPaginationController);

})();

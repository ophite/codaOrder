/// <reference path="~/Scripts/app/common/Constant.js" />
(function () {
    'use strict';

    function DocumentDateController($scope, $filter, parameterService) {
        
        $scope.format = 'dd.MM.yyyy';

        $scope.today = function () {
            $scope.dateStart = $filter('date')(new Date(), $scope.format);
            var dateEnd = new Date();
            dateEnd.setDate(dateEnd.getDate() + 7);
            $scope.dateEnd = $filter('date')(dateEnd, $scope.format);
        };
        $scope.today();

        $scope.clear = function () {
            $scope.dateStart = null;
            $scope.dateEnd = null;
        };

        // Disable weekend selection
        //$scope.disabled = function (date, mode) {
        //    return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
        //};

        $scope.toggleMin = function () {
            $scope.minDate = '31.12.2000'; // $scope.minDate ? null : new Date();
        };
        $scope.toggleMin();

        $scope.openDateStart = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.openedStart = true;
        };

        $scope.openDateEnd = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.openedEnd = true;
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        $scope.dates = [$scope.dateStart, $scope.dateEnd];
        $scope.$watchCollection('dates', function (newValues, oldValues) {

            parameterService.setDocumentParams(ConstantHelper.Document.paramDateBegin.value, newValues[0]);
            parameterService.setDocumentParams(ConstantHelper.Document.paramDateEnd.value, newValues[1]);
        });
    };

    DocumentDateController.$inject = ['$scope', '$filter', 'parameterService'];
    angular.module('app').controller('DocumentDateController', DocumentDateController);
})();
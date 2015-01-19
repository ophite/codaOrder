/// <reference path="~/Scripts/app/common/Constant.js" />
(function () {
    'use strict';

    function DocumentDateController($scope, $filter, parameterService) {
        
        $scope.format = 'dd.MM.yyyy';

        function formatDate(dateRow) {
            return $filter('date')(dateRow, $scope.format);
        }

        $scope.today = function () {
            $scope.dateStart = formatDate(new Date());
            var dateEnd = new Date();
            dateEnd.setDate(dateEnd.getDate() + 7);
            $scope.dateEnd = formatDate(dateEnd);

            //test
            $scope.dateStart = '01.01.2014';
            $scope.dateEnd = '31.01.2014';

            parameterService.setDocumentParam(ConstantHelper.Document.paramDateBegin.value, $scope.dateStart);
            parameterService.setDocumentParam(ConstantHelper.Document.paramDateEnd.value, $scope.dateEnd);
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

        function openDate($event) {
            $event.preventDefault();
            $event.stopPropagation();
        };
        $scope.openDateStart = function ($event) {
            openDate($event);
            $scope.openedStart = true;
        };
        $scope.openDateEnd = function ($event) {
            openDate($event);
            $scope.openedEnd = true;
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        $scope.$watchCollection('[dateStart, dateEnd]', function (newValues, oldValues) {
            parameterService.setDocumentParam(ConstantHelper.Document.paramDateBegin.value, formatDate(newValues[0]));
            parameterService.setDocumentParam(ConstantHelper.Document.paramDateEnd.value, formatDate(newValues[1]));
        });
    };

    DocumentDateController.$inject = ['$scope', '$filter', 'parameterService'];
    angular.module('app').controller('DocumentDateController', DocumentDateController);
})();
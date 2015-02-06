/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular.js" />

(function () {
    'use strict';

    function DocumentDateController($scope, $filter, parameterService) {
        // init 
        function formatDate(dateRow) {
            return $filter('date')(dateRow, $scope.format);
        }

        $scope.today = function () {
            var date = new Date();
            $scope.dates.dateStart = formatDate(date);
            date.setDate(date.getDate() + 7);
            $scope.dates.dateEnd = formatDate(date);

            //test
            $scope.dates.dateStart = '01.01.2014';
            $scope.dates.dateEnd = '31.01.2014';

            parameterService.setDocumentParam(ConstantHelper.Document.paramDateBegin.value, $scope.dates.dateStart);
            parameterService.setDocumentParam(ConstantHelper.Document.paramDateEnd.value, $scope.dates.dateEnd);
        };

        $scope.format = 'dd.MM.yyyy';
        $scope.dates = {};
        $scope.today();

        // watch change
        $scope.$watchCollection('[dates.dateStart, dates.dateEnd]', function (newValues, oldValues) {
            parameterService.setDocumentParam(ConstantHelper.Document.paramDateBegin.value, formatDate(newValues[0]));
            parameterService.setDocumentParam(ConstantHelper.Document.paramDateEnd.value, formatDate(newValues[1]));
        });

        $scope.init = function (templateDatePicker, templateValidation) {

            $scope.templateDirDatePicker = templateDatePicker;
            $scope.templateDirDatesValidation = templateValidation;
        }
    };

    DocumentDateController.$inject = ['$scope', '$filter', 'parameterService'];
    angular.module(ConstantHelper.App).controller('DocumentDateController', DocumentDateController);
})();
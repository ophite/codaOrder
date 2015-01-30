/// <reference path="~/Scripts/app/common/Constant.js" />

(function () {
    'use strict';

    function DocumentDateController($scope, $filter, parameterService) {

        $scope.format = 'dd.MM.yyyy';

        function formatDate(dateRow) {
            return $filter('date')(dateRow, $scope.format);
        }

        $scope.today = function () {
            var date = new Date();
            $scope.dateStart = formatDate(date);
            date.setDate(date.getDate() + 7);
            $scope.dateEnd = formatDate(date);

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

    angular.module('app').directive('dateLowerThan', ["$filter",
      function ($filter) {
          return {
              restrict: 'A',
              require: 'ngModel',
              link: function (scope, elm, attrs, ctrl) {
                  var validateDateRange = function (inputValue) {
                      var fromDate = $filter('date')(inputValue, 'short');
                      var toDate = $filter('date')(attrs.dateLowerThan, 'short');
                      var isValid = isValidDateRange(fromDate, toDate, attrs.datepickerPopup);
                      ctrl.$setValidity('dateLowerThan', isValid);
                      return inputValue;
                  };

                  ctrl.$parsers.unshift(validateDateRange);
                  ctrl.$formatters.push(validateDateRange);
                  attrs.$observe('dateLowerThan', function () {
                      validateDateRange(ctrl.$viewValue);
                  });
              }
          };
      }
    ]);

    angular.module('app').directive('dateGreaterThan', ["$filter",
      function ($filter) {
          return {
              restrict: 'A',
              require: 'ngModel',
              link: function (scope, elm, attrs, ctrl) {
                  var validateDateRange = function (inputValue) {
                      var fromDate = $filter('date')(attrs.dateGreaterThan, 'short');
                      var toDate = $filter('date')(inputValue, 'short');
                      var isValid = isValidDateRange(fromDate, toDate, attrs.datepickerPopup);
                      ctrl.$setValidity('dateGreaterThan', isValid);
                      return inputValue;
                  };

                  ctrl.$parsers.unshift(validateDateRange);
                  ctrl.$formatters.push(validateDateRange);
                  attrs.$observe('dateGreaterThan', function () {
                      validateDateRange(ctrl.$viewValue);
                  });
              }
          };
      }
    ]);

    var isValidDate = function (dateStr, dateFormat) {
        if (dateStr === undefined)
            return false;

        var dateTime = Date.parse(dateStr, dateFormat);
        if (isNaN(dateTime))
            return false;

        return true;
    };

    var getDateDifference = function (fromDate, toDate, dateFormat) {
        return Date.parse(toDate, dateFormat) - Date.parse(fromDate, dateFormat);
    };

    var isValidDateRange = function (fromDate, toDate, dateFormat) {
        if (fromDate === "" || toDate === "")
            return true;

        if (isValidDate(fromDate, dateFormat) === false)
            return false;

        if (isValidDate(toDate, dateFormat) === true) {
            var days = getDateDifference(fromDate, toDate, dateFormat);
            if (days < 0)
                return false;
        }

        return true;
    };
})();
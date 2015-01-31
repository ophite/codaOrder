/// <reference path="~/Scripts/app/common/Constant.js" />
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
        $scope.$watchCollection('[dateStart, dateEnd]', function (newValues, oldValues) {
            parameterService.setDocumentParam(ConstantHelper.Document.paramDateBegin.value, formatDate(newValues[0]));
            parameterService.setDocumentParam(ConstantHelper.Document.paramDateEnd.value, formatDate(newValues[1]));
        });
    };

    DocumentDateController.$inject = ['$scope', '$filter', 'parameterService'];
    angular.module('app').controller('DocumentDateController', DocumentDateController);

    angular.module('app').directive('dirDatePicker', ["$filter",
        function ($filter) {
            return {
                restrict: 'E',
                require: 'ngModel',
                templateUrl: 'directive/DocumentDatepicker',
                scope: {
                    dateValue: "=ngModel",
                    dateFormat: "@dateFormat"
                },
                controller: function ($scope) {
                    $scope.dateOptions = {
                        formatYear: 'yy',
                        startingDay: 1
                    };
                    $scope.openDate = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.openedPicker = true;
                    };
                    $scope.disabled = function (date, mode) {
                        return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
                    };
                },
            };
        }
    ]);

    angular.module('app').directive('dirDatesValidation', ["$filter",
        function ($filter) {

            // help functions
            var isValidDate = function (dateStr, dateFormat) {
                if (dateStr === "" || dateStr === undefined)
                    return 'is empty';

                var dateTime = Date.parse(dateStr, dateFormat);
                if (dateTime === null)
                    dateTime = Date.parse(dateStr);

                if (isNaN(dateTime))
                    return 'in bad format';

                if (dateTime === null)
                    return 'in bad format';

                return undefined;
            };

            var isValidDateRange = function (fromDate, toDate, dateFormat) {
                var errorMessage = isValidDate(fromDate, dateFormat);
                if (errorMessage)
                    return 'FromDateError';

                errorMessage = isValidDate(toDate, dateFormat);
                if (errorMessage)
                    return 'ToDateError';

                var days = Date.parse(toDate, dateFormat) - Date.parse(fromDate, dateFormat);
                if (days < 0)
                    return 'DateRangeError';

                return undefined;
            };

            return {
                restrict: 'E',
                require: 'ngModel',
                replace: true,
                templateUrl: 'directive/DatesValidation',
                scope: {
                    dateStartValidate: "@dateStartValidate",
                    dateEndValidate: "@dateEndValidate",
                    dateFormat: "@dateFormat",
                },
                link: function (scope, element, attrs, ctrl) {

                    var last_error;
                    var validateDateRange = function (inputValue) {
                        var dateStart = $filter('date')(attrs.dateStartValidate, 'short');
                        var dateEnd = $filter('date')(attrs.dateEndValidate, 'short');
                        var errorMessage = isValidDateRange(dateStart, dateEnd, attrs.dateFormat);

                        if (last_error)
                            ctrl.$setValidity(last_error, true);

                        if (errorMessage) {
                            ctrl.$setValidity(errorMessage, false);
                            last_error = errorMessage;
                        }

                        return inputValue;
                    };

                    ctrl.$parsers.unshift(validateDateRange);
                    ctrl.$formatters.push(validateDateRange);

                    attrs.$observe('dateStartValidate', function () {
                        validateDateRange(ctrl.$viewValue);
                    });
                    attrs.$observe('dateEndValidate', function () {
                        validateDateRange(ctrl.$viewValue);
                    });
                }
            };
        }
    ]);
    //angular.module('app').directive('dateLowerThan', ["$filter",
    //  function ($filter) {
    //      return {
    //          restrict: 'A',
    //          require: 'ngModel',
    //          link: function (scope, elm, attrs, ctrl) {
    //              var validateDateRange = function (inputValue) {
    //                  var fromDate = $filter('date')(inputValue, 'short');
    //                  var toDate = $filter('date')(attrs.dateLowerThan, 'short');
    //                  var isValid = isValidDateRange(fromDate, toDate, attrs.datepickerPopup);
    //                  ctrl.$setValidity('dateLowerThan', isValid);
    //                  return inputValue;
    //              };

    //              ctrl.$parsers.unshift(validateDateRange);
    //              ctrl.$formatters.push(validateDateRange);
    //              attrs.$observe('dateLowerThan', function () {
    //                  validateDateRange(ctrl.$viewValue);
    //              });
    //          }
    //      };
    //  }
    //]);

    //angular.module('app').directive('dateGreaterThan', ["$filter",
    //  function ($filter) {
    //      return {
    //          restrict: 'A',
    //          require: 'ngModel',
    //          link: function (scope, elm, attrs, ctrl) {
    //              var validateDateRange = function (inputValue) {
    //                  var fromDate = $filter('date')(attrs.dateGreaterThan, 'short');
    //                  var toDate = $filter('date')(inputValue, 'short');
    //                  var isValid = isValidDateRange(fromDate, toDate, attrs.datepickerPopup);
    //                  ctrl.$setValidity('dateGreaterThan', isValid);
    //                  return inputValue;
    //              };

    //              ctrl.$parsers.unshift(validateDateRange);
    //              ctrl.$formatters.push(validateDateRange);
    //              attrs.$observe('dateGreaterThan', function () {
    //                  validateDateRange(ctrl.$viewValue);
    //              });
    //          }
    //      };
    //  }
    //]);

})();
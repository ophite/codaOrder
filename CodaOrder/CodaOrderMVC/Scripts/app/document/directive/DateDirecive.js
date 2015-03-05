/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular_source/angular.js" />

(function () {
    'use strict';

    angular.module(ConstantHelper.App).directive('dirDatePicker', ["$filter",
        function ($filter) {
            return {
                restrict: 'E',
                require: 'ngModel',
                templateUrl: 'directive/DirDatePicker',
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

    angular.module(ConstantHelper.App).directive('dirDatesValidation', ["$filter",
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
                templateUrl: 'directive/DirDatesValidation',
                scope: {
                    dateStartValidate: "@dateStartValidate",
                    dateEndValidate: "@dateEndValidate",
                    dateFormat: "@dateFormat"
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
})();
/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular_source/angular.js" />

(function () {
    'use strict';

    angular.module(ConstantHelper.App).directive('dirShowError', ["$compile",
        function ($compile) {
            return {
                restrict: 'E',
                replace: true,
                templateUrl: '/Scripts/app/common/directive/DirShowError.html'
            };
        }
    ]);
})();
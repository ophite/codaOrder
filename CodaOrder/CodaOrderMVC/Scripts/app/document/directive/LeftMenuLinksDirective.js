/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/linq-vsdoc.js" />
/// <reference path="~/Scripts/angular.js" />

(function () {
    'use strict';

    angular.module('app').directive('dirLeftMenuLinks', ["$compile",
        function ($compile) {
            return {
                restrict: 'E',
                replace: true,
                //scope: {},
                link: function (scope, element, attrs) {

                    var template = Enumerable.From(ConstantHelper.router)
                        .OrderBy(function (x) { return x.Value.order })
                        .Select(function (x) { return '<a ui-sref="' + x.Value.name + '" class="list-group-item">' + x.Value.fullName + '</a>' })
                        .Aggregate(function (a, b) { return a + b });

                    element.html(template);
                    $compile(element.contents())(scope);
                },
            };
        }
    ]);
})();
/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/3rdparty/linq-vsdoc.js" />
/// <reference path="~/Scripts/angular_source/angular.js" />

(function () {
    'use strict';

    angular.module(ConstantHelper.App).directive('dirLeftMenuLinks', ["$compile",
        function ($compile) {
            return {
                restrict: 'E',
                replace: true,
                //scope: {},
                link: function (scope, element, attrs) {
                    var template = Enumerable.From(ConstantHelper.router)
                        .Where(function (x) { return x.Value.isMenu === true })
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
/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular_source/angular.js" />

'use strict';

angular.module(ConstantHelper.App).factory('gridFilterBarService',
    function () {
        return {
            getPlugin: function ($scope) {

                // filter plugin
                var filterBarPlugin = {
                    init: function (scope, grid) {
                        filterBarPlugin.scope = scope;
                        filterBarPlugin.grid = grid;

                        $scope.$watch(function () {

                            var searchQuery = "";
                            angular.forEach(filterBarPlugin.scope.columns, function (col) {
                                if (col.visible && col.filterText) {
                                    var filterText = (col.filterText.indexOf('*') == 0 ? col.filterText.replace('*', '') : "^" + col.filterText) + ";";
                                    searchQuery += col.displayName + ": " + filterText;
                                }
                            });

                            return searchQuery;
                        },

                        function (searchQuery) {

                            filterBarPlugin.scope.$parent.filterText = searchQuery;
                            filterBarPlugin.grid.searchProvider.evalFilter();
                        });
                    },
                    scope: undefined,
                    grid: undefined,
                };

                return filterBarPlugin;
            },
        };
    });
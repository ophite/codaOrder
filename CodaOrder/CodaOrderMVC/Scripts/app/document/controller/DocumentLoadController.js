/// <reference path="~/Scripts/app/Constant.js" />
'use strict';

var app = angular.module(ConstantHelper.App);
app.controller('DocumentLoadController', ['$scope', 'DSCacheFactory', function ($scope, DSCacheFactory) {

    $scope.refresh = function () {

        var defaultCache = DSCacheFactory.get('defaultCache');
        defaultCache.removeAll();
        $scope.$emit(ConstantHelper.Watchers.startLoadingDocuments);
    };
}]);
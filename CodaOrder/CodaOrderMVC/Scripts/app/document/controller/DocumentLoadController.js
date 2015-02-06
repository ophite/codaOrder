/// <reference path="~/Scripts/app/Constant.js" />
'use strict';

var app = angular.module(ConstantHelper.App);
app.controller('DocumentLoadController', ['$scope', function ($scope) {

    $scope.refresh = function () {
        $scope.$emit(ConstantHelper.Watchers.startLoadingDocuments);
    };
}]);
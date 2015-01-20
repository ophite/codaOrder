/// <reference path="~/Scripts/app/common/Constant.js" />
'use strict';

var app = angular.module('app');
app.controller('DocumentLoadController', ['$scope', function ($scope) {

    $scope.getDocuments = function () {
        $scope.$emit(ConstantHelper.Watchers.startLoadingDocuments);
    };
}]);
'use strict';

var app = angular.module('app');
app.controller('DocumentLoadController', ['$scope', function ($scope) {

    $scope.getDocuments = function () {
        $scope.$emit('startLoadingDocuments');
    };
}]);
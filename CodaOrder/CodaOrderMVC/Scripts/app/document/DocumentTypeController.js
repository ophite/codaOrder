/// <reference path="~/Scripts/linq-vsdoc.js" />
'use strict';

var app = angular.module('app');
app.controller('DocumentTypeController', ['$scope', 'parameterService', function ($scope, parameterService) {

    $scope.disabled = undefined;
    $scope.searchEnabled = undefined;

    $scope.enable = function () {
        $scope.disabled = false;
    };

    $scope.disable = function () {
        $scope.disabled = true;
    };

    $scope.enableSearch = function () {
        $scope.searchEnabled = true;
    }

    $scope.disableSearch = function () {
        $scope.searchEnabled = false;
    };

    $scope.documentType = [
      { name: 'Продажа', code: 'DocSale' },
      { name: 'Продажа возврат', code: 'DocSaleRet' },
      { name: 'Покупка', code: 'DocBuy' },
      { name: 'Покупка возврат', code: 'DocBuyRet' },
    ];

    $scope.data = {};
    $scope.data.selectedDocumentType = ['Продажа', 'Продажа возврат'];

    $scope.$watch('data.selectedDocumentType', function (newValues, oldValues) {

        var docTypes = Enumerable.From($scope.documentType);
        var docTypesSelected = docTypes
            .Where(function (x) { return newValues.indexOf(x.name) != -1 })
            .Select(function (x) { return x.code })
            .ToArray();
        parameterService.setDocumentParams('documentType', docTypesSelected);
    }, true);

}]);
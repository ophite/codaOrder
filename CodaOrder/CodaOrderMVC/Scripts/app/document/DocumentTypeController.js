'use strict';

var app = angular.module('app');
app.controller('DocumentTypeController', function ($scope) {
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
    }

    $scope.documentType = [
      { name: 'Продажа', code: 'DocSale' },
      { name: 'Продажа возврат', code: 'DocSaleRet' },
      { name: 'Покупка', code: 'DocBuy' },
      { name: 'Покупка возврат', code: 'DocBuyRet' },
    ];

    $scope.multipleDemo = {};
    $scope.multipleDemo.selectedDocumentType = ['Продажа'];
});
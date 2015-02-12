/// <reference path="~/Scripts/linq-vsdoc.js" />
/// <reference path="~/Scripts/app/Constant.js" />
'use strict';

angular.module(ConstantHelper.App).controller('DocumentTypeController', ['$scope', 'parameterService',
    function ($scope, parameterService) {

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
        setParams($scope.data.selectedDocumentType);

        function setParams(params) {
            var docTypes = Enumerable.From($scope.documentType);
            var docTypesSelected = docTypes
                .Where(function (x) { return params.indexOf(x.name) != -1 })
                .Select(function (x) { return x.code })
                .ToArray();
            parameterService.setDocumentParam(ConstantHelper.Document.paramDocTypeClasses.value, docTypesSelected);
        }

        $scope.$watch('data.selectedDocumentType', function (newValues, oldValues) {
            setParams(newValues);
        }, true);
    }
]);
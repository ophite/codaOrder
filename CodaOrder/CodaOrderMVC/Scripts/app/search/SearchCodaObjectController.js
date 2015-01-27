/// <reference path="~/Scripts/app/common/Constant.js" />
(function () {
    'use strict';

    function SearchCodaObjectController($scope, $http, searchSubject, parameterService) {

        $scope.init = function(url_searchSubject) {
            $scope.url_searchSubject = url_searchSubject;
        };
        $scope.getObjects = function (val) {
            return searchSubject.query(val, $scope.url_searchSubject).query().$promise.then(function (response) {
                return response.map(function (item) {
                    return item;
                });
            });

            //var items = $http.get('http://localhost:35133/Search/Find', {
            //    params: {
            //        searchText: val
            //    }
            //}).then(function (response) {
            //    return response.data.map(function (item) {
            //        return item.FullName;
            //    });
            //});

            //return items;
        };

        $scope.data = {};
        $scope.$watch('data.searchCodaObject', function (newValue, oldValue) {
            if (newValue != undefined && typeof (newValue) === 'object' && 'OID' in newValue)
                parameterService.setDocumentParam(ConstantHelper.Document.paramSubjectID.value, newValue.OID);
            else
                parameterService.setDocumentParam(ConstantHelper.Document.paramSubjectID.value, null);
        });
    };

    SearchCodaObjectController.$inject = ['$scope', '$http', 'searchSubject', 'parameterService'];
    angular.module('app').controller('SearchCodaObjectController', SearchCodaObjectController);
})();
(function () {
    'use strict';

    function SearchCodaObjectController($scope, $http, searchSubject, parameterService) {

        $scope.getObjects = function (val) {
            return searchSubject.query(val).query().$promise.then(function (response) {
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

        $scope.$watch('searchCodaObject', function (newValue, oldValue) {
            if (newValue != undefined && typeof (newValue) === 'object' && 'OID' in newValue)
                parameterService.setDocumentParams('filterID', newValue.OID);
            else
                parameterService.setDocumentParams('filterID', null);
        });
    };

    SearchCodaObjectController.$inject = ['$scope', '$http', 'searchSubject', 'parameterService'];
    angular.module('app').controller('SearchCodaObjectController', SearchCodaObjectController);
})();
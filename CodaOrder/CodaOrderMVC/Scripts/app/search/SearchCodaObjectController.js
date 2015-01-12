(function () {
    'use strict';

    function SearchCodaObjectController($scope, $http, SearchSubject) {

        $scope.getObjects = function (val) {
            return SearchSubject.query(val).query().$promise.then(function (response) {
                return response.map(function (item) {
                    return item.FullName;
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
    };

    SearchCodaObjectController.$inject = ['$scope', '$http', 'SearchSubject'];
    angular.module('app').controller('SearchCodaObjectController', SearchCodaObjectController);
})();
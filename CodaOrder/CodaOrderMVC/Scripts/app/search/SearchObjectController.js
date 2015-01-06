(function () {
    'use strict';

    function SearchObjectController($scope, $http, SearchObject) {

        $scope.getObjects = function (val) {

            //return SearchObject.query({ searchText: val }, function (jsonData) {

            //    return jsonData.map(function (item) {

            //        return item.FullName;
            //    });
            //});

            return $http.get('http://localhost:35133/Search/Find', {
                params: {
                    searchText: val
                }
            }).then(function (response) {
                return response.data.map(function (item) {
                    return item.FullName;
                });
            });
        };
    };

    SearchObjectController.$inject = ['$scope', '$http', 'SearchObject'];
    angular.module('app').controller('SearchObjectController', SearchObjectController);
})();
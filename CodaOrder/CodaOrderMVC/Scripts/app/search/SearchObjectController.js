(function () {
    'use strict';

    function SearchObjectController($scope, $http, SearchObject) {

        //var res = SearchObject.query("Стол", function (jsonData, header) {
        //    alert(jsonData)
        //});

        $scope.getObjects = function (val) {

            return SearchObject.query(val).query().$promise.then(function (response) {

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

    SearchObjectController.$inject = ['$scope', '$http', 'SearchObject'];
    angular.module('app').controller('SearchObjectController', SearchObjectController);
})();
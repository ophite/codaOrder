(function () {
    'use strict';

    // api goto Factory
    function SearchObject($resource) {

        //defaults: new { controller = "SearchController", action = "Find", searchText = UrlParameter.Optional }
        var promise = $resource("http://localhost:35133/Search/Find");
        return promise;
    }

    SearchObject.$inject = ['$resource'];
    angular.module('app').factory('SearchObject', SearchObject);
})();
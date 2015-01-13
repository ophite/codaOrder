(function () {
    'use strict';

    // api goto Factory
    function getDocuments($resource) {

        var promise = $resource("getDocuments");
        return promise;
    }

    getDocuments.$inject = ['$resource'];
    angular.module('app').factory('getDocuments', getDocuments);
})();
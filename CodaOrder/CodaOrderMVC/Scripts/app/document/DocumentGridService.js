(function () {
    'use strict';

    // api goto Factory
    function GetDocuments($resource) {

        var promise = $resource("GetDocuments");
        return promise;
    }

    GetDocuments.$inject = ['$resource'];
    angular.module('app').factory('GetDocuments', GetDocuments);
})();
/// <reference path="~/Scripts/angular.js" />

(function () {
    'use strict';

    // api goto Factory
    function getDocuments($resource) {

        var promise = $resource("getDocuments");
        return promise;
    }

    angular.module('app').factory('getDocuments', ['$resource',
        function ($resource) {
            return {
                get: function (paramDict) {
                    return $resource('getDocuments', {}, {
                        get: {
                            method: 'GET',
                            params: paramDict,
                            isArray: true
                        }
                    });

                }
            };
        }]);

    getDocuments.$inject = ['$resource'];
    angular.module('app').factory('getDocuments', getDocuments);
})();
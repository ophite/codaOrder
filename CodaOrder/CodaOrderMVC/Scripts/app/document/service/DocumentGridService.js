/// <reference path="~/Scripts/angular.js" />

(function () {
    'use strict';

    angular.module('app').factory('documentService', ['$resource', 'DSCacheFactory', '$q',
        function ($resource, DSCacheFactory, $q) {
            return {
                get: function (paramDict, url_getDocument) {

                    return $resource(window.location.origin + url_getDocument, {}, {
                        fn: {
                            method: 'POST',
                            params: paramDict,
                            transformRequest: function (data, headersGetter) {
                                var headers = headersGetter();
                                //headers['Content-Type'] = 'application/json';
                                //headers['Content-Type'] = 'multipart/form-data';
                                headers['Content-Type'] = 'application/x-www-form-urlencoded';
                                //headers['X-Requested-With'] = 'XMLHttpRequest';
                                var formData = new FormData();
                                formData.append("model", angular.toJson(paramDict))
                                return formData;
                            },
                            //cache: DSCacheFactory.get('defaultCache')
                            cache: true
                        }
                    });
                }
            };
        }]);

    angular.module('app').factory('apiService', ['documentService', 'DSCacheFactory',
        function (documentService, DSCacheFactory) {
            return {
                getDocuments: function (paramDict, url_getDocument, callbackFunc) {

                    var defaultCache = DSCacheFactory.get('defaultCache');
                    var result = defaultCache.get(angular.toJson(paramDict));

                    if (result) {
                        callbackFunc(result)
                    }
                    else {
                        var resPost = documentService.get(paramDict, url_getDocument).fn().$promise.then(
                            function (jsonData) {
                                var defaultCache = DSCacheFactory.get('defaultCache');
                                defaultCache.put(angular.toJson(paramDict), jsonData)
                                callbackFunc(jsonData);
                            });
                    }
                }
            }
        }
    ]);

    //angular.module('app').factory('getDocuments', ['$resource',
    //    function ($resource) {
    //        return {
    //            get: function (paramDict) {
    //                return $resource('getDocuments', { }, {
    //                    get: {
    //                        method: 'GET',
    //                        params: paramDict,
    //                        isArray: true,
    //                        transformRequest: transformRequest
    //                    }
    //                });
    //            },
    //        };
    //    }]);
    //getDocuments.$inject = ['$resource'];
    //angular.module('app').factory('getDocuments', getDocuments);
})();
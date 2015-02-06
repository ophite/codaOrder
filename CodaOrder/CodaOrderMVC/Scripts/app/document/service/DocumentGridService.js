/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/app/Constant.js" />

(function () {
    'use strict';

    angular.module(ConstantHelper.App).factory('documentService', ['$resource', 'DSCacheFactory', '$q',
        function ($resource, DSCacheFactory, $q) {
            return {
                get: function (paramDict, urlGetDocument) {

                    return $resource(window.location.origin + urlGetDocument, {}, {
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
                },
            };
        }]);


    angular.module(ConstantHelper.App).factory('apiService', ['documentService', 'DSCacheFactory',
        function (documentService, DSCacheFactory) {
            return {
                getDocuments: function (paramDict, urlGetDocument, callbackFunc) {

                    var defaultCache = DSCacheFactory.get('defaultCache');
                    var result = defaultCache.get(angular.toJson(paramDict));

                    if (result) {
                        callbackFunc(result)
                    }
                    else {
                        var resPost = documentService.get(paramDict, urlGetDocument).fn().$promise.then(
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

    //angular.module(ConstantHelper.App).factory('getDocuments', ['$resource',
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
    //angular.module(ConstantHelper.App).factory('getDocuments', getDocuments);
})();
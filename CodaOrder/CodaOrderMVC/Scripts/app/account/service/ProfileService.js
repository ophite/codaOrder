/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/linq-vsdoc.js" />

'use strict';

angular.module(ConstantHelper.App).factory('userProfileService', ['$resource', 'DSCacheFactory',
    function ($resource, DSCacheFactory) {
        return {
            api: function (urlGetUserProfile, params) {
                return $resource(window.location.origin + urlGetUserProfile, {}, {
                    get: {
                        method: 'GET',
                        transformRequest: function (data, headersGetter) {
                            var headers = headersGetter();
                            headers['Content-Type'] = 'application/json';
                            headers['Data-Type'] = 'json';
                        },
                        cache: true,
                    },
                    save: {
                        method: 'POST',
                        params: params,
                        transformRequest: function (data, headersGetter) {
                            var headers = headersGetter();
                            headers['Content-Type'] = 'application/json';
                            headers['Data-Type'] = 'json';
                        },
                    }
                });
            },
        };
    }
]);
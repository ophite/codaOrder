/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/app/Constant.js" />

(function () {
    'use strict';

    angular.module(ConstantHelper.App).factory('searchSubject', ['$resource',
        function ($resource) {
            return {
                query: function (textValue, url_searchSubject) {
                    return $resource(window.location.origin + url_searchSubject, {}, {
                        query: {
                            method: 'GET',
                            params: {
                                searchText: textValue
                            },
                            isArray: true,
                            //transformResponse: function (data, headersGetter) {
                            //},
                            transformRequest: function (data, headersGetter) {
                                var headers = headersGetter();
                                headers['X-Requested-With'] = 'XMLHttpRequest';
                                return data;
                            }
                        }
                    });
                }
            };
        }
    ]);

    //function SearchObject($resource) {

    //    //defaults: new { controller = "SearchController", action = "Find", searchText = UrlParameter.Optional }
    //    var promise = $resource("http://localhost:35133/Search/Find");
    //    return promise;
    //}

    //SearchObject.$inject = ['$resource'];
    //angular.module(ConstantHelper.App).factory('SearchObject', SearchObject);
})();
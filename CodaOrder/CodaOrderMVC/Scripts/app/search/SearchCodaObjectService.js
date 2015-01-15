(function () {
    'use strict';

    angular.module('app').factory('searchSubject', ['$resource',
        function ($resource) {
            return {
                query: function (textValue, className) {
                    return $resource('http://localhost:35133/SearchCodaObject/Subject', {}, {
                        query: {
                            method: 'GET',
                            params: {
                                searchText: textValue
                            },
                            isArray: true
                            //transformResponse: function (data, header) {
                            //}
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
    //angular.module('app').factory('SearchObject', SearchObject);
})();
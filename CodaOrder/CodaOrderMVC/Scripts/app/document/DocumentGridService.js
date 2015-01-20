/// <reference path="~/Scripts/angular.js" />
(function () {
    'use strict';

    angular.module('app').factory('getDocuments', ['$resource',
       function ($resource) {
           return {
               getDocs: function (paramDict) {
                   return $resource('http://localhost:35133/Documents/GetDocumentsPost', {}, {
                       save: {
                           method: 'POST',
                           params: paramDict,
                           //isArray: true
                           transformRequest: function (data, headersGetter) {
                               var headers = headersGetter();
                               //headers['Content-Type'] = 'application/json';
                               headers['Content-Type'] = 'multipart/form-data';
                               var formData = new FormData();
                               formData.append("model", angular.toJson(paramDict))
                               return formData;
                           }
                       }
                   });
               }
           };
       }]);

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
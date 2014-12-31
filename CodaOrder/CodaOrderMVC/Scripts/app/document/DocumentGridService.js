//(function () {
//    'use strict';

//    angular
//        .module('app_grid')
//        .service('service_grid', service_grid);

//    service_grid.$inject = ['$http'];

//    function service_grid($http) {
//        this.getData = getData;

//        function getData() { }
//    }
//})();

(function () {
    'use strict';

    // api goto Factory
    function GetDocuments($resource) {

        var promise = $resource("GetDocuments")
        return promise;
    }

    GetDocuments.$inject = ['$resource'];
    angular.module('app').factory('GetDocuments', GetDocuments);
})();
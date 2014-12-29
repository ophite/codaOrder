(function () {
    'use strict';
    var app = angular.module('app', [
        // Angular modules 
        //'ngAnimate',
        //'ngRoute',
        'ngResource',

        // Custom modules 

        // 3rd Party Modules
        //'ui.grid',
        'ngGrid',
        'ui.bootstrap'
    ])

    //app.config(function ($locationProvider, $httpProvider) {
    //});


    app.run(['$rootScope',
        function ($rootScope) {
            $rootScope.$on('pageChanged', function (event, args) {
                $rootScope.$broadcast('broadcastGetDocuments', args);
            });
        }
    ]);
})();
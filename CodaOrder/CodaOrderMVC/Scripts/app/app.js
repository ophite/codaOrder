﻿(function () {
    'use strict';

    var app = angular.module('app', [
        // Angular modules 
        //'ngAnimate',
        //'ngRoute',
        'ngResource',

        // 3rd Party Modules
        //'ui.grid',
        'ngGrid',
        'ui.bootstrap',
        'ui.bootstrap.tpls',

        // ui-select
        'ngSanitize',
        'ui.select'
    ])

    //app.config(function ($locationProvider, $httpProvider) {
    //});

    app.run(['$rootScope', function ($rootScope) {

        $rootScope.$on('startLoadingDocuments', function (event, args) {
            $rootScope.$broadcast('broadcastGetDocuments', args)
        });
        $rootScope.$on('pageChanged', function (event, args) {
            $rootScope.$broadcast('broadcastGetDocuments', args);
        });
        $rootScope.$on('pagingChange', function (event, args) {
            $rootScope.$broadcast('broadcastPagingChange', args);
        });
    }]);
})();
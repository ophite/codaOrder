/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular_source/angular.js" />

(function () {
    'use strict';

    var app = angular.module('app', [
        // Angular modules 
        //'ngAnimate',
        'ngResource',

        // 3rd Party Modules
        //'ui.grid',
        'ngGrid',
        'ui.bootstrap',
        'ui.bootstrap.tpls',

        'ngSanitize',
        'ui.select',
        'ngRoute',
        'ui.router',
        'angular-data.DSCacheFactory',
        'angularSpinner'
    ])

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'usSpinnerConfigProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider, usSpinnerConfigProvider) {

            //$urlRouterProvider.otherwise('Account/Login');
            $locationProvider.html5Mode({
                enabled: false,
                requireBase: false
            });
            $locationProvider.hashPrefix('!');
            $stateProvider
                .state(ConstantHelper.router.documents.name, {
                    url: ConstantHelper.router.documents.url,
                    templateUrl: ConstantHelper.router.documents.templateUrl
                })
                .state(ConstantHelper.router.newOrder.name, {
                    url: ConstantHelper.router.newOrder.url,
                    templateUrl: ConstantHelper.router.newOrder.templateUrl
                })
                .state(ConstantHelper.router.ordersDraft.name, {
                    url: ConstantHelper.router.ordersDraft.url,
                    templateUrl: ConstantHelper.router.ordersDraft.templateUrl
                })
                .state(ConstantHelper.router.ordersHistory.name, {
                    url: ConstantHelper.router.ordersHistory.url,
                    templateUrl: ConstantHelper.router.ordersHistory.templateUrl
                })
                .state(ConstantHelper.router.lines.name, {
                    url: ConstantHelper.router.lines.url + '/:documentID',
                    templateUrl: ConstantHelper.router.lines.templateUrl
                })
                .state(ConstantHelper.router.userProfile.name, {
                    url: ConstantHelper.router.userProfile.url,
                    templateUrl: ConstantHelper.router.userProfile.templateUrl
                });

            // spinner
            usSpinnerConfigProvider.setDefaults();
        }
    ]);

    app.run(['$rootScope', 'DSCacheFactory',
        function ($rootScope, DSCacheFactory) {
            // listeners
            $rootScope.$on(ConstantHelper.Watchers.startLoadingDocuments, function (event, args) {
                $rootScope.$broadcast(ConstantHelper.Watchers.broadcastStartLoadingDocuments);
            });
            $rootScope.$on(ConstantHelper.Watchers.pageChanged, function (event, args) {
                $rootScope.$broadcast(ConstantHelper.Watchers.broadcastGetDocuments, args);
            });
            $rootScope.$on(ConstantHelper.Watchers.setPagingInfo, function (event, args) {
                $rootScope.$broadcast(ConstantHelper.Watchers.broadcastPagingInfoChange, args);
            });

            // cache
            DSCacheFactory('defaultCache', {
                maxAge: 900000, // Items added to this cache expire after 15 minutes.
                cacheFlushInterval: 6000000, // This cache will clear itself every hour.
                deleteOnExpire: 'aggressive' // Items will be deleted from this cache right when they expire.
            });
        }]);
})();
/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular.js" />


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

        // ui-select
        'ngSanitize',
        'ui.select',
        //'ngRoute',
        'ui.router'
    ])

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider) {

            //$urlRouterProvider.otherwise('Account/Login');
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
                });
        }
    ]);

    app.run(['$rootScope', function ($rootScope) {
        $rootScope.$on(ConstantHelper.Watchers.startLoadingDocuments, function (event, args) {
            $rootScope.$broadcast(ConstantHelper.Watchers.broadcastStartLoadingDocuments);
        });
        $rootScope.$on(ConstantHelper.Watchers.pageChanged, function (event, args) {
            $rootScope.$broadcast(ConstantHelper.Watchers.broadcastGetDocuments, args);
        });
        $rootScope.$on(ConstantHelper.Watchers.setPagingInfo, function (event, args) {
            $rootScope.$broadcast(ConstantHelper.Watchers.broadcastPagingInfoChange, args);
        });
    }]);


})();
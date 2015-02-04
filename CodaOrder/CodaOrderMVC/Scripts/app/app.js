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
                .state('documents', {
                    url: ConstantHelper.urlDocuments,
                    templateUrl: ConstantHelper.templateDocuments
                })
                .state('newOrder', {
                    url: ConstantHelper.urlAddNew,
                    templateUrl: ConstantHelper.templateAddNew
                })
                .state('ordersDraft', {
                    url: ConstantHelper.urlOrdersDraft,
                    templateUrl: ConstantHelper.templateOrdersDraft
                })
                .state('ordersHistory', {
                    url: ConstantHelper.urlOrdersHistory,
                    templateUrl: ConstantHelper.templateOrdersHistory
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
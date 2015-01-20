/// <reference path="~/Scripts/app/common/Constant.js" />
(function () {
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
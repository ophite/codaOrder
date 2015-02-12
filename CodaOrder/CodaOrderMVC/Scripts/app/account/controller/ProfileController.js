/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular.js" />

(function () {
    'use strict';

    angular.module(ConstantHelper.App).factory('userProfileService', ['$resource', 'DSCacheFactory',
        function ($resource, DSCacheFactory) {
            return {
                getUserProfile: function (urlGetUserProfile) {
                    return $resource(window.location.origin + urlGetUserProfile), {}, {

                        fn: {
                            method: 'POST',
                            transformRequest: function (data, headersGetter) {
                                var headers = headersGetter();
                                headers['Content-Type'] = 'application/json';
                                headers['Data-Type'] = 'json';
                            },
                            cache: true,
                        }
                    }
                },
            };
        }
    ]);

    angular.module(ConstantHelper.App).controller('UserProfileController', ['$scope', 'userProfileService',
        function ($scope, userProfileService) {

            $scope.model = {};
            $scope.model.items = [];
            $scope.init = function (urlGetUserProfile) {
                $scope.urlGetUserProfile = urlGetUserProfile;
            };

            userProfileService.getUserProfile($scope.urlGetUserProfile).fn().$promise.then(
                function (jsonData) {
                    console.log(jsonData);
                });

            $scope.tabs = [
   { title: 'Dynamic Title 1', content: 'Dynamic content 1' },
   { title: 'Dynamic Title 2', content: 'Dynamic content 2', disabled: true }
            ];

            $scope.alertMe = function () {
                setTimeout(function () {
                    $window.alert('You\'ve selected the alert tab!');
                });
            };
        }
    ]);
})();
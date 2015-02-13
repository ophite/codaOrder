/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/linq-vsdoc.js" />

(function () {
    'use strict';

    angular.module(ConstantHelper.App).factory('userProfileService', ['$resource', 'DSCacheFactory',
        function ($resource, DSCacheFactory) {
            return {
                api: function (urlGetUserProfile, params) {
                    return $resource(window.location.origin + urlGetUserProfile, {}, {
                        get: {
                            method: 'GET',
                            transformRequest: function (data, headersGetter) {
                                var headers = headersGetter();
                                headers['Content-Type'] = 'application/json';
                                headers['Data-Type'] = 'json';
                            },
                            cache: true,
                        },
                        save: {
                            method: 'POST',
                            params: params,
                            transformRequest: function (data, headersGetter) {
                                var headers = headersGetter();
                                headers['Content-Type'] = 'application/json';
                                headers['Data-Type'] = 'json';
                            },
                        }
                    });
                },
            };
        }
    ]);

    angular.module(ConstantHelper.App).controller('UserProfileController', ['$scope', 'userProfileService', 'showErrorService',
        function ($scope, userProfileService, showErrorService) {

            $scope.model = {
                tabs: [],
            };

            $scope.init = function (urlGetUserProfile) {
                $scope.model.urlGetUserProfile = urlGetUserProfile;
                var p = userProfileService.api($scope.model.urlGetUserProfile).get().$promise.then(
                    function (jsonData) {
                        var items = angular.fromJson(jsonData);

                        // profile
                        items.Profile.isProfile = true;
                        $scope.model.tabs[$scope.model.tabs.length] = items.Profile;

                        // firm
                        angular.forEach(items.Firms, function (firm) {
                            $scope.model.tabs[$scope.model.tabs.length] = firm;
                        });
                    });
            };

            $scope.save = function () {
                var data = {
                    Profile: $scope.model.tabs[0],
                    Firms:$scope.model.tabs.slice(1, $scope.model.tabs.length)
                }
                userProfileService.api($scope.model.urlGetUserProfile, [angular.toJson(data)]).save().$promise.then(
                    function (jsonData) {
                        var isError = jsonData[ConstantHelper.IsResponseError];
                        if (isError.toLowerCase() === 'true') {
                            showErrorService.show('Error during save profile', jsonData[ConstantHelper.ResponseErrorMessage]);
                        }
                        else
                            $scope.userProfileForm.$setPristine();
                    });
            };
        }
    ]);
})();
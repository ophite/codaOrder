/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/linq-vsdoc.js" />

(function () {
    'use strict';

    angular.module(ConstantHelper.App).factory('userProfileService', ['$resource', 'DSCacheFactory',
        function ($resource, DSCacheFactory) {
            return {
                getUserProfile: function (urlGetUserProfile) {
                    return $resource(window.location.origin + urlGetUserProfile, {}, {

                        fn: {
                            method: 'POST',
                            transformRequest: function (data, headersGetter) {
                                var headers = headersGetter();
                                //headers['Content-Type'] = 'application/x-www-form-urlencoded';
                                headers['Content-Type'] = 'application/json';
                                headers['Data-Type'] = 'json';
                            },
                            cache: true,
                        }
                    });
                },
            };
        }
    ]);

    angular.module(ConstantHelper.App).controller('UserProfileController', ['$scope', 'userProfileService',
        function ($scope, userProfileService) {

            $scope.model = {};
            $scope.model.tabs = [];
            $scope.init = function (urlGetUserProfile) {
                $scope.model.urlGetUserProfile = urlGetUserProfile;
                var p = userProfileService.getUserProfile($scope.model.urlGetUserProfile).fn().$promise.then(
                    function (jsonData) {
                        $scope.model.items = angular.fromJson(jsonData);
                        var tabInfo = Enumerable.From($scope.model.items);
                        var tabs = [];

                        var profile = $scope.model.items.Profile
                        var profileTab = {
                            title: profile.Name,
                            isProfile: true,
                            content: {
                                firstName: profile.FirstName,
                                lastName: profile.LastName,
                                phone: profile.phone,
                                email: profile.email,
                            }
                        };
                        $scope.model.tabs.push(profileTab);
                    })
                };

   //         $scope.tabs = [
   //{ title: 'Dynamic Title 1', content: 'Dynamic content 1' },
   //{ title: 'Dynamic Title 2', content: 'Dynamic content 2', disabled: true }
   //         ];

   //         $scope.alertMe = function () {
   //             setTimeout(function () {
   //                 $window.alert('You\'ve selected the alert tab!');
   //             });
   //         };
        }
    ]);
})();
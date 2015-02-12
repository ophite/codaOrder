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
            //$scope.model.selectedSubject = {};

            $scope.init = function (urlGetUserProfile) {
                $scope.model.urlGetUserProfile = urlGetUserProfile;
                var p = userProfileService.getUserProfile($scope.model.urlGetUserProfile).fn().$promise.then(
                    function (jsonData) {
                        var items = angular.fromJson(jsonData);

                        // profile
                        var profile = items.Profile;
                        profile.Profile = {}
                        profile.Profile.Name = "Profile";
                        profile.isProfile = true;
                        $scope.model.tabs.push(profile);

                        angular.forEach(items.Firms, function (firm) {
                            firm.selectedSubject = firm.Subjects[0];
                            $scope.model.tabs.push(firm);
                        });
                    })
            };

            $scope.$watch('model.selectedSubject', function (newValues, oldValues) {
                console.log(newValues, oldValues);
            }, true);

            $scope.save = function () {
                console.log('save');
            };
        }
    ]);
})();
/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/linq-vsdoc.js" />

(function () {
    'use strict';

    angular.module(ConstantHelper.App).controller('UserProfileController', ['$scope', 'userProfileService', 'showErrorService',
        function ($scope, userProfileService, showErrorService) {

            $scope.model = {
                tabs: [],
            };

            $scope.init = function (urlGetUserProfile) {
                $scope.model.urlGetUserProfile = urlGetUserProfile;
                var p = userProfileService.api($scope.model.urlGetUserProfile).get().$promise.then(
                    function (jsonData) {
                        // unpack data
                        var items = angular.fromJson(jsonData);
                        // profile
                        $scope.model.tabProfile = items.Profile;
                        // firm
                        angular.forEach(items.Firms, function (firm) {
                            $scope.model.tabs[$scope.model.tabs.length] = firm;
                        });
                    });
            };

            $scope.save = function () {
                //pack dataq
                var data = {
                    Profile: $scope.model.tabProfile,
                    Firms:$scope.model.tabs.slice(0, $scope.model.tabs.length)
                }
                // save
                userProfileService.api($scope.model.urlGetUserProfile, [angular.toJson(data)]).save().$promise.then(
                    function (jsonData) {
                        if (!showErrorService.show('Error during save profile', jsonData))
                            $scope.form.$setPristine();
                    });
            };
        }
    ]);
})();
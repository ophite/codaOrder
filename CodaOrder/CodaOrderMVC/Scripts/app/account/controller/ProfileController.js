/// <reference path="~/Scripts/app/Constant.js" />
/// <reference path="~/Scripts/angular.js" />

(function () {
    'use strict';

    angular.module(ConstantHelper.App).controller('ProfileController', ['$scope', '$modal', '$log',
        function ($scope, $modal, $log) {

            $scope.items = ['item1', 'item2', 'item3'];

            $scope.open = function (size) {

                var modalInstance = $modal.open({
                    templateUrl: 'profile.html',
                    controller: 'ProfileControllerModal',
                    size: size,
                    resolve: {
                        items: function () {
                            return $scope.items;
                        }
                    }
                });

                modalInstance.result.then(function (selectedItem) {
                    $scope.selected = selectedItem;
                }, function () {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            };
        }
    ]);

    // Please note that $modalInstance represents a modal window (instance) dependency.
    // It is not the same as the $modal service used above.
    angular.module(ConstantHelper.App).controller('ProfileControllerModal', ['$scope', '$modalInstance', 'items',
        function ($scope, $modalInstance, items) {

            $scope.items = items;
            $scope.selected = {
                item: $scope.items[0]
            };

            $scope.ok = function () {
                $modalInstance.close($scope.selected.item);
            };

            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            };
        }
    ]);

})();
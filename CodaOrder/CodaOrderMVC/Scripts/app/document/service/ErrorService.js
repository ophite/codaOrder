/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/app/Constant.js" />

'use strict';

angular.module(ConstantHelper.App).factory('showErrorService', ['$modal',
    function ($modal) {

        return {
            show: function (errorTitle, jsonDataResponse) {
                // get error message from json response
                var isError = jsonDataResponse[ConstantHelper.IsResponseError];
                if (isError.toLowerCase() !== 'true') {
                    return false;
                }

                var errorDescription = jsonDataResponse[ConstantHelper.ResponseErrorMessage];

                // show modal window
                var modalInstance = $modal.open({
                    templateUrl: 'error.html',
                    controller: 'ErrorControllerModal',
                    resolve: {
                        errorTitle: function () { return errorTitle; },
                        errorDescription: function () { return errorDescription; }
                    }
                });

                modalInstance.result.then(function (errorDescriptionFromModel) {
                    console.log('ok modal');
                    if (errorDescriptionFromModel !== undefined)
                        console.log(errorDescriptionFromModel);
                }, function () {
                    console.log('cancel/error modal');
                });

                return true;
            }
        };
    }
]);

//Please note that $modalInstance represents a modal window (instance) dependency.
//It is not the same as the $modal service used above.
angular.module(ConstantHelper.App).controller('ErrorControllerModal', ['$scope', '$modalInstance', 'errorTitle', 'errorDescription',
    function ($scope, $modalInstance, errorTitle, errorDescription) {

        $scope.model = {}
        $scope.model.errorTitle = errorTitle;
        $scope.model.errorDescription = errorDescription;

        $scope.ok = function () {
            $modalInstance.close($scope.errorDesctiption);
        };

        //$scope.cancel = function () {
        //    $modalInstance.dismiss('cancel');
        //};
    }
]);

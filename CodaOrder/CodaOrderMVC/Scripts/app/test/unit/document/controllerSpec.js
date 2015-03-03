describe('controller document grid', function () {
    beforeEach(module('app'));

    var $controller, $location, $rootScope;

    beforeEach(inject(function (_$controller_, _$location_, _$rootScope_) {
        $controller = _$controller_;
        $location = _$location_;
        $rootScope = _$rootScope_;
    }));

    describe('documentGridController', function () {
        var $scope, controller;

        beforeEach(function () {
            $scope = $rootScope.$new();
            controller = $controller('DocumentGridController', {$scope: $scope});
        });

        it('should run init function and change scope.model.urlGetDocument', function () {
            spyOn($scope, 'init').and.callThrough();
            var testUrl = 'testUrl';
            $scope.init(testUrl);
            expect($scope.init).toHaveBeenCalledWith(testUrl);
            expect($scope.model.urlGetDocument).toBe(testUrl);
        });

        it('should run onDblClickRow and check row param', function () {
            spyOn($scope, 'onDblClickRow').and.callThrough();
            var row = {};
            row.entity = {OID: 1};
            $scope.onDblClickRow(row);
            expect($scope.onDblClickRow).toHaveBeenCalled();
            expect($scope.onDblClickRow).toHaveBeenCalledWith(row);
            $rootScope.$apply();
            expect($location.path()).toBe(ConstantHelper.router.lines.url + '/' + row.entity.OID);
        });

        it('should exists setPagingData function', function () {
            expect($scope.setPagingData).toBeDefined();
        });

        it('should exists startSpin, stopSpin function', function () {
            expect($scope.startSpin).toBeDefined();
            expect($scope.stopSpin).toBeDefined();
        });

        it('check gridOptions properties', function () {
            expect($scope.gridOptions).toBeDefined();
            var items = Enumerable.From($scope.gridOptions.columnDefs);
            expect(items.Where(function (x) {
                return x.field == 'Amount'
            }).ToArray().length).toBe(1);

            expect($scope.gridOptions.data).toBe('model.data');
            expect($scope.gridOptions.plugins.length).toBeGreaterThan(0);
        });

        it('check scope.on', function () {
            spyOn($rootScope, '$broadcast').and.callThrough();
            $rootScope.$broadcast(ConstantHelper.Watchers.broadcastGetDocuments, [{id: 1, name: 'test'}]);
            expect($scope.$broadcast).toHaveBeenCalledWith(ConstantHelper.Watchers.broadcastGetDocuments, [{
                id: 1,
                name: 'test'
            }]);
        });
    });
});
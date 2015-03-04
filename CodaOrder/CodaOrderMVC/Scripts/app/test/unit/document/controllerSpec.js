describe('controller document grid', function () {
    beforeEach(module('app'));

    var $controller, $location, $rootScope;

    beforeEach(inject(function (_$controller_, _$location_, _$rootScope_) {
        $controller = _$controller_;
        $location = _$location_;
        $rootScope = _$rootScope_;
    }));

    describe('after injection', function () {
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
            spyOn($scope, 'init').and.callThrough();
            var testUrl = 'testUrl';
            $scope.init(testUrl);

            spyOn($rootScope, '$broadcast').and.callThrough();
            $rootScope.$broadcast(ConstantHelper.Watchers.broadcastGetDocuments, [{id: 1, name: 'test'}]);
            expect($scope.$broadcast).toHaveBeenCalledWith(ConstantHelper.Watchers.broadcastGetDocuments, [{
                id: 1,
                name: 'test'
            }]);
        });
    });
});

//expect(result).toEqual("[{OID:1, Amount:22.1, ItemName:'Safe gard'}]");

//function getFiles() {
//    var maxTime = 55;
//    this.files = [
//        {name: 'f1', time: 11},
//        {name: 'f2', time: 20},
//        {name: 'f3', time: 21},
//        {name: 'f4', time: 22},
//        {name: 'f5', time: 2},
//        {name: 'f6', time: 3},
//    ];
//
//    function fn(fileCurrent, filesSelected, filesAvailable, filesForIteration, filesResult) {
//        // check for exit
//        if (filesForIteration.length == 0)
//            return filesResult;
//
//        // 1st time
//        if (fileCurrent == null) {
//            var file = filesForIteration.pop();
//            var fileForSum = Enumerable.From(this.files).Where(function (x) {
//                return x.name != file.name
//            }).ToArray();
//            return fn(file, [file], fileForSum, filesForIteration, filesResult);
//        }
//
//        // end of available times for current time
//        if (filesAvailable.length == 0) {
//            var result = {
//                name: fileCurrent.name,
//                files: filesSelected
//            };
//
//            filesResult.push(result);
//
//            if (filesForIteration.length > 0)
//                return fn(null, null, null, filesForIteration, filesResult);
//            else
//                return filesResult;
//        }
//
//        // calc time sum
//        var newFile = filesAvailable.pop();
//        var files = Enumerable.From(filesSelected);
//        var time = files.Select(function (x) {
//            return x.time;
//        }).Sum();
//
//        if (newFile.time + time < maxTime)
//            filesSelected.push(newFile);
//
//        return fn(fileCurrent, filesSelected, filesAvailable, filesForIteration, filesResult);
//    };
//
//    // run recursion
//    return fn(null, null, this.files, this.files, []);
//}
//var result = getFiles();
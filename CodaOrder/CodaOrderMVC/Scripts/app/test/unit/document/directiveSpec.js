describe('directive: document', function () {
    var $compile, $rootScope, $httpBackend, $filter, $scope;

    beforeEach(module('app'));
    beforeEach(module('Scripts/app/document/template/DirDatePicker.html'));

    beforeEach(inject(function (_$compile_, _$rootScope_, _$httpBackend_, _$filter_) {
        $compile = _$compile_;
        $rootScope = _$rootScope_;
        $httpBackend = _$httpBackend_;
        $filter = _$filter_;
        $scope = $rootScope.$new();
    }))

    describe('test.dirDatePicker', function () {
        it('should test dir date picker', function () {
            function formatDate(dateRow) {
                return $filter('date')(dateRow, $scope.format);
            }

            var element = angular.element('<div><dir-date-picker ng-model="dates.dateStart" date-format="{{format}}" /></div>');
            $compile(element)($scope);
            $scope.$digest();
            var childIsolateScope = element.children().isolateScope();
            var input = element.find('input');

            expect(childIsolateScope).toBeDefined();
            expect(input).toBeDefined();
            expect(element.html()).toContain('min-date');
            expect(element.html()).toContain('max-date');
            expect(element.html()).toContain('datepicker-popup');
            expect(element.html()).toContain('datepicker-options');
            expect(childIsolateScope.maxDate).toBeDefined();
            expect(childIsolateScope.minDate).toBeDefined();
            expect(childIsolateScope.dateOptions).toBeDefined();
            expect(childIsolateScope.disabled).toBeDefined();
        });
    });

    describe('test.dirLeftMenuLinks', function () {
        it('should be true', function () {
            // fire run request by all state's templateUrl and if not setting up url then get Error
            // in javascript test mode not setting state data because it setting up from C# backend
            // all data save in  backend C#
            $httpBackend.whenGET('').respond();
            $rootScope.$apply(function () {
                var element = $compile('<dir-left-menu-links></dir-left-menu-links>')($rootScope);
                // check contains state ui-sref
                expect(element).toBeDefined();
                expect(element.html()).toContain('ui-sref');

                // check all menu items
                var menuItems = Enumerable.From(ConstantHelper.router).Where(function (x) {
                    return x.Value.isMenu == true;
                }).ToArray();

                angular.forEach(menuItems, function (value, index) {
                    expect(element.html()).toContain(value.Value.name);
                    expect(element.html()).toContain(value.Value.fullName);
                });
            });
        })
    });
});
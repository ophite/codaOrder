describe('directive: document', function () {
    var $compile, $rootScope, $httpBackend, $state;

    beforeEach(module('app'));
    beforeEach(inject(function (_$compile_, _$rootScope_, _$httpBackend_, _$state_) {
        $compile = _$compile_;
        $rootScope = _$rootScope_;
        $httpBackend = _$httpBackend_;
        $state = _$state_;
    }))

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
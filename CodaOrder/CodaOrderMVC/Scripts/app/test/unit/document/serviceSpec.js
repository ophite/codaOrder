describe("service: document", function () {
    var mockDocumentService, mockApiService;
    var $httpBackend, $controller;
    var generateUrl;

    beforeEach(module('app'));
    beforeEach(inject(function (_$httpBackend_, _$controller_, _documentService_, _apiService_) {
        mockDocumentService = _documentService_;
        mockApiService = _apiService_;
        $httpBackend = _$httpBackend_;
        $controller = _$controller_;

        generateUrl = function (params) {
            var expectedUrl = '?';
            angular.forEach(Object.keys(params).sort(), function (key, value) {
                expectedUrl = expectedUrl + key + '=' + params[key] + '&';
            });
            expectedUrl = expectedUrl.substring(0, expectedUrl.length - 1);

            return expectedUrl;
        }
    }));

    afterEach(function () {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    it('services defined', function () {
        expect(mockDocumentService).toBeDefined();
        expect(mockApiService).toBeDefined();
    });

    it('check getDocuments method', function () {
        var urlGetDocument = ConstantHelper.router.documents.url || '/Document/GetDocuments';

        var params = {
            subjectID: ConstantHelper.Document.paramSubjectID.default,
            dateBegin: ConstantHelper.Document.paramDateBegin.default,
            dateEnd: ConstantHelper.Document.paramDateEnd.default,
            pagesCount: ConstantHelper.Document.paramPagesCount.default,
            pageSize: ConstantHelper.Document.paramPageSize.default,
            currentPage: ConstantHelper.Document.paramCurrentPage.default,
            totalRows: ConstantHelper.Document.paramTotalRows.default
        };

        var expectedUrl = generateUrl(params);
        var jsonTestData = {
            documents: [{
                OID: 1,
                Amount: 22.4,
                ItemName: 'test item name'
            }]
        };
        var jsonStr = JSON.stringify(jsonTestData);

        $httpBackend.whenGET(urlGetDocument + expectedUrl).respond(jsonStr);
        $httpBackend.expectGET(urlGetDocument + expectedUrl).respond(200, jsonStr);

        mockDocumentService.api(params, urlGetDocument).get().$promise.then(function (jsonData) {
            expect(JSON.stringify(jsonData.documents)).toBe(JSON.stringify(jsonTestData.documents));
        });

        $httpBackend.flush();
    });
});

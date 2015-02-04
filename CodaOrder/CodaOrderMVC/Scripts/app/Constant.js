'use strict';

var ConstantHelper = new function () {

    // document
    this.urlDocuments = "";
    this.templateDocuments = "";
    // add new order
    this.urlAddNew = "";
    this.templateAddNew = "";
    // orders draft
    this.urlOrdersDraft = "";
    this.templateOrdersDraft = "";
    // orders history
    this.urlOrdersHistory = "";
    this.templateOrdersHistory = "";

    this.Watchers = new function () {
        this.startLoadingDocuments = 'startLoadingDocuments';
        this.broadcastStartLoadingDocuments = 'broadcastStartLoadingDocuments';
        this.pageChanged = 'pageChanged';
        this.setPagingInfo = 'setPagingInfo';
        this.broadcastGetDocuments = 'broadcastGetDocuments';
        this.broadcastPagingInfoChange = 'broadcastPagingInfoChange';
    };

    this.Document = new function () {
        this.paramSubjectID = {
            value: 'subjectID',
            default: ''
        };
        this.paramDateBegin = {
            value: 'dateBegin',
            default: ''
        };
        this.paramDateEnd = {
            value: 'dateEnd',
            default: ''
        };
        this.paramDocTypeClasses = {
            value: 'docTypeClasses',
            default: ''
        };
        // paging
        this.paramPagesCount = {
            value: 'pagesCount',
            default: '1'
        };
        this.paramPageSize = {
            value: 'pageSize',
            default: '20'
        };
        this.paramPageNumberCount = {
            value: 'pageNumberCount',
            default: '5'
        };
        this.paramCurrentPage = {
            value: 'currentPage',
            default: '1'
        };
        this.paramTotalRows = {
            value: 'totalRows',
            default: '0'
        };
        // filtering
        this.paramFullTextFilter = {
            value: 'fullTextFilter',
            default: ''
        };
        this.paramWhereText = {
            value: 'whereText',
            default: ''
        };
    };

    this.prepareAllDocumentParams = function (paramsDict) {
        var resultDict = {};
        for (var propName in this.Document) {

            var prop = this.Document[propName];
            var value = paramsDict[prop.value];
            if (value === undefined)
                resultDict[prop.value] = prop.default;
            else {

                if (Array.isArray(value))
                    value = value.join();
                resultDict[prop.value] = value;
            }
        }

        return resultDict;
    };
}

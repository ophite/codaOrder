'use strict';

var ConstantHelper = new function () {

    // document
    this.router = {
        documents: {
            isMenu: true,
            order: 1,
            name: "documents",
            fullName: "documents",
            url: "",
            templateUrl: ""
        },
        newOrder: {
            isMenu: true,
            order: 2,
            name: "newOrder",
            fullName: "new order",
            url: "",
            templateUrl: ""
        },
        ordersDraft: {
            isMenu: true,
            order: 3,
            name: "ordersDraft",
            fullName: "orders draft",
            url: "",
            templateUrl: ""
        },
        ordersHistory: {
            isMenu: true,
            order: 4,
            name: "ordersHistory",
            fullName: "orders history",
            url: "",
            templateUrl: ""
        },
        lines: {
            isMenu: false,
            order: 1,
            name: "lines",
            fullName: "lines",
            url: "",
            templateUrl: "",
            urlGetJSON: "",
            urlSaveJSON: "",
        },
        userProfile: {
            isMenu: false,
            order: 1,
            name: "userProfile",
            fullName: "user profile",
            url: "",
            templateUrl: ""
        }
    };

    this.Watchers = new function () {
        this.startLoadingDocuments = 'startLoadingDocuments';
        this.broadcastStartLoadingDocuments = 'broadcastStartLoadingDocuments';
        this.pageChanged = 'pageChanged';
        this.setPagingInfo = 'setPagingInfo';
        this.broadcastGetDocuments = 'broadcastGetDocuments';
        this.broadcastPagingInfoChange = 'broadcastPagingInfoChange';
    };

    this.IsResponseError = '';
    this.ResponseErrorMessage = '';
    this.GridData = '';
    this.DocumentID = 'documentID';
    this.IsEditable = '';
    this.App = 'app';

    this.Document = new function () {
        this.paramSubjectID = {
            value: '',
            default: ''
        };
        this.paramDateBegin = {
            value: '',
            default: ''
        };
        this.paramDateEnd = {
            value: '',
            default: ''
        };
        this.paramDocTypeClasses = {
            value: '',
            default: ''
        };
        // paging
        this.paramPagesCount = {
            value: '',
            default: '1'
        };
        this.paramPageSize = {
            value: '',
            default: '20'
        };
        this.paramPageNumberCount = {
            value: '',
            default: '5'
        };
        this.paramCurrentPage = {
            value: '',
            default: '1'
        };
        this.paramTotalRows = {
            value: '',
            default: '0'
        };
        // filtering
        this.paramFullTextFilter = {
            value: '',
            default: ''
        };
        this.paramWhereText = {
            value: '',
            default: ''
        };
    };

    this.prepareAllDocumentParams = function (paramsDict) {
        var resultDict = {};
        if (paramsDict === undefined || paramsDict === null)
            return resultDict;

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
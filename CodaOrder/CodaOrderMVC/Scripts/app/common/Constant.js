'use strict';

var ConstantHelper = new function () {

    this.Document = new function () {
        this.paramSubjectID = {
            value: 'subjectID',
            default: ''
        }
        this.paramDateBegin = {
            value: 'dateBegin',
            default: ''
        }
        this.paramDateEnd = {
            value: 'dateEnd',
            default: ''
        }
        this.paramDocTypeClasses = {
            value: 'docTypeClasses',
            default: ''
        }
        this.paramPageSize = {
            value: 'pageSize',
            default: ''
        }
        this.paramCurrentPage = {
            value: 'currentPage',
            default: ''
        }
        this.paramWhereText = {
            value: 'whereText',
            default: ''
        }
    };

    this.getAllDocumentParams = function (paramsDict) {
        var resultDict = {};
        for (var propName in this.Document) {

            var prop = this.Document[propName];
            var value = paramsDict[prop.value];
            if (value === undefined)
                resultDict[prop.value] = prop.default;
            else
                resultDict[prop.value] = value;
        }

        return resultDict;
    };
}

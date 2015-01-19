'use strict';

var app = angular.module('app');
app.service('parameterService', function () {

    var paramDict = {};
    var getDocuments = 'getDocuments';
    //var paging = 'paging';

    this.setDocumentParam = function (paramName, paramValue) {
        this.setParametrForFunc(getDocuments, paramName, paramValue);
    };

    this.getDocumentParams = function () {
        return paramDict[getDocuments];
    };

    //this.setPagingParam = function (paramName, paramValue) {
    //    this.setParametrForFunc(paging, paramName, paramValue);
    //};

    //this.getPagingParams = function () {
    //    return paramDict[paging];
    //};

    this.setParametrForFunc = function (functionName, paramName, paramValue) {
        if (paramDict[functionName] === undefined)
            paramDict[functionName] = {};

        var pdict = paramDict[functionName];
        pdict[paramName] = paramValue;
    }
});
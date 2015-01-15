'use strict';

var app = angular.module('app');
app.service('parameterService', function () {

    var paramDict = {};
    var getDocuments = 'getDocuments';

    this.setDocumentParams = function (paramName, paramValue) {
        this.setParametrForFunc(getDocuments, paramName, paramValue);
    }

    this.getDocumentParams = function () {
        return paramDict[getDocuments];
    };

    this.setParametrForFunc = function (functionName, paramName, paramValue) {
        if (paramDict[functionName] === undefined)
            paramDict[functionName] = {};

        var pdict = paramDict[functionName];
        pdict[paramName] = paramValue;
    }
});
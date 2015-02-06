/// <reference path="~/Scripts/app/Constant.js" />

'use strict';

var app = angular.module(ConstantHelper.App);
app.service('parameterService', function () {

    var paramDict = {};
    var getDocuments = 'getDocuments';
    
    this.setDocumentParam = function (paramName, paramValue) {
        this.setParametrForFunc(getDocuments, paramName, paramValue);
    };

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
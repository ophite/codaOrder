/// <reference path="~/Scripts/3rdparty/linq-vsdoc.js" />
/// <reference path="~/Scripts/app/Constant.js" />

(function () {
    'use strict';

    // helpers convert grid filter string to sql query where and full text query
    function filterStrToSqlFunc(filterStr, columns) {

        // 1 prepare
        var dict = {
            whereQuery: '',
            fullTextQuery: '<Root/>'
        };

        if (filterStr.indexOf(';') < 0)
            return dict;

        var whereQueryArr = [];
        var fullTextQueryArr = [];
        var iColumns = Enumerable.From(columns);

        // 2 main func
        function callBack(element, index, array) {
            if (!element)
                return;

            var captionValue = element.split(':');
            var item = iColumns
                .Where(function (x) { return x.displayName === captionValue[0] })
                .Select(function (x) { return x })
                .FirstOrDefault();

            var value = captionValue[1].replace(' ^', '');
            var fieldStr = String(item.field);

            //N'<Root><Filter TableID="-1" Not="false" Value="Новус" TableName="Subject" ColumnName="Name"/></Root>'
            if (fieldStr.indexOf('_') > -1) {

                var table = fieldStr.split('_')[0];
                var field = fieldStr.split('_')[1];
                fullTextQueryArr.push('<Filter TableID ="-1" Not="false" Value="' + value + '" TableName="' + table + '" ColumnName="' + field + '"/>');
            }
                //N' and ((isnull(charindex(''прод'', _journalalias_.name), 0) > 0) and (isnull(charindex(''71'', _journalalias_.doccode), 0) > 0))'
            else {
                if (whereQueryArr.length > 0)
                    whereQueryArr.push('and ');

                whereQueryArr.push('(isnull(charindex(\'' + value + '\', _journalalias_.' + fieldStr.toLowerCase() + '), 0) > 0) ');
            }
        }
        filterStr.split(';').forEach(callBack);

        if (fullTextQueryArr.length > 0) {
            fullTextQueryArr.splice(0, 0, '<Root>');
            fullTextQueryArr.push('</Root>');
        }
        else
            fullTextQueryArr.push('<Root/>')

        if (whereQueryArr.length > 0) {
            whereQueryArr.splice(0, 0, ' and (');
            whereQueryArr.push(')');
        }

        // 3 result
        dict[ConstantHelper.Document.paramWhereText.value] = whereQueryArr.join('');
        dict[ConstantHelper.Document.paramFullTextFilter.value] = fullTextQueryArr.join('');

        return dict;
    }

    angular.module(ConstantHelper.App).service('filterStrToSql', function () {
        this.fn = function (filterStr, columns) {
            return filterStrToSqlFunc(filterStr, columns);
        };
    });
})();
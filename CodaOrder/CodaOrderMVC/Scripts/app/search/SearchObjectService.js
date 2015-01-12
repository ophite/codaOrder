(function () {
    'use strict';


    angular.module('app').factory('SearchObject', ['$resource',
        function ($resource) {

            return {
                query: function (textValue) {

                    var a1 = textValue;
                    return $resource('http://localhost:35133/Search/Find', {}, {
                        query: {
                            method: 'GET',
                            params: {
                                searchText: textValue
                            },
                            isArray: true
                            //transformResponse: function (data, header) {
                            //    alert('transformResponse')
                            //    //funcCallBack(data, header);
                            //    //Getting string data in response
                            //    //var jsonData = JSON.parse(data); //or angular.fromJson(data)
                            //    //var notes = [];

                            //    ////angular.forEach(jsonData, function (item) {
                            //    ////    var note = new Note();
                            //    ////    note.noteTitle = item.title;
                            //    ////    notes.push(note);
                            //    ////});

                            //    //return notes;
                            //}
                        }
                    });
                }
            };

            //var res = $resource('http://localhost:35133/Search/Find', {}, {
            //    query: {
            //        method: 'GET',
            //        params: {
            //            searchText: ''
            //        },
            //        isArray: false
            //        //transformResponse: function (data, header) {
            //        //    //Getting string data in response
            //        //    var jsonData = JSON.parse(data); //or angular.fromJson(data)
            //        //    var notes = [];

            //        //    //angular.forEach(jsonData, function (item) {
            //        //    //    var note = new Note();
            //        //    //    note.noteTitle = item.title;
            //        //    //    notes.push(note);
            //        //    //});

            //        //    return notes;
            //        //}
            //    }
            //});
            //return res;
        }
    ]);


    //function SearchObject($resource) {

    //    //defaults: new { controller = "SearchController", action = "Find", searchText = UrlParameter.Optional }
    //    var promise = $resource("http://localhost:35133/Search/Find");
    //    return promise;
    //}

    //SearchObject.$inject = ['$resource'];
    //angular.module('app').factory('SearchObject', SearchObject);
})();
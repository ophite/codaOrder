// Karma configuration
// Generated on Mon Mar 02 2015 17:01:18 GMT+0200 (FLE Standard Time)

module.exports = function (config) {
    config.set({

        // base path that will be used to resolve all patterns (eg. files, exclude)
        basePath: '../../../',


        // frameworks to use
        // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
        frameworks: ['jasmine'],


        // list of files / patterns to load in the browser
        files: [
            'Scripts/angular_source/angular.js',
            'Scripts/angular_source/angular-sanitize.js',
            'Scripts/angular_source/angular-mocks.js',
            'Scripts/angular_source/angular-resource.js',
            'Scripts/angular_source/angular-route.js',
            'Scripts/angular_ui/angular-ui-router.js',
            'Scripts/angular_ui/angular-cache.js',
            'Scripts/angular_ui/angular-spinner.js',
            'Scripts/angular_ui/ui-bootstrap.js',
            'Scripts/angular_ui/ui-bootstrap-tpls.js',
            'Scripts/angular_ui/ui-select.js',
            'Scripts/jquery/jquery-2.1.1.js',
            'Scripts/3rdparty/ng-grid.js',
            'Scripts/3rdparty/bootstrap.js',
            'Scripts/3rdparty/linq-vsdoc.js',
            'Scripts/3rdparty/linq.js',
            'Scripts/app/*.js',
            'Scripts/app/**/*.js',
            'Scripts/app/**/**/*.js',
            'Scripts/app/test/unit/**/*Spec.js',
            'Scripts/app/view/*.html'
        ],

        //ngHtml2JsPreprocessor: {
        //    // strip this from the file path
        //    stripPrefix: 'Scripts/'
        //    //stripSufix: '.ext',
        //    //// prepend this to the
        //    //prependPrefix: 'served/',
        //    //
        //    //// or define a custom transform function
        //    //cacheIdFromPath: function(filepath) {
        //    //    return cacheId;
        //    //},
        //
        //    // setting this option will create only a single module that contains templates
        //    // from all the files, so you can load them all with module('foo')
        //    //moduleName: 'preprocessTemplate'
        //},

        // list of files to exclude
        exclude: [],


        // preprocess matching files before serving them to the browser
        // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
        preprocessors: {
            'Scripts/**/**/*.html': ['ng-html2js']
        },


        // test results reporter to use
        // possible values: 'dots', 'progress'
        // available reporters: https://npmjs.org/browse/keyword/karma-reporter
        reporters: ['progress'],


        // web server port
        port: 9876,

        plugins: [
            'karma-chrome-launcher',
            'karma-jasmine',
            'karma-ng-html2js-preprocessor'
        ],

        // enable / disable colors in the output (reporters and logs)
        colors: true,


        // level of logging
        // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
        logLevel: config.LOG_INFO || config.LOG_DEBUG,


        // enable / disable watching file and executing tests whenever any file changes
        autoWatch: false,


        // start these browsers
        // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
        browsers: ['Chrome'],


        // Continuous Integration mode
        // if true, Karma captures browsers, runs the tests and exits
        singleRun: false
    });
};

//var app = angular.module('app', ['ngRoute', 'ui.bootstrap']);
//var app = angular.module('app', ['ngRoute', 'ui.bootstrap', 'd3']);
var app = angular.module('app', ['ngRoute', 'ngSanitize', 'ui.bootstrap']);

app.config(function ($routeProvider, $locationProvider) {

    $routeProvider
    .when('/', {
        templateUrl: 'partials/home.html'
    })
    .when('/login', {
        templateUrl: 'partials/login.html'
    })
    .when('/news', {
        templateUrl: 'partials/news.html'
    })
    .otherwise({
        redirectTo: '/'
    });

    //$locationProvider.html5Mode(true); // this breaks refresh

});

// put in a global 401 handler to redirect to the login page
// http://blog.thesparktree.com/post/75952317665/angularjs-interceptors-globally-handle-401-and
app.factory('authHttpResponseInterceptor', ['$q', '$location', function ($q, $location) {
    return {
        response: function (response) {
            if (response.status === 401) {
                console.log("Response 401");
            }
            return response || $q.when(response);
        },
        responseError: function (rejection) {
            if (rejection.status === 401) {
                console.log("Response Error 401", rejection);

                if ($location.path() != '/login') {
                    // login page is ok to not be logged in
                    var p = $location.path();
                    $location.path('/login').search('returnTo', p);
                }
            }
            return $q.reject(rejection);
        }
    }
}]);

app.config(['$httpProvider', function ($httpProvider) {
    //Http Interceptor to check auth failures for xhr requests
    $httpProvider.interceptors.push('authHttpResponseInterceptor');
}]);

app.filter('escape', function () {
    return function (input) {
        //return encodeURIComponent(encodeURIComponent(input));
        return encodeURIComponent(input);
    };
})

app.filter('unescape', function () {
    return function (input) {
        return decodeURIComponent(input);
    };
});

app.filter('unsafe', ['$sce', function ($sce) {
    return function (val) {
        return $sce.trustAsHtml(val);
    };
}]);

////// my stuff /////////

app.directive('s7Logout', function () {
    return {
        templateUrl: 'partials/directives/logout.html'
    };
})

app.directive('s7UserLinks', function () {
    return {
        templateUrl: 'partials/directives/userLinks.html'
    };
})

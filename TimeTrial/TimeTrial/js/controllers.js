app.controller('loginController', function ($scope, $http, $location, userData) {

    $scope.uname = '';
    $scope.pwd = '';

    $http.get('api/hello')
    .success(function (data, status, headers, config) {
        $location.path("/");
    });

    // detect enter key on login inputs and call login
    $scope.uName_onKeyPress = function (e) { if (e.keyCode == 13) { DoLogin(); } }
    $scope.pwd_onKeyPress = function (e) { if (e.keyCode == 13) { DoLogin(); } }

    $scope.OnLoginClick = function () { DoLogin(); }

    function DoLogin() {

        $scope.errorMessage = null;
        $scope.loginPending = true;

        var cred = { username: $scope.uname, password: $scope.pwd }

        $http.post('api/auth', cred)
        .success(function (data, status, headers, config) {
            $scope.loginPending = false;

            userData.load();

            // if applicable, redirect to whence we came
            var p = $location.search().returnTo;
            if (!p) p = '';

            $location.path(p).search('returnTo', null);
        })
        .error(function (data, status, headers, config) {
            $scope.loginPending = false;
            $scope.errorMessage = data.ResponseStatus.Message;
        });
    }
});

app.controller('logoutController', function ($scope, $http, $location, userData) {

    $scope.logout = function () {

        $http.post('api/auth/logout')
        .success(function (data, status, headers, config) {
            userData.restore();
            $location.path("/login");
            //$route.reload();
        });

    }
});

app.controller('homeController', function ($scope, $routeParams, userData) {


});

app.controller('userSettingsController', function ($scope, $http) {

    // detect enter key on new PW input
    $scope.newPwd_onKeyPress = function (e) { if (e.keyCode == 13) { ChangePassword(); } }
    $scope.OnBtnChangePasswordClick = function () { ChangePassword(); }

    function ChangePassword() {

        $scope.errorMessage = '';
        $scope.pwChangePending = true;
        $scope.pwChangeOk = false;
        $scope.pwChangeErrorMessage = null;

        var DTO = {
            'OldPassword': $scope.oldPwd,
            'NewPassword': $scope.newPwd
        };

        $http({ url: 'api/changePassword', method: 'PATCH', data: DTO }) // angular $http.patch shortcut method doesn't work, use longhand
        .success(function (data, status, headers, config) {
            $scope.oldPwd = '';
            $scope.newPwd = '';
            $scope.pwChangePending = false;
            $scope.pwChangeOk = true;
        })
        .error(function (data, status, headers, config) {
            $scope.pwChangeErrorMessage = data.ResponseStatus.Message;
            $scope.pwChangePending = false;
        });
    }
});

/////////////////////////////////// CONTROLLERS FOR DIRECTIVES ////////////////////////////////////

////////////// SERVICES ///////////////

app.controller('userDataController', function ($scope, userData) {

    $scope.styleUrl = function () {
        return userData.getBootstrapCss();
    };

    $scope.setStyleUrl = function (newStyleUrl) {
        userData.setBootstrapCss(newStyleUrl);
    }

    /////////////////////////////////////////

    $scope.projectId = function () {
        return userData.getProjectId();
    };

    $scope.setProjectId = function (value) {
        userData.setProjectId(value);
    }

});

app.service('userData', function ($http) {

    var BootstrapCss;
    var ProjectId;
    var HomeScreenTabName;

    load();

    function initDefaults() {
        BootstrapCss = '//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css';
        ProjectId = 3;
        HomeScreenTabName = '';
        GridStateByGridName = [];
    }

    this.restore = function () {
        initDefaults();
    }

    this.load = function () {
        load();
    }

    function load() {

        initDefaults();

        $http.get('api/userData')
        .success(function (data, status, headers, config) {

            var tmp;

            tmp = data.BootstrapCss;
            if (!NullOrEmpty(tmp)) BootstrapCss = tmp;

            tmp = data.ProjectId;
            if (!NullOrEmpty(tmp)) ProjectId = tmp;

            tmp = data.HomeScreenTabName;
            if (!NullOrEmpty(tmp)) HomeScreenTabName = tmp;

            tmp = data.GridStateByGridName;
            if (!NullOrEmpty(tmp)) GridStateByGridName = tmp;

        });
    }

    function NullOrEmpty(value) { return value == null || value == ''; }

    function save() {

        var tmp = BootstrapCss;
        if (tmp != '') BootstrapCss = tmp;

        var DTO = {
            'item': {
                'BootstrapCss': BootstrapCss,
                'ProjectId': ProjectId,
                'HomeScreenTabName': HomeScreenTabName,
                'GridStateByGridName': GridStateByGridName
            }
        };

        $http({ url: 'api/userData', method: 'PATCH', data: DTO }); // angular $http.patch shortcut method doesn't work, use longhand

    }

    this.set = function (value) {
        data = value;
    };

    this.getBootstrapCss = function () { return BootstrapCss; };
    this.setBootstrapCss = function (value) {
        BootstrapCss = value;
        save();
    };

    this.getProjectId = function () { return ProjectId; };
    this.setProjectId = function (value) {
        ProjectId = value;
        save();
    };

    this.getHomeScreenTabName = function () { return HomeScreenTabName; };
    this.setHomeScreenTabName = function (value) {
        HomeScreenTabName = value;
        save();
    };

    var GridStateByGridName;

    this.getGridStateByGridName = function (gridName) {
        var rtn = null;

        if (GridStateByGridName == null) return rtn; // early exit check

        for (var i = 0; i < GridStateByGridName.length; i++) {
            var item = GridStateByGridName[i];

            if (item.Key == gridName) {
                rtn = JSON.parse(item.Value);
                break;
            }
        }

        return rtn;
    };
    this.setGridStateByGridName = function (gridName, state) {

        var stateString = JSON.stringify(state);

        if (GridStateByGridName == null)
            GridStateByGridName = [];

        var gotIt = false;

        for (var i = 0; i < GridStateByGridName.length; i++) {
            var item = GridStateByGridName[i];

            if (item.Key == gridName) {
                item.Value = stateString;
                gotIt = true;
                break;
            }
        }

        if (!gotIt) {
            GridStateByGridName.push({ 'Key': gridName, 'Value': stateString });
        }

        save();
    };

});


////////////// TEST CONTROLLERS ///////////////

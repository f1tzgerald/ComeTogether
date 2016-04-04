(function () {

    "use strict";

    angular.module("tasks_app").controller("userController", userController);

    function userController ($http) {

        var vm = this;
        vm.users = [];

        // Get All Users
        $http.get("/api/users")
            .then(function (response) {
                //Success
                vm.users = response.data.name;
            }, function (error) {
                //Fail
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () { });
    }
})();
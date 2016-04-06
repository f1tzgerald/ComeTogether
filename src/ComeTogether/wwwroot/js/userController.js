(function () {

    "use strict";

    angular.module("tasks_app_user").controller("userController", userController);

    function userController ($http) {

        var vm = this;
        vm.errorMessage = "";
        vm.isBusy = true;

        // CHANGE THIS COUNT TO 25
        vm.selectorCount = 2; //25 always
        vm.skipCount = 0;

        vm.users = [];

        // Get All Users (TOP 25)
        $http.get("/api/users/" + vm.selectorCount + "/" + vm.skipCount)
            .then(function (response) {
                //Success
                vm.users = response.data;
                vm.skipCount += vm.selectorCount;
                console.log(vm.users);
            }, function (error) {
                //Fail
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        console.log(vm.users);

        // Get more users (+25)
        vm.ShowMoreUsers = function () {
            vm.isBusy = true;
            vm.newusers = {};

            $http.get("/api/users/" + vm.selectorCount + "/" + vm.skipCount)
                .then(function (response) {
                    //success
                    vm.skipCount += vm.selectorCount;
                    vm.newusers = response.data;
                    console.log(vm.newusers);
                    console.log(response.data.length);

                    for (var i = 0; i < response.data.length; i++) {
                        vm.users.push(vm.newusers[i]);
                    }

                }, function (error) {
                    //error
                }).finally(function () {
                    vm.isBusy = false;
                });
        }
    }
})();
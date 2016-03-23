(function () {
    "use strict";

    angular.module("app").
        controller("categoryEditController", categoryEditController);

    function categoryEditController($routeParams, $http) {
        var vm = this;

        vm.categoryName = $routeParams.categoryName;
        vm.errorMessage = "";
        vm.isBusy = true;

        vm.todoItems = [];


        $http.get("/api/category/" + vm.categoryName + "/tasks")
            .then(function (response) {
                //success
                angular.copy(response.data, vm.todoItems);
            }, function () {
                //error
                vm.errorMessage = "Failed to load To Do Items from db";
            }).finally(function () {
                vm.isBusy = false;
            });

    }
})();
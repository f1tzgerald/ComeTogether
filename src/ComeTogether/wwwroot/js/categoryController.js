(function () {

    "use strict";

    angular.module("tasks_app").controller("categoryController", categoryController);

    function categoryController($http) {

        vm.categories = [];

        vm.addCategory = function () {

            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trips", vm.newCategory)
            .then(function (response) {
                //Success
                vm.categories.push(response.data);
                vm.newCategory = {}; // clear the form

            }, function () {
                //Failed
                vm.errorMessage = "Failed to save category in db";
            }).finally(function () {
                vm.isBusy = false;
            });

            vm.categories.push({ name: vm.name });
            vm.newCategory = {};
        };

        $http.get("/api/category")
             .then(function (response) {
                 //Success
                 angular.copy(response.data, vm.categories);
             }, function (error) {
                 //Fail
                 vm.errorMessage = "Failed to load data: " + error;
             })
             .finally(function () {
                 vm.isBusy = false;
             });
    };
})();
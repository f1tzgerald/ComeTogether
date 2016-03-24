(function () {

    "use strict";

    angular.module("tasks_app").controller("categoryController", categoryController);

    function categoryController($http) {

        var vm = this;

        vm.categories = [];
        vm.errorMessage = "";

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

        vm.addCategory = function () {

            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/category", vm.newCategory)
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
        };

        vm.editCategory = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.put("/api/category", vm.editCateg)
                .then(function (response) {
                    //success


                })
        };

    };
})();
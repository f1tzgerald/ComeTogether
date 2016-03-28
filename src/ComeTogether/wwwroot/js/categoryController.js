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
                vm.categories = response.data;
                console.log("/api/category :");
                console.log("vm.categories");
                console.log(vm.categories);
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
                        console.log("category to add: ");
                        console.log(response.data);
                    vm.categories.push(response.data);
                    vm.newCategory = {}; // clear the form

                }, function () {
                    //Failed
                    vm.errorMessage = "Failed to save category in db";
                }).finally(function () {
                    vm.isBusy = false;
                });
        };


        vm.editCategory = function (id) {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.put("/api/category", vm.editCateg)
                .then(function (response) {
                    //success

                })
        };


        vm.delete = function (id) {
            $http.delete('/api/category/' + id)
                .then(function (response) {
                    //Success
                        console.log("category to delete");
                        console.log(id);

                    //refresh view
                        var index = vm.categories.indexOf(id);
                        vm.categories.splice(index, 1);

                }, function () {
                    //Failed
                    console.log("Error");

                }).finally(function () {

                });
        };
    };
})();